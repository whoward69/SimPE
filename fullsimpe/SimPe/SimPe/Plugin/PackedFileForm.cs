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

using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for MyPackedFileForm.
	/// </summary>
	public class RefFileForm : Form
	{
		internal Panel wrapperPanel;
		private Panel panel3;
		private Label label1;
		internal ListBox lblist;
		private GroupBox gbtypes;
		private Panel pntypes;
		internal TextBox tbsubtype;
		internal TextBox tbinstance;
		private Label label11;
		internal TextBox tbtype;
		private Label label8;
		private Label label9;
		private Label label10;
		internal TextBox tbgroup;
		internal ComboBox cbtypes;
		internal LinkLabel llcommit;
		internal LinkLabel lldelete;
		internal LinkLabel lladd;
		private Button button1;
		internal Button btup;
		internal Button btdown;
		private Button button4;
		private Button button2;
		internal PictureBox pb;
		private ContextMenu contextMenu1;
		private MenuItem miAdd;
		internal MenuItem miRem;
		private System.ComponentModel.IContainer components;
		internal System.Drawing.Image imge;

		public RefFileForm()
		{
			components = null;
			//
			// Required designer variable.
			//
			InitializeComponent();
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
			this.wrapperPanel = new Panel();
			this.pb = new PictureBox();
			this.button2 = new Button();
			this.button4 = new Button();
			this.btdown = new Button();
			this.btup = new Button();
			this.button1 = new Button();
			this.gbtypes = new GroupBox();
			this.pntypes = new Panel();
			this.lladd = new LinkLabel();
			this.lldelete = new LinkLabel();
			this.tbsubtype = new TextBox();
			this.tbinstance = new TextBox();
			this.label11 = new Label();
			this.tbtype = new TextBox();
			this.label8 = new Label();
			this.label9 = new Label();
			this.label10 = new Label();
			this.tbgroup = new TextBox();
			this.cbtypes = new ComboBox();
			this.llcommit = new LinkLabel();
			this.lblist = new ListBox();
			this.contextMenu1 = new ContextMenu();
			this.miAdd = new MenuItem();
			this.miRem = new MenuItem();
			this.panel3 = new Panel();
			this.label1 = new Label();
			this.wrapperPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
			this.gbtypes.SuspendLayout();
			this.pntypes.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			//
			// wrapperPanel
			//
			this.wrapperPanel.AutoScroll = true;
			this.wrapperPanel.BackColor = System.Drawing.Color.Transparent;
			this.wrapperPanel.Controls.Add(this.pb);
			this.wrapperPanel.Controls.Add(this.button2);
			this.wrapperPanel.Controls.Add(this.button4);
			this.wrapperPanel.Controls.Add(this.btdown);
			this.wrapperPanel.Controls.Add(this.btup);
			this.wrapperPanel.Controls.Add(this.button1);
			this.wrapperPanel.Controls.Add(this.gbtypes);
			this.wrapperPanel.Controls.Add(this.lblist);
			this.wrapperPanel.Controls.Add(this.panel3);
			this.wrapperPanel.Location = new System.Drawing.Point(8, 8);
			this.wrapperPanel.Name = "wrapperPanel";
			this.wrapperPanel.Size = new System.Drawing.Size(664, 328);
			this.wrapperPanel.TabIndex = 3;
			//
			// pb
			//
			this.pb.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Bottom
						) | AnchorStyles.Right
					)
				)
			);
			this.pb.BorderStyle = BorderStyle.FixedSingle;
			this.pb.ImeMode = ImeMode.NoControl;
			this.pb.Location = new System.Drawing.Point(240, 168);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(152, 152);
			this.pb.SizeMode = PictureBoxSizeMode.Zoom;
			this.pb.TabIndex = 43;
			this.pb.TabStop = false;
			this.pb.SizeChanged += new EventHandler(this.pb_SizeChanged);
			//
			// button2
			//
			this.button2.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.button2.FlatStyle = FlatStyle.Popup;
			this.button2.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.button2.ImeMode = ImeMode.NoControl;
			this.button2.Location = new System.Drawing.Point(320, 28);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 21);
			this.button2.TabIndex = 42;
			this.button2.Text = "Package";
			this.button2.Click += new EventHandler(this.ShowPackageSelector);
			//
			// button4
			//
			this.button4.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.button4.FlatStyle = FlatStyle.Popup;
			this.button4.Font = new System.Drawing.Font(
				"Microsoft Sans Serif",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.button4.ImeMode = ImeMode.NoControl;
			this.button4.Location = new System.Drawing.Point(288, 28);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(21, 21);
			this.button4.TabIndex = 41;
			this.button4.Text = "u";
			this.button4.Click += new EventHandler(this.ChooseFile);
			//
			// btdown
			//
			this.btdown.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.btdown.FlatStyle = FlatStyle.System;
			this.btdown.ImeMode = ImeMode.NoControl;
			this.btdown.Location = new System.Drawing.Point(176, 192);
			this.btdown.Name = "btdown";
			this.btdown.Size = new System.Drawing.Size(48, 23);
			this.btdown.TabIndex = 22;
			this.btdown.Text = "down";
			this.btdown.Click += new EventHandler(this.MoveDown);
			//
			// btup
			//
			this.btup.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.btup.FlatStyle = FlatStyle.System;
			this.btup.ImeMode = ImeMode.NoControl;
			this.btup.Location = new System.Drawing.Point(176, 168);
			this.btup.Name = "btup";
			this.btup.Size = new System.Drawing.Size(48, 23);
			this.btup.TabIndex = 21;
			this.btup.Text = "up";
			this.btup.Click += new EventHandler(this.MoveUp);
			//
			// button1
			//
			this.button1.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.button1.FlatStyle = FlatStyle.System;
			this.button1.ImeMode = ImeMode.NoControl;
			this.button1.Location = new System.Drawing.Point(176, 224);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 23);
			this.button1.TabIndex = 20;
			this.button1.Text = "Commit";
			this.button1.Click += new EventHandler(this.CommitAll);
			//
			// gbtypes
			//
			this.gbtypes.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.gbtypes.Controls.Add(this.pntypes);
			this.gbtypes.FlatStyle = FlatStyle.System;
			this.gbtypes.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.gbtypes.Location = new System.Drawing.Point(176, 32);
			this.gbtypes.Name = "gbtypes";
			this.gbtypes.Size = new System.Drawing.Size(480, 128);
			this.gbtypes.TabIndex = 19;
			this.gbtypes.TabStop = false;
			this.gbtypes.Text = "File Properties";
			//
			// pntypes
			//
			this.pntypes.Controls.Add(this.lladd);
			this.pntypes.Controls.Add(this.lldelete);
			this.pntypes.Controls.Add(this.tbsubtype);
			this.pntypes.Controls.Add(this.tbinstance);
			this.pntypes.Controls.Add(this.label11);
			this.pntypes.Controls.Add(this.tbtype);
			this.pntypes.Controls.Add(this.label8);
			this.pntypes.Controls.Add(this.label9);
			this.pntypes.Controls.Add(this.label10);
			this.pntypes.Controls.Add(this.tbgroup);
			this.pntypes.Controls.Add(this.cbtypes);
			this.pntypes.Controls.Add(this.llcommit);
			this.pntypes.Location = new System.Drawing.Point(8, 24);
			this.pntypes.Name = "pntypes";
			this.pntypes.Size = new System.Drawing.Size(464, 96);
			this.pntypes.TabIndex = 19;
			//
			// lladd
			//
			this.lladd.AutoSize = true;
			this.lladd.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.lladd.ImeMode = ImeMode.NoControl;
			this.lladd.LinkArea = new LinkArea(0, 6);
			this.lladd.Location = new System.Drawing.Point(384, 80);
			this.lladd.Name = "lladd";
			this.lladd.Size = new System.Drawing.Size(28, 18);
			this.lladd.TabIndex = 19;
			this.lladd.TabStop = true;
			this.lladd.Text = "add";
			this.lladd.UseCompatibleTextRendering = true;
			this.lladd.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(this.AddFile);
			//
			// lldelete
			//
			this.lldelete.AutoSize = true;
			this.lldelete.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.lldelete.ImeMode = ImeMode.NoControl;
			this.lldelete.LinkArea = new LinkArea(0, 6);
			this.lldelete.Location = new System.Drawing.Point(416, 80);
			this.lldelete.Name = "lldelete";
			this.lldelete.Size = new System.Drawing.Size(48, 13);
			this.lldelete.TabIndex = 18;
			this.lldelete.TabStop = true;
			this.lldelete.Text = "delete";
			this.lldelete.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.DeleteFile
				);
			//
			// tbsubtype
			//
			this.tbsubtype.Font = new System.Drawing.Font(
				"Microsoft Sans Serif",
				8.25F
			);
			this.tbsubtype.Location = new System.Drawing.Point(120, 24);
			this.tbsubtype.Name = "tbsubtype";
			this.tbsubtype.Size = new System.Drawing.Size(100, 20);
			this.tbsubtype.TabIndex = 12;
			this.tbsubtype.TextChanged += new EventHandler(this.AutoChange);
			//
			// tbinstance
			//
			this.tbinstance.Font = new System.Drawing.Font(
				"Microsoft Sans Serif",
				8.25F
			);
			this.tbinstance.Location = new System.Drawing.Point(120, 72);
			this.tbinstance.Name = "tbinstance";
			this.tbinstance.Size = new System.Drawing.Size(100, 20);
			this.tbinstance.TabIndex = 14;
			this.tbinstance.TextChanged += new EventHandler(this.AutoChange);
			//
			// label11
			//
			this.label11.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label11.ImeMode = ImeMode.NoControl;
			this.label11.Location = new System.Drawing.Point(0, 72);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(112, 17);
			this.label11.TabIndex = 10;
			this.label11.Text = "Instance:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// tbtype
			//
			this.tbtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.tbtype.Location = new System.Drawing.Point(120, 0);
			this.tbtype.Name = "tbtype";
			this.tbtype.Size = new System.Drawing.Size(100, 20);
			this.tbtype.TabIndex = 11;
			this.tbtype.TextChanged += new EventHandler(this.tbtype_TextChanged);
			//
			// label8
			//
			this.label8.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label8.ImeMode = ImeMode.NoControl;
			this.label8.Location = new System.Drawing.Point(0, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 17);
			this.label8.TabIndex = 7;
			this.label8.Text = "File Type:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// label9
			//
			this.label9.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label9.ImeMode = ImeMode.NoControl;
			this.label9.Location = new System.Drawing.Point(0, 24);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(112, 17);
			this.label9.TabIndex = 8;
			this.label9.Text = "SubType/Class ID:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// label10
			//
			this.label10.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.label10.ImeMode = ImeMode.NoControl;
			this.label10.Location = new System.Drawing.Point(0, 48);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 17);
			this.label10.TabIndex = 9;
			this.label10.Text = "Group:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// tbgroup
			//
			this.tbgroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.tbgroup.Location = new System.Drawing.Point(120, 48);
			this.tbgroup.Name = "tbgroup";
			this.tbgroup.Size = new System.Drawing.Size(100, 20);
			this.tbgroup.TabIndex = 13;
			this.tbgroup.TextChanged += new EventHandler(this.AutoChange);
			//
			// cbtypes
			//
			this.cbtypes.DropDownStyle =
				ComboBoxStyle
				.DropDownList;
			this.cbtypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.cbtypes.ItemHeight = 13;
			this.cbtypes.Location = new System.Drawing.Point(224, 0);
			this.cbtypes.Name = "cbtypes";
			this.cbtypes.Size = new System.Drawing.Size(240, 21);
			this.cbtypes.Sorted = true;
			this.cbtypes.TabIndex = 16;
			this.cbtypes.SelectedIndexChanged += new EventHandler(
				this.SelectType
			);
			//
			// llcommit
			//
			this.llcommit.AutoSize = true;
			this.llcommit.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.llcommit.ImeMode = ImeMode.NoControl;
			this.llcommit.LinkArea = new LinkArea(0, 6);
			this.llcommit.Location = new System.Drawing.Point(328, 80);
			this.llcommit.Name = "llcommit";
			this.llcommit.Size = new System.Drawing.Size(54, 13);
			this.llcommit.TabIndex = 17;
			this.llcommit.TabStop = true;
			this.llcommit.Text = "change";
			this.llcommit.Visible = false;
			this.llcommit.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.ChangeFile
				);
			//
			// lblist
			//
			this.lblist.AllowDrop = true;
			this.lblist.Anchor = (
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
			this.lblist.ContextMenu = this.contextMenu1;
			this.lblist.HorizontalScrollbar = true;
			this.lblist.IntegralHeight = false;
			this.lblist.Location = new System.Drawing.Point(8, 32);
			this.lblist.Name = "lblist";
			this.lblist.Size = new System.Drawing.Size(160, 288);
			this.lblist.TabIndex = 1;
			this.lblist.DragDrop += new DragEventHandler(
				this.PackageItemDrop
			);
			this.lblist.DragEnter += new DragEventHandler(
				this.PackageItemDragEnter
			);
			this.lblist.SelectedIndexChanged += new EventHandler(
				this.SelectFile
			);
			//
			// contextMenu1
			//
			this.contextMenu1.MenuItems.AddRange(
				new MenuItem[] { this.miAdd, this.miRem }
			);
			//
			// miAdd
			//
			this.miAdd.Index = 0;
			this.miAdd.Text = "&Add";
			this.miAdd.Click += new EventHandler(this.miAdd_Click);
			//
			// miRem
			//
			this.miRem.Index = 1;
			this.miRem.Text = "&Delete";
			this.miRem.Click += new EventHandler(this.menuItem1_Click);
			//
			// panel3
			//
			this.panel3.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.panel3.Controls.Add(this.label1);
			this.panel3.Font = new System.Drawing.Font(
				"Verdana",
				9.75F,
				System.Drawing.FontStyle.Bold
			);
			this.panel3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Margin = new Padding(0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(664, 24);
			this.panel3.TabIndex = 0;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.ImeMode = ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(0, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(201, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "3D Referencing File Editor";
			//
			// RefFileForm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(856, 350);
			this.Controls.Add(this.wrapperPanel);
			this.Font = new System.Drawing.Font("Verdana", 8.25F);
			this.FormBorderStyle =
				FormBorderStyle
				.SizableToolWindow;
			this.Name = "RefFileForm";
			this.Text = "MyPackedFileForm";
			this.WindowState = FormWindowState.Maximized;
			this.wrapperPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
			this.gbtypes.ResumeLayout(false);
			this.pntypes.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion


		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal IFileWrapperSaveExtension wrapper = null;

		private void SelectType(object sender, EventArgs e)
		{
			if (cbtypes.Tag != null)
			{
				return;
			}

			tbtype.Text =
				"0x"
				+ Helper.HexString(
					((Data.TypeAlias)cbtypes.Items[cbtypes.SelectedIndex]).Id
				);
		}

		private void tbtype_TextChanged(object sender, EventArgs e)
		{
			cbtypes.Tag = true;
			Data.TypeAlias a = Data.MetaData.FindTypeAlias(
				Helper.HexStringToUInt(tbtype.Text)
			);

			this.AutoChange(sender, e);
			int ct = 0;
			foreach (Data.TypeAlias i in cbtypes.Items)
			{
				if (i == a)
				{
					cbtypes.SelectedIndex = ct;
					cbtypes.Tag = null;
					return;
				}
				ct++;
			}

			cbtypes.SelectedIndex = -1;
			cbtypes.Tag = null;
		}

		private void SelectFile(object sender, EventArgs e)
		{
			if (lblist.SelectedIndex < 0)
			{
				llcommit.Enabled =
					lldelete.Enabled =
					btup.Enabled =
					btdown.Enabled =
					miAdd.Enabled =
					miRem.Enabled =
						false;
				return;
			}
			llcommit.Enabled =
				lldelete.Enabled =
				btup.Enabled =
				btdown.Enabled =
				miAdd.Enabled =
				miRem.Enabled =
					true;

			if (tbtype.Tag != null)
			{
				return;
			}

			try
			{
				tbtype.Tag = true;
				Interfaces.Files.IPackedFileDescriptor pfd =
					(Interfaces.Files.IPackedFileDescriptor)
						lblist.Items[lblist.SelectedIndex];
				this.tbgroup.Text = "0x" + Helper.HexString(pfd.Group);
				this.tbinstance.Text = "0x" + Helper.HexString(pfd.Instance);
				this.tbsubtype.Text = "0x" + Helper.HexString(pfd.SubType);
				this.tbtype.Text = "0x" + Helper.HexString(pfd.Type);

				//get Texture
				if (pfd.GetType() == typeof(RefFileItem))
				{
					RefFile wrp = (RefFile)wrapper;
					SkinChain sc = ((RefFileItem)pfd).Skin;
					GenericRcol txtr = null;
					if (sc != null)
					{
						txtr = sc.TXTR;
					}

					//show the Image
					if (txtr == null)
					{
						pb.Image = imge;
					}
					else
					{
						MipMap mm = ((ImageData)txtr.Blocks[0]).GetLargestTexture(
							pb.Size
						);
						if (mm != null)
						{
							pb.Image = mm.Texture;
						}
						else
						{
							pb.Image = imge;
						}
					}
				}
				else
				{
					pb.Image = imge;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(
					Localization.Manager.GetString("errconvert"),
					ex
				);
			}
			finally
			{
				tbtype.Tag = null;
			}
		}

		private void ChangeFile(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			try
			{
				Packages.PackedFileDescriptor pfd = null;
				if (lblist.SelectedIndex >= 0)
				{
					pfd = (Packages.PackedFileDescriptor)
						lblist.Items[lblist.SelectedIndex];
				}
				else
				{
					pfd = new Packages.PackedFileDescriptor();
				}

				pfd.Group = Convert.ToUInt32(this.tbgroup.Text, 16);
				pfd.Instance = Convert.ToUInt32(this.tbinstance.Text, 16);
				pfd.SubType = Convert.ToUInt32(this.tbsubtype.Text, 16);
				pfd.Type = Convert.ToUInt32(this.tbtype.Text, 16);

				if (lblist.SelectedIndex >= 0)
				{
					lblist.Items[lblist.SelectedIndex] = pfd;
					try
					{
						RefFileItem rfi = (RefFileItem)pfd;
						rfi.Skin = null;
					}
					catch { }
				}
				else
				{
					lblist.Items.Add(pfd);
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(
					Localization.Manager.GetString("errconvert"),
					ex
				);
			}
		}

		private void DeleteFile(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			llcommit.Enabled = false;
			lldelete.Enabled = false;
			btup.Enabled = false;
			btdown.Enabled = false;
			miRem.Enabled = lldelete.Enabled;
			if (lblist.SelectedIndex < 0)
			{
				return;
			}

			llcommit.Enabled = true;
			lldelete.Enabled = true;
			btup.Enabled = true;
			btdown.Enabled = true;
			miRem.Enabled = lldelete.Enabled;

			lblist.Items.Remove(lblist.Items[lblist.SelectedIndex]);
		}

		private void AddFile(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			lblist.SelectedIndex = -1;
			ChangeFile(null, null);
			lblist.SelectedIndex = lblist.Items.Count - 1;
		}

		private void CommitAll(object sender, EventArgs e)
		{
			try
			{
				RefFile wrp = (RefFile)wrapper;

				Interfaces.Files.IPackedFileDescriptor[] pfds =
					new Interfaces.Files.IPackedFileDescriptor[lblist.Items.Count];
				for (int i = 0; i < pfds.Length; i++)
				{
					pfds[i] = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[i];
				}

				wrp.Items = pfds;
				wrapper.SynchronizeUserData();
				MessageBox.Show(Localization.Manager.GetString("commited"));
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(
					Localization.Manager.GetString("errwritingfile"),
					ex
				);
			}
		}

		private void MoveUp(object sender, EventArgs e)
		{
			if (lblist.SelectedIndex < 1)
			{
				return;
			}

			Interfaces.Files.IPackedFileDescriptor pfd =
				(Interfaces.Files.IPackedFileDescriptor)
					lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex - 1];
			lblist.Items[lblist.SelectedIndex - 1] = pfd;
			lblist.SelectedIndex--;
		}

		private void MoveDown(object sender, EventArgs e)
		{
			if (lblist.SelectedIndex < 0)
			{
				return;
			}

			if (lblist.SelectedIndex > lblist.Items.Count - 2)
			{
				return;
			}

			Interfaces.Files.IPackedFileDescriptor pfd =
				(Interfaces.Files.IPackedFileDescriptor)
					lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex + 1];
			lblist.Items[lblist.SelectedIndex + 1] = pfd;
			lblist.SelectedIndex++;
		}

		private void AutoChange(object sender, EventArgs e)
		{
			if (tbtype.Tag != null)
			{
				return;
			}

			tbtype.Tag = true;
			if (lblist.SelectedIndex >= 0)
			{
				ChangeFile(null, null);
			}

			tbtype.Tag = null;
		}

		private void ChooseFile(object sender, EventArgs e)
		{
			try
			{
				RefFile wrp = (RefFile)wrapper;
				Interfaces.Files.IPackedFileDescriptor pfd = FileSelect.Execute();
				if (pfd != null)
				{
					tbtype.Tag = true;
					this.tbgroup.Text = "0x" + Helper.HexString(pfd.Group);
					this.tbinstance.Text = "0x" + Helper.HexString(pfd.Instance);
					this.tbsubtype.Text = "0x" + Helper.HexString(pfd.SubType);
					this.tbtype.Text = "0x" + Helper.HexString(pfd.Type);
					tbtype.Tag = null;
					this.AutoChange(sender, e);
				}
			}
			catch (Exception) { }
			finally
			{
				tbtype.Tag = null;
			}
		}

		#region Package Selector
		private void ShowPackageSelector(object sender, EventArgs e)
		{
			PackageSelectorForm form = new PackageSelectorForm();
			form.Execute(((RefFile)wrapper).Package);
		}

		private void PackageItemDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(Packages.PackedFileDescriptor)))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void PackageItemDrop(
			object sender,
			DragEventArgs e
		)
		{
			try
			{
				Interfaces.Files.IPackedFileDescriptor pfd = null;
				pfd = (Interfaces.Files.IPackedFileDescriptor)
					e.Data.GetData(typeof(Packages.PackedFileDescriptor));
				lblist.Items.Add(pfd);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}
		#endregion

		private void pb_SizeChanged(object sender, EventArgs e)
		{
			if (pb.Height < 421)
			{
				pb.Width = pb.Height;
			}
			else
			{
				pb.Width = 420;
			}
		}

		private void miAdd_Click(object sender, EventArgs e)
		{
			AddFile(null, null);
		}

		private void menuItem1_Click(object sender, EventArgs e)
		{
			DeleteFile(null, null);
		}
	}
}
