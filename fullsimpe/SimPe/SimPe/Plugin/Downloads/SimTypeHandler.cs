namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// Summary description for SimTypeHandler.
	/// </summary>
	public class SimTypeHandler : SimpleTypeHandler
	{
		public SimTypeHandler()
		{
		}

		protected override void SetName(Interfaces.Files.IPackageFile pkg)
		{
			Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(
				Data.MetaData.CTSS_FILE
			);

			if (pfds.Length > 0)
			{
				PackedFiles.Wrapper.StrItemList items =
					DefaultTypeHandler.GetCtssItems(pfds[0], pkg);
				if (items.Length > 0)
				{
					nfo.Name = items[0].Title;
				}

				if (items.Length > 2)
				{
					nfo.Name += " " + items[2].Title;
				}
			}
		}

		protected override void SetImage(Interfaces.Files.IPackageFile pkg)
		{
			SetImage(0x856DDBAC, pkg);
			nfo.KnockoutThumbnail = true;
		}
	}
}
