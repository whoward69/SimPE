// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later

using System;

using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	class SimAspirationEditor : IAspirationEditor
	{
		public const uint SEC_ASP_TOKEN_GUID = 0x53D08989;

		public Data.MetaData.AspirationTypes[] LoadAspirations(SDesc sim)
		{
			if (sim == null)
			{
				return null;
			}

			LoadMemoryResource(sim);
			ushort sval = GetSecondaryAspirationValue(sim);
			Data.MetaData.AspirationTypes[] res =
				new Data.MetaData.AspirationTypes[]
				{
					Data.MetaData.AspirationTypes.Nothing,
					Data.MetaData.AspirationTypes.Nothing,
				};
			ushort a = (ushort)sim.CharacterDescription.Aspiration;

			if (sval != 0)
			{
				res[0] = (Data.MetaData.AspirationTypes)(a ^ sval);
				res[1] = (Data.MetaData.AspirationTypes)(a & sval);
			}
			else
			{
				Array arr = Enum.GetValues(typeof(Data.MetaData.AspirationTypes));
				foreach (ushort i in arr)
				{
					if ((a & i) == i)
					{
						if (res[0] == Data.MetaData.AspirationTypes.Nothing)
						{
							res[0] = (Data.MetaData.AspirationTypes)i;
						}
						else
						{
							res[1] = (Data.MetaData.AspirationTypes)i;
						}
					}
				}
			}

			return res;
		}

		public void StoreAspirations(
			Data.MetaData.AspirationTypes[] asps,
			SDesc sim
		)
		{
			if (sim == null)
			{
				return;
			}

			if (asps == null)
			{
				return;
			}

			if (asps.Length < 2)
			{
				return;
			}

			Data.MetaData.AspirationTypes[] old = LoadAspirations(sim);
			bool chg = false;
			bool chg2 = asps[1] != old[1];
			for (int i = 0; i < 2; i++)
			{
				if (asps[i] != old[i])
				{
					chg = true;
				}
			}

			if (!chg)
			{
				return;
			}
			//LoadMemoryResource(sim);
			ushort a = 0;
			ushort v = 0;
			for (int i = 0; i < 2; i++)
			{
				if (i == 0)
				{
					v = (ushort)asps[i];
				}
				else if (i == 1 && ((ushort)asps[i]) == v)
				{
					v = 0;
				}
				else if (i == 1)
				{
					v = (ushort)asps[i];
				}

				a |= (ushort)asps[i];
			}

			sim.CharacterDescription.Aspiration =
				(Data.MetaData.AspirationTypes)a;

			if (Helper.WindowsRegistry.AllowChangeOfSecondaryAspiration && chg2)
			{
				NgbhItem itm = GetSecondaryAspirationToken(sim, true);
				itm.Value = v;

				ngbh.SynchronizeUserData();
			}
		}

		Ngbh ngbh = null;
		Interfaces.Files.IPackageFile pkg = null;

		protected void LoadMemoryResource(SDesc sim)
		{
			if (sim != null && pkg == sim.Package)
			{
				return;
			}

			pkg = sim != null ? sim.Package : null;

			ngbh = null;
			if (sim == null)
			{
				return;
			}

			if (sim.Package == null)
			{
				return;
			}

			if (!Helper.WindowsRegistry.AllowChangeOfSecondaryAspiration)
			{
				return;
			}

			Interfaces.Plugin.IFileWrapper wrapper =
				(Interfaces.Plugin.IFileWrapper)
					FileTableBase.WrapperRegistry.FindHandler(Data.MetaData.MEMORIES);

			if (wrapper == null)
			{
				return;
			}

			Interfaces.Files.IPackedFileDescriptor[] mems = sim.Package.FindFiles(
				Data.MetaData.MEMORIES
			);
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in mems)
			{
				ngbh = new Ngbh(FileTableBase.ProviderRegistry);
				ngbh.ProcessData(pfd, pkg, false);
				return;
			}
		}

		protected NgbhItem GetSecondaryAspirationToken(SDesc sim, bool create)
		{
			if (ngbh == null)
			{
				return null;
			}

			NgbhSlot slot = ngbh.Sims.GetInstanceSlot(sim.Instance, true);

			if (slot != null)
			{
				NgbhItem item = slot.FindItem(SEC_ASP_TOKEN_GUID);
				if (create && item == null)
				{
					item = slot.ItemsB.AddNew(SimMemoryType.Token);
					item.Guid = SEC_ASP_TOKEN_GUID;
					item.Value = 0;
				}
				return item;
			}
			return null;
		}

		protected ushort GetSecondaryAspirationValue(SDesc sim)
		{
			if (!Helper.WindowsRegistry.AllowChangeOfSecondaryAspiration)
			{
				return 0;
			}

			NgbhItem ni = GetSecondaryAspirationToken(sim, false);
			return ni == null ? (ushort)0 : ni.Value;
		}
	}
}
