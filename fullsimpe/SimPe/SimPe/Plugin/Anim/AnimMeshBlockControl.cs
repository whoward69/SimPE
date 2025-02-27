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
using System.Windows.Forms;

namespace SimPe.Plugin.Anim
{
	/// <summary>
	/// Summary description for AnimationMeshBlock.
	/// </summary>
	public class AnimMeshBlockControl : UserControl
	{
		private Panel panel2;
		internal ComboBox cbJoint;
		private Label label1;
		private Panel pnJoint;
		private Panel pnSubMesh;
		private Label label2;
		internal ComboBox cbSubMesh;
		private AnimFrameBlockControl pnFrames;
		private LinkLabel llExport;
		private LinkLabel llImport;
		private CheckBox cbCorrect;
		private MenuItem miAdd;
		private MenuItem miRem;
		private ContextMenu cmJoint;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AnimMeshBlockControl()
		{
			SetStyle(
				ControlStyles.SupportsTransparentBackColor
					| ControlStyles.AllPaintingInWmPaint
					|
					//ControlStyles.Opaque |
					ControlStyles.UserPaint
					| ControlStyles.ResizeRedraw
					| ControlStyles.DoubleBuffer,
				true
			);

			// Required designer variable.
			InitializeComponent();

			this.pnFrames = new AnimFrameBlockControl();
			this.pnFrames.Dock = DockStyle.Fill;
			this.pnFrames.FrameBlock = null;
			this.pnFrames.Location = new System.Drawing.Point(0, 32);
			this.pnFrames.Name = "pnFrames";
			this.pnFrames.Size = new System.Drawing.Size(776, 368);
			this.pnFrames.TabIndex = 3;
			this.pnFrames.Changed += new System.EventHandler(this.pnFrames_Changed);
			this.Controls.Add(this.pnFrames);
			pnFrames.BringToFront();

			Clear();

			this.cbCorrect.Checked = Helper
				.WindowsRegistry
				.CorrectJointDefinitionOnExport;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel2 = new Panel();
			this.cbCorrect = new CheckBox();
			this.llExport = new LinkLabel();
			this.llImport = new LinkLabel();
			this.pnJoint = new Panel();
			this.cbJoint = new ComboBox();
			this.label1 = new Label();
			this.pnSubMesh = new Panel();
			this.cbSubMesh = new ComboBox();
			this.label2 = new Label();
			this.cmJoint = new ContextMenu();
			this.miAdd = new MenuItem();
			this.miRem = new MenuItem();
			this.panel2.SuspendLayout();
			this.pnJoint.SuspendLayout();
			this.pnSubMesh.SuspendLayout();
			this.SuspendLayout();
			//
			// panel2
			//
			this.panel2.Controls.Add(this.cbCorrect);
			this.panel2.Controls.Add(this.llExport);
			this.panel2.Controls.Add(this.llImport);
			this.panel2.Controls.Add(this.pnJoint);
			this.panel2.Controls.Add(this.pnSubMesh);
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(776, 32);
			this.panel2.TabIndex = 4;
			//
			// cbCorrect
			//
			this.cbCorrect.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.cbCorrect.Location = new System.Drawing.Point(584, 0);
			this.cbCorrect.Name = "cbCorrect";
			this.cbCorrect.Size = new System.Drawing.Size(88, 32);
			this.cbCorrect.TabIndex = 44;
			this.cbCorrect.Text = "correct Joint Definition";
			this.cbCorrect.CheckedChanged += new System.EventHandler(
				this.cbCorrect_CheckedChanged
			);
			//
			// llExport
			//
			this.llExport.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.llExport.Enabled = false;
			this.llExport.Location = new System.Drawing.Point(672, 8);
			this.llExport.Name = "llExport";
			this.llExport.Size = new System.Drawing.Size(48, 16);
			this.llExport.TabIndex = 42;
			this.llExport.TabStop = true;
			this.llExport.Text = "Export";
			this.llExport.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.llExport.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.llExport_LinkClicked
				);
			//
			// llImport
			//
			this.llImport.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.llImport.Enabled = false;
			this.llImport.Location = new System.Drawing.Point(728, 8);
			this.llImport.Name = "llImport";
			this.llImport.Size = new System.Drawing.Size(40, 16);
			this.llImport.TabIndex = 43;
			this.llImport.TabStop = true;
			this.llImport.Text = "Import";
			this.llImport.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.llImport.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.llImport_LinkClicked
				);
			//
			// pnJoint
			//
			this.pnJoint.Controls.Add(this.cbJoint);
			this.pnJoint.Controls.Add(this.label1);
			this.pnJoint.Dock = DockStyle.Left;
			this.pnJoint.Location = new System.Drawing.Point(256, 0);
			this.pnJoint.Name = "pnJoint";
			this.pnJoint.Size = new System.Drawing.Size(248, 32);
			this.pnJoint.TabIndex = 7;
			//
			// cbJoint
			//
			this.cbJoint.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.cbJoint.ContextMenu = this.cmJoint;
			this.cbJoint.DropDownStyle =
				ComboBoxStyle
				.DropDownList;
			this.cbJoint.Location = new System.Drawing.Point(34, 0);
			this.cbJoint.Name = "cbJoint";
			this.cbJoint.Size = new System.Drawing.Size(214, 21);
			this.cbJoint.Sorted = true;
			this.cbJoint.TabIndex = 3;
			this.cbJoint.SelectedIndexChanged += new System.EventHandler(
				this.cbSubMesh_SelectedIndexChanged
			);
			//
			// label1
			//
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Joint:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			//
			// pnSubMesh
			//
			this.pnSubMesh.Controls.Add(this.cbSubMesh);
			this.pnSubMesh.Controls.Add(this.label2);
			this.pnSubMesh.Dock = DockStyle.Left;
			this.pnSubMesh.Location = new System.Drawing.Point(0, 0);
			this.pnSubMesh.Name = "pnSubMesh";
			this.pnSubMesh.Size = new System.Drawing.Size(256, 32);
			this.pnSubMesh.TabIndex = 8;
			//
			// cbSubMesh
			//
			this.cbSubMesh.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.cbSubMesh.DropDownStyle =
				ComboBoxStyle
				.DropDownList;
			this.cbSubMesh.Location = new System.Drawing.Point(56, 0);
			this.cbSubMesh.Name = "cbSubMesh";
			this.cbSubMesh.Size = new System.Drawing.Size(190, 21);
			this.cbSubMesh.TabIndex = 5;
			this.cbSubMesh.SelectedIndexChanged += new System.EventHandler(
				this.cbSubMesh_SelectedIndexChanged_1
			);
			//
			// label2
			//
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "SubMesh:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			//
			// cmJoint
			//
			this.cmJoint.MenuItems.AddRange(
				new MenuItem[] { this.miAdd, this.miRem }
			);
			//
			// miAdd
			//
			this.miAdd.Enabled = false;
			this.miAdd.Index = 0;
			this.miAdd.Text = "&Create Joint";
			this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
			//
			// miRem
			//
			this.miRem.Enabled = false;
			this.miRem.Index = 1;
			this.miRem.Text = "&Remove Joint";
			this.miRem.Click += new System.EventHandler(this.miRem_Click);
			//
			// AnimMeshBlockControl
			//
			this.Controls.Add(this.panel2);
			this.Name = "AnimMeshBlockControl";
			this.Size = new System.Drawing.Size(776, 400);
			this.panel2.ResumeLayout(false);
			this.pnJoint.ResumeLayout(false);
			this.pnSubMesh.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion

