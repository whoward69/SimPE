// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later

using System;

namespace SimPe.Plugin.Tool.Dockable.Finder
{
	public partial class FindInStr : Interfaces.AFinderTool
	{
		public enum CompareType
		{
			Unknown = -1,
			Equal = 0,
			Start = 1,
			End = 2,
			Contain = 3,
			RegExp = 4,
		}

		public FindInStr(Interfaces.IFinderResultGui rgui)
			: base(rgui)
		{
			InitializeComponent();

			cbType.SelectedIndex = 3;
			reg = null;
			name = "";
			compareType = CompareType.Unknown;
		}

		public FindInStr()
			: this(null) { }

		protected System.Text.RegularExpressions.Regex reg;
		protected string name;
		protected CompareType compareType;

		protected override bool OnPrepareStart()
		{
			compareType = (CompareType)cbType.SelectedIndex;
			name = tbMatch.Text.Trim().ToLower();
			reg = null;

			if (compareType == CompareType.Unknown || name == "")
			{
				return false;
			}

			try
			{
				reg = new System.Text.RegularExpressions.Regex(
					tbMatch.Text,
					System.Text.RegularExpressions.RegexOptions.IgnoreCase
				);
			}
			catch (Exception ex)
			{
				if (cbType.SelectedIndex == 4)
				{
					Helper.ExceptionMessage(ex);
				}
			}

			return true;
		}

		public override void SearchPackage(
			Interfaces.Files.IPackageFile pkg,
			Interfaces.Files.IPackedFileDescriptor pfd
		)
		{
			if (
				pfd.Type != Data.MetaData.STRING_FILE
				&& pfd.Type != Data.MetaData.CTSS_FILE
			)
			{
				return;
			}

			PackedFiles.Wrapper.Str str = new PackedFiles.Wrapper.Str();
			str.ProcessData(pfd, pkg);

			PackedFiles.Wrapper.StrItemList sitems = str.Items;
			//check all stored nMap entries for a match
			foreach (PackedFiles.Wrapper.StrToken item in sitems)
			{
				bool found = false;
				string n = item.Title.Trim().ToLower();
				if (compareType == CompareType.Equal)
				{
					found = n == name;
				}
				else if (compareType == CompareType.Start)
				{
					found = n.StartsWith(name);
				}
				else if (compareType == CompareType.End)
				{
					found = n.EndsWith(name);
				}
				else if (compareType == CompareType.Contain)
				{
					found = n.IndexOf(name) > -1;
				}
				else if (compareType == CompareType.RegExp && reg != null)
				{
					found = reg.IsMatch(n);
				}

				//we have a match, so add the result item
				if (found)
				{
					ResultGui.AddResult(pkg, pfd);
					break;
				}
			}
		}
	}
}
