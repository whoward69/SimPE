// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;

using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Named Glob File
	/// </summary>
	public class NamedGlob : Glob
	{
		public override string ToString()
		{
			return SemiGlobalName;
		}
	}

	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class Glob
		: AbstractWrapper //Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
			,
			IFileWrapper //This Interface is used when loading a File
			,
			IFileWrapperSaveExtension //This Interface (if available) will be used to store a File
	{
		#region Attributes
		/// <summary>
		/// Contains the Filename
		/// </summary>
		byte[] filename;

		public bool faulty = false;
		public bool bloaty = false;

		/// <summary>
		/// Returns the Filename
		/// </summary>
		public string FileName
		{
			get => Helper.ToString(filename);
			set
			{
				if (!Helper.ToString(filename).Equals(value))
				{
					filename = Helper.ToBytes(value, 0x40);
				}
			}
		}

		/// <summary>
		/// Just A Flag
		/// </summary>
		byte[] semiglobal;

		/// <summary>
		/// Returns /Sets the Flag
		/// </summary>
		public string SemiGlobalName
		{
			get => Helper.ToString(semiglobal);
			set
			{
				semiglobal = Helper.ToBytes(value, 0);
				Attributes["SemiGlobalName"] = value;
				Attributes["SemiGlobalGroup"] = SemiGlobalGroup;
			}
		}

		/// <summary>
		/// Retursn the Group for the current SemiGlobal Name
		/// </summary>
		public uint SemiGlobalGroup
		{
			get
			{
				uint grp = Hashes.GroupHash(SemiGlobalName);
				return grp;
			}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Glob()
			: base()
		{
			//items = new IPackedFileProperties[0];
			Attributes = new Hashtable();
			semiglobal = new byte[0];
			filename = new byte[64];
		}

		/*public new string ToString()
		{
			return this.SemiGlobalName;
		}*/


		#region IPackedFileProperties Member
		//IPackedFileProperties[] items;

		/// <summary>
		/// Returns all Attributes tored in the File.
		/// </summary>
		/// <remarks>Each Attribute is unique!</remarks>
		public Hashtable Attributes
		{
			get; private set;
		}

		/*/// <summary>
		/// Returns all Items stored in the File (can be null)
		/// </summary>
		/// <remarks>
		/// All Items returned here have the same structure,
		/// however each Item can have SubItmes of it's own.
		///
		/// If null is returned, no Items are provided by this File
		/// </remarks>
		public IPackedFileProperties[] Items
		{
			get
			{
				return items;
			}
		}*/
		#endregion
		/*
		#region IWrapper member
		public override bool CheckVersion(uint version)
		{
			if ( (version==0009) //0.00
				|| (version==0010) //0.10
				)
			{
				return true;
			}
			return false;
		}
		#endregion
		*/
		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new GlobUI();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo("Global Data Wrapper", "Quaxi", "---", 4);
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			faulty = bloaty = false;
			filename = reader.ReadBytes(64);
			byte len = reader.ReadByte();
			if ((byte)(reader.BaseStream.Length - reader.BaseStream.Position) < len) // some early files ommit len so the first letter is read as len
			{
				faulty = true;
				reader.BaseStream.Seek(-1, System.IO.SeekOrigin.Current);
				len = (byte)(reader.BaseStream.Length - reader.BaseStream.Position);
			}
			else if (
				(byte)(reader.BaseStream.Length - reader.BaseStream.Position) > len
			) // some early files contain a whole bunch of extra junk at the end
			{
				bloaty = true;
			}

			semiglobal = reader.ReadBytes(len);
			Attributes = new Hashtable
			{
				["SemiGlobalName"] = SemiGlobalName,
				["SemiGlobalGroup"] = SemiGlobalGroup
			};
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
			writer.Write(filename);
			writer.Write((byte)Math.Min(0xff, semiglobal.Length));
			writer.Write(semiglobal);
		}
		#endregion

		#region IFileWrapperSaveExtension Member
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member
		public override string Description => "SemiGlobalName="
					+ SemiGlobalName
					+ ", Group=0x"
					+ Helper.HexString(SemiGlobalGroup);

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature => new byte[0];

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types = { 0x474C4F42 }; //handles the GLOB File
				return types;
			}
		}
		#endregion
	}
}
