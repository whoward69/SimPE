// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;

using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Wrapper;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Used to decode the Group Cache
	/// </summary>
	public class GroupCache
		: AbstractWrapper //Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
			,
			IFileWrapper //This Interface is used when loading a File
			,
			IFileWrapperSaveExtension //This Interface (if available) will be used to store a File
			,
			IGroupCache
	{
		#region Attributes
		uint id;

		/// <summary>
		/// Returns the Items stored in the FIle
		/// </summary>
		/// <remarks>Do not add Items based on this List! use the Add Method!!</remarks>
		internal GroupCacheItems Items
		{
			get;
		}

		Hashtable map;
		uint maxgroup;
		byte[] over;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GroupCache()
			: base()
		{
			id = 0x05;
			Items = new GroupCacheItems();
			map = new Hashtable();
			maxgroup = 0x6f000000;
			over = new byte[0];
		}

		/// <summary>
		/// returns an Absoluet FileName
		/// </summary>
		/// <param name="flname"></param>
		/// <returns></returns>
		string AbsoluteFileName(string flname)
		{
			flname = flname.Replace(
				"%userdatadir%",
				PathProvider.SimSavegameFolder.Trim().ToLower()
			);
			foreach (ExpansionItem ei in PathProvider.Global.Expansions)
			{
				string add = ei.Version.ToString();
				if (add == "0")
				{
					add = "";
				}

				flname = flname.Replace(
					"%gamedatadir" + add + "%",
					ei.InstallFolder.Trim().ToLower()
				);
			}

			return flname;
		}

		/// <summary>
		/// Add a new Item
		/// </summary>
		/// <param name="gci">The Item to Add</param>
		public void Add(GroupCacheItem gci)
		{
			if (gci.LocalGroup > maxgroup)
			{
				maxgroup = gci.LocalGroup;
			}

			Items.Add(gci);
			map[AbsoluteFileName(gci.FileName)] = gci;
		}

		/// <summary>
		/// Remove a Item
		/// </summary>
		/// <param name="gci">The Item you want to remove</param>
		public void Remove(GroupCacheItem gci)
		{
			Items.Remove(gci);
			map.Remove(AbsoluteFileName(gci.FileName));
		}

		/// <summary>
		/// Return an apropriate Item for the passed File
		/// </summary>
		/// <param name="flname"></param>
		/// <returns></returns>
		public IGroupCacheItem GetItem(string flname)
		{
			GroupCacheItem gci = (GroupCacheItem)map[flname.Trim().ToLower()];
			if (gci == null)
			{
				gci = new GroupCacheItem
				{
					FileName = flname,
					LocalGroup = maxgroup + 1
				};
				Add(gci);
			}

			return gci;
		}

		#region IWrapper member
		public override bool CheckVersion(uint version)
		{
			return true;
		}
		#endregion

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new UserInterface.GroupCacheUI();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo("Group Cache Wrapper", "Quaxi", "---", 1);
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			maxgroup = 0x6f000000;
			Items.Clear();
			map.Clear();
			//return;
			id = reader.ReadUInt32();
			uint ct = reader.ReadUInt32();

			for (int i = 0; i < ct; i++)
			{
				try
				{
					GroupCacheItem gci = new GroupCacheItem();
					gci.Unserialize(reader);
					Add(gci);
				}
#if DEBUG
				catch (Exception ex)
				{
					Helper.ExceptionMessage("", ex);
				}
#else
				catch (Exception) { }
#endif
			}

			over = reader.ReadBytes(
				(int)(reader.BaseStream.Length - reader.BaseStream.Position)
			);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(id);

			writer.Write((uint)Items.Length);
			for (int i = 0; i < Items.Length; i++)
			{
				Items[i].Serialize(writer);
			}

			writer.Write(over);
		}
		#endregion

		#region IFileWrapperSaveExtension Member
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature
		{
			get
			{
				byte[] sig = { };
				return sig;
			}
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types =
				{
					0x54535053, //group
				};

				return types;
			}
		}

		#endregion
	}
}
