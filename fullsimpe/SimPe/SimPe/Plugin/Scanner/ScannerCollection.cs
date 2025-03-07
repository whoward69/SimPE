// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later

using System.Collections;

using SimPe.Interfaces.Plugin.Scanner;

namespace SimPe.Plugin.Scanner
{
	/// <summary>
	/// Summary description for ScannerCollection.
	/// </summary>
	public class ScannerCollection : IEnumerable, System.IDisposable
	{
		ArrayList list;

		internal ScannerCollection()
		{
			list = new ArrayList();
		}

		public virtual void Add(IScannerPluginBase item)
		{
			if (item == null)
			{
				return;
			}

			list.Add(item);
		}

		public int Count => list.Count;

		public bool Contains(IScannerPluginBase item)
		{
			return list.Contains(item);
		}

		public void Sort(IComparer cmp)
		{
			list.Sort(cmp);
		}

		internal void Clear()
		{
			list.Clear();
		}

		public IScannerPluginBase this[int index] => list[index] as IScannerPluginBase;

		#region IEnumerable Member

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			list?.Clear();

			list = null;
		}

		#endregion
	}
}
