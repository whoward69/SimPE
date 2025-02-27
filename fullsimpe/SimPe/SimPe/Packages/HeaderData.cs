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
using System;
using System.ComponentModel;
using System.IO;

namespace SimPe.Packages
{
	/// <summary>
	/// Structural Data of a .package Header
	/// </summary>
	public class HeaderData : Interfaces.Files.IPackageHeader, IDisposable
	{
		/// <summary>
		/// Constructor for the class
		/// </summary>
		internal HeaderData()
		{
			LockIndexDuringLoad = false;
			index = new HeaderIndex(this);
			hole = new HeaderHole();
			id = new char[4];
			reserved_00 = new Int32[3];
			reserved_02 = new Int32[7];

			id[0] = 'D';
			id[1] = 'B';
			id[2] = 'P';
			id[3] = 'F';

			majorversion = 1;
			minorversion = 1;
			index.Type = 7;

			epicon = 0;
			showicon = 0;

			indextype = Data.MetaData.IndexTypes.ptLongFileIndex;
		}

		/// <summary>
		/// Identifier of the File
		/// </summary>
		internal char[] id;

		/// <summary>
		/// Returns the Identifier of the File
		/// </summary>
		/// <remarks>This value should be DBPF</remarks>
		[Description("Package Identifier"), DefaultValue("DBPF")]
		public string Identifier
		{
			get
			{
				string ret = "";
				foreach (char c in id)
				{
					ret += c;
				}

				return ret;
			}
		}

		/// <summary>
		/// The Icon to display (for lot packages)
		/// </summary>
		internal Int16 epicon;

		[
			Description("The Icon to display for this Package"),
			Category("Icon"),
			DefaultValue(0)
		]
		public Int16 Epicon
		{
			get
			{
				return epicon;
			}
			set
			{
				epicon = value;
			}
		}

		/// <summary>
		/// Should the defined Icon be shown : 1 is true (for lot packages)
		/// </summary>
		internal Int16 showicon;

		[
			Description("Should an Icon display for this Package"),
			Category("Icon"),
			DefaultValue(0)
		]
		public Int16 Showicon
		{
			get
			{
				return showicon;
			}
			set
			{
				showicon = value;
			}
		}

		/// <summary>
		/// The Major Version (part before the .) of the Package File Format
		/// </summary>
		internal Int32 majorversion;

		/// <summary>
		/// Returns the Major Version of The Packages FileFormat
		/// </summary>
		/// <remarks>This value should be 1</remarks>
		[
			Description("Major Version Number of this Package"),
			Category("Version"),
			DefaultValue(1)
		]
		public Int32 MajorVersion => majorversion;

		/// <summary>
		/// The Minor Version (part after the .) of the Package File Format
		/// </summary>
		internal Int32 minorversion;

		/// <summary>
		/// Returns the Minor Version of The Packages FileFormat
		/// </summary>
		/// <remarks>This value should be 0 or 1</remarks>
		[
			Description("Minor Version Number of this Package"),
			Category("Version"),
			DefaultValue(1)
		]
		public Int32 MinorVersion => minorversion;

		/// <summary>
		/// Returns the Overall Version of this Package
		/// </summary>
		[
			Description("Overall Versionnumber of this Package"),
			Category("Version"),
			DefaultValue(4294967297)
		]
		public long Version => (long)((ulong)MajorVersion << 0x20) | (uint)MinorVersion;

		/// <summary>
		/// 3 dwords of reserved Data
		/// </summary>
#if DEBUG
		public Int32[] reserved_00;
#else
		internal Int32[] reserved_00;
#endif

		/// <summary>
		/// Createion Date of the File
		/// </summary>
#if DEBUG
		public uint created;

		[
			Description("Creation Date of the Package"),
			Category("Debug"),
			ReadOnly(true)
		]
#else
		public uint Ident
		{
			get
			{
				return Created;
			}
		}
		internal uint created;

		[
			DescriptionAttribute("Creation Date of the Package"),
			CategoryAttribute("Debug"),
			Browsable(false)
		]
#endif
		public uint Created
		{
			get
			{
				return created;
			}
			set
			{
				created = value;
			}
		}

		/// <summary>
		/// Modification Date of the File
		/// </summary>
#if DEBUG
		public Int32 modified;

		[
			Description("Modification Date of the Package"),
			Category("Debug")
		]
		public Int32 Modified => modified;
#else
		internal Int32 modified;
#endif

		/// <summary>
		/// holds Index Informations stored in the Header
		/// </summary>
		internal HeaderIndex index;

		/// <summary>
		/// Returns Index Informations stored in the Header
		/// </summary>
		[Browsable(false)]
		public Interfaces.Files.IPackageHeaderIndex Index => index;

		/// <summary>
		/// Holds Hole Index Informations stored in the Header
		/// </summary>
		internal HeaderHole hole;

		/// <summary>
		/// Returns Hole Index Informations stored in the Header
		/// </summary>
		[Browsable(false)]
		public Interfaces.Files.IPackageHeaderHoleIndex HoleIndex => hole;

		/// <summary>
		/// Only available for versions >= 1.1
		/// </summary>
		private Data.MetaData.IndexTypes indextype;

