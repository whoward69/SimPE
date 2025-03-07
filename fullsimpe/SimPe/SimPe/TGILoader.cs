// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections;
using System.Xml;

namespace SimPe
{
	/// <summary>
	/// Used to build the List of known TGI Types
	/// </summary>
	public class TGILoader
	{
		Hashtable map;
		ArrayList alist;

		/// <summary>
		/// Create a new Instance
		/// </summary>
		/// <param name="filename">The file that contains the TGI definition</param>
		public TGILoader(string filename)
		{
			map = new Hashtable();
			FileTypes = new Data.TypeAlias[0];
			alist = new ArrayList();
			LoadTGI(filename);
		}

		/// <summary>
		/// Create a new Instance from the default File
		/// </summary>
		public TGILoader()
			: this(System.IO.Path.Combine(Helper.SimPeDataPath, "tgi.xml")) { }

		/// <summary>
		/// Load the Values from File
		/// </summary>
		/// <param name="xmlfilename"></param>
		void LoadTGI(string xmlfilename)
		{
			map.Clear();
			if (!System.IO.File.Exists(xmlfilename))
			{
				Helper.ExceptionMessage(
					new Warning(
						"Unable to load TGI description",
						"The File \"" + xmlfilename + "\" was not found on the system"
					)
				);
				return;
			}

			//read XML File
			XmlDocument xmlfile = new XmlDocument();
			xmlfile.Load(xmlfilename);

			//seek Root Node
			XmlNodeList XMLData = xmlfile.GetElementsByTagName("tgi");

			//Process all Root Node Entries
			for (int i = 0; i < XMLData.Count; i++)
			{
				XmlNode node = XMLData.Item(i);
				ParseSubNode(node);
			}

			FileTypes = new Data.TypeAlias[alist.Count];
			alist.CopyTo(FileTypes);
			alist.Clear();
		}

		/// <summary>
		/// Parse the various TGI Fields
		/// </summary>
		/// <param name="node"></param>
		void ParseSubNode(XmlNode node)
		{
			foreach (XmlNode subnode in node)
			{
				if (subnode.Name == "type")
				{
					LoadType(subnode);
				}
			}
		}

		/// <summary>
		/// Parse the various TGI Fields
		/// </summary>
		/// <param name="node"></param>
		void LoadType(XmlNode node)
		{
			uint type = 0;
			try
			{
				type = Convert.ToUInt32(node.Attributes["value"].Value, 16);
			}
			catch { }

			bool known = false;
			string name = "";
			string shortname = "";
			string ext = "";
			bool contfl = false;
			bool nodecomp = false;
			foreach (XmlNode subnode in node)
			{
				if (subnode.Name == "known")
				{
					known = true;
				}

				if (subnode.Name == "embeddedfilename")
				{
					contfl = true;
				}

				if (subnode.Name == "name")
				{
					name = subnode.InnerText;
				}

				if (subnode.Name == "shortname")
				{
					shortname = subnode.InnerText;
				}

				if (subnode.Name == "extension")
				{
					ext = subnode.InnerText;
				}

				if (subnode.Name == "nodecompressforcache")
				{
					nodecomp = true;
				}
			}

			//Data.TypeAlias ta = new Data.TypeAlias(contfl, SimPe.Localization.GetString(shortname), type, SimPe.Localization.GetString(name), ext, known);
			Data.TypeAlias ta = new Data.TypeAlias(
				contfl,
				shortname,
				type,
				Localization.GetString(name),
				ext,
				known,
				nodecomp
			);
			map[type] = ta;
			alist.Add(ta);
		}

		/// <summary>
		/// Returns the Information about the given Type (or null if the Type is not known!)
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Data.TypeAlias GetByType(uint type)
		{
			return (Data.TypeAlias)map[type];
		}

		/// <summary>
		/// Returns a List of all available FileTypes
		/// </summary>
		public Data.TypeAlias[] FileTypes
		{
			get; private set;
		}
	}
}
