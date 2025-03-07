// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;
using System.Xml;

using SimPe.Interfaces.Wrapper;

namespace SimPe
{
	/// <summary>
	/// Do not use this class direct, use <see cref="FileTable"/> instead!
	/// </summary>
	public class FileTableBase
	{
		/// <summary>
		/// Returns the FileIndex
		/// </summary>
		/// <remarks>This will be initialized by the RCOL Factory</remarks>
		public static Interfaces.Scenegraph.IScenegraphFileIndex FileIndex
		{
			get; set;
		}

		/// <summary>
		/// Returns a List of all Folders, even those the User doesn't want to scan for Content
		/// </summary>
		public static ArrayList DefaultFolders
		{
			get
			{
				string filename = Helper.DataFolder.FoldersXREG;
				if (!System.IO.File.Exists(filename))
				{
					BuildFolderXml();
					filename = Helper.DataFolder.FoldersXREGW;
				} // as that's what we just wrote

				System.Collections.Generic.Dictionary<string, ExpansionItem> shortmap =
					new System.Collections.Generic.Dictionary<string, ExpansionItem>();
				foreach (ExpansionItem ei in PathProvider.Global.Expansions)
				{
					shortmap[ei.ShortId.ToLower()] = ei;
				}

				ArrayList folders = new ArrayList();

				XmlReaderSettings xrs = new XmlReaderSettings
				{
					CloseInput = true,
					IgnoreComments = true,
					IgnoreProcessingInstructions = true,
					IgnoreWhitespace = true
				};
				XmlReader xr = XmlReader.Create(filename, xrs);
				try
				{
					xr.ReadStartElement("folders");
					xr.ReadStartElement("filetable");
					while (xr.IsStartElement())
					{
						int ftiver = -1;
						bool ftiignore = false;
						bool ftirec = false;
						FileTableItemType ftitype = FileTablePaths.Absolute;

						if (xr.Name != "path" && xr.Name != "file")
						{
							xr.Skip();
							continue;
						}
						while (xr.MoveToNextAttribute())
						{
							if (xr.Name == "recursive" && xr.Value != "0")
							{
								ftirec = true;
							}
							else if (xr.Name == "ignore" && xr.Value != "0")
							{
								ftiignore = true;
							}
							else if (xr.Name == "version" || xr.Name == "epversion")
							{
								try
								{
									int ver = Convert.ToInt32(xr.Value);
									ftiver = ver;
								}
								catch { }
							}
							else if (xr.Name == "root")
							{
								string root = xr.Value.ToLower();

								if (shortmap.ContainsKey(root))
								{
									ExpansionItem ei = shortmap[root];
									ftitype = ei.Expansion;
									root = ei.InstallFolder;
								}
								else if (root == "save")
								{
									root = PathProvider.SimSavegameFolder;
									ftitype = FileTablePaths.SaveGameFolder;
								}
								else if (root == "simpe")
								{
									root = Helper.SimPePath;
									ftitype = FileTablePaths.SimPEFolder;
								}
								else if (root == "simpedata")
								{
									root = Helper.SimPeDataPath;
									ftitype = FileTablePaths.SimPEDataFolder;
								}
								else if (root == "simpeplugin")
								{
									root = Helper.SimPePluginPath;
									ftitype = FileTablePaths.SimPEPluginFolder;
								}
							} // root
						} // MoveToNextAttribute
						xr.MoveToElement();

						FileTableItem fti = xr.Name == "file"
							? new FileTableItem(
								xr.ReadString(),
								ftitype,
								false,
								true,
								ftiver,
								ftiignore
							)
							: new FileTableItem(
								xr.ReadString(),
								ftitype,
								ftirec,
								false,
								ftiver,
								ftiignore
							);

						folders.Add(fti);
						xr.ReadEndElement();
					}
					xr.ReadEndElement();
					xr.ReadEndElement();

					return folders;
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage("", ex);
					return new ArrayList();
				}
				finally
				{
					xr.Close();
					xr = null;
				}
			}
		}

