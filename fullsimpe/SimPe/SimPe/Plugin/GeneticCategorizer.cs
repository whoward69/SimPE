// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using SimPe.Data;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Scenegraph;
using SimPe.Packages;
using SimPe.PackedFiles.Cpf;
using SimPe.PackedFiles.ThreeIdr;
using SimPe.PackedFiles.Txtr;

namespace SimPe.Plugin
{
	/// <summary>
	/// A big mess
	/// </summary>
	public class GeneticCategorizer : System.ComponentModel.Component
	{
		PackageInfoTable packages;
		ListDictionary recolorItems;
		Hashtable loadedFiles;

		public PackageSettings Settings
		{
			get; set;
		}

		public bool IsEmpty => packages.Count == 0;

		public GeneticCategorizer()
		{
			packages = new PackageInfoTable();
			recolorItems = new ListDictionary();
			loadedFiles = new Hashtable();
			//this.settings = new PackageSettings();
		}

		public GeneticCategorizer(System.ComponentModel.IContainer container)
			: this()
		{
			container?.Add(this);
		}

		public bool AddPackage(HairColor key, string fileName)
		{
			bool ret = false;
			GeneratableFile file = File.LoadFromFile(fileName, true);
			if (ret = AddPackage(key, file))
			{
				loadedFiles[key] = fileName;
			}
			return ret;
		}

		PackageSettings GetSettings(RecolorType type)
		{
			PackageSettings ret = null;
			switch (type)
			{
				case RecolorType.Hairtone:
					ret = new HairtoneSettings(Settings);
					break;

				case RecolorType.Skintone:
					ret = new SkintoneSettings(Settings);
					break;

				default:
					ret = new PackageSettings(Settings, type);
					break;
			}
			return ret;
		}

		protected virtual bool AddPackage(HairColor key, IPackageFile package)
		{
			if (package != null)
			{
				if (!packages.ContainsKey(key))
				{
					PackageInfo pnfo = new PackageInfo(package);
					packages.Add(key, pnfo);

					pnfo.RecolorItems = GetRecolorItems(key);

					#region Determine package type

					RecolorType newType = RecolorType.Unsupported;
					if (pnfo.PropertySet != null)
					{
						newType = pnfo.Type;
					}
					else
					{
						RecolorItem[] items = GetRecolorItems(key);
						foreach (RecolorItem item in items)
						{
							RecolorType type = item.Type;
							if (type != RecolorType.Unsupported)
							{
								newType = type;
								break;
							}
						}
					}

					#endregion

					if (
						Settings == null
						|| Settings.PackageType == RecolorType.Unsupported
					)
					{
						Settings = GetSettings(newType);
					}
					else if (newType != Settings.PackageType)
					{
						Clear(key);
						Helper.ExceptionMessage(
							new ApplicationException(
								string.Format(
									"The package being added is not a {0}",
									Settings.PackageType
								)
							)
						);
						return false;
					}

					if (pnfo.Package != null)
					{
						if (Settings is SkintoneSettings)
						{
							((SkintoneSettings)Settings).GeneticWeight = pnfo
								.PropertySet.GetSaveItem("genetic")
								.SingleValue;
						}

						if (Settings.FamilyGuid == Guid.Empty)
						{
							Settings.FamilyGuid = pnfo.Family;
						}
					}
					/*
					if (!FileTable.FileIndex.Loaded && WrapperFactory.Settings.ForceTableLoad)
					{
						FileTable.FileIndex.Load();
						FileTable.FileIndex.AddIndexFromPackage(package, false);
					}*/

					if (Utility.IsNullOrEmpty(Settings.Description))
					{
						Settings.Description = pnfo.Description; //this.GetPackageText(key);
					}

					return true;
				}
			}
			return false;
		}

		public void Clear(HairColor key)
		{
			if (packages.ContainsKey(key))
			{
				packages.RemovePackage(key);
				recolorItems.Remove(key);
				loadedFiles.Remove(key);
				//this.loadedTextures.Clear();

				if (packages.Count == 0)
				{
					Settings = null; //mode = RecolorType.Unsupported;
				}
			}
		}

		public void Clear()
		{
			packages.RemoveAll();
			recolorItems.Clear();
			loadedFiles.Clear();
			//this.loadedTextures.Clear();
			Settings = null; //.mode = RecolorType.Unsupported;
		}