		#region public Properties
		AnimationMeshBlock[] ambs;
		public AnimationMeshBlock[] MeshBlocks
		{
			get
			{
				return ambs;
			}
			set
			{
				ambs = value;
				RefreshData();
				this.pnSubMesh.Visible = (ambs != null);
			}
		}

		AnimationMeshBlock amb;
		public AnimationMeshBlock MeshBlock
		{
			get
			{
				return amb;
			}
			set
			{
				amb = value;
				RefreshData(amb);
				this.pnSubMesh.Visible = false;
			}
		}
		#endregion
		internal void ClearJoint()
		{
			this.cbJoint.Items.Clear();
			pnFrames.Clear();
		}

		internal void Clear()
		{
			ClearJoint();

			this.cbSubMesh.Items.Clear();
			llImport.Enabled = false;
			llExport.Enabled = llImport.Enabled;
		}

		public void RefreshData()
		{
			Clear();
			if (ambs != null)
			{
				foreach (AnimationMeshBlock amb in ambs)
				{
					cbSubMesh.Items.Add(amb);
				}

				if (cbSubMesh.Items.Count > 0)
				{
					cbSubMesh.SelectedIndex = 0;
				}
			}
		}

		protected void RefreshData(AnimationMeshBlock amb)
		{
			ClearJoint();
			if (amb != null)
			{
				foreach (AnimationFrameBlock afb in amb.Part2)
				{
					cbJoint.Items.Add(afb);
				}

				if (cbJoint.Items.Count > 0)
				{
					cbJoint.SelectedIndex = 0;
				}
			}
		}

