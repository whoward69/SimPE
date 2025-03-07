// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.Collections;
using System.Drawing;
using System.IO;

namespace SimPe.Cache
{
	/// <summary>
	/// Type of a Package
	/// </summary>
	public enum PackageType : uint
	{
		/// <summary>
		/// This package was never scaned
		/// </summary>
		Undefined = 0x40,

		/// <summary>
		/// The Package was scanned, but the Type is unknown
		/// </summary>
		Unknown = 0x0,

		/// <summary>
		/// The package contains a Skin
		/// </summary>
		Skin = 0x1,

		/// <summary>
		/// The package contains a Wallpaper
		/// </summary>
		Wallpaper = 0x2,

		/// <summary>
		/// The package contains a Floor
		/// </summary>
		Floor = 0x4,

		/// <summary>
		/// The package contains a Clothing
		/// </summary>
		Clothing = 0x8,

		/// <summary>
		/// The package contains a Crap Object or Clone
		/// </summary>
		CustomObject = 0x10,

		/// <summary>
		/// The package contains a Recolour
		/// </summary>
		Recolour = 0x20,

		/// <summary>
		/// An Object properly created
		/// </summary>
		Object = 0x80,

		/// <summary>
		/// A CEP Related File
		/// </summary>
		CEP = 0x100,

		/// <summary>
		/// A Sim or Sim Template
		/// </summary>
		Sim = 0x200,

		/// <summary>
		/// Hairtones
		/// </summary>
		Hair = 0x1000,

		/// <summary>
		/// Makeup for Sims
		/// </summary>
		Makeup = 0x400,
		Accessory = 0x800,
		Eye = 0x401,
		Beard = 0x402,
		EyeBrow = 0x403,
		Lipstick = 0x404,
		Mask = 0x405,
		Blush = 0x406,
		EyeShadow = 0x407,
		Glasses = 0x801,

		/// <summary>
		/// Contains a Neighborhood
		/// </summary>
		Neighbourhood = 0x2000,

		/// <summary>
		/// Contains a Lot
		/// </summary>
		Lot = 0x4000,

		/// <summary>
		/// Describes a Fence
		/// </summary>
		Fence = 0x8000,

		/// <summary>
		/// Describes a Roof
		/// </summary>
		Roof = 0x10000,

		/// <summary>
		/// Describes TerrainPaint
		/// </summary>
		Terrain = 0x20000,

		/// <summary>
		/// Describes the Game Wide Inventory
		/// </summary>
		GameInventory = 0x40000,
	}

	/// <summary>
	/// Adds the Null State to the Bollen states
	/// </summary>
	public enum TriState : byte
	{
		False = 0,
		True = 1,
		Null = 2,
	}

	/// <summary>
	/// This class can give Informations about the State of a Package
	/// </summary>
	/// <remarks>
	/// You can save diffrent informations along with a package file, each state (like contains duplicate GUID)
	/// has it's own uid. A TriState::Null measn, that the state was not ionvestigated yet
	/// </remarks>
	public class PackageState
	{
		uint[] data;

		public PackageState(uint uid, TriState state, string info)
		{
			Uid = uid;
			State = state;
			Info = info;
			data = new uint[0];
		}

		internal PackageState()
		{
			State = TriState.Null;
			Info = "";
		}

		public TriState State
		{
			get; set;
		}

		public uint Uid
		{
			get; set;
		}

		public string Info
		{
			get; set;
		}

		public uint[] Data
		{
			get
			{
				if (data == null)
				{
					data = new uint[0];
				}

				return data;
			}
			set => data = value;
		}

		internal virtual void Load(BinaryReader reader)
		{
			State = (TriState)reader.ReadByte();
			Uid = reader.ReadUInt32();
			Info = reader.ReadString();
			byte ct = reader.ReadByte();
			data = new uint[ct];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = reader.ReadUInt32();
			}
		}

