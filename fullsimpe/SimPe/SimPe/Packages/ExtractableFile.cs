// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.IO;

using SimPe.Interfaces.Files;

namespace SimPe.Packages
{
	/// <summary>
	/// Extends the Package Files for Extraction Methods
	/// </summary>
	public class ExtractableFile : File
	{
		/// <summary>
		/// Constructor For the Class
		/// </summary>
		/// <param name="br">The BinaryReader representing the Package File</param>
		internal ExtractableFile(BinaryReader br)
			: base(br) { }

		internal ExtractableFile(string flname)
			: base(flname) { }

		/// <summary>
		/// Init the Clone for this Package
		/// </summary>
		/// <returns>An INstance of this Class</returns>
		protected override IPackageFile NewCloneBase()
		{
			ExtractableFile fl = new ExtractableFile((BinaryReader)null)
			{
				header = header
			};

			return fl;
		}

		/// <summary>
		/// Extracts the Content of a Packed File and returs them as a MemoryStream
		/// </summary>
		/// <param name="pfd">The PackedFileDescriptor</param>
		/// <returns>The MemoryStream representing the PackedFile</returns>
		public MemoryStream Extract(PackedFileDescriptor pfd)
		{
			IPackedFile pf = Read(pfd);
			return new MemoryStream(pf.UncompressedData);
		}

		/// <summary>
		/// Stores a MemoryStream to a File
		/// </summary>
		/// <param name="flname">The Filename</param>
		/// <param name="pf">
		/// The Memorystream representing the PackedFile. If null and pfd is not null, the Packedfile
		/// will be loaded with Extract().
		/// </param>
		/// <param name="pfd">
		/// The description of the File, or null. If not null an additional XML File will be created
		/// representing the Information like TypeId, SubId, Instance and Group.
		/// </param>
		/// <param name="meta">set false if you do not want to create the Meta Xml File</param>
		///
		public void SavePackedFile(
			string flname,
			MemoryStream pf,
			PackedFileDescriptor pfd,
			bool meta
		)
		{
			if (pfd != null)
			{
				if (pf == null)
				{
					pf = Extract(pfd);
				}

				if (meta)
				{
					SaveMetaInfo(flname + ".xml", pfd);
				}
			}

			if (pf != null)
			{
				SavePackedFile(flname, pf);
			}
		}

		/// <summary>
		/// Saves thhe MemoryStream to a File on the local Filesystem
		/// </summary>
		/// <param name="flname">The Filename</param>
		/// <param name="pf">The Memorystream representing the Data</param>
		protected void SavePackedFile(string flname, MemoryStream pf)
		{
			StreamItem si = StreamFactory.GetStreamItem(flname, false);

			FileStream fs = null;
			if (si == null)
			{
				fs = new FileStream(flname, FileMode.Create);
			}
			else
			{
				si.SetFileAccess(FileAccess.Write);
				fs = si.FileStream;
			}

			try
			{
				byte[] d = pf.ToArray();
				fs.Write(d, 0, d.Length);
			}
			finally
			{
				if (si != null)
				{
					si.Close();
				}
				else
				{
					fs.Close();
					fs.Dispose();
					fs = null;
				}
			}
		}

		/// <summary>
		/// Saves Metainformations about a PackedFile as xml output
		/// </summary>
		/// <param name="flname">The Filename</param>
		/// <param name="pfd">The description of the File</param>
		protected void SaveMetaInfo(string flname, PackedFileDescriptor pfd)
		{
			TextWriter fs = System.IO.File.CreateText(flname);
			try
			{
				fs.WriteLine(
					"<?xml version=\"1.0\" encoding=\""
						+ fs.Encoding.HeaderName
						+ "\" ?>"
				);
				fs.WriteLine(
					"<package type=\"" + ((uint)Header.IndexType).ToString() + "\">"
				);
				fs.Write(pfd.GenerateXmlMetaInfo());
				fs.WriteLine("</package>");
			}
			finally
			{
				fs.Close();
				fs.Dispose();
				fs = null;
			}
		}

		/// <summary>
		/// Generates a Package XML File containing all informations needed to recreate the Package
		/// </summary>
		/// <returns>The Package Content as XML encoded File</returns>
		public string GeneratePackageXML()
		{
			return GeneratePackageXML(true);
		}

		/// <summary>
		/// Generates a Package XML File containing all informations needed to recreate the Package
		/// </summary>
		/// <param name="header">true, if you want to generate the xml Header</param>
		/// <returns>The Package Content as XML encoded File</returns>
		public string GeneratePackageXML(bool header)
		{
			string xml = "";
			if (header)
			{
				xml += "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + Helper.lbr;
			}

			xml +=
				"<package type=\""
				+ ((uint)Header.IndexType).ToString()
				+ "\">"
				+ Helper.lbr;
			foreach (PackedFileDescriptor pfd in fileindex)
			{
				xml += pfd.GenerateXmlMetaInfo() + Helper.lbr;
			}
			xml += "</package>" + Helper.lbr;

			return xml;
		}

		/// <summary>
		/// Generates a Package XML File containing all informations needed to recreate the Package
		/// </summary>
		/// <param name="flname">The Filename for the File</param>
		public void GeneratePackageXML(string flname)
		{
			TextWriter fs = System.IO.File.CreateText(flname);
			try
			{
				fs.WriteLine(
					"<?xml version=\"1.0\" encoding=\""
						+ fs.Encoding.HeaderName
						+ "\" ?>"
				);
				fs.Write(GeneratePackageXML(false));
			}
			finally
			{
				fs.Close();
				fs.Dispose();
				fs = null;
			}
		}
	}
}
