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
			this.ArchiveName = filename;
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

				if (!FileTable.FileIndex.Contains(file))
				{
					SimPe.Plugin.DownloadsToolFactory.TeleportFileIndex.AddIndexFromPackage(
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

				SimPe.Packages.StreamFactory.CloseStream(file);
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
					SimPe.Plugin.DownloadsToolFactory.TeleportFileIndex.AddIndexFromPackage(
						file
					);

					if (
						type == SimPe.Cache.PackageType.CustomObject
						|| type == SimPe.Cache.PackageType.Object
						|| type == SimPe.Cache.PackageType.Sim
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
			if (Nfos != null)
			{
				Nfos.Clear();
			}

			Nfos = null;
			ArchiveName = null;
		}

		#endregion
	}
}
