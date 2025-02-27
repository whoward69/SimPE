/***************************************************************************
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
using System;
using System.Resources;

using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;

namespace SimPe.Custom
{
	public class Settings : GlobalizedObject, ISettings
	{
		static ResourceManager rm = new ResourceManager(typeof(Localization));

		private static Settings settings;

		static Settings()
		{
			settings = new Settings();
		}

		public static bool Persistent => settings.KeepFilesOpen;

		public Settings()
			: base(rm) { }

		const string BASENAME = "Settings";
		XmlRegistryKey xrk = SimPe.Helper.WindowsRegistry.RegistryKey;

		[System.ComponentModel.Category("SimPE")]
		public bool KeepFilesOpen
		{
			get
			{
				XmlRegistryKey rkf = xrk.CreateSubKey(BASENAME);
				object o = rkf.GetValue("keepFilesOpen", true);
				return Convert.ToBoolean(o);
			}
			set
			{
				XmlRegistryKey rkf = xrk.CreateSubKey(BASENAME);
				rkf.SetValue("keepFilesOpen", value);
			}
		}

		#region ISettings Members

		public override string ToString()
		{
			return "SimPE";
		}

		[System.ComponentModel.Browsable(false)]
		public System.Drawing.Image Icon => null;

		object ISettings.GetSettingsObject()
		{
			return this;
		}

		#endregion
	}

	public class SettingsFactory : AbstractWrapperFactory, ISettingsFactory
	{
		#region ISettingsFactory Members

		public ISettings[] KnownSettings => new ISettings[] { new Settings() };

		#endregion
	}
}
