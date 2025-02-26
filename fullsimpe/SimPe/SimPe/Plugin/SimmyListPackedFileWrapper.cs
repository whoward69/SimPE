using System;

using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	public class SimmyListPackedFileWrapper
		: AbstractWrapper,
			IFileWrapper,
			IFileWrapperSaveExtension
	{
		#region CreationIndex Attribute
		public string twine; // Show Good girls as well as damaged
		public uint sluts; // Number of Sims
		private uint slut; // Current Sim Instance
		public string strung; // the actaul string,
		public string Strung
		{
			get
			{
				return strung;
			}
			set
			{
				strung = value;
			}
		}
		public string Twine
		{
			get
			{
				return twine;
			}
			set
			{
				twine = value;
			}
		}
		#endregion

		public SimmyListPackedFileWrapper()
			: base() { }

		#region IWrapper member
		public override bool CheckVersion(uint version)
		{
			return true;
		}
		#endregion

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new SimmyListPackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Sim List (Popups) Viewer Wrapper",
				"Chris",
				"To View the List of Neighbours this Neighbourhood has Parsed",
				1,
				SimPe.GetIcon.Writable
			);
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			reader.BaseStream.Seek(4, System.IO.SeekOrigin.Begin); // move to the Index (Number of Sims)
			strung = "";
			twine = "";
			SimPe.Plugin.Idno idno = SimPe.Plugin.Idno.FromPackage(package);
			if (idno != null)
			{
				if (idno.Type != SimPe.Plugin.NeighborhoodType.Normal)
				{
					strung =
						"-INVALID FILE-\r\nData only Valid in a Primary Neighbourhood\r\n\r\n";
					twine =
						"-INVALID FILE-\r\nData only Valid in a Primary Neighbourhood\r\n\r\n";
				}
			}
			sluts = reader.ReadUInt32(); // Number of Neighbours
			if (sluts != 0)
			{
				bool dided;
				for (int i = 0; i < sluts; i++)
				{
					slut = reader.ReadUInt32();
					if (Helper.IsLotCatalogFile(package.FileName)) // Search by Nid, slow but accurate
					{
						dided = false;
						PackedFiles.Wrapper.ExtSDesc sdesc =
							new SimPe.PackedFiles.Wrapper.ExtSDesc();
						SimPe.Interfaces.Files.IPackedFileDescriptor[] files =
							package.FindFiles(SimPe.Data.MetaData.SIM_DESCRIPTION_FILE);
						foreach (
							SimPe.Interfaces.Files.IPackedFileDescriptor pfd in files
						)
						{
							sdesc.ProcessData(pfd, package);
							if (sdesc.Instance == slut)
							{
								strung +=
									"0x"
									+ Helper.HexString(Convert.ToInt16(slut))
									+ " - "
									+ sdesc.SimName
									+ " "
									+ sdesc.SimFamilyName
									+ "\r\n";
								dided = true;
							}
						}
						if (!dided)
						{
							strung +=
								"0x"
								+ Helper.HexString(Convert.ToInt16(slut))
								+ " - MISSING\r\n";
							twine +=
								"0x"
								+ Helper.HexString(Convert.ToInt16(slut))
								+ " - MISSING\r\n";
						}
					}
					else // search by instance, fast but not as accurate
					{
						SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
							package.FindFile(
								Data.MetaData.SIM_DESCRIPTION_FILE,
								0,
								0xFFFFFFFF,
								slut
							);
						if (pfd == null)
						{
							strung +=
								"0x"
								+ Helper.HexString(Convert.ToInt16(slut))
								+ " - MISSING\r\n";
							twine +=
								"0x"
								+ Helper.HexString(Convert.ToInt16(slut))
								+ " - MISSING\r\n";
						}
						else
						{
							PackedFiles.Wrapper.ExtSDesc sdesc =
								new SimPe.PackedFiles.Wrapper.ExtSDesc();
							sdesc.ProcessData(pfd, package);
							strung +=
								"0x"
								+ Helper.HexString(Convert.ToInt16(slut))
								+ " - "
								+ sdesc.SimName
								+ " "
								+ sdesc.SimFamilyName
								+ "\r\n";
						}
					}
				}
			}
		}

		protected override void Serialize(System.IO.BinaryWriter writer)
		{
		}
		#endregion

		#region IFileWrapperSaveExtension Member
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		public byte[] FileSignature
		{
			get
			{
				return new byte[0];
			}
		}

		public uint[] AssignableTypes
		{
			get
			{
				uint[] types =
				{
					0x2C310F46, //handles the Popups (List of Parsed Neighbour Ids)
				};
				return types;
			}
		}

		#endregion
	}
}
