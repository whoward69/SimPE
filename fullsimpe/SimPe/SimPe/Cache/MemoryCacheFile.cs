// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;

using SimPe.Plugin;

namespace SimPe.Cache
{
	/// <summary>
	/// Contains an Instance of a CacheFile
	/// </summary>
	public class MemoryCacheFile : CacheFile
	{
		/// <summary>
		/// Updates and Loads the Memory Cache
		/// </summary>
		/// <returns></returns>
		public static MemoryCacheFile InitCacheFile()
		{
			FileTableBase.FileIndex.Load();
			return InitCacheFile(FileTableBase.FileIndex);
		}

		/// <summary>
		/// Updates and Loads the Memory Cache
		/// </summary>
		/// <returns></returns>
		public static MemoryCacheFile InitCacheFile(
			Interfaces.Scenegraph.IScenegraphFileIndex fileindex
		)
		{
			Wait.SubStart();
			Wait.Message = "Loading Memorycache";

			MemoryCacheFile cachefile = new MemoryCacheFile();

			cachefile.Load(Helper.SimPeLanguageCache, true);
			cachefile.ReloadCache(fileindex, true);
			Wait.SubStop();

			return cachefile;
		}

		public void ReloadCache()
		{
			ReloadCache(true);
		}

		public void ReloadCache(bool save)
		{
			FileTableBase.FileIndex.Load();
			ReloadCache(FileTableBase.FileIndex, save);
		}

		public void ReloadCache(
			Interfaces.Scenegraph.IScenegraphFileIndex fileindex,
			bool save
		)
		{
			Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = fileindex.FindFile(
				Data.MetaData.OBJD_FILE,
				true
			);

			bool added = false;
			Wait.MaxProgress = items.Length;
			Wait.Message = "Updating Cache";
			int ct = 0;
			foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem item in items)
			{
				Interfaces.Scenegraph.IScenegraphFileIndexItem[] citems =
					FileIndex.FindFile(item.GetLocalFileDescriptor(), null);
				if (citems.Length == 0)
				{
					PackedFiles.Wrapper.ExtObjd objd =
						new PackedFiles.Wrapper.ExtObjd();
					objd.ProcessData(item);

					AddItem(objd);
					added = true;
				}
				Wait.Progress = ct++;
			}

			if (added)
			{
				map = null;
				Wait.Message = "Saving Chache";
				if (save)
				{
					Save(FileName);
				}

				LoadMemTable();
				LoadMemList();
			}
		}

		/// <summary>
		/// Creaet a new Instance for an empty File
		/// </summary>
		public MemoryCacheFile()
			: base()
		{
			DEFAULT_TYPE = ContainerType.Memory;
		}

		/// <summary>
		/// Add a MaterialOverride to the Cache
		/// </summary>
		/// <param name="objd">The Object Data File</param>
		public MemoryCacheItem AddItem(PackedFiles.Wrapper.ExtObjd objd)
		{
			CacheContainer mycc = UseConatiner(
				ContainerType.Memory,
				objd.Package.FileName
			);

			MemoryCacheItem mci = new MemoryCacheItem
			{
				FileDescriptor = objd.FileDescriptor,
				Guid = objd.Guid,
				ObjectType = objd.Type,
				ObjdName = objd.FileName,
				ParentCacheContainer = mycc
			};

			try
			{
				Interfaces.Scenegraph.IScenegraphFileIndexItem[] sitems =
					FileTableBase.FileIndex.FindFile(
						Data.MetaData.CTSS_FILE,
						objd.FileDescriptor.Group,
						objd.CTSSInstance + (ulong)1,
						null
					);
				if (sitems.Length == 0)
				{
					sitems = FileTableBase.FileIndex.FindFile(
						Data.MetaData.CTSS_FILE,
						objd.FileDescriptor.Group,
						objd.CTSSInstance,
						null
					);
				}

				if (sitems.Length > 0)
				{
					PackedFiles.Wrapper.Str str =
						new PackedFiles.Wrapper.Str();
					str.ProcessData(sitems[0]);
					PackedFiles.Wrapper.StrItemList strs = str.LanguageItems(
						Helper.WindowsRegistry.LanguageCode
					);
					if (strs.Length > 0)
					{
						mci.Name = strs[0].Title;
					}

					//not found?
					if (mci.Name == "")
					{
						strs = str.LanguageItems(1);
						if (strs.Length > 0)
						{
							mci.Name = strs[0].Title;
						}
					}
				}
			}
			catch (Exception) { }

			try
			{
				Interfaces.Scenegraph.IScenegraphFileIndexItem[] sitems =
					FileTableBase.FileIndex.FindFile(
						Data.MetaData.STRING_FILE,
						objd.FileDescriptor.Group,
						0x100,
						null
					);
				if (sitems.Length > 0)
				{
					PackedFiles.Wrapper.Str str =
						new PackedFiles.Wrapper.Str();
					str.ProcessData(sitems[0]);
					PackedFiles.Wrapper.StrItemList strs = str.LanguageItems(
						Data.MetaData.Languages.English
					);
					string[] res = new string[strs.Count];
					for (int i = 0; i < res.Length; i++)
					{
						res[i] = strs[i].Title;
					}

					mci.ValueNames = res;
				}
			}
			catch (Exception) { }

			//still no name?
			if (mci.Name == "")
			{
				mci.Name = objd.FileName;
			}
			//having an icon?
			PackedFiles.Wrapper.Picture pic =
				new PackedFiles.Wrapper.Picture();
			Interfaces.Scenegraph.IScenegraphFileIndexItem[] iitems = mci.IsBadge
				? FileTableBase.FileIndex.FindFile(
					Data.MetaData.SIM_IMAGE_FILE,
					objd.FileDescriptor.Group,
					3,
					null
				)
				: FileTableBase.FileIndex.FindFile(
					Data.MetaData.SIM_IMAGE_FILE,
					objd.FileDescriptor.Group,
					1,
					null
				);

			if (iitems.Length > 0)
			{
				pic.ProcessData(iitems[0]);
				mci.Icon = pic.Image;
				Wait.Image = mci.Icon;
			}

			Wait.Message = mci.Name;
			//mci.ParentCacheContainer = mycc; //why was this disbaled?
			mycc.Items.Add(mci);

			return mci;
		}