		public bool Contains(HairColor key)
		{
			return packages.ContainsKey(key);
		}

		public bool MovePackage(HairColor currentKey, HairColor newKey)
		{
			if (
				packages.ContainsKey(currentKey)
				&& !packages.ContainsKey(newKey)
			)
			{
				packages.Add(newKey, packages[currentKey]);
				packages.Remove(currentKey);

				if (loadedFiles[currentKey] != null)
				{
					loadedFiles.Add(newKey, loadedFiles[currentKey]);
					loadedFiles.Remove(currentKey);
				}

				if (recolorItems[currentKey] != null)
				{
					recolorItems.Add(newKey, recolorItems[currentKey]);
					recolorItems.Remove(currentKey);

					foreach (RecolorItem item in GetRecolorItems(newKey))
					{
						bool greyRecolor =
							(item.Age == Ages.Elder ^ newKey == HairColor.Grey)
							&& newKey != HairColor.Unbinned;
						item.ColorBin = !greyRecolor ? newKey : HairColor.Grey;
					}
				}

				loadedTextures.Clear();

				return true;
			}

			return false;
		}

		public RecolorItem[] GetRecolorItems(HairColor key)
		{
			if (!recolorItems.Contains(key))
			{
				ArrayList list = new ArrayList();

				PackageInfo pnfo = packages[key];
				if (pnfo != null)
				{
					IPackedFileDescriptor[] files = pnfo.FindFiles(
						MetaData.GZPS
					);
					if (Utility.IsNullOrEmpty(files))
					{
						files = pnfo.FindFiles(Utility.DataType.XTOL);
					}

					if (Utility.IsNullOrEmpty(files))
					{
						files = pnfo.FindFiles(Utility.DataType.XMOL);
					}

					list.AddRange(ProcessCpfItems(files, pnfo.Package));
				}

				foreach (RecolorItem item in list)
				{
					bool greyRecolor =
						(item.Age == Ages.Elder ^ key == HairColor.Grey)
						&& key != HairColor.Unbinned;
					item.ColorBin = !greyRecolor ? key : HairColor.Grey;
				}

				recolorItems[key] = list.ToArray(typeof(RecolorItem));
			}

			return (RecolorItem[])recolorItems[key];
		}

		private ArrayList ProcessCpfItems(
			IPackedFileDescriptor[] cpfs,
			IPackageFile package
		)
		{
			ArrayList ret = new ArrayList();
			if (!Utility.IsNullOrEmpty(cpfs))
			{
				int i = -1;
				while (++i < cpfs.Length)
				{
					Cpf cpf = new Cpf();
					IPackedFileDescriptor pfd = cpfs[i];

					cpf.ProcessData(pfd, package);

					RecolorItem item = new RecolorItem(cpf);

					// fix for 0.2.28.3 blunder
					if (item.ContainsItem("subtype"))
					{
						CpfItem ci = item.GetProperty("subtype");
						switch (ci.Datatype)
						{
							case MetaData.DataTypes.dtBoolean:
							case MetaData.DataTypes.dtInteger:
							case MetaData.DataTypes.dtSingle:
							case MetaData.DataTypes.dtString:
							case MetaData.DataTypes.dtUInteger:
								break;

							default:
								List<CpfItem> items = new List<CpfItem>();
								foreach (CpfItem c in item.PropertySet.Items)
								{
									if (c.Name != "subtype")
									{
										items.Add(c);
									}
								}

								item.PropertySet.Items = items.ToArray();
								break;
						}
					} //end fix

					item.Materials.AddRange(GetMaterials(package, cpf));

					ret.Add(item);
				}
			}
			return ret;
		}

		public PackageInfo GetPackageInfo(HairColor key)
		{
			return packages.ContainsKey(key) ? packages[key] : null;
		}