		private void cbSubMesh_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.pnFrames.FrameBlock = (AnimationFrameBlock)cbJoint.SelectedItem;
		}

		private void cbSubMesh_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			RefreshData((AnimationMeshBlock)cbSubMesh.SelectedItem);
			llImport.Enabled = cbSubMesh.SelectedItem != null;
			llExport.Enabled = llImport.Enabled;

			this.miAdd.Enabled = (cbJoint.Items.Count > 0);
			this.miRem.Enabled = (cbJoint.Items.Count > 0);
		}

		#region Events
		public event System.EventHandler Changed;
		#endregion

		private void pnFrames_Changed(object sender, System.EventArgs e)
		{
			if (Changed != null)
			{
				Changed(this, e);
			}
		}

		private void llExport_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			AnimationMeshBlock ab1 = (AnimationMeshBlock)cbSubMesh.SelectedItem;
			GenericRcol gmdc = ab1.FindUsedGMDC(ab1.FindDefiningCRES());
			if (gmdc != null)
			{
				GeometryDataContainer gdc = (GeometryDataContainer)gmdc.Blocks[0];
				gdc.LinkedAnimation = ab1;

				fGeometryDataContainer.StartExport(
					new SaveFileDialog(),
					gdc,
					".txt",
					gdc.Groups,
					(Gmdc.ElementSorting)
						fGeometryDataContainer.DefaultSelectedAxisIndex,
					this.cbCorrect.Checked
				);
			}
			else
			{
				Helper.ExceptionMessage(
					new Warning(
						"Unable to Find Model File for \"" + ab1.Name + "\".",
						"SimPe was not able to Find the Model File that defines the specified Hirarchy. The Animation will not get exported!"
					)
				);
			}
		}

		private void llImport_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			AnimationMeshBlock ab1 = (AnimationMeshBlock)cbSubMesh.SelectedItem;
			GenericRcol gmdc = ab1.FindUsedGMDC(ab1.FindDefiningCRES());
			if (gmdc != null)
			{
				GeometryDataContainer gdc = (GeometryDataContainer)gmdc.Blocks[0];
				gdc.LinkedAnimation = ab1;

				fGeometryDataContainer.StartImport(
					new OpenFileDialog(),
					gdc,
					".txt",
					(Gmdc.ElementSorting)
						fGeometryDataContainer.DefaultSelectedAxisIndex,
					true
				);
				ab1.Parent.Changed = true;
				this.RefreshData();
			}
			else
			{
				Helper.ExceptionMessage(
					new Warning(
						"Unable to Find Model File for \"" + ab1.Name + "\".",
						"SimPe was not able to Find the Model File that defines the specified Hirarchy. The Animation will not get imported!"
					)
				);
			}
		}

		private void miAdd_Click(object sender, System.EventArgs e)
		{
			AnimationMeshBlock ab1 = (AnimationMeshBlock)cbSubMesh.SelectedItem;
			if (ab1 != null)
			{
				AnimationFrameBlock afb = new AnimationFrameBlock(ab1);
				afb.Name = "SimPE Dummy";
				afb.TransformationType = FrameType.Rotation;
				afb.CreateBaseAxisSet();

				ab1.Part2 = (AnimationFrameBlock[])Helper.Add(ab1.Part2, afb);

				cbJoint.SelectedIndex = -1;
				cbJoint.Items.Add(afb);
				cbJoint.SelectedIndex = cbJoint.Items.Count - 1;
			}
		}

		private void miRem_Click(object sender, System.EventArgs e)
		{
			AnimationMeshBlock ab1 = (AnimationMeshBlock)cbSubMesh.SelectedItem;
			AnimationFrameBlock afb = (AnimationFrameBlock)cbJoint.SelectedItem;
			if (ab1 != null && afb != null)
			{
				ab1.Part2 = (AnimationFrameBlock[])Helper.Delete(ab1.Part2, afb);
				int sel = cbJoint.SelectedIndex + 1;
				if (sel >= cbJoint.Items.Count)
				{
					sel = cbJoint.Items.Count - 1;
				}

				cbJoint.Items.Remove(afb);
				cbJoint.SelectedIndex = sel;
			}
		}

		private void cbCorrect_CheckedChanged(object sender, System.EventArgs e)
		{
			Helper.WindowsRegistry.CorrectJointDefinitionOnExport =
				this.cbCorrect.Checked;
		}
	}
}
