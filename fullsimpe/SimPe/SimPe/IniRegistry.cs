// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Collections.Generic;
using System.IO;

namespace SimPe
{
	/// <summary>
	/// Simple "[section name]", "key name=value" ini file reader/writer.
	/// Any comments and blank lines read are lost if the file is written.
	/// </summary>
	public class IniRegistry
		: IEnumerable<string>,
			IEnumerable<IniRegistry.SectionContent>
	{
		class Sectionlist : Dictionary<string, SectionContent>
		{
		}

		bool fileIsReadonly = true;
		Sectionlist reg = null;

		public IniRegistry(string inifile, bool ro)
			: this(inifile)
		{
			fileIsReadonly = ro;
		}

		public IniRegistry(string inifile)
			: this(new StreamReader(inifile))
		{
			IniFile = inifile;
		}

		public IniRegistry(StreamReader sr)
		{
			reg = new Sectionlist();
			string keyBase = "";
			string keyName = "";
			string keyValue = "";
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine().Trim();
				if (line.Length == 0 || line.StartsWith(";"))
				{
					continue;
				}

				//remove comment
				int pos = line.IndexOf(';');
				if (pos > 0)
				{
					line = line.Substring(0, pos).Trim();
				};

				if (line.StartsWith("["))
				{
					if (line.EndsWith("]"))
					{
						keyBase = line.Substring(1, line.Length - 2).Trim();
						reg[keyBase] = new SectionContent();
						continue;
					}
					// fall through!
				}
				else if (line.Contains("="))
				{
					string[] a = line.Split(new char[] { '=' }, 2);
					keyName = a[0].Trim(); //.ToLower();
					keyValue = a[1].Trim(); //.ToLower();
					reg[keyBase].SetValue(keyName, keyValue, true);
					continue;
				}
				throw new Exception("Invalid inifile line: " + line);
			}
		}

		public string IniFile { get; set; } = null;

		public bool Flush()
		{
			if (fileIsReadonly)
			{
				return false;
			}

			if (IniFile.Length.Equals(0))
			{
				return false;
			}

			if (!File.Exists(IniFile))
			{
				return false;
			}

			try
			{
				StreamWriter sw = new StreamWriter(IniFile);
				bool wantBlank = false;
				foreach (string section in reg.Keys)
				{
					if (wantBlank)
					{
						sw.WriteLine("");
					}

					sw.WriteLine("[" + section + "]");
					wantBlank = true;
					foreach (string key in reg[section].Keys)
					{
						sw.WriteLine(key + "=" + reg[section].GetValue(key));
					}
				}
				sw.Close();
			}
			catch
			{
				return false;
			}
			return true;
		}

		static bool KeyCompare(string k1, string k2)
		{
			return k1.Trim().ToLower() == k2.Trim().ToLower();
		}

		#region Sections
		public SectionContent CreateSection(string section)
		{
			return Section(section, true);
		}

		public SectionContent Section(string section)
		{
			return Section(section, true);
		}

		public SectionContent Section(string section, bool create)
		{
			foreach (string k in reg.Keys)
			{
				if (KeyCompare(section, k))
				{
					return reg[k];
				}
			}

			if (!create)
			{
				return null;
			}

			SectionContent kl = new SectionContent();
			reg.Add(section, kl);

			return kl;
		}

		public bool ContainsSection(string section)
		{
			SectionContent kl = Section(section, false);
			return kl != null;
		}

		public bool RemoveSection(string section)
		{
			string rm = null;
			foreach (string s in reg.Keys)
			{
				if (KeyCompare(s, section))
				{
					rm = s;
					break;
				}
			}
			if (rm != null)
			{
				reg.Remove(rm);
				return true;
			}
			return false;
		}

		public void ClearSection(string section)
		{
			SectionContent kl = Section(section, false);
			kl?.Clear();
		}

		public Sectionlist.KeyCollection Sections => reg.Keys;

		public SectionContent this[string section] => Section(section, false);
		public string this[string section, string key]
		{
			get => GetValue(section, key);
			set => SetValue(section, key, value, true);
		}
		#endregion

		#region keys
		public class SectionContent : IEnumerable<string>
		{
			class List : Dictionary<string, string>
			{
			}

			List list;

			internal SectionContent()
			{
				list = new List();
			}

			public void CreateKey(string key)
			{
				SetValue(key, "");
			}

			public void SetValue(string key, string value)
			{
				SetValue(key, value, true);
			}

			public void SetValue(string key, string value, bool create)
			{
				if (!ContainsKey(key))
				{
					list.Add(key, value);
				}
				else if (create)
				{
					string kv = null;
					foreach (string s in list.Keys)
					{
						if (KeyCompare(s, key))
						{
							kv = s;
							break;
						}
					}

					if (kv != null)
					{
						list[kv] = value;
					}
				}
			}

			public List.KeyCollection Keys => list.Keys;

			public string GetValue(string key)
			{
				return GetValue(key, null);
			}

			public string GetValue(string key, string def)
			{
				foreach (string lk in list.Keys)
				{
					if (KeyCompare(lk, key))
					{
						return list[lk];
					}
				}
				return def;
			}

			public bool ContainsKey(string key)
			{
				foreach (string lk in list.Keys)
				{
					if (KeyCompare(lk, key))
					{
						return true;
					}
				}

				return false;
			}

			public bool RemoveKey(string key)
			{
				string rm = null;
				foreach (string s in list.Keys)
				{
					if (KeyCompare(s, key))
					{
						rm = s;
					}
				}

				if (rm != null)
				{
					list.Remove(rm);
					return true;
				}
				return false;
			}

			public void Clear()
			{
				list.Clear();
			}

			public string this[string key]
			{
				get => GetValue(key);
				set => SetValue(key, value, true);
			}

			#region IEnumerable<string> Member

			public IEnumerator<string> GetEnumerator()
			{
				return list.Keys.GetEnumerator();
			}

			#endregion

			#region IEnumerable Member

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return list.GetEnumerator();
			}

			#endregion



			#region IDisposable Member

			public void Dispose()
			{
				list.Clear();
			}

			#endregion
		}
		#endregion

		#region direct key access
		public string GetValue(string section, string key)
		{
			return GetValue(section, key, null);
		}

		public string GetValue(string section, string key, string def)
		{
			SectionContent kl = Section(section, false);
			return kl != null ? kl.GetValue(key, def) : def;
		}

		public void SetValue(string section, string key, string value)
		{
			SetValue(section, key, value, true);
		}

		public void SetValue(string section, string key, string value, bool create)
		{
			SectionContent kl = Section(section, true);
			kl?.SetValue(key, value, create);
		}
		#endregion

		#region IEnumerable<string> Member

		public IEnumerator<string> GetEnumerator()
		{
			return reg.Keys.GetEnumerator();
		}

		#endregion

		#region IEnumerable Member

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return reg.GetEnumerator();
		}

		#endregion

		#region IEnumerable<Keylist> Member

		IEnumerator<SectionContent> IEnumerable<SectionContent>.GetEnumerator()
		{
			return reg.Values.GetEnumerator();
		}

		#endregion
	}
}