		public void RevertToBaseTextures(RcolTable materials)
		{
			ArrayList temp = new ArrayList();
			foreach (MaterialDefinitionRcol rcol in materials)
			{
				if (rcol.MaterialDefinition != null)
				{
					MaterialDefinition mmatd = rcol.MaterialDefinition;

					if (!Utility.IsNullOrEmpty(mmatd.Listing))
					{
						string baseTextureName = mmatd.Listing[0];

						if (mmatd.Listing.Length == 1)
						{
							string newTextureName = NewTextureName(
								baseTextureName,
								rcol.ColorBin
							);
							rcol.BaseTextureName = newTextureName;
						}
						else
						{
							string newTextureName = NewTextureName(
								mmatd.Listing[1],
								rcol.ColorBin
							);
							rcol.NormalMapTextureName = baseTextureName;
							rcol.BaseTextureName = newTextureName;
						}

						ReloadTextureDescriptor(rcol);
					}
				}
			}
		}

		string NewTextureName(string baseTextureName, HairColor key)
		{
			if (key == HairColor.Unbinned)
			{
				return baseTextureName;
			}

			string[] parts = baseTextureName.Split(new char[] { '-' });
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			for (int i = 0; i < parts.Length - 1; i++)
			{
				str.Append(parts[i]);
				str.Append("-");
			}
			str.Append(key.ToString().ToLower());

			return str.ToString();
		}

		public IPackedFileDescriptor[] GetTextureDescriptor(RcolTable table)
		{
			ArrayList ret = new ArrayList();

			foreach (Rcol rcol in table)
			{
				IPackedFileDescriptor[] txtr = GetTextureDescriptor(rcol);
				if (!Utility.IsNullOrEmpty(txtr))
				{
					ret.AddRange(txtr);
				}
			}
			return (IPackedFileDescriptor[])ret.ToArray(typeof(IPackedFileDescriptor));
		}

		IScenegraphFileIndexItem FindFileByReference(IPackedFileDescriptor reference)
		{
			/*if (!FileTable.FileIndex.Loaded && WrapperFactory.Settings.ForceTableLoad)
				FileTable.FileIndex.Load();*/

			IScenegraphFileIndexItem ret = null;

			// find in local packages
			foreach (DictionaryEntry de in packages)
			{
				object key = de.Key;
				PackageInfo pnfo = de.Value as PackageInfo;

				IPackedFileDescriptor local = pnfo.Package.FindFile(
					reference.Type,
					reference.SubType,
					reference.Group,
					reference.Instance
				);
				if (local != null)
				{
					ret = new FileIndexItem(local, pnfo.Package);
					break;
				}
			}

			if (ret == null)
			{
				// find in all packages
				IScenegraphFileIndexItem[] items =
					FileTableBase.FileIndex.FindFileByGroupAndInstance(
						reference.Group,
						reference.LongInstance
					);
				if (!Utility.IsNullOrEmpty(items))
				{
					foreach (IScenegraphFileIndexItem sfi in items)
					{
						if (sfi.FileDescriptor.Type == reference.Type)
						{
							ret = sfi;
							break;
						}
					}
				}
			}

			if (ret == null)
			{
				IScenegraphFileIndexItem[] sfi =
					FileTableBase.FileIndex.FindFileDiscardingGroup(reference); //, pnfo.Package);
				if (!Utility.IsNullOrEmpty(sfi))
				{
					ret = sfi[0];
				}
			}

			return ret;
		}

		public System.Drawing.Image GetImage(Rcol rcol, System.Drawing.Size size)
		{
			if (rcol is MaterialDefinitionRcol txmt)
			{
				if (!Utility.IsNullOrEmpty(txmt.Textures))
				{
					Txtr txtr = txmt.Textures[0] as Txtr;
					if (!Utility.IsNullOrEmpty(txtr.Blocks))
					{
						ImageData data1 = (ImageData)txtr.Blocks[0];
						MipMap map1 = data1.GetLargestTexture(size);
						if (map1.Texture != null)
						{
							return ImageLoader.Preview(map1.Texture, size);
						}
					}
				}
			}

			return null;
		}

		public Rcol[] GetMaterials(IPackageFile package, Cpf cpf)
		{
			ArrayList ret = new ArrayList();

			ResourceReference[] rrs = FindTXMTReferences(
				package,
				cpf.FileDescriptor.Group,
				cpf.FileDescriptor.Instance
			);
			foreach (ResourceReference rr in rrs)
			{
				IPackedFileDescriptor pfd = package.FindFile(
					rr.Type,
					rr.SubType,
					rr.Group,
					rr.Instance
				);
				if (pfd != null)
				{
					MaterialDefinitionRcol rcol = new MaterialDefinitionRcol(); // GenericRcol(null, false);

					rcol.ProcessData(pfd, package, false);

					ReloadTextureDescriptor(rcol);

					ret.Add(rcol);
				}
			}

			return (Rcol[])ret.ToArray(typeof(Rcol));
		}

