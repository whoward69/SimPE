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
using SimPe.Interfaces;

namespace SimPe.Plugin
{
	/// <summary>
	/// Lists all Plugins (=FileType Wrappers) available in this Package
	/// </summary>
	/// <remarks>
	/// GetWrappers() has to return a list of all Plugins provided by this Library.
	/// If a Plugin isn't returned, SimPe won't recognize it!
	/// </remarks>
	public class WorkshopToolFactory
		: Interfaces.Plugin.AbstractWrapperFactory,
			Interfaces.Plugin.IToolFactory,
			Interfaces.Plugin.IHelpFactory
	{
		internal static IToolPlugin[] Last;

		public WorkshopToolFactory()
		{
			SimPe.Plugin.MmatWrapper.GlobalCpfPreview =
				new PackedFiles.UserInterface.CpfUI.ExecutePreview(
					SimPe.Plugin.PreviewForm.Execute
				);
		}

		#region AbstractWrapperFactory Member
		/// <summary>
		/// Returns a List of all available Plugins in this Package
		/// </summary>
		/// <returns>A List of all provided Plugins (=FileType Wrappers)</returns>
		public override IWrapper[] KnownWrappers
		{
			get
			{
				// TODO:  You can add more Wrappers here
				IWrapper[] wrappers = { };
				return wrappers;
			}
		}

		#endregion

		#region IToolFactory Member

		delegate void LoadDocksHandler(System.Collections.ArrayList docks);

		void InvokeLoadDocks(System.Collections.ArrayList docks)
		{
			if (Helper.StartedGui == Executable.Classic)
			{
				docks.Add(new WorkshopTool(this.LinkedRegistry, this.LinkedProvider));
			}
			else
			{
				docks.Add(new Tool.Dockable.ObectWorkshopDockTool());
			}

			docks.Add(new Tool.Dockable.PackageDetailDockTool());
			if (Helper.WindowsRegistry.HiddenMode)
			{
				docks.Add(new Tool.Window.PackageRepairTool());
			}
		}

		public IToolPlugin[] KnownTools
		{
			get
			{
				if (Last != null)
				{
					return Last;
				}

				System.Collections.ArrayList list = new System.Collections.ArrayList();
				InvokeLoadDocks(list);

				Last = new IToolPlugin[list.Count];
				list.CopyTo(Last);
				return Last;

#if UNREACHABLE
				if (Helper.StartedGui == Executable.Classic)
				{
					if (Helper.WindowsRegistry.HiddenMode)
					{
						Last = new IToolPlugin[]
						{
							new SimPe.Plugin.Tool.Dockable.PackageDetailDockTool(),
							new SimPe.Plugin.Tool.Window.PackageRepairTool(),
						};
					}
					else
					{
						Last = new IToolPlugin[]
						{
							new SimPe.Plugin.Tool.Dockable.PackageDetailDockTool(),
						};
					}
				}
				else
				{
					if (Helper.WindowsRegistry.HiddenMode)
					{
						Last = new IToolPlugin[]
						{
							new SimPe.Plugin.Tool.Dockable.ObectWorkshopDockTool(),
							new SimPe.Plugin.Tool.Dockable.PackageDetailDockTool(),
							new SimPe.Plugin.Tool.Window.PackageRepairTool(),
						};
					}
					else
					{
						Last = new IToolPlugin[]
						{
							new SimPe.Plugin.Tool.Dockable.ObectWorkshopDockTool(),
							new SimPe.Plugin.Tool.Dockable.PackageDetailDockTool(),
						};
					}
				}
				return Last;
#endif
			}
		}
		#endregion

		#region IHelpFactory Members

		class obwHelp : IHelp
		{
			public System.Drawing.Image Icon => null;

			public override string ToString()
			{
				return "Object Workshop";
			}

			public void ShowHelp(ShowHelpEventArgs e)
			{
				SimPe.RemoteControl.ShowHelp(
					"file://" + SimPe.Helper.SimPePath + "/Doc/OWoptions.htm"
				);
			}
		}

		public IHelp[] KnownHelpTopics
		{
			get
			{
				if (Helper.StartedGui == Executable.Classic)
				{
					return new IHelp[0];
				}
				else
				{
					IHelp[] helpTopics = { new obwHelp() };
					return helpTopics;
				}
			}
		}
		#endregion
	}
}
