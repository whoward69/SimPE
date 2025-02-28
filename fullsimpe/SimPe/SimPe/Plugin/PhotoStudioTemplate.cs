// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Drawing;

namespace SimPe.Plugin
{
	/// <summary>
	/// Contain a Template Package
	/// </summary>
	public class PhotoStudioTemplate
	{
		PackedFiles.Wrapper.Cpf pset;
		PackedFiles.Wrapper.Str ctss;

		/// <summary>
		/// Create a new Instance and load the main Template Files
		/// </summary>
		/// <param name="package"></param>
		public PhotoStudioTemplate(Interfaces.Files.IPackageFile package)
		{
			Package = package;

			Interfaces.Files.IPackedFileDescriptor pfd = package.FindFile(
				0xEBCF3E27,
				0xffffffff,
				0xffffffff,
				0xffffffff
			);
			pset = new PackedFiles.Wrapper.Cpf();
			ctss = null;
			if (pfd != null)
			{
				pset.ProcessData(pfd, package);

				pfd = package.FindFile(
					Data.MetaData.CTSS_FILE,
					0xffffffff,
					0xffffffff,
					pset.GetSaveItem("description").UIntegerValue
				);
				if (pfd != null)
				{
					ctss = new PackedFiles.Wrapper.Str();
					ctss.ProcessData(pfd, package);
				}
			}
		}

		/// <summary>
		/// eturns the Base Package
		/// </summary>
		public Interfaces.Files.IPackageFile Package
		{
			get;
		}

		/// <summary>
		/// The Title of this Package
		/// </summary>
		public string Title
		{
			get
			{
				if (ctss == null)
				{
					return Package.FileName;
				}

				PackedFiles.Wrapper.StrItemList items =
					ctss.FallbackedLanguageItems(Helper.WindowsRegistry.LanguageCode);
				return items.Length > 0 ? items[0].Title : Package.FileName;
			}
		}

		/// <summary>
		/// The Type of the Template
		/// </summary>
		/// <remarks>
		/// 0x01 : Picture Template
		/// </remarks>
		public uint Type => pset.GetSaveItem("type").UIntegerValue;

		/// <summary>
		/// The Name of the Texture File
		/// </summary>
		public string TxtrFile => pset.GetSaveItem("texture").StringValue;

		/// <summary>
		/// Returns the Image of the stored Texture
		/// </summary>
		public Image Texture
		{
			get
			{
				try
				{
					Txtr txtr = new Txtr(null, false);

					//load TXTR
					Interfaces.Files.IPackedFileDescriptor[] pfd = Package.FindFile(
						TxtrFile + "_txtr",
						0x1C4A276C
					);
					if (pfd.Length > 0)
					{
						txtr.ProcessData(pfd[0], Package);
					}

					ImageData id = (ImageData)txtr.Blocks[0];
					return id.MipMapBlocks[0]
						.MipMaps[id.MipMapBlocks[0].MipMaps.Length - 1]
						.Texture;
				}
				catch (Exception)
				{
					return new Bitmap(1, 1);
				}
			}
		}

		/// <summary>
		/// The Name of the MATD File
		/// </summary>
		public string MatdFile => pset.GetSaveItem("materialDescription").StringValue;

		/// <summary>
		/// The Instance of the MMAT File
		/// </summary>
		/// <remarks>group=0xffffffff, subType=0x00000000</remarks>
		public uint MmatInstance => pset.GetSaveItem("materialOverride").UIntegerValue;

		/// <summary>
		/// Returns the Rectangle you should place the Custom Image to
		/// </summary>
		public Rectangle TargetRectangle => new Rectangle(
					pset.GetSaveItem("left").IntegerValue,
					pset.GetSaveItem("top").IntegerValue,
					pset.GetSaveItem("width").IntegerValue,
					pset.GetSaveItem("height").IntegerValue
				);

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			Rectangle rect = TargetRectangle;
			return rect.Width.ToString() + "x" + rect.Height.ToString() + ": " + Title;
		}
	}
}
