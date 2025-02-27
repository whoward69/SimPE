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
using System.Collections;

using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class Tatt
		: AbstractWrapper //Implements some of the default Behaviur of a Handler, you can Implement yourself if you want more flexibility!
			,
			IFileWrapper //This Interface is used when loading a File
			,
			IFileWrapperSaveExtension //This Interface (if available) will be used to store a File
			,
			IMultiplePackedFileWrapper,
			IEnumerable
	{
		#region Attributes
		/// <summary>
		/// The FileName
		/// </summary>

		public string FileName
		{
			get; set;
		}

		byte[] id;

		/// <summary>
		/// The Version
		/// </summary>
		public uint Version
		{
			get; set;
		}

		/// <summary>
		/// Reserved
		/// </summary>
		public uint Reserved
		{
			get; set;
		}
		ArrayList items;
		#endregion


		/// <summary>
		/// Constructor
		/// </summary>
		public Tatt()
			: base()
		{
			id = new byte[] { (byte)'T', (byte)'T', (byte)'A', (byte)'T' };
			FileName = "";
			Version = 0x4f;

			items = new ArrayList();
		}

		#region IWrapper member
		public override bool CheckVersion(uint version)
		{
			if (
				(version == 0012) //0.10
				|| (version == 0013) //0.12
			)
			{
				return true;
			}

			return false;
		}
		#endregion

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new TattUI();
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
			return new AbstractWrapperInfo(
				"Tatt Wrapper",
				"Quaxi",
				"Content of this File is unknown.",
				1,
				null
			);
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
		{
			items.Clear();
			FileName = Helper.ToString(reader.ReadBytes(0x40));

			id = reader.ReadBytes(0x4);
			Version = reader.ReadUInt32();
			Reserved = reader.ReadUInt32();

			uint ct = reader.ReadUInt32();
			for (int i = 0; i < ct; i++)
			{
				TattItem ti = new TattItem();
				ti.Unserialize(reader);

				items.Add(ti);
			}
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(Helper.ToBytes(FileName, 0x40));

			writer.Write(id);
			writer.Write(Version);
			writer.Write(Reserved);

			writer.Write((uint)items.Count);
			foreach (TattItem ti in items)
			{
				ti.Serialize(writer);
			}
		}
		#endregion

		#region IFileWrapperSaveExtension Member
		//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature => new byte[0];

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
				uint[] types =
				{
					0x54415454, //handles the TATT File
				};
				return types;
			}
		}

		#endregion

		#region IMultiplePackedFileWrapper Member


		#endregion

		/// <summary>
		/// Number of stored Items
		/// </summary>
		public int Count => items.Count;

		public TattItem this[int index]
		{
			get
			{
				return ((TattItem)items[index]);
			}
			set
			{
				items[index] = value;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}
	}
}
