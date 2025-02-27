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
using SimPe.Interfaces.Plugin.Scanner;

namespace SimPe.Plugin
{
	/// <summary>
	/// Identifies a Recolor Package
	/// </summary>
	internal class NeighborhoodIdentifier : IIdentifier
	{
		public NeighborhoodIdentifier()
		{
		}

		#region IIdentifierBase Member
		public uint Version => 1;

		public int Index => 500;

		public ScannerPluginType PluginType => ScannerPluginType.Identifier;
		#endregion

		#region IIdentifier Member

		public Cache.PackageType GetType(Interfaces.Files.IPackageFile pkg)
		{
			return pkg.FindFiles(Data.MetaData.IDNO).Length != 0
				? Cache.PackageType.Neighbourhood
				: pkg.FileName.Contains("Tutorial_Neighborhood")
				? Cache.PackageType.Neighbourhood
				: pkg.FindFiles(Data.MetaData.HOUS).Length != 0
				? Cache.PackageType.Lot
				: pkg.FindFiles(Data.MetaData.GINV).Length != 0 ? Cache.PackageType.GameInventory : Cache.PackageType.Unknown;
		}

		#endregion
	}
}