		/// <summary>
		/// Returns or Sets the Type of the Package
		/// </summary>
		[
			Description(
				"The Indextype used in the Package. ptLongFileIndex allows the use of the \"Instance (high)\" Value."
			),
			DefaultValue(Data.MetaData.IndexTypes.ptLongFileIndex)
		]
		public Data.MetaData.IndexTypes IndexType
		{
			get
			{
				return (Data.MetaData.IndexTypes)indextype;
			}
			set
			{
				indextype = value;
			}
		}

		/// <summary>
		/// 7 dwords of reserved Data - was 8 but have lost one for Icon in lot files
		/// </summary>
#if DEBUG
		public Int32[] reserved_02;

		[Description("Reserved Values"), Category("Debug")]
		public Int32[] Reserved => reserved_02;
#else
		internal Int32[] reserved_02;
#endif

		/// <summary>
		/// true if the version is greater or equal than 1.1
		/// </summary>
		[Browsable(false)]
		public bool IsVersion0101 => (Version >= 0x100000001); //((majorversion>1) || ((majorversion==1) && (minorversion>=1)));

		internal bool LockIndexDuringLoad
		{
			get; set;
		}

		#region File Processing Methods
		static string spore =
			"\r\n\r\nSimPe is a package editor for Sims2 packages only.";

		/// <summary>
		/// Initializes the Structure from a BinaryReader
		/// </summary>
		/// <param name="reader">The Reader representing the Package File</param>
		/// <remarks>Reader must be on the correct Position since no Positioning is performed</remarks>
		internal void Load(BinaryReader reader)
		{
			//this.id = new char[4];
			for (uint i = 0; i < this.id.Length; i++)
			{
				this.id[i] = reader.ReadChar();
			}

			if (!Helper.AnyPackage && Identifier != "DBPF")
			{
				throw new InvalidOperationException(
					"SimPe does not support this type of file." + spore
				);
			}

			this.majorversion = reader.ReadInt32();
			if (!Helper.AnyPackage && this.majorversion > 1)
			{
				throw new InvalidOperationException(
					"SimPe does not support this version of DBPF file." + spore
				);
			}

			this.minorversion = reader.ReadInt32();

			//this.reserved_00 = new Int32[3];
			for (uint i = 0; i < this.reserved_00.Length; i++)
			{
				this.reserved_00[i] = reader.ReadInt32();
			}

			this.created = reader.ReadUInt32();
			this.modified = reader.ReadInt32();

			this.index.type = reader.ReadInt32();
			if (!LockIndexDuringLoad)
			{
				this.index.count = reader.ReadInt32();
				this.index.offset = reader.ReadUInt32();
				this.index.size = reader.ReadInt32();
			}
			else
			{
				reader.ReadInt32();
				reader.ReadInt32();
				reader.ReadInt32(); //count, offset, size
			}

			this.hole.count = reader.ReadInt32();
			this.hole.offset = reader.ReadUInt32();
			this.hole.size = reader.ReadInt32();

			if (IsVersion0101)
			{
				this.indextype = (Data.MetaData.IndexTypes)reader.ReadUInt32();
			}

			this.epicon = reader.ReadInt16();
			this.showicon = reader.ReadInt16();

			//this.reserved_02 = new Int32[8];
			for (uint i = 0; i < this.reserved_02.Length; i++)
			{
				this.reserved_02[i] = reader.ReadInt32();
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="writer"></param>
		/// <remarks>Writer must be on the correct Position since no Positioning is performed</remarks>
		internal void Save(BinaryWriter writer)
		{
			for (uint i = 0; i < this.id.Length; i++)
			{
				writer.Write(this.id[i]);
			}

			writer.Write(this.majorversion);
			writer.Write(this.minorversion);

			for (uint i = 0; i < this.reserved_00.Length; i++)
			{
				writer.Write(this.reserved_00[i]);
			}

			writer.Write(this.created);
			writer.Write(this.modified);

			writer.Write(this.index.type);
			writer.Write(this.index.count);
			writer.Write(this.index.offset);
			writer.Write(this.index.size);

			writer.Write(this.hole.count);
			writer.Write(this.hole.offset);
			writer.Write(this.hole.size);

			if (IsVersion0101)
			{
				writer.Write((uint)this.IndexType);
			}

			writer.Write(this.epicon);
			writer.Write(this.showicon);

			for (uint i = 0; i < this.reserved_02.Length; i++)
			{
				writer.Write(this.reserved_02[i]);
			}
		}
		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			this.hole = null;
			this.index = null;
			this.reserved_00 = null;
			this.reserved_02 = null;
			this.id = null;
		}

		#endregion

		public object Clone()
		{
			HeaderData iph = new HeaderData();
			iph.created = this.created;
			iph.id = this.id;
			iph.indextype = this.indextype;
			iph.majorversion = this.majorversion;
			iph.minorversion = this.minorversion;
			iph.modified = this.modified;

			iph.reserved_00 = this.reserved_00;
			iph.reserved_02 = this.reserved_02;

			iph.epicon = this.epicon;
			iph.showicon = this.showicon;
			return (Interfaces.Files.IPackageHeader)iph;
		}
	}
}
