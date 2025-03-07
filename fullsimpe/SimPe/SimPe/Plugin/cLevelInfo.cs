// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.Drawing;

namespace SimPe.Plugin
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class LevelInfo : AbstractRcolBlock
	{
		#region Attributes
		byte[] data;
		Size texturesize;
		Image img;
		MipMapType datatype;

		public Size TextureSize => texturesize;

		public int ZLevel
		{
			get; set;
		}

		public ImageLoader.TxtrFormats Format
		{
			get; set;
		}

		public Image Texture
		{
			get
			{
				if (img == null)
				{
					System.IO.BinaryReader sr = new System.IO.BinaryReader(
						new System.IO.MemoryStream(data)
					);
					img = ImageLoader.Load(
						TextureSize,
						data.Length,
						Format,
						sr,
						1,
						-1
					);
				}
				return img;
			}
			set
			{
				datatype = MipMapType.Texture;
				data = null;
				img = value;
			}
		}

		public byte[] Data
		{
			get => data;
			set
			{
				datatype = MipMapType.SimPE_PlainData;
				data = value;
			}
		}

		#endregion

		//Rcol parent;
		/*public Rcol Parent
		{
			get { return parent; }
		}*/

		/// <summary>
		/// Constructor
		/// </summary>
		public LevelInfo(Rcol parent)
			: base(parent)
		{
			texturesize = new Size(0, 0);
			ZLevel = 0;
			sgres = new SGResource(null);
			BlockID = 0xED534136;
			data = new byte[0];
			datatype = MipMapType.SimPE_PlainData;
		}

		#region IRcolBlock Member

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public override void Unserialize(System.IO.BinaryReader reader)
		{
			version = reader.ReadUInt32();
			string s = reader.ReadString();

			sgres.BlockID = reader.ReadUInt32();
			sgres.Unserialize(reader);

			int w = reader.ReadInt32();
			int h = reader.ReadInt32();
			texturesize = new Size(w, h);
			ZLevel = reader.ReadInt32();

			int size = reader.ReadInt32();

			if (Parent.Fast)
			{
				reader.BaseStream.Seek(size, System.IO.SeekOrigin.Current);
				texturesize = new Size(0, 0);
				img = null;
				return;
			}
			/*if (size == w*h) format = ImageLoader.TxtrFormats.DXT3Format;
			else*/
			Format = ImageLoader.TxtrFormats.DXT1Format;
			//Pumckl Contribution
			//-- 8< --------------------------------------------- 8< -----
			if (size == 4 * w * h)
			{
				Format = ImageLoader.TxtrFormats.Raw32Bit;
			}
			else if (size == 3 * w * h)
			{
				Format = ImageLoader.TxtrFormats.Raw24Bit;
			}
			else if (size == w * h) // could be RAW8, DXT3 or DXT5
			{
				// it seems to be difficult to determine the right format
				if (sgres.FileName.IndexOf("bump") > 0)
				{ // its a bump-map
					Format = ImageLoader.TxtrFormats.Raw8Bit;
				}
				else
				{
					// i expect the upper left 4x4 corner of the pichture have
					// all the same alpha so i can determine if it's DXT5
					// i guess, it's somewhat dirty but what can i do else?
					long pos = reader.BaseStream.Position;
					ulong alpha = reader.ReadUInt64(); // read the first 8 byte of the image
					reader.BaseStream.Position = pos;
					// on DXT5 if all alpha are the same the bytes 0 or 1 are not zero
					// and the bytes 2-7 (codebits) ara all zero
					Format = ((alpha & 0xffffffffffff0000) == 0) && ((alpha & 0xffff) != 0)
						? ImageLoader.TxtrFormats.DXT5Format
						: ImageLoader.TxtrFormats.DXT3Format;
				}
			}
			else
			{
				Format = ImageLoader.TxtrFormats.DXT1Format; // size < w*h
			}
			//-- 8< --------------------------------------------- 8< -----


			long p1 = reader.BaseStream.Position;
			size = (int)(reader.BaseStream.Length - p1);
			{
				datatype = MipMapType.SimPE_PlainData;
			}

			data = reader.ReadBytes(size);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(version);
			string s = sgres.Register(null);
			writer.Write(s);
			writer.Write(sgres.BlockID);
			sgres.Serialize(writer);

			writer.Write(texturesize.Width);
			writer.Write(texturesize.Height);
			writer.Write(ZLevel);

			if (datatype == MipMapType.Texture)
			{
				data = ImageLoader.Save(Format, img);
			}

			if (data == null)
			{
				data = new byte[0];
			}

			writer.Write(data.Length);
			writer.Write(data);
		}
		#endregion

		#region IDisposable Member

		public override void Dispose()
		{
		}

		#endregion
	}
}
