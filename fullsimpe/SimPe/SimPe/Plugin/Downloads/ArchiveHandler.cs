// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later

namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// Summary description for ArchiveHandler.
	/// </summary>
	public abstract class ArchiveHandler : IPackageHandler, System.IDisposable
	{
		protected PackageInfoCollection Nfos
		{
			get; private set;
		}

		protected string ArchiveName
		{
			get; private set;
		}

		public ArchiveHandler(string filename)
		{
			DoInit(filename);
		}

		protected void DoInit(string filename)
		{
			Nfos = new PackageInfoCollection();
			ArchiveName = filename;
			Reset();
			LoadContent();
		}

		~ArchiveHandler()
		{
			Reset();
		}

		protected virtual void OnReset()
		{
		}

		protected abstract StringArrayList ExtractArchive();

		protected void LoadContent()
		{
			Wait.Message = "Extracting Archive";
			StringArrayList files = ExtractArchive();

			Wait.SubStart(files.Count);

			files = SortFilesByType(files);
			LoadFiles(files);

			Wait.SubStop();
		}

		private void LoadFiles(StringArrayList files)
		{
			int nr = 0;
			foreach (string file in files)
			{
				Wait.Progress = nr++;
				Wait.Message = System.IO.Path.GetFileName(file);

				if (!FileTableBase.FileIndex.Contains(file))
				{
					DownloadsToolFactory.TeleportFileIndex.AddIndexFromPackage(
						file
					);
				}

				IPackageHandler hnd = HandlerRegistry.Global.LoadFileHandler(
					file
				);
				if (hnd != null)
				{
					Nfos.AddRange(hnd.Objects);
				}

				Packages.StreamFactory.CloseStream(file);
			}
		}

		private StringArrayList SortFilesByType(StringArrayList files)
		{
			StringArrayList objects = new StringArrayList();
			StringArrayList other = new StringArrayList();
			foreach (string file in files)
			{
				if (file.EndsWith(".package", true, null))
				{
					Cache.PackageType type = PackageInfo.ClassifyPackage(file);
					DownloadsToolFactory.TeleportFileIndex.AddIndexFromPackage(
						file
					);

					if (
						type == Cache.PackageType.CustomObject
						|| type == Cache.PackageType.Object
						|| type == Cache.PackageType.Sim
					)
					{
						objects.Add(file);
					}
					else
					{
						other.Add(file);
					}
				}
			}
			objects.AddRange(other);
			other.Clear();
			other = null;
			files.Clear();
			files = null;

			return objects;
		}

		public virtual void FreeResources()
		{
		}

		protected void Reset()
		{
			OnReset();
			Nfos.Clear();
		}

		#region IPackageHandler Member

		public IPackageInfo[] Objects => Nfos.ToArray();

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			Nfos?.Clear();

			Nfos = null;
			ArchiveName = null;
		}

		#endregion
	}
}
