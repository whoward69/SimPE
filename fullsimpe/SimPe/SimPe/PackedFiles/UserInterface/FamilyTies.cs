// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System.Windows.Forms;

using SimPe.Data;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper.Supporting;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// handles Packed XmlFiles
	/// </summary>
	public class FamilyTies : UIBase, IPackedFileUI
	{
		protected void ResetPanel(Wrapper.FamilyTies famt)
		{
			form.cballtieablesims.Items.Clear();
			form.cballtieablesims.Sorted = false;
			Interfaces.Files.IPackedFileDescriptor[] pfds = famt.Package.FindFiles(
				MetaData.SIM_DESCRIPTION_FILE
			);
			Wrapper.SDesc sdesc = new Wrapper.SDesc(
				famt.NameProvider,
				null,
				null
			);
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfds)
			{
				FamilyTieSim fts = new FamilyTieSim(
					(ushort)pfd.Instance,
					null,
					famt
				);
				form.cballtieablesims.Items.Add(fts);
			}

			form.cballtieablesims.Sorted = true;

			form.cbtietype.Items.Clear();
			form.cbtietype.Items.Add(
				new LocalizedFamilyTieTypes(MetaData.FamilyTieTypes.MyMotherIs)
			);
			form.cbtietype.Items.Add(
				new LocalizedFamilyTieTypes(MetaData.FamilyTieTypes.MyFatherIs)
			);
			form.cbtietype.Items.Add(
				new LocalizedFamilyTieTypes(MetaData.FamilyTieTypes.ImMarriedTo)
			);
			form.cbtietype.Items.Add(
				new LocalizedFamilyTieTypes(MetaData.FamilyTieTypes.MySiblingIs)
			);
			form.cbtietype.Items.Add(
				new LocalizedFamilyTieTypes(MetaData.FamilyTieTypes.MyChildIs)
			);
			form.cbtietype.SelectedIndex = 2;

			form.btaddtie.Enabled = false;
			form.btdeletetie.Enabled = false;
			form.btnewtie.Enabled = false;

			form.lbties.Items.Clear();
		}

		#region IPackedFileHandler Member

		public Control GUIHandle => form.familytiePanel;

		public void UpdateGUI(IFileWrapper wrapper)
		{
			Wrapper.FamilyTies famt = (Wrapper.FamilyTies)wrapper;
			form.wrapper = famt;
			ResetPanel(famt);

			FamilyTieSim[] sims = famt.Sims;
			form.cbtiesims.Items.Clear();
			//form.cbtiesims.Sorted = false;
			foreach (FamilyTieSim sim in sims)
			{
				form.cbtiesims.Items.Add(sim);
			}
			//form.cbtiesims.Sorted = true;
		}

		#endregion
	}
}
