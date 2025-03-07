// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.Drawing;
using System.IO;

namespace SimPe.Cache
{
	/// <summary>
	/// Contains one ObjectCacheItem
	/// </summary>
	public class MemoryCacheItem : ICacheItem, System.IDisposable
	{
		/// <summary>
		/// The current Version
		/// </summary>
		public const byte VERSION = 3;
		internal const byte DISCARD_VERSIONS_SMALLER_THAN = 3;

		public MemoryCacheItem()
		{
			Version = VERSION;
			Name = "";
			pfd = new Packages.PackedFileDescriptor();
			valuenames = new string[0];
		}

		Interfaces.Files.IPackedFileDescriptor pfd;

		/// <summary>
		/// Returns an (unitialized) FileDescriptor
		/// </summary>
		public Interfaces.Files.IPackedFileDescriptor FileDescriptor
		{
			get
			{
				pfd.Tag = this;
				return pfd;
			}
			set => pfd = value;
		}

		public uint Guid
		{
			get; set;
		}

		public Data.ObjectTypes ObjectType
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		string[] valuenames;
		public string[] ValueNames
		{
			get => valuenames;
			set
			{
				valuenames = value;
				if (valuenames == null)
				{
					valuenames = new string[0];
				}
			}
		}

		string objdname;
		public string ObjdName
		{
			get => objdname ?? Name;
			set => objdname = value;
		}

		static Image emptyimg;

		/// <summary>
		/// Returns the loaded Icon, or an Empty Image if no Icon was found
		/// </summary>
		public Image Image
		{
			get
			{
				if (Icon == null)
				{
					if (emptyimg == null)
					{
						emptyimg = new Bitmap(1, 1);
					}

					return emptyimg;
				}
				return Icon;
			}
			set => Icon = value;
		}

		/// <summary>
		/// Returns the loaded Icon, this can be null!
		/// </summary>
		public Image Icon
		{
			get; set;
		}

		public bool IsToken => IsAspiration
					|| (
						(
							ObjdName.Trim().ToLower().StartsWith("token")
							|| ObjdName.Trim().ToLower().StartsWith("cs - token")
						)
						&& (
							ObjectType == Data.ObjectTypes.Normal
							|| ObjectType == Data.ObjectTypes.Memory
						)
					);

		public bool IsJobData => ObjdName.Trim().ToLower().StartsWith("jobdata")
					&& ObjectType == Data.ObjectTypes.Normal;

		public bool IsMemory => IsToken || ObjectType == Data.ObjectTypes.Memory;

		public bool IsBadge => (
						ObjdName.ToLower().Trim().StartsWith("token - badge")
						|| ObjdName.ToLower().Trim().StartsWith("cs - token - badge")
					)
					&& ObjectType == Data.ObjectTypes.Normal;

		public bool IsSkill => (ObjdName.ToLower().Trim().IndexOf("skill") >= 0)
					&& ObjectType == Data.ObjectTypes.Normal
					&& IsToken;

		public bool IsAspiration => ObjdName.Trim().ToLower().StartsWith("aspiration")
					&& ObjectType == Data.ObjectTypes.Normal;

		public bool IsInventory => !IsAspiration
					&& !IsToken
					&& !IsJobData
					&& !IsMemory
					&& ObjectType == Data.ObjectTypes.Normal;

		public CacheContainer ParentCacheContainer
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
			Version = reader.ReadByte();
			if (Version > VERSION)
			{
				throw new CacheException("Unknown CacheItem Version.", null, Version);
			}

			Name = reader.ReadString();
			objdname = Version >= 2 ? reader.ReadString() : null;

			if (Version >= 3)
			{
				int ct = reader.ReadUInt16();
				valuenames = new string[ct];
				for (int i = 0; i < ct; i++)
				{
					valuenames[i] = reader.ReadString();
				}
			}
			else
			{
				valuenames = new string[0];
			}

			ObjectType = (Data.ObjectTypes)reader.ReadUInt16();
			pfd = new Packages.PackedFileDescriptor
			{
				Type = reader.ReadUInt32(),
				Group = reader.ReadUInt32(),
				LongInstance = reader.ReadUInt64()
			};
			Guid = reader.ReadUInt32();

			int size = reader.ReadInt32();
			if (size == 0)
			{
				Icon = null;
			}
			else
			{
				byte[] data = reader.ReadBytes(size);
				MemoryStream ms = new MemoryStream(data);

				Icon = Image.FromStream(ms);
			}
		}

		public void Save(BinaryWriter writer)
		{
			Version = VERSION;
			writer.Write(Version);
			writer.Write(Name);
			writer.Write(objdname);
			writer.Write((ushort)valuenames.Length);
			foreach (string s in valuenames)
			{
				writer.Write(s);
			}

			writer.Write((ushort)ObjectType);
			writer.Write(pfd.Type);
			writer.Write(pfd.Group);
			writer.Write(pfd.LongInstance);
			writer.Write(Guid);

			if (Icon == null)
			{
				writer.Write(0);
			}
			else
			{
				MemoryStream ms = new MemoryStream();
				Icon.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				byte[] data = ms.ToArray();
				writer.Write(data.Length);
				writer.Write(data);
			}
		}

		public byte Version
		{
			get; private set;
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			Icon = null;
			pfd = null;
			Name = null;
		}

		#endregion
	}
}
