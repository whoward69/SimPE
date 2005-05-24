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
using System.IO;
using System.Globalization;
using System.Collections;

namespace SimPe.Plugin.Gmdc
{
	#region Vectors
	/// <summary>
	/// Contains the a 3D Vector
	/// </summary>
	public class Vector3f 
	{
		float x, y, z;
		
		/// <summary>
		/// The X Coordinate of teh Vector
		/// </summary>
		public float X 
		{
			get { return x; }
			set { x = value; }
		}
		/// <summary>
		/// The Y Coordinate of teh Vector
		/// </summary>
		public float Y 
		{
			get { return y; }
			set { y = value; }
		}
		/// <summary>
		/// The Z Coordinate of teh Vector
		/// </summary>
		public float Z 
		{
			get { return z; }
			set { z = value; }
		}

		internal Vector3f ()
		{
			x = 0; y = 0; z = 0;
		}

		/// <summary>
		/// Creates new Vector Instance
		/// </summary>
		/// <param name="x">X-Coordinate</param>
		/// <param name="y">Y-Coordinate</param>
		/// <param name="z">Z-Coordinate</param>
		public Vector3f (float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			x = reader.ReadSingle();			
			y = reader.ReadSingle();
			z = reader.ReadSingle();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal virtual void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
		}

		/// <summary>
		/// This output is used in the ListBox View
		/// </summary>
		/// <returns>A String Describing the Data</returns>
		public override string ToString()
		{
			return x.ToString("N6")+", "+y.ToString("N6")+", "+z.ToString("N6");
		}
	}

	/// <summary>
	/// Contains the a 3D Vector
	/// </summary>
	public class Vector3i 
	{
		int x, y, z;
		
		/// <summary>
		/// The X Coordinate of the Vector
		/// </summary>
		public int X 
		{
			get { return x; }
			set { x = value; }
		}
		/// <summary>
		/// The Y Coordinate of the Vector
		/// </summary>
		public int Y 
		{
			get { return y; }
			set { y = value; }
		}
		/// <summary>
		/// The Z Coordinate of the Vector
		/// </summary>
		public int Z 
		{
			get { return z; }
			set { z = value; }
		}

		internal Vector3i ()
		{
			x = 0; y = 0; z = 0;
		}

		/// <summary>
		/// Creates new Vector Instance
		/// </summary>
		/// <param name="x">X-Coordinate</param>
		/// <param name="y">Y-Coordinate</param>
		/// <param name="z">Z-Coordinate</param>
		public Vector3i (int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			x = reader.ReadInt32();
			y = reader.ReadInt32();
			z = reader.ReadInt32();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal virtual void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
		}

		/// <summary>
		/// This output is used in the ListBox View
		/// </summary>
		/// <returns>A String Describing the Data</returns>
		public override string ToString()
		{
			return Helper.HexString(x) + ", " + Helper.HexString(y) + ", " + Helper.HexString(z);
		}

	}

	/// <summary>
	/// Contains the a 4D Vector
	/// </summary>
	public class Vector4f : Vector3f
	{
		float w;
		/// <summary>
		/// The 4th Component of an Vector (often used as focal Point)
		/// </summary>
		public float W
		{
			get { return w; }
			set { w = value; }
		}

		internal Vector4f () : base()
		{
			w = 0;
		}