		public void ReloadTextureDescriptor(MaterialDefinitionRcol rcol)
		{
			rcol.Textures.Clear();
			rcol.Textures.AddRange(GetMaterialTextures(rcol));
		}

		public IPackedFileDescriptor[] GetTextureDescriptor(Rcol rcol)
		{
			return rcol is MaterialDefinitionRcol mmat ? mmat.Textures.GetFileDescriptor() : (new IPackedFileDescriptor[0]);
		}

		Hashtable loadedTextures = new Hashtable();

		public Rcol[] GetMaterialTextures(MaterialDefinitionRcol rcol)
		{
			ArrayList ret = new ArrayList();

			Hashtable table = rcol.GetTextureDescriptor();

			if (table[TextureType.Base] is IPackedFileDescriptor pfdBaseTexture)
			{
				ResourceReference key = new ResourceReference(pfdBaseTexture);

				IScenegraphFileIndexItem item = FindFileByReference(
					pfdBaseTexture
				);
				if (item != null)
				{
					Txtr txtr = null;
					if (!loadedTextures.ContainsKey(key))
					{
						txtr = new Txtr(null, false);
						txtr.ProcessData(item.FileDescriptor, item.Package, false);
						loadedTextures.Add(key, txtr);
					}
					else
					{
						txtr = loadedTextures[key] as Txtr;
					}

					ret.Add(txtr);
				}
			}

			if (table.ContainsKey(TextureType.NormalMap))
			{
				if (table[TextureType.NormalMap] is IPackedFileDescriptor pfdNormalMapTexture)
				{
					ResourceReference key = new ResourceReference(pfdNormalMapTexture);

					IScenegraphFileIndexItem item = FindFileByReference(
						pfdNormalMapTexture
					);
					if (item != null)
					{
						Txtr txtr = null;
						if (!loadedTextures.ContainsKey(key))
						{
							txtr = new Txtr(null, false);
							txtr.ProcessData(item.FileDescriptor, item.Package, false);
							loadedTextures.Add(key, txtr);
						}
						else
						{
							txtr = loadedTextures[key] as Txtr;
						}

						ret.Add(txtr);
					}
				}
			}

			return (Rcol[])ret.ToArray(typeof(Rcol));
		}

		private ResourceReference[] FindTXMTReferences(
			IPackageFile package,
			uint refGroup,
			uint refInstance
		)
		{
			ArrayList ret = new ArrayList();

			IPackedFileDescriptor[] pfds = Utility.FindFiles(
				package,
				MetaData.REF_FILE,
				refGroup,
				refInstance
			);
			if (!Utility.IsNullOrEmpty(pfds))
			{
				// we're not even trying to touch these, so let's just release
				// their resources after we take a peek...
				using (ThreeIdr refFile = new ThreeIdr())
				{
					foreach (IPackedFileDescriptor pfd in pfds) // there should be only one!
					{
						try
						{
							refFile.ProcessData(pfd, package, false);
							foreach (IPackedFileDescriptor ptr in refFile.Items)
							{
								if (ptr.Type == MetaData.TXMT)
								{
									ret.Add(new ResourceReference(ptr));
								}
							}
						}
						catch { }
						finally
						{
							refFile.Package = null;
							refFile.FileDescriptor = null;
						}
					}
				} //using
			}
			return (ResourceReference[])ret.ToArray(typeof(ResourceReference));
		}

		IPackedFileDescriptor Get3IDRResource(RecolorItem item)
		{
			if (item.PropertySet != null)
			{
				IPackedFileDescriptor[] pfds = Utility.FindFiles(
					item.PropertySet.Package,
					MetaData.REF_FILE,
					item.PropertySet.FileDescriptor.Group,
					item.PropertySet.FileDescriptor.Instance
				);
				if (pfds.Length == 1)
				{
					return pfds[0];
				}
			}
			return null;
		}

