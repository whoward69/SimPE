// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.Collections;

using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;

namespace SimPe.Plugin
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class GenericRcol : Rcol, IScenegraphItem
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GenericRcol(Interfaces.IProviderRegistry provider, bool fast)
			: base(provider, fast) { }

		public GenericRcol()
			: base() { }

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new RcolUI();
		}
		#endregion


		#region IFileWrapper Member
		public override string Description
		{
			get
			{
				string str = "filename=";
				str += FileName;
				str += ", references=";
				Hashtable map = ReferenceChains;
				foreach (string s in map.Keys)
				{
					str += s + ": ";
					foreach (
						Interfaces.Files.IPackedFileDescriptor pfd in (ArrayList)map[s]
					)
					{
						str += pfd.Filename + " (" + pfd.ToString() + ") | ";
					}
					if (((ArrayList)map[s]).Count > 0)
					{
						str = str.Substring(0, str.Length - 2);
					}

					str += ",";
				}
				if (map.Count > 0)
				{
					str = str.Substring(0, str.Length - 1);
				}

				return str;
			}
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public override uint[] AssignableTypes
		{
			get
			{
				uint[] types =
				{
					ScenegraphHelper.TXMT,
					ScenegraphHelper.CRES,
					ScenegraphHelper.GMND,
					ScenegraphHelper.GMDC,
					ScenegraphHelper.SHPE,
					Data.MetaData.ANIM, //ANIM
					0x4D51F042, //CINE
					Data.MetaData.LDIR,
					Data.MetaData.LAMB,
					Data.MetaData.LPNT,
					Data.MetaData.LSPT,
				};
				return types;
			}
		}

		#endregion

		/// <summary>
		/// Subcallses can reimplement this Method to add additional References
		/// </summary>
		/// <param name="refmap">The Reference Map, Keys are the name of the Reference type, values are ArrayLists containing IPackedFileDescriptors</param>
		protected virtual void FindReferences(Hashtable refmap)
		{
		}

		/// <summary>
		/// Add te References stored in the Reference Section
		/// </summary>
		/// <param name="refmap">The Reference Map, Keys are the name of the Reference type, values are ArrayLists containing IPackedFileDescriptors</param>
		void FindGenericReferences(Hashtable refmap)
		{
			ArrayList list = new ArrayList();
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in ReferencedFiles)
			{
				list.Add(pfd);
			}

			refmap["Generic"] = list;

			//now check each stored block if it implements IScenegraphBlock
			foreach (IRcolBlock irb in Blocks)
			{
				if (
					typeof(IScenegraphBlock)
					== irb.GetType().GetInterface("IScenegraphBlock")
				)
				{
					IScenegraphBlock sgb = (IScenegraphBlock)irb;
					sgb.ReferencedItems(refmap, FileDescriptor.Group);
				}
			}
		}

		#region IScenegraphItem Member
		public IScenegraphFileIndexItem FindReferencedType(
			uint type
		)
		{
			foreach (ArrayList list in ReferenceChains.Values)
			{
				foreach (object o in list)
				{
					Interfaces.Files.IPackedFileDescriptor opfd =
						(Interfaces.Files.IPackedFileDescriptor)o;
					if (opfd.Type == type)
					{
						Interfaces.Files.IPackedFileDescriptor pfd = Package.FindFile(
							opfd
						);
						if (pfd == null)
						{
							opfd.Group = FileDescriptor.Group;
							pfd = Package.FindFile(opfd);
						}
						if (pfd == null)
						{
							opfd.Group = Data.MetaData.LOCAL_GROUP;
							pfd = Package.FindFile(opfd);
						}
						IScenegraphFileIndexItem item = null;
						if (pfd == null)
						{
							FileTableBase.FileIndex.Load();
							IScenegraphFileIndexItem[] items =
								FileTableBase.FileIndex.FindFile(
									(Interfaces.Files.IPackedFileDescriptor)o,
									null
								);
							if (items.Length > 0)
							{
								item = items[0];
							}
						}
						else
						{
							item = FileTableBase.FileIndex.CreateFileIndexItem(pfd, Package);
						}

						if (item != null)
						{
							return item;
						}
					}
				}
			}

			return null;
		}

		public Hashtable ReferenceChains
		{
			get
			{
				Hashtable refmap = new Hashtable();
				FindGenericReferences(refmap);
				FindReferences(refmap);
				return refmap;
			}
		}

		#endregion
	}
}