		/// <summary>
		/// Creates a default Folder xml
		/// </summary>
		public static void BuildFolderXml()
		{
			try
			{
				XmlWriterSettings xws = new XmlWriterSettings
				{
					CloseOutput = true,
					Indent = true,
					Encoding = System.Text.Encoding.UTF8
				};
				XmlWriter xw = XmlWriter.Create(Helper.DataFolder.FoldersXREGW, xws);

				try
				{
					xw.WriteStartElement("folders");
					xw.WriteStartElement("filetable");
					if (PathProvider.Global.GameVersion < 18)
					{
						xw.WriteStartElement("file");
						xw.WriteAttributeString("root", "save");
						xw.WriteAttributeString("ignore", "1");
						xw.WriteValue(
							"Downloads"
								+ Helper.PATH_SEP
								+ "_EnableColorOptionsGMND.package"
						);
						xw.WriteEndElement();

						xw.WriteStartElement("file");
						xw.WriteAttributeString("root", "game");
						xw.WriteAttributeString("ignore", "1");
						xw.WriteValue(
							"TSData"
								+ Helper.PATH_SEP
								+ "Res"
								+ Helper.PATH_SEP
								+ "Sims3D"
								+ Helper.PATH_SEP
								+ "_EnableColorOptionsMMAT.package"
						);
						xw.WriteEndElement();

						xw.WriteStartElement("path");
						xw.WriteAttributeString("root", "save");
						xw.WriteAttributeString("recursive", "1");
						xw.WriteAttributeString("ignore", "1");
						xw.WriteValue("zCEP-EXTRA");
						xw.WriteEndElement();

						xw.WriteStartElement("path");
						xw.WriteAttributeString("root", "game");
						xw.WriteAttributeString("recursive", "1");
						xw.WriteAttributeString("ignore", "1");
						xw.WriteValue(
							"TSData"
								+ Helper.PATH_SEP
								+ "Res"
								+ Helper.PATH_SEP
								+ "Catalog"
								+ Helper.PATH_SEP
								+ "zCEP-EXTRA"
						);
						xw.WriteEndElement();
					}

					for (int i = PathProvider.Global.Expansions.Count - 1; i >= 0; i--)
					{
						ExpansionItem ei = PathProvider.Global.Expansions[i];
						string s = ei.ShortId.ToLower();
						{
							foreach (string folder in ei.PreObjectFileTableFolders)
							{
								writenode(xw, shouldignor(ei, folder), s, null, folder);
							}

							if (
								ei.Flag.Class == ExpansionItem.Classes.Story
								|| !ei.Flag.FullObjectsPackage
							)
							{
								writenode(
									xw,
									shouldignor(ei, ei.ObjectsSubFolder),
									s,
									null,
									ei.ObjectsSubFolder
								);
							}
							else
							{
								writenode(
									xw,
									shouldignor(ei, ei.ObjectsSubFolder),
									s,
									ei.Version.ToString(),
									ei.ObjectsSubFolder
								);
							}

							foreach (string folder in ei.FileTableFolders)
							{
								writenode(xw, shouldignor(ei, folder), s, null, folder);
							}
						}
					}

					xw.WriteEndElement();
					xw.WriteEndElement();
				}
				finally
				{
					xw.Close();
					xw = null;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("Unable to create default Folder File!", ex);
			}
		}

		private static bool shouldignor(ExpansionItem ei, string folder)
		{
			return (PathProvider.Global.GameVersion < 21 && ei.Flag.SimStory)
				|| (!ei.Exists && ei.InstallFolder == "")
|| ((ei.Version != PathProvider.Global.GameVersion
				|| (!folder.EndsWith("\\Objects")
					&& !folder.EndsWith("\\Overrides")
					&& !folder.EndsWith("\\UI")
					&& !folder.EndsWith("\\Wants")))
					&& !folder.EndsWith("\\3D")
				&& !folder.EndsWith("\\Sims3D")
				&& !folder.EndsWith("\\Stuffpack\\Objects")
				&& !folder.EndsWith("\\Materials"));
		}

		/// <summary>
		/// Write folders.xreg
		/// </summary>
		/// <param name="lfti">A <typeparamref name="List&lt;&gt;"/> of <typeparamref name="FileTableItem"/> entries</param>
		public static void StoreFoldersXml(
			System.Collections.Generic.List<FileTableItem> lfti
		)
		{
			try
			{
				XmlWriterSettings xws = new XmlWriterSettings
				{
					CloseOutput = true,
					Indent = true,
					Encoding = System.Text.Encoding.UTF8
				};
				XmlWriter xw = XmlWriter.Create(Helper.DataFolder.FoldersXREGW, xws);

				try
				{
					xw.WriteStartElement("folders");
					xw.WriteStartElement("filetable");
					foreach (FileTableItem fti in lfti)
					{
						xw.WriteStartElement(fti.IsFile ? "file" : "path");

						if (fti.Type != FileTablePaths.Absolute)
						{
							bool ok = false;
							foreach (ExpansionItem ei in PathProvider.Global.Expansions)
							{
								if (fti.Type == ei.Expansion)
								{
									xw.WriteAttributeString(
										"root",
										ei.ShortId.ToLower()
									);
									ok = true;
									break;
								}
							}
							if (!ok)
							{
								switch (fti.Type.AsUint)
								{
									case (uint)FileTablePaths.SaveGameFolder:
									{
										xw.WriteAttributeString("root", "save");
										break;
									}
									case (uint)FileTablePaths.SimPEFolder:
									{
										xw.WriteAttributeString("root", "simpe");
										break;
									}
									case (uint)FileTablePaths.SimPEDataFolder:
									{
										xw.WriteAttributeString("root", "simpeData");
										break;
									}
									case (uint)FileTablePaths.SimPEPluginFolder:
									{
										xw.WriteAttributeString("root", "simpePlugin");
										break;
									}
								} //switch
							}
						}

						if (fti.IsRecursive)
						{
							xw.WriteAttributeString("recursive", "1");
						}

						if (fti.EpVersion >= 0)
						{
							xw.WriteAttributeString(
								"version",
								fti.EpVersion.ToString()
							);
						}

						if (fti.Ignore)
						{
							xw.WriteAttributeString("ignore", "1");
						}

						xw.WriteValue(fti.RelativePath);
						xw.WriteEndElement();
					}
					xw.WriteEndElement();
					xw.WriteEndElement();
				}
				finally
				{
					xw.Close();
					xw = null;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private static void writenode(
			XmlWriter xw,
			bool ign,
			string root,
			string version,
			string folder
		)
		{
			xw.WriteStartElement("path");
			if (ign)
			{
				xw.WriteAttributeString("ignore", "1");
			}

			xw.WriteAttributeString("root", root);
			if (version != null)
			{
				xw.WriteAttributeString("version", version);
			}

			xw.WriteValue(folder);
			xw.WriteEndElement();
		}

		/// <summary>
		/// Returns/Sets a WrapperRegistry (can be null)
		/// </summary>
		public static Interfaces.IWrapperRegistry WrapperRegistry
		{
			get; set;
		}

		/// <summary>
		/// Returns/Sets a ProviderRegistry (can be null)
		/// </summary>
		public static Interfaces.IProviderRegistry ProviderRegistry
		{
			get; set;
		}

		/// <summary>
		/// Returns The Group Cache used to determin local Groups
		/// </summary>
		public static IGroupCache GroupCache
		{
			get; set;
		}
	}
}
