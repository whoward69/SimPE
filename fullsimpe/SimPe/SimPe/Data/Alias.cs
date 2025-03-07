// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;
using System.Xml;

namespace SimPe.Data
{
	/// <summary>
	/// Conects an value with a name
	/// </summary>
	public class StaticAlias : Interfaces.IAlias, IDisposable
	{
		/// <summary>
		/// Stores arbitary Data
		/// </summary>
		protected object[] tag;

		/// <summary>
		/// The id Value
		/// </summary>
		protected uint id;

		/// <summary>
		/// The long Name
		/// </summary>
		protected string name;

		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		public StaticAlias(uint val, string name)
			: this(val, name, new object[0]) { }

		~StaticAlias()
		{
			try
			{
				Dispose();
			}
			catch { }
		}

		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		/// <param name="tag"></param>
		public StaticAlias(uint val, string name, object[] tag)
		{
			id = val;
			this.name = name;
			this.tag = tag;
		}

		/// <summary>
		/// Craetes a String from the Object
		/// </summary>
		/// <returns>Simply Returns the Name Attribute</returns>
		public override string ToString()
		{
			return name;
		}

		#region IAlias Member

		public uint Id => id;

		public string Name
		{
			get => name;
			set => name = value;
		}

		public object[] Tag
		{
			get => tag;
			set => tag = value;
		}

		#endregion

		#region IDisposable Member

		public virtual void Dispose()
		{
			tag = null;
			name = null;
		}

		#endregion
	}

	/// <summary>
	/// Conects an value with a name
	/// </summary>
	public class Alias : StaticAlias
	{
		/// <summary>
		/// This is used to format the ToString() Output
		/// </summary>
		private string template;

		static string DefaultTemplate =>
#if DEBUG
				"{name} (0x{id})";
#else
				return "{name} (0x{id})";
#endif


		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		public Alias(uint val, string name)
			: this(val, name, DefaultTemplate) { }

		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		/// <param name="tag"></param>
		public Alias(uint val, string name, object[] tag)
			: this(val, name, tag, DefaultTemplate) { }

		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		/// <param name="template">The ToString Template</param>
		public Alias(uint val, string name, string template)
			: this(val, name, null, template) { }

		/// <summary>
		/// Cosntructor of the class
		/// </summary>
		/// <param name="val">The id</param>
		/// <param name="name">The name</param>
		/// <param name="tag"></param>
		/// <param name="template">The ToString Template</param>
		public Alias(uint val, string name, object[] tag, string template)
			: base(val, name, tag)
		{
			this.template = template;
		}

		/// <summary>
		/// Craetes a String from the Object
		/// </summary>
		/// <returns>Simply Returns the Name Attribute</returns>
		public override string ToString()
		{
			string ret = template;

			ret = ret.Replace("{name}", name);
			ret = ret.Replace("{id}", id.ToString("X"));

			if (tag != null)
			{
				for (int i = 0; i < tag.Length; i++)
				{
					object o = tag[i];
					ret = o != null ? ret.Replace("{" + i.ToString() + "}", o.ToString()) : ret.Replace("{" + i.ToString() + "}", "");
				}
			}

			return ret;
		}

		#region static Loader
		/// <summary>
		/// Load a List of Aliases form an XML File
		/// </summary>
		/// <param name="flname">Name of the File</param>
		/// <returns>The IAlias List</returns>
		public static Interfaces.IAlias[] LoadFromXml(string flname)
		{
			if (!System.IO.File.Exists(flname))
			{
				return new Interfaces.IAlias[0];
			}

			try
			{
				//read XML File
				XmlDocument xmlfile = new XmlDocument();
				xmlfile.Load(flname);

				//seek Root Node
				XmlNodeList XMLData = xmlfile.GetElementsByTagName("alias");

				ArrayList list = new ArrayList();
				//Process all Root Node Entries
				for (int i = 0; i < XMLData.Count; i++)
				{
					XmlNode node = XMLData.Item(i);
					foreach (XmlNode subnode in node)
					{
						if (subnode.LocalName.Trim().ToLower() == "item")
						{
							string sval = subnode
								.Attributes["value"]
								.Value.Trim()
								.ToString();
							uint val = sval.StartsWith("0x") ? Convert.ToUInt32(sval, 16) : Convert.ToUInt32(sval);

							Alias a = new Alias(val, subnode.InnerText.Trim());
							list.Add(a);
						}
					}
				} // for i

				Interfaces.IAlias[] ret = new Interfaces.IAlias[list.Count];
				list.CopyTo(ret);
				return ret;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}

			return new Interfaces.IAlias[0];
		}
		#endregion
	}
}
