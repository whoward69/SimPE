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
namespace SimPe.Interfaces
{
	/// <summary>
	/// Stores a List of dedicated Providers
	/// </summary>
	public interface IProviderRegistry
	{
		/// <summary>
		/// Returns the Provider for SimNames
		/// </summary>
		Providers.ISimNames SimNameProvider
		{
			get;
		}

		/// <summary>
		/// Returns the Provider for Sim Family Names
		/// </summary>
		Providers.ISimFamilyNames SimFamilynameProvider
		{
			get;
		}

		/// <summary>
		/// Returns the Provider for SimDescription Files
		/// </summary>
		Providers.ISimDescriptions SimDescriptionProvider
		{
			get;
		}

		/// <summary>
		/// Returns the Provider for Opcode Names
		/// </summary>
		Providers.IOpcodeProvider OpcodeProvider
		{
			get;
		}

		/// <summary>
		/// Returns the Provider for Skin Data
		/// </summary>
		Providers.ISkinProvider SkinProvider
		{
			get;
		}

		Providers.ILotProvider LotProvider
		{
			get;
		}
	}
}