		/// <summary>
		/// Creates new Vector Instance
		/// </summary>
		/// <param name="x">X-Coordinate</param>
		/// <param name="y">Y-Coordinate</param>
		/// <param name="z">Z-Coordinate</param>
		/// <param name="w">4th-Coordinate (often the focal Point)</param>
		public Vector4f (float x, float y, float z, float w) : base(x, y, z)
		{
			this.w = w;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal override void Unserialize(System.IO.BinaryReader reader)
		{
			base.Unserialize(reader);
			w = reader.ReadSingle();				
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal override void Serialize(System.IO.BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(w);
		}

		/// <summary>
		/// This output is used in the ListBox View
		/// </summary>
		/// <returns>A String Describing the Data</returns>
		public override string ToString()
		{
			return base.ToString()+", "+w.ToString("N6");
		}
	}
	#endregion

	/// <summary>
	/// Stores two String Items
	/// </summary>
	public class GmdcNamePair 
	{
		string blendname;
		/// <summary>
		/// The Name of the Belnding Group
		/// </summary>
		public string BlendGroupName 
		{
			get { return blendname; }
			set { blendname = value; }
		}

		string elementname;
		/// <summary>
		/// The Name of the Element that should be assigned to that Group
		/// </summary>
		public string AssignedElementName
		{
			get { return elementname; }
			set { elementname = value; }
		}

		internal GmdcNamePair()
		{
			blendname = "";
			elementname = "";
		}

		/// <summary>
		/// Creates a new Instance
		/// </summary>
		/// <param name="blend">Name of the Blendgroup</param>
		/// <param name="element">Name of the Element that should be assigned to that Blend Group</param>
		public GmdcNamePair(string blend, string element)
		{
			blendname = blend;
			elementname = element;
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		internal virtual void Unserialize(System.IO.BinaryReader reader)
		{
			blendname = reader.ReadString();
			elementname = reader.ReadString();
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		internal virtual void Serialize(System.IO.BinaryWriter writer)
		{
			writer.Write(blendname);
			writer.Write(elementname);
		}

		/// <summary>
		/// This output is used in the ListBox View
		/// </summary>
		/// <returns>A String Describing the Data</returns>
		public override string ToString()
		{
			return blendname+", "+elementname;
		}

	}

	/// <summary>
	/// Contains the Model Section of a GMDC
	/// </summary>
	public class GmdcModel : GmdcLinkBlock
	{
		#region Attributes
		Vectors3f transforms;
		/// <summary>
		/// Set of Transformations
		/// </summary>
		/// <remarks>Number of Items stored in <see cref="Transformations"/> 
		/// and <see cref="Rotations"/> must be the same. If one of them Contains 
		/// more Items, those will be cut off during the Save</remarks>
		public Vectors3f Transformations 
		{
			get { return transforms; }
			set {transforms = value; }
		}

		Vectors4f rotations;
		/// <summary>
		/// Set of Rotatins
		/// </summary>
		/// <remarks>Number of Items stored in <see cref="Transformations"/> 
		/// and <see cref="Rotations"/> must be the same. If one of them Contains 
		/// more Items, those will be cut off during the Save</remarks>
		public Vectors4f Rotations 
		{
			get { return rotations; }
			set {rotations = value; }
		}

		GmdcNamePairs names;
		/// <summary>
		/// Groups to BlendGroup assignements
		/// </summary>
		public GmdcNamePairs BlendGroupDefinition 
		{
			get { return names; }
			set {names = value; }
		}		

		GmdcBone subset;
		/// <summary>
		/// Some SubSet Data (yet unknown)
		/// </summary>
		public GmdcBone Bone
		{
			get { return subset; }
			set { subset = value; }
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public GmdcModel(GeometryDataContainer parent) : base(parent)
		{
			transforms = new Vectors3f();
			rotations = new Vectors4f();

			names = new GmdcNamePairs();

			subset = new GmdcBone(parent);
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		public  void Unserialize(System.IO.BinaryReader reader)
		{
			int count = reader.ReadInt32();
			transforms.Clear();
			rotations.Clear();
			for (int i=0; i<count; i++)
			{
				Vector4f r = new Vector4f();
				r.Unserialize(reader);
				rotations.Add(r);

				Vector3f t = new Vector3f();
				t.Unserialize(reader);
				transforms.Add(t);
			}

			count = reader.ReadInt32();
			names.Clear();
			for (int i=0; i<count; i++)
			{
				GmdcNamePair p = new GmdcNamePair();
				p.Unserialize(reader);
				names.Add(p);
			}

			subset.Unserialize(reader);
		}

		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		public  void Serialize(System.IO.BinaryWriter writer)
		{
			int count = Math.Min(rotations.Length, transforms.Length);
			writer.Write((int)count);
			for (int i=0; i<count; i++)
			{
				rotations[i].Serialize(writer);
				transforms[i].Serialize(writer);
			}

			writer.Write((int)names.Length);
			for (int i=0; i<names.Length; i++) names[i].Serialize(writer);

			subset.Serialize(writer);
		}
	}
	
	#region Container
	/// <summary>
	/// Typesave ArrayList for GmdcModel Objects
	/// </summary>
	public class GmdcModels : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new GmdcModel this[int index]
		{
			get { return ((GmdcModel)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public GmdcModel this[uint index]
		{
			get { return ((GmdcModel)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(GmdcModel item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, GmdcModel item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(GmdcModel item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(GmdcModel item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			GmdcModels list = new GmdcModels();
			foreach (GmdcModel item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for Vector3i Objects
	/// </summary>
	public class Vectors3i : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new Vector3i this[int index]
		{
			get { return ((Vector3i)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public Vector3i this[uint index]
		{
			get { return ((Vector3i)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(Vector3i item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, Vector3i item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(Vector3i item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(Vector3i item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			Vectors3i list = new Vectors3i();
			foreach (Vector3i item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for Vector3f Objects
	/// </summary>
	public class Vectors3f : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new Vector3f this[int index]
		{
			get { return ((Vector3f)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public Vector3f this[uint index]
		{
			get { return ((Vector3f)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(Vector3f item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, Vector3f item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(Vector3f item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(Vector3f item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			Vectors3f list = new Vectors3f();
			foreach (Vector3f item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for Vector4f Objects
	/// </summary>
	public class Vectors4f : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new Vector4f this[int index]
		{
			get { return ((Vector4f)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public Vector4f this[uint index]
		{
			get { return ((Vector4f)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(Vector4f item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, Vector4f item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(Vector4f item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(Vector4f item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			Vectors4f list = new Vectors4f();
			foreach (Vector4f item in this) list.Add(item);

			return list;
		}
	}

	/// <summary>
	/// Typesave ArrayList for GmdcNamePair Objects
	/// </summary>
	public class GmdcNamePairs : ArrayList 
	{
		/// <summary>
		/// Integer Indexer
		/// </summary>
		public new GmdcNamePair this[int index]
		{
			get { return ((GmdcNamePair)base[index]); }
			set { base[index] = value; }
		}

		/// <summary>
		/// unsigned Integer Indexer
		/// </summary>
		public GmdcNamePair this[uint index]
		{
			get { return ((GmdcNamePair)base[(int)index]); }
			set { base[(int)index] = value; }
		}

		/// <summary>
		/// add a new Element
		/// </summary>
		/// <param name="item">The object you want to add</param>
		/// <returns>The index it was added on</returns>
		public int Add(GmdcNamePair item)
		{
			return base.Add(item);
		}

		/// <summary>
		/// insert a new Element
		/// </summary>
		/// <param name="index">The Index where the Element should be stored</param>
		/// <param name="item">The object that should be inserted</param>
		public void Insert(int index, GmdcNamePair item)
		{
			base.Insert(index, item);
		}

		/// <summary>
		/// remove an Element
		/// </summary>
		/// <param name="item">The object that should be removed</param>
		public void Remove(GmdcNamePair item)
		{
			base.Remove(item);
		}

		/// <summary>
		/// Checks wether or not the object is already stored in the List
		/// </summary>
		/// <param name="item">The Object you are looking for</param>
		/// <returns>true, if it was found</returns>
		public bool Contains(GmdcNamePair item)
		{
			return base.Contains(item);
		}		

		/// <summary>
		/// Number of stored Elements
		/// </summary>
		public int Length 
		{
			get { return this.Count; }
		}

		/// <summary>
		/// Create a clone of this Object
		/// </summary>
		/// <returns>The clone</returns>
		public override object Clone()
		{
			GmdcNamePairs list = new GmdcNamePairs();
			foreach (GmdcNamePair item in this) list.Add(item);

			return list;
		}
	}
	#endregion
}
