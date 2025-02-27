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
using System;
using System.Windows.Forms;

namespace SimPe.Plugin.TabPage
{
	/// <summary>
	/// Summary description for fShapeRefNode.
	/// </summary>
	public class Cres : System.Windows.Forms.TabPage
	//System.Windows.Forms.UserControl
	{
		private ToolTip toolTip1;
		internal TreeView cres_tv;
		public ImageList iCres;
		private Label label58;
		internal TextBox tbfjoint;
		private System.ComponentModel.IContainer components;

		public Cres()
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

			//
			// Required designer variable.
			//
			InitializeComponent();

			this.Text = Localization.GetString("CRES Hierarchie");
			this.UseVisualStyleBackColor = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ClearCresTv();
				this.Tag = null;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources =
				new System.Resources.ResourceManager(typeof(Cres));
			this.tbfjoint = new TextBox();
			this.label58 = new Label();
			this.cres_tv = new TreeView();
			this.iCres = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.SuspendLayout();
			//
			// tbfjoint
			//
			this.tbfjoint.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.tbfjoint.Location = new System.Drawing.Point(88, 8);
			this.tbfjoint.Name = "tbfjoint";
			this.tbfjoint.Size = new System.Drawing.Size(696, 20);
			this.tbfjoint.TabIndex = 2;
			this.tbfjoint.Text = "";
			this.tbfjoint.TextChanged += new EventHandler(
				this.tbfjoint_TextChanged
			);
			//
			// label58
			//
			this.label58.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((System.Byte)(0))
			);
			this.label58.Location = new System.Drawing.Point(8, 8);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(72, 23);
			this.label58.TabIndex = 1;
			this.label58.Text = "Find Joint:";
			this.label58.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// cres_tv
			//
			this.cres_tv.Anchor = (
				(AnchorStyles)(
					(
						(
							(
								AnchorStyles.Top
								| AnchorStyles.Bottom
							) | AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.cres_tv.FullRowSelect = true;
			this.cres_tv.HideSelection = false;
			this.cres_tv.ImageList = this.iCres;
			this.cres_tv.Location = new System.Drawing.Point(8, 32);
			this.cres_tv.Name = "cres_tv";
			this.cres_tv.Size = new System.Drawing.Size(776, 226);
			this.cres_tv.TabIndex = 0;
			this.cres_tv.DoubleClick += new EventHandler(
				this.cres_tv_DoubleClick
			);
			this.cres_tv.AfterSelect += new TreeViewEventHandler(
				this.SelectCresTv
			);
			//
			// iCres
			//
			this.iCres.ColorDepth = ColorDepth.Depth32Bit;
			this.iCres.ImageSize = new System.Drawing.Size(16, 16);
			this.iCres.ImageStream = (
				(ImageListStreamer)(
					resources.GetObject("iCres.ImageStream")
				)
			);
			this.iCres.TransparentColor = System.Drawing.Color.Transparent;
			//
			// Cres
			//
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Controls.Add(this.tbfjoint);
			this.Controls.Add(this.label58);
			this.Controls.Add(this.cres_tv);
			this.Location = new System.Drawing.Point(4, 22);
			this.Name = "Cres";
			this.Size = new System.Drawing.Size(792, 262);
			this.ResumeLayout(false);
		}
		#endregion

		private void cres_tv_DoubleClick(object sender, EventArgs e)
		{
			TreeNode sel = cres_tv.SelectedNode;
			cres_tv.SelectedNode = null;
			cres_tv.SelectedNode = sel;
		}

		internal void ClearCresTv()
		{
			if (cres_tv == null)
			{
				return;
			}

			try
			{
				cres_tv.BeginUpdate();
				ClearCresTv(cres_tv.Nodes);
				cres_tv.EndUpdate();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		void ClearCresTv(TreeNodeCollection nodes)
		{
			foreach (TreeNode n in nodes)
			{
				n.Tag = null;
				ClearCresTv(n.Nodes);
			}

			nodes.Clear();
		}

		private void tbfjoint_TextChanged(object sender, EventArgs e)
		{
			tbfjoint.Tag = true;
			try
			{
				string name = tbfjoint.Text.Trim().ToLower();
				if (name != "")
				{
					SelectJoint(cres_tv.Nodes, name);
				}
			}
			finally
			{
				tbfjoint.Tag = null;
			}
		}

		private void SelectCresTv(
			object sender,
			TreeViewEventArgs e
		)
		{
			if (tbfjoint.Tag != null)
			{
				return;
			}

			if (e == null)
			{
				return;
			}

			if (e.Node == null)
			{
				return;
			}

			if (e.Node.Tag == null)
			{
				return;
			}

			int index = (int)e.Node.Tag;
			if (index < 0)
			{
				return;
			}

			ComboBox cb = (ComboBox)(((TabControl)this.Parent).Tag);
			cb.SelectedIndex = index;
			((TabControl)this.Parent).SelectedIndex = 0;
		}

		bool SelectJoint(TreeNodeCollection nodes, string name)
		{
			foreach (TreeNode tn in nodes)
			{
				if (tn.Tag != null)
				{
					ComboBox cb = (ComboBox)(((TabControl)this.Parent).Tag);

					object o = (cb.Items[(int)tn.Tag] as CountedListItem).Object;
					if (o is AbstractCresChildren)
					{
						if (
							((AbstractCresChildren)o)
								.GetName()
								.Trim()
								.ToLower()
								.StartsWith(name)
						)
						{
							cres_tv.SelectedNode = tn;
							tn.EnsureVisible();
							return true;
						}
					}
				}
				if (SelectJoint(tn.Nodes, name))
				{
					return true;
				}
			}

			return false;
		}
	}
}
