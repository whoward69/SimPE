// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections.Generic;
using System.Drawing;

using SimPe.Forms.MainUI;
using SimPe.Interfaces;
using SimPe.Interfaces.Scenegraph;

namespace SimPe.Plugin
{
	public class BuildTxtr : ICommandLine
	{
		#region Build TXTR
		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadTXTR(
			ImageData id,
			string flname,
			Size sz,
			int levels,
			ImageLoader.TxtrFormats format
		)
		{
			try
			{
				Image src = Image.FromFile(flname);
				LoadTXTR(id, src, sz, levels, format);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadTXTR(
			ImageData id,
			Image src,
			Size sz,
			int levels,
			ImageLoader.TxtrFormats format
		)
		{
			try
			{
				id.TextureSize = sz;
				id.Format = format;
				id.MipMapLevels = (uint)levels;

				Image img = new Bitmap(sz.Width, sz.Height);

				Graphics gr = Graphics.FromImage(img);

				gr.CompositingQuality = System
					.Drawing
					.Drawing2D
					.CompositingQuality
					.HighQuality;
				gr.InterpolationMode = System
					.Drawing
					.Drawing2D
					.InterpolationMode
					.HighQualityBicubic;
				gr.DrawImage(
					src,
					new Rectangle(new Point(0, 0), img.Size),
					new Rectangle(new Point(0, 0), src.Size),
					GraphicsUnit.Pixel
				);

				MipMap[] maps = new MipMap[levels];
				int wd = 1;
				int hg = 1;

				//build default Sizes
				for (int i = 0; i < levels; i++)
				{
					MipMap mm = new MipMap(id)
					{
						Texture = new Bitmap(wd, hg)
					};

					if ((wd == hg) && (wd == 1))
					{
						if (id.TextureSize.Width > id.TextureSize.Height)
						{
							wd = id.TextureSize.Width / id.TextureSize.Height;
							hg = 1;
						}
						else
						{
							hg = id.TextureSize.Height / id.TextureSize.Width;
							wd = 1;
						}

						if ((wd == hg) && (wd == 1))
						{
							wd *= 2;
							hg *= 2;
						}
					}
					else
					{
						wd *= 2;
						hg *= 2;
					}

					maps[i] = mm;
				}

				//create a Scaled Version for each testure
				for (int i = 0; i < maps.Length; i++)
				{
					MipMap mm = maps[i];
					if (img != null)
					{
						Image bm = mm.Texture;
						gr = Graphics.FromImage(bm);

						gr.CompositingQuality = System
							.Drawing
							.Drawing2D
							.CompositingQuality
							.HighQuality;
						gr.InterpolationMode = System
							.Drawing
							.Drawing2D
							.InterpolationMode
							.HighQualityBicubic;
						gr.DrawImage(
							img,
							new Rectangle(new Point(0, 0), bm.Size),
							new Rectangle(new Point(0, 0), img.Size),
							GraphicsUnit.Pixel
						);

						id.TextureSize = new Size(bm.Width, bm.Height);
					}
				} // for i

				MipMapBlock[] mmps = new MipMapBlock[1];
				mmps[0] = new MipMapBlock(id)
				{
					MipMaps = maps
				};

				id.MipMapBlocks = mmps;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.Message);
			}
		}

		/// <summary>
		/// Assemble a Picture File
		/// </summary>
		/// <param name="data"></param>
		public static void LoadDDS(ImageData id, DDSData[] data)
		{
			if (data == null)
			{
				return;
			}

			if (data.Length > 0)
			{
				try
				{
					id.TextureSize = data[0].ParentSize;
					id.Format = data[0].Format;
					id.MipMapLevels = (uint)data.Length;

					MipMap[] maps = new MipMap[data.Length];
					int ct = 0;
					for (int i = data.Length - 1; i >= 0; i--)
					{
						DDSData item = data[i];
						MipMap mm = new MipMap(id)
						{
							Texture = item.Texture,
							Data = item.Data
						};

						maps[ct++] = mm;
					}

					MipMapBlock[] mmps = new MipMapBlock[1];
					mmps[0] = new MipMapBlock(id)
					{
						MipMaps = maps
					};

					id.MipMapBlocks = mmps;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Exception: " + ex.Message);
				}
			}
		}
		#endregion

		#region ICommandLine Members

		public bool Parse(List<string> argv)
		{
			int i = ArgParser.Parse(argv, "-txtr");
			if (i < 0)
			{
				return false;
			}

			//get Parameters
			string filename = "";
			string output = "";
			string texturename = "";
			string num = "";
			int levels = 9;
			Size sz = new Size(512, 512);
			ImageLoader.TxtrFormats format =

				ImageLoader
				.TxtrFormats
				.DXT1Format;

			while (argv.Count > i)
			{
				if (ArgParser.Parse(argv, i, "-image", ref filename))
				{
					continue;
				}

				if (ArgParser.Parse(argv, i, "-out", ref output))
				{
					continue;
				}

				if (ArgParser.Parse(argv, i, "-name", ref texturename))
				{
					continue;
				}

				if (ArgParser.Parse(argv, i, "-levels", ref num))
				{
					levels = Convert.ToInt32(num);
					continue;
				}
				if (ArgParser.Parse(argv, i, "-width", ref num))
				{
					sz.Width = Convert.ToInt32(num);
					continue;
				}
				if (ArgParser.Parse(argv, i, "-height", ref num))
				{
					sz.Height = Convert.ToInt32(num);
					continue;
				}
				if (ArgParser.Parse(argv, i, "-format", ref num))
				{
					switch (num)
					{
						case "dxt1":
							format = ImageLoader.TxtrFormats.DXT1Format;
							break;
						case "dxt3":
							format = ImageLoader.TxtrFormats.DXT3Format;
							break;
						case "dxt5":
							format = ImageLoader.TxtrFormats.DXT5Format;
							break;
						case "raw24":
							format = ImageLoader.TxtrFormats.Raw24Bit;
							break;
						case "raw32":
							format = ImageLoader.TxtrFormats.Raw32Bit;
							break;
						case "raw8":
							format = ImageLoader.TxtrFormats.Raw8Bit;
							break;
					}
					continue;
				}
				Message.Show(Help()[0]);
				return true;
			}

			//check if the File exists
			if (!System.IO.File.Exists(filename))
			{
				Message.Show(filename + " was not found.");
				return true;
			}
			if (output.Trim() == "")
			{
				Message.Show("Please specify an output file using -out");
				return true;
			}

			//build TXTR File
			ImageData id = new ImageData(null);

			if (
				System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool)
				&& (
					(format == ImageLoader.TxtrFormats.DXT1Format)
					|| (format == ImageLoader.TxtrFormats.DXT3Format)
					|| (format == ImageLoader.TxtrFormats.DXT5Format)
				)
			)
			{
				LoadDDS(
					id,
					DDSTool.BuildDDS(
						filename,
						levels,
						format,
						"-sharpenMethod Smoothen"
					)
				);
			}
			else
			{
				LoadTXTR(id, filename, sz, levels, format);
			}

			Rcol rcol = new GenericRcol(null, false)
			{
				FileName = texturename,
				FileDescriptor = new Packages.PackedFileDescriptor(),
				Blocks = new IRcolBlock[1]
			};
			rcol.Blocks[0] = id;

			rcol.SynchronizeUserData();
			System.IO.FileStream fs = System.IO.File.Create(output);
			System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
			bw.Write(rcol.FileDescriptor.UserData);
			bw.Close();
			bw = null;
			fs.Close();
			fs.Dispose();
			fs = null;

			return true;
		}

		public string[] Help()
		{
			return new string[]
			{
				"-txtr -image [imgfile] -out [output].package -name [textureNam] "
					+ "-format [dxt1|dxt3|dxt5|raw8|raw24|raw32] -levels [nr] -width [max. Width] -height [max. Height]",
				null,
			};
		}

		#endregion
	}
}