		Hashtable map;

		/// <summary>
		/// Return the FileIndex represented by the Cached Files
		/// </summary>
		public Hashtable Map
		{
			get
			{
				if (map == null)
				{
					LoadMem();
				}

				return map;
			}
		}

		/// <summary>
		/// Creates the Map
		/// </summary>
		/// <returns>the FileIndex</returns>
		/// <remarks>
		/// The Tags of the FileDescriptions contain the MMATCachItem Object,
		/// the FileNames of the FileDescriptions contain the Name of the package File
		/// </remarks>
		public void LoadMem()
		{
			map = new Hashtable();

			foreach (CacheContainer cc in Containers)
			{
				if (cc.Type == ContainerType.Memory && cc.Valid)
				{
					foreach (MemoryCacheItem mci in cc.Items)
					{
						map[mci.Guid] = mci;
					}
				}
			} //foreach
		}

		ArrayList list;

		/// <summary>
		/// Return a List of all cached Memory Items
		/// </summary>
		public ArrayList List
		{
			get
			{
				if (list == null)
				{
					LoadMemList();
				}

				return list;
			}
		}

		/// <summary>
		/// Creates the List
		/// </summary>
		/// <returns>the FileIndex</returns>
		/// <remarks>
		/// The Tags of the FileDescriptions contain the MMATCachItem Object,
		/// the FileNames of the FileDescriptions contain the Name of the package File
		/// </remarks>
		public void LoadMemList()
		{
			list = new ArrayList();

			foreach (CacheContainer cc in Containers)
			{
				if (cc.Type == ContainerType.Memory && cc.Valid)
				{
					foreach (MemoryCacheItem mci in cc.Items)
					{
						list.Add(mci);
					}
				}
			} //foreach
		}

		FileIndex fi;

		/// <summary>
		/// Return the FileIndex represented by the Cached Files
		/// </summary>
		public FileIndex FileIndex
		{
			get
			{
				if (fi == null)
				{
					LoadMemTable();
				}

				return fi;
			}
		}

		/// <summary>
		/// Creates a FileIndex with all available MMAT Files
		/// </summary>
		/// <returns>the FileIndex</returns>
		/// <remarks>
		/// The Tags of the FileDescriptions contain the MMATCachItem Object,
		/// the FileNames of the FileDescriptions contain the Name of the package File
		/// </remarks>
		public void LoadMemTable()
		{
			fi = new FileIndex(new ArrayList())
			{
				Duplicates = false
			};

			foreach (CacheContainer cc in Containers)
			{
				if (cc.Type == ContainerType.Memory && cc.Valid)
				{
					foreach (MemoryCacheItem mci in cc.Items)
					{
						Interfaces.Files.IPackedFileDescriptor pfd = mci.FileDescriptor;
						pfd.Filename = cc.FileName;
						fi.AddIndexFromPfd(
							pfd,
							null,
							FileIndex.GetLocalGroup(pfd.Filename)
						);
					}
				}
			} //foreach
		}

		/// <summary>
		/// Returns an Alias for the given Guid
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public MemoryCacheItem FindItem(uint guid)
		{
			MemoryCacheItem mci = (MemoryCacheItem)Map[guid];
			return mci;
		}

		/// <summary>
		/// Returns an Alias for the given Guid
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public Interfaces.IAlias FindObject(uint guid)
		{
			MemoryCacheItem mci = FindItem(guid);
			Data.Alias a = mci == null
				? new Data.Alias(
					guid,
					Localization.Manager.GetString("Unknown")
				)
				: new Data.Alias(guid, mci.Name);

			object[] o = new object[3];
			o[0] = mci.FileDescriptor;
			o[1] = mci.ObjectType;
			o[2] = mci.Icon;
			a.Tag = o;

			return a;
		}
	}
}