		protected void SanitizeFilenames(PackageInfo pnfo)
		{
			foreach (RecolorItem item in pnfo.RecolorItems)
			{
				foreach (MaterialDefinitionRcol txmt in item.Materials)
				{
					txmt.FileName = string.Format(
						"#0x{0:x8}!{1}",
						pnfo.PackageHash,
						Hashes.StripHashFromName(txmt.FileName)
					);

					foreach (Txtr txtr in txmt.Textures)
					{
						try
						{
							txtr.FileName = string.Format(
								"#0x{0:x8}!{1}",
								pnfo.PackageHash,
								Hashes.StripHashFromName(txtr.FileName)
							);
						}
						catch (Exception x)
						{
							x.GetType();
						}
						finally { }
					}
				}
			}
		}

		/// <summary>
		/// This is where color binning happens...
		/// </summary>
		/// <param name="key">The color key to categorize</param>
		/// <returns></returns>
		protected virtual GeneratableFile ProcessPackage(HairColor key)
		{
			GeneratableFile ret = null;

			PackageInfo pnfo = packages[key];
			if (pnfo != null)
			{
				//Experimental
				//this.SanitizeFilenames(pnfo);
				ret = pnfo.Package as GeneratableFile;
			}

			if (ret != null)
			{
				Guid hairtoneGuid = new Guid((uint)key, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
				ArrayList keep = new ArrayList();
				ArrayList discard = new ArrayList();

				RecolorItem[] items = GetRecolorItems(key);
				foreach (RecolorItem item in items)
				{
					item.Pinned = Settings.KeepDisabledItems;

					// used to check if a given set of textures can be deleted
					IPackedFileDescriptor[] txtr = GetTextureDescriptor(item.Materials);

					if (item.Enabled || item.Pinned)
					{
						keep.AddRange(txtr);
						foreach (IPackedFileDescriptor pfd in txtr)
						{
							if (discard.Contains(pfd))
							{
								discard.Remove(pfd);
							}
						}
					}
					else
					{
						foreach (IPackedFileDescriptor pfd in txtr)
						{
							if (!keep.Contains(pfd))
							{
								discard.Add(pfd);
							}
						}
					}

					item.Family = Settings.FamilyGuid;

					switch (Settings.PackageType)
					{
						// this is the hairtone processing
						case RecolorType.Hairtone:
						case RecolorType.TextureOverlay:
						case RecolorType.MeshOverlay:

							if (
								(
									item.Type == RecolorType.TextureOverlay
									|| item.Type == RecolorType.MeshOverlay
								)
								&&
									item.TextureOverlayType
										!= TextureOverlayTypes.EyeBrow
									&& item.TextureOverlayType
										!= TextureOverlayTypes.Beard

							)
							{
								goto default;
							}

							if (key == HairColor.Unbinned)
							{
								hairtoneGuid = item.Family;
							}

							// special case
							// TODO: re-check conditions
							item.Hairtone = item.Age == Ages.Elder
								&& key != HairColor.Unbinned
								? Utility.HairtoneGuid.Grey
								: hairtoneGuid;

							#region New name
							string oldName = item.Name;
							System.Text.StringBuilder str =
								new System.Text.StringBuilder();
							str.Append(oldName.Split(new char[] { '_' })[0]);
							str.Append("_");
							// special case (again)
							if (item.Age == Ages.Elder)
							{
								str.Append(HairColor.Grey.ToString().ToLower());
							}
							else
							{
								str.Append(key.ToString().ToLower());
							}

							item.Name = str.ToString();
							#endregion

							break;

						default:
							break;
					}

					// process textures
					if (Settings.CompressTextures)
					{
						foreach (MaterialDefinitionRcol mmat in item.Materials)
						{
							foreach (Txtr textr in mmat.Textures)
							{
								if (textr.Package == pnfo.Package) // this is important!!
								{
									textr.FileDescriptor.MarkForReCompress = true;
								}
							}
						}
					}

					item.CommitChanges();
				}

				// textures deemed unnecessary are now marked for deletion
				IPackedFileDescriptor[] textureFiles = ret.FindFiles(
					MetaData.TXTR
				);
				foreach (IPackedFileDescriptor pfd in textureFiles)
				{
					if (!keep.Contains(pfd))
					{
						pfd.MarkForDelete = true;
					}
				}

				if (pnfo.PropertySet != null)
				{
					if (pnfo.Type == RecolorType.Hairtone)
					{
						HairtoneSettings hset = (HairtoneSettings)Settings;

						pnfo.Name = key.ToString();
						if (key != HairColor.Unbinned)
						{
							pnfo.SetValue("proxy", hairtoneGuid.ToString());
						}
						else if (hset.DefaultProxy != Guid.Empty)
						{
							pnfo.SetValue("proxy", hset.DefaultProxy.ToString());
						}
					}
					else if (pnfo.Type == RecolorType.Skintone)
					{
						SkintoneSettings sset = (SkintoneSettings)Settings;
						pnfo.PropertySet.GetItem("genetic").SingleValue =
							sset.GeneticWeight;
					}

					pnfo.Family = Settings.FamilyGuid;
				}

				pnfo.Description = Settings.Description;

				pnfo.CommitChanges();
			}
			return ret;
		}

		/// <summary>
		/// Processes all color keys and saves the files,
		/// abiding by the output options :)
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public int ProcessPackages(string fileName, bool namechange) // namechange is true for Save As - will be applied to make single package and RenameFiles
		{
			int ret = 0;
			Array values = Enum.GetValues(typeof(HairColor));
			ListDictionary files = new ListDictionary();

			// intermediate step: determine which color keys have packages
			int i = -1;
			while (++i < values.Length)
			{
				HairColor key = (HairColor)values.GetValue(i);
				GeneratableFile file = ProcessPackage(key);
				if (file != null)
				{
					files.Add(key, file);
				}
			}

			if (namechange) //  if Save As then is true, will go no further than this
			{
				// create THE output file
				GeneratableFile file = File.LoadFromFile(null);
				foreach (DictionaryEntry de in files)
				{
					GeneratableFile package = de.Value as GeneratableFile;
					foreach (IPackedFileDescriptor pfd in package.Index)
					{
						if (!pfd.MarkForDelete)
						{
							IPackedFile fl = package.Read(pfd);
							IPackedFileDescriptor newpfd = file.NewDescriptor(
								pfd.Type,
								pfd.SubType,
								pfd.Group,
								pfd.Instance
							);
							newpfd.UserData = fl.UncompressedData;
							newpfd.MarkForReCompress = pfd.MarkForReCompress;
							file.Add(newpfd);
						}
					}
				}

				SaveFile(file, fileName); // done!
			}
			else
			{
				// just save each file
				foreach (DictionaryEntry de in files)
				{
					HairColor key = (HairColor)de.Key;
					GeneratableFile package = de.Value as GeneratableFile;
					SaveFile(package, fileName, key);
				}
			}
			return ret;
		}

		private void SaveFile(GeneratableFile file, string fileName, HairColor key)
		{
			string filename = Convert.ToString(loadedFiles[key]); /* here we lose the fileName parameter if not Rename files
            if (this.settings.RenameFiles) // namechange ?? -will never get here
			{
				// generate new name
				filename = String.Format("{0}_{1}.package", System.IO.Path.GetFileNameWithoutExtension(fileName), key);
			}
            */
			SaveFile(file, filename);
		}

		private void SaveFile(GeneratableFile file, string fileName)
		{
			using (file)
			{
				file.Save(fileName);
			}
			StreamFactory.CloseStream(fileName);
		}

		/*
		private void DisablePackage(HairColor key)
		{
			string originalFilePath = "";
			try
			{
				originalFilePath = Convert.ToString(this.loadedFiles[key]);
				if (
					System.IO.Path.GetExtension(originalFilePath) != DisabledPackageFileExtension &&
					System.IO.File.Exists(originalFilePath)
					)
					System.IO.File.Move(originalFilePath, System.IO.Path.ChangeExtension(originalFilePath, DisabledPackageFileExtension));
			}
			catch (Exception x)
			{
				Helper.ExceptionMessage("Cannot move file " + originalFilePath, x);
			}

		}

		const string DisabledPackageFileExtension = ".packagedisabled";
		*/
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose loaded package files
				foreach (PackageInfo file in packages.Values)
				{
					file.Dispose();
				}

				Clear();
			}
			base.Dispose(disposing);
		}
	}
}
