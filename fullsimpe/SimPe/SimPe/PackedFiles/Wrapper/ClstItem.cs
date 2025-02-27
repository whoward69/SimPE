/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/
namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// An Item stored in a CPF File
	/// </summary>
	public class ClstItem
	{
		Data.MetaData.IndexTypes format;

		/// <summary>
		/// Constructor
		/// </summary>
		public ClstItem(Data.MetaData.IndexTypes format)
			: this(null, format) { }

		/// <summary>
		/// Constructor
		/// </summary>
		public ClstItem(
			Interfaces.Files.IPackedFileDescriptor pfd,
			Data.MetaData.IndexTypes format
		)
		{
			this.format = format;
			if (pfd != null)
			{
				Type = pfd.Type;
				Instance = pfd.Instance;
				SubType = pfd.SubType;
				Group = pfd.Group;
			}
		}

		/// <summary>
		/// Returns the Type of the referenced File
		/// </summary>
		public uint Type
		{
			get; set;
		}

		/// <summary>
		/// Returns the Name of the represented Type
		/// </summary>
		public Data.TypeAlias TypeName => Data.MetaData.FindTypeAlias(Type);

		/// <summary>
		/// Returns the Group the referenced file is assigned to
		/// </summary>
		public uint Group
		{
			get; set;
		}

		/// <summary>
		/// Returns the Instance Data
		/// </summary>
		public uint Instance
		{
			get; set;
		}

		/// <summary>
		/// Returns an yet unknown Type
		/// </summary>
		/// <remarks>Only in Version 1.1 of package Files</remarks>
		public uint SubType
		{
			get; set;
		}

		/// <summary>
		/// Returns the (real) uncompressed Size of the File
		/// </summary>
		public uint UncompressedSize
		{
			get; set;
		}

		public override int GetHashCode()
		{
			return (int)(Type | Instance) - (int)(Type & Instance);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj is ClstItem)
			{
				ClstItem ci = (ClstItem)obj;
				return (
					ci.Group == Group
					&& ci.Instance == Instance
					&& ci.Type == Type
					&& (
						ci.SubType == SubType
						|| ci.format == Data.MetaData.IndexTypes.ptShortFileIndex
						|| format == Data.MetaData.IndexTypes.ptShortFileIndex
					)
				);
			}
			else
			{
				return obj is Interfaces.Files.IPackedFileDescriptor ci
					? ci.Group == Group
									&& ci.Instance == Instance
									&& ci.Type == Type
									&& (
										ci.SubType == SubType
										|| format == Data.MetaData.IndexTypes.ptShortFileIndex
									)
					: base.Equals(obj);
			}
		}

		public override string ToString()
		{
			string name = TypeName + ": 0x" + Helper.HexString(Type);
			if (format == Data.MetaData.IndexTypes.ptLongFileIndex)
			{
				name += " - 0x" + Helper.HexString(SubType);
			}

			name +=
				" - 0x"
				+ Helper.HexString(Group)
				+ " - 0x"
				+ Helper.HexString(Instance);

			name += " = 0x" + Helper.HexString(UncompressedSize) + " byte";
			return name;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal void Unserialize(System.IO.BinaryReader reader)
		{
			Type = reader.ReadUInt32();
			Group = reader.ReadUInt32();
			Instance = reader.ReadUInt32();
			SubType = format == Data.MetaData.IndexTypes.ptLongFileIndex ? reader.ReadUInt32() : 0;

			UncompressedSize = reader.ReadUInt32();
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal void Serialize(
			System.IO.BinaryWriter writer,
			Data.MetaData.IndexTypes format
		)
		{
			this.format = format;

			writer.Write(Type);
			writer.Write(Group);
			writer.Write(Instance);
			if (format == Data.MetaData.IndexTypes.ptLongFileIndex)
			{
				writer.Write(SubType);
			}

			writer.Write(UncompressedSize);
		}
	}
}
