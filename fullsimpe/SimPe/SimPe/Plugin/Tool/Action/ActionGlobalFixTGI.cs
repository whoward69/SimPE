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
namespace SimPe.Plugin.Tool.Action
{
	/// <summary>
	/// The Intrigued Neighborhood Action
	/// </summary>
	public class ActionGlobalFixTGI : Interfaces.IToolAction
	{
		#region IToolAction Member

		public virtual bool ChangeEnabledStateEventHandler(
			object sender,
			Events.ResourceEventArgs es
		)
		{
			return es.Loaded;
		}

		public void ExecuteEventHandler(object sender, Events.ResourceEventArgs e)
		{
			if (!ChangeEnabledStateEventHandler(null, e))
			{
				return;
			}

			foreach (
				Interfaces.Files.IPackedFileDescriptor pfd in e.LoadedPackage
					.Package
					.Index
			)
			{
				//Do we have a registred handler?
				Interfaces.Plugin.IFileWrapper wrapper =
					(Interfaces.Plugin.IFileWrapper)
						FileTable.WrapperRegistry.FindHandler(pfd.Type);
				Interfaces.Files.IPackedFile file = e.LoadedPackage.Package.Read(
					pfd
				);
				if (wrapper == null)
				{
					wrapper = FileTable.WrapperRegistry.FindHandler(
						file.UncompressedData
					);
				}

				if (wrapper != null)
				{
					wrapper.ProcessData(pfd, e.LoadedPackage.Package);
					wrapper.Fix(FileTable.WrapperRegistry);
				}
			}
		}

		#endregion


		#region IToolPlugin Member
		public override string ToString()
		{
			return "Set TGI Values";
		}
		#endregion

		#region IToolExt Member
		public System.Windows.Forms.Shortcut Shortcut => System.Windows.Forms.Shortcut.None;

		public System.Drawing.Image Icon => SimPe.GetIcon.actionFixTGI;

		public virtual bool Visible => true;

		#endregion
	}
}