		internal virtual void Save(BinaryWriter writer)
		{
			writer.Write((byte)State);
			writer.Write(Uid);
			writer.Write(Info);

			if (data == null)
			{
				writer.Write((byte)0);
			}
			else
			{
				byte ct = (byte)data.Length;
				writer.Write(ct);
				for (int i = 0; i < ct; i++)
				{
					writer.Write(data[i]);
				}
			}
		}
	}

	/// <summary>
	/// Typesave ArrayList for PackageState Objects
	/// </summary>
	public class PackageStates : ArrayList
	{
		public new PackageState this[int index]
		{
			get => (PackageState)base[index];
			set => base[index] = value;
		}

		public PackageState this[uint index]
		{
			get => (PackageState)base[(int)index];
			set => base[(int)index] = value;
		}

		public int Add(PackageState item)
		{
			return base.Add(item);
		}

		public void Insert(int index, PackageState item)
		{
			base.Insert(index, item);
		}

		public void Remove(PackageState item)
		{
			base.Remove(item);
		}

		public bool Contains(PackageState item)
		{
			return base.Contains(item);
		}

		public int Length => Count;

		public override object Clone()
		{
			PackageStates list = new PackageStates();
			foreach (PackageState item in this)
			{
				list.Add(item);
			}

			return list;
		}
	}

	/// <summary>
	/// Contains one ObjectCacheItem
	/// </summary>
	public class PackageCacheItem : ICacheItem
	{
		/// <summary>
		/// The current Version
		/// </summary>
		public const byte VERSION = 1;

		public PackageCacheItem()
		{
			version = VERSION;
			Name = "";
			Guids = new uint[0];
			Type = PackageType.Undefined;
			States = new PackageStates();
		}

		protected byte version;

		public uint[] Guids
		{
			get; set;
		}

		public PackageType Type
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public Image Thumbnail
		{
			get; set;
		}

		public PackageStates States
		{
			get; set;
		}

		public int StateCount => States.Count;

		/// <summary>
		/// Returns a matching Item for the passed State-uid
		/// </summary>
		/// <param name="uid">the unique ID of the state</param>
		/// <param name="create">true if you want to create a new state (and add it) if it did not exist</param>
		/// <returns></returns>
		public PackageState FindState(uint uid, bool create)
		{
			foreach (PackageState ps in States)
			{
				if (ps.Uid == uid)
				{
					return ps;
				}
			}

			if (create)
			{
				PackageState ps = new PackageState
				{
					Uid = uid
				};

				States.Add(ps);
				return ps;
			}

			return null;
		}

		public bool Enabled
		{
			get; set;
		}

		public override string ToString()
		{
			return "name=" + Name;
		}

		#region ICacheItem Member

		public void Load(BinaryReader reader)
		{
			States.Clear();
			version = reader.ReadByte();
			if (version > VERSION)
			{
				throw new CacheException("Unknown CacheItem Version.", null, version);
			}

			Name = reader.ReadString();
			Type = (PackageType)reader.ReadUInt32();
			Enabled = reader.ReadBoolean();
			int ct = reader.ReadByte();
			Guids = new uint[ct];
			for (int i = 0; i < Guids.Length; i++)
			{
				Guids[i] = reader.ReadUInt32();
			}

			ct = reader.ReadByte();
			for (int i = 0; i < ct; i++)
			{
				PackageState ps = new PackageState();
				ps.Load(reader);
				States.Add(ps);
			}

			int size = reader.ReadInt32();
			if (size == 0)
			{
				Thumbnail = null;
			}
			else
			{
				byte[] data = reader.ReadBytes(size);
				MemoryStream ms = new MemoryStream(data);

				Thumbnail = Image.FromStream(ms);
			}
		}

		public void Save(BinaryWriter writer)
		{
			version = VERSION;
			writer.Write(version);
			writer.Write(Name);
			writer.Write((uint)Type);
			writer.Write(Enabled);

			writer.Write((byte)Guids.Length);
			for (int i = 0; i < Guids.Length; i++)
			{
				writer.Write(Guids[i]);
			}

			byte ct = (byte)States.Count;
			writer.Write(ct);
			for (int i = 0; i < ct; i++)
			{
				States[i].Save(writer);
			}

			if (Thumbnail == null)
			{
				writer.Write(0);
			}
			else
			{
				MemoryStream ms = new MemoryStream();
				Thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				byte[] data = ms.ToArray();
				writer.Write(data.Length);
				writer.Write(data);
			}
		}

		public byte Version => version;

		#endregion
	}
}
