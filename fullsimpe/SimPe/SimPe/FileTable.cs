/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
namespace SimPe
{
	/// <summary>
	/// use this Class to access the FileIndex
	/// </summary>
	public class FileTable : FileTableBase
	{
		/// <summary>
		/// Returns/Sets a ToolRegistry (can be null)
		/// </summary>
		public static Interfaces.IToolRegistry ToolRegistry
		{
			get; set;
		}

		/// <summary>
		/// Returns/Sets a HelpTopicRegistry (can be null)
		/// </summary>
		public static Interfaces.IHelpRegistry HelpTopicRegistry
		{
			get; set;
		}

		/// <summary>
		/// Returns/Sets a SettingsRegistry (can be null)
		/// </summary>
		public static Interfaces.ISettingsRegistry SettingsRegistry
		{
			get; set;
		}

		public static Interfaces.ICommandLineRegistry CommandLineRegistry
		{
			get; set;
		}

		public static void Reload()
		{
			FileIndex.BaseFolders.Clear();
			FileIndex.BaseFolders = DefaultFolders;
			FileIndex.ForceReload();
		}
	}
}
