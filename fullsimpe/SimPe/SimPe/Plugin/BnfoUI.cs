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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Ambertation.Windows.Forms;

using SimPe.Data;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for ExtNgbhUI.
	/// </summary>
	public class BnfoUI
		: Windows.Forms.WrapperBaseControl,
			Interfaces.Plugin.IPackedFileUI
	{
		private BnfoCustomerItemsUI lv;
		private BnfoCustomerItemUI bnfoCustomerItemUI1;
		private ToolStrip toolBar1;
		private ToolStripButton biMax;
		private ToolStripButton biReward;
		private ToolStripButton biWorkers;
		private Panel panel1;
		private Panel panel2;
		private Panel Panel3;
		private Panel Panel4;
		private Panel gpreven;
		private Panel gpexpen;
		private ExtProgressBar pbpay;
		private Label label1;
		private Label lblot;
		private Label label2;
		private Label label3;
		private Label lbright;
		private Label lbassi;
		private Label lbsgender;
		private Label lbwages;
		private Label lbsimage;
		private Label lbLaball;
		private Label lbpay;
		private Label lbaward;
		private Label lbadvice;
		private Label lbcashf;
		private Label lbchgender;
		private Label lbchage;
		private Label lbchoose;
		private Label lbOgender;
		private Label lbOage;
		private Label lbOchoos;
		private TextBox tbOgender;
		private TextBox tbOchage;
		private TextBox tbchgender;
		private TextBox tbchage;
		private TextBox tbCur;
		private TextBox tbMax;
		private TextBox tbLeft;
		private TextBox tbright;
		private TextBox tbassi;
		private TextBox tbwages;
		private TextBox tbsgender;
		private TextBox ybsimage;
		private TextBox tbunknown;
		private Button btchcancel;
		private Button btchadd;
		private Button btaddim;
		private Button btchngeOwn;
		private Button btOcancel;
		private Button btOadd;
		private Button btClearim;
		private Button btdelety;
		private ComboBox cbsimselect;
		private ComboBox cbOsimselect;
		private ListView lvEmployees;
		private ImageList ilist;
		private ToolTip toolTip1;
		private PictureBox pbox;
		private IContainer components;
		int[] edatas;
		int[] rdatas;

		public BnfoUI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			if (Helper.WindowsRegistry.UseBigIcons)
			{
				this.lv.Font = new Font("Tahoma", 12F);
			}

			string ttip =
				"This info is from the sim\'s character\r\nfile so it can\'t be changed here";
			this.toolTip1.SetToolTip(this.tbLeft, ttip);
			this.toolTip1.SetToolTip(this.tbright, ttip);
			this.toolTip1.SetToolTip(this.ybsimage, ttip);
			this.toolTip1.SetToolTip(this.tbsgender, ttip);
			this.toolTip1.SetToolTip(this.tbwages, ttip);
			this.toolTip1.SetToolTip(this.tbassi, ttip);
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
			this.components = new Container();
			ComponentResourceManager resources =
				new ComponentResourceManager(typeof(BnfoUI));
			this.lv = new BnfoCustomerItemsUI();
			this.bnfoCustomerItemUI1 = new BnfoCustomerItemUI();
			this.label1 = new Label();
			this.lblot = new Label();
			this.toolBar1 = new ToolStrip();
			this.biMax = new ToolStripButton();
			this.biReward = new ToolStripButton();
			this.biWorkers = new ToolStripButton();
			this.panel1 = new Panel();
			this.lbcashf = new Label();
			this.gpexpen = new Panel();
			this.gpreven = new Panel();
			this.btClearim = new Button();
			this.tbMax = new TextBox();
			this.tbCur = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.panel2 = new Panel();
			this.btaddim = new Button();
			this.Panel3 = new Panel();
			this.btchcancel = new Button();
			this.btchadd = new Button();
			this.lbchgender = new Label();
			this.lbchage = new Label();
			this.tbchgender = new TextBox();
			this.tbchage = new TextBox();
			this.lbchoose = new Label();
			this.cbsimselect = new ComboBox();
			this.lbadvice = new Label();
			this.btdelety = new Button();
			this.pbox = new PictureBox();
			this.lbaward = new Label();
			this.tbunknown = new TextBox();
			this.lbpay = new Label();
			this.pbpay = new ExtProgressBar();
			this.tbassi = new TextBox();
			this.lbassi = new Label();
			this.tbwages = new TextBox();
			this.lbwages = new Label();
			this.tbsgender = new TextBox();
			this.lbsgender = new Label();
			this.ybsimage = new TextBox();
			this.lbsimage = new Label();
			this.lbLaball = new Label();
			this.tbright = new TextBox();
			this.lbright = new Label();
			this.tbLeft = new TextBox();
			this.lvEmployees = new ListView();
			this.ilist = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.btchngeOwn = new Button();
			this.Panel4 = new Panel();
			this.btOcancel = new Button();
			this.btOadd = new Button();
			this.lbOgender = new Label();
			this.lbOage = new Label();
			this.tbOgender = new TextBox();
			this.tbOchage = new TextBox();
			this.lbOchoos = new Label();
			this.cbOsimselect = new ComboBox();
			this.toolBar1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.Panel4.SuspendLayout();
			((ISupportInitialize)(this.pbox)).BeginInit();
			this.SuspendLayout();
			//
			// lv
			//
			this.lv.Anchor = (
				(AnchorStyles)(
					(
						(
							System.Windows.Forms.AnchorStyles.Top
							| System.Windows.Forms.AnchorStyles.Bottom
						) | System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.lv.Font = new Font("Tahoma", 8.25F);
			this.lv.Items = null;
			this.lv.Location = new Point(8, 32);
			this.lv.Name = "lv";
			this.lv.Size = new Size(328, 229);
			this.lv.TabIndex = 1;
			//
			// bnfoCustomerItemUI1
			//
			this.bnfoCustomerItemUI1.Anchor = (
				(AnchorStyles)(
					(
						(
							System.Windows.Forms.AnchorStyles.Top
							| System.Windows.Forms.AnchorStyles.Bottom
						) | System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.bnfoCustomerItemUI1.BackColor = System.Drawing.Color.Transparent;
			this.bnfoCustomerItemUI1.BnfoCustomerItemsUI = this.lv;
			this.bnfoCustomerItemUI1.Font = new Font("Tahoma", 8.25F);
			this.bnfoCustomerItemUI1.Item = null;
			this.bnfoCustomerItemUI1.Location = new Point(344, 32);
			this.bnfoCustomerItemUI1.Name = "bnfoCustomerItemUI1";
			this.bnfoCustomerItemUI1.Size = new Size(408, 140);
			this.bnfoCustomerItemUI1.TabIndex = 2;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(41, 19);
			this.label1.TabIndex = 5;
			this.label1.Text = "Lot:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// lblot
			//
			this.lblot.AutoSize = true;
			this.lblot.BackColor = System.Drawing.Color.Transparent;
			this.lblot.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lblot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblot.Location = new Point(54, 8);
			this.lblot.Name = "lblot";
			this.lblot.Size = new Size(0, 19);
			this.lblot.TabIndex = 6;
			this.lblot.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			//
			// toolBar1
			//
			this.toolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolBar1.Items.AddRange(
				new ToolStripItem[]
				{
					this.biMax,
					this.biReward,
					this.biWorkers,
				}
			);
			this.toolBar1.Location = new Point(0, 24);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.Size = new Size(760, 54);
			this.toolBar1.TabIndex = 7;
			this.toolBar1.Text = "toolBar1";
			//
			// biMax
			//
			this.biMax.Image = (
				(Image)(resources.GetObject("biMax.Image"))
			);
			this.biMax.ImageScaling = System
				.Windows
				.Forms
				.ToolStripItemImageScaling
				.None;
			this.biMax.Name = "biMax";
			this.biMax.Size = new Size(62, 51);
			this.biMax.Text = "Maximize";
			this.biMax.TextImageRelation = System
				.Windows
				.Forms
				.TextImageRelation
				.ImageAboveText;
			this.biMax.Click += new EventHandler(this.biMax_Activate);
			//
			// biReward
			//
			this.biReward.Image = (
				(Image)(resources.GetObject("biReward.Image"))
			);
			this.biReward.ImageScaling = System
				.Windows
				.Forms
				.ToolStripItemImageScaling
				.None;
			this.biReward.Name = "biReward";
			this.biReward.Size = new Size(82, 51);
			this.biReward.Text = "Reward again";
			this.biReward.TextImageRelation = System
				.Windows
				.Forms
				.TextImageRelation
				.ImageAboveText;
			this.biReward.Click += new EventHandler(this.biReward_Activate);
			//
			// biWorkers
			//
			this.biWorkers.Image = (
				(Image)(resources.GetObject("biWorkers.Image"))
			);
			this.biWorkers.ImageScaling = System
				.Windows
				.Forms
				.ToolStripItemImageScaling
				.None;
			this.biWorkers.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.biWorkers.Name = "biWorkers";
			this.biWorkers.Size = new Size(68, 51);
			this.biWorkers.Text = "Employees";
			this.biWorkers.TextImageRelation = System
				.Windows
				.Forms
				.TextImageRelation
				.ImageAboveText;
			this.biWorkers.ToolTipText = "Change to Employees or Customers";
			this.biWorkers.Click += new EventHandler(this.biWorkers_Click);
			//
			// panel1
			//
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.lbcashf);
			this.panel1.Controls.Add(this.gpexpen);
			this.panel1.Controls.Add(this.gpreven);
			this.panel1.Controls.Add(this.btClearim);
			this.panel1.Controls.Add(this.tbMax);
			this.panel1.Controls.Add(this.tbCur);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.lv);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.bnfoCustomerItemUI1);
			this.panel1.Controls.Add(this.lblot);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new Font("Tahoma", 8.25F);
			this.panel1.Location = new Point(0, 78);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(760, 320);
			this.panel1.TabIndex = 8;
			//
			// lbcashf
			//
			this.lbcashf.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.lbcashf.AutoSize = true;
			this.lbcashf.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Bold
			);
			this.lbcashf.Location = new Point(359, 182);
			this.lbcashf.Name = "lbcashf";
			this.lbcashf.Size = new Size(170, 19);
			this.lbcashf.TabIndex = 14;
			this.lbcashf.Text = "Last Cashflow  = $0";
			//
			// gpexpen
			//
			this.gpexpen.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.gpexpen.BackColor = System.Drawing.Color.Transparent;
			this.gpexpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.gpexpen.Location = new Point(551, 212);
			this.gpexpen.Name = "gpexpen";
			this.gpexpen.Size = new Size(201, 105);
			this.gpexpen.TabIndex = 13;
			//
			// gpreven
			//
			this.gpreven.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.gpreven.BackColor = System.Drawing.Color.Transparent;
			this.gpreven.Location = new Point(344, 212);
			this.gpreven.Name = "gpreven";
			this.gpreven.Size = new Size(201, 105);
			this.gpreven.TabIndex = 12;
			//
			// btClearim
			//
			this.btClearim.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.btClearim.Location = new Point(261, 270);
			this.btClearim.Name = "btClearim";
			this.btClearim.Size = new Size(75, 25);
			this.btClearim.TabIndex = 11;
			this.btClearim.Text = "Clean Up";
			this.toolTip1.SetToolTip(
				this.btClearim,
				"Remove Unknown\r\nSims from this list"
			);
			this.btClearim.UseVisualStyleBackColor = true;
			this.btClearim.Click += new EventHandler(this.btClearim_Click);
			//
			// tbMax
			//
			this.tbMax.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.tbMax.Location = new Point(120, 293);
			this.tbMax.Name = "tbMax";
			this.tbMax.Size = new Size(100, 21);
			this.tbMax.TabIndex = 10;
			this.tbMax.TextChanged += new EventHandler(this.tbMax_TextChanged);
			//
			// tbCur
			//
			this.tbCur.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.tbCur.Location = new Point(120, 269);
			this.tbCur.Name = "tbCur";
			this.tbCur.Size = new Size(100, 21);
			this.tbCur.TabIndex = 9;
			this.tbCur.TextChanged += new EventHandler(this.tbCur_TextChanged);
			//
			// label3
			//
			this.label3.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new Font(
				"Tahoma",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new Point(8, 293);
			this.label3.Name = "label3";
			this.label3.Size = new Size(104, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "Rewarded Level:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// label2
			//
			this.label2.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new Font(
				"Tahoma",
				8.25F,
				System.Drawing.FontStyle.Bold
			);
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new Point(8, 269);
			this.label2.Name = "label2";
			this.label2.Size = new Size(104, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Current Level:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			//
			// panel2
			//
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this.btchngeOwn);
			this.panel2.Controls.Add(this.btaddim);
			this.panel2.Controls.Add(this.Panel3);
			this.panel2.Controls.Add(this.Panel4);
			this.panel2.Controls.Add(this.lbadvice);
			this.panel2.Controls.Add(this.btdelety);
			this.panel2.Controls.Add(this.pbox);
			this.panel2.Controls.Add(this.lbaward);
			this.panel2.Controls.Add(this.tbunknown);
			this.panel2.Controls.Add(this.lbpay);
			this.panel2.Controls.Add(this.pbpay);
			this.panel2.Controls.Add(this.tbassi);
			this.panel2.Controls.Add(this.lbassi);
			this.panel2.Controls.Add(this.tbwages);
			this.panel2.Controls.Add(this.lbwages);
			this.panel2.Controls.Add(this.tbsgender);
			this.panel2.Controls.Add(this.lbsgender);
			this.panel2.Controls.Add(this.ybsimage);
			this.panel2.Controls.Add(this.lbsimage);
			this.panel2.Controls.Add(this.lbLaball);
			this.panel2.Controls.Add(this.tbright);
			this.panel2.Controls.Add(this.lbright);
			this.panel2.Controls.Add(this.tbLeft);
			this.panel2.Controls.Add(this.lvEmployees);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Font = new Font("Tahoma", 8.25F);
			this.panel2.Location = new Point(0, 78);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(760, 320);
			this.panel2.TabIndex = 9;
			this.panel2.Visible = false;
			//
			// btaddim
			//
			this.btaddim.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.btaddim.Location = new Point(710, 291);
			this.btaddim.Name = "btaddim";
			this.btaddim.Size = new Size(47, 23);
			this.btaddim.TabIndex = 23;
			this.btaddim.Text = "Add";
			this.toolTip1.SetToolTip(
				this.btaddim,
				"This just adds an employee to\r\nhere. You may need to open\r\nthe sim\'s SDSC file an"
					+ "d add the\r\nbusiness data and career."
			);
			this.btaddim.UseVisualStyleBackColor = true;
			this.btaddim.Click += new EventHandler(this.btaddim_Click);
			//
			// Panel3
			//
			this.Panel3.BackColor = System.Drawing.Color.Transparent;
			this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel3.Controls.Add(this.btchcancel);
			this.Panel3.Controls.Add(this.btchadd);
			this.Panel3.Controls.Add(this.lbchgender);
			this.Panel3.Controls.Add(this.lbchage);
			this.Panel3.Controls.Add(this.tbchgender);
			this.Panel3.Controls.Add(this.tbchage);
			this.Panel3.Controls.Add(this.lbchoose);
			this.Panel3.Controls.Add(this.cbsimselect);
			this.Panel3.Font = new Font("Tahoma", 12F);
			this.Panel3.Location = new Point(357, 46);
			this.Panel3.Name = "Panel3";
			this.Panel3.Size = new Size(365, 215);
			this.Panel3.TabIndex = 22;
			this.Panel3.Visible = false;
			//
			// btchcancel
			//
			this.btchcancel.Location = new Point(168, 164);
			this.btchcancel.Name = "btchcancel";
			this.btchcancel.Size = new Size(84, 29);
			this.btchcancel.TabIndex = 13;
			this.btchcancel.Text = "Cancel";
			this.btchcancel.UseVisualStyleBackColor = true;
			this.btchcancel.Click += new EventHandler(this.btchcancel_Click);
			//
			// btchadd
			//
			this.btchadd.Location = new Point(269, 164);
			this.btchadd.Name = "btchadd";
			this.btchadd.Size = new Size(84, 29);
			this.btchadd.TabIndex = 12;
			this.btchadd.Text = "Apply";
			this.btchadd.UseVisualStyleBackColor = true;
			this.btchadd.Click += new EventHandler(this.btchadd_Click);
			//
			// lbchgender
			//
			this.lbchgender.AutoSize = true;
			this.lbchgender.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbchgender.Location = new Point(120, 114);
			this.lbchgender.Name = "lbchgender";
			this.lbchgender.Size = new Size(60, 19);
			this.lbchgender.TabIndex = 11;
			this.lbchgender.Text = "Gender";
			//
			// lbchage
			//
			this.lbchage.AutoSize = true;
			this.lbchage.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbchage.Location = new Point(143, 80);
			this.lbchage.Name = "lbchage";
			this.lbchage.Size = new Size(37, 19);
			this.lbchage.TabIndex = 10;
			this.lbchage.Text = "Age";
			//
			// tbchgender
			//
			this.tbchgender.Location = new Point(186, 110);
			this.tbchgender.Name = "tbchgender";
			this.tbchgender.Size = new Size(167, 27);
			this.tbchgender.TabIndex = 3;
			//
			// tbchage
			//
			this.tbchage.Location = new Point(186, 76);
			this.tbchage.Name = "tbchage";
			this.tbchage.Size = new Size(167, 27);
			this.tbchage.TabIndex = 2;
			//
			// lbchoose
			//
			this.lbchoose.AutoSize = true;
			this.lbchoose.Location = new Point(22, 19);
			this.lbchoose.Name = "lbchoose";
			this.lbchoose.Size = new Size(106, 19);
			this.lbchoose.TabIndex = 1;
			this.lbchoose.Text = "Choose a Sim";
			//
			// cbsimselect
			//
			this.cbsimselect.FormattingEnabled = true;
			this.cbsimselect.Location = new Point(134, 15);
			this.cbsimselect.Name = "cbsimselect";
			this.cbsimselect.Size = new Size(219, 27);
			this.cbsimselect.TabIndex = 0;
			this.cbsimselect.SelectedIndexChanged += new EventHandler(
				this.cbsimselect_SelectedIndexChanged
			);

			//
			// Panel4
			//
			this.Panel4.BackColor = System.Drawing.Color.Transparent;
			this.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Panel4.Controls.Add(this.btOcancel);
			this.Panel4.Controls.Add(this.btOadd);
			this.Panel4.Controls.Add(this.lbOgender);
			this.Panel4.Controls.Add(this.lbOage);
			this.Panel4.Controls.Add(this.tbOgender);
			this.Panel4.Controls.Add(this.tbOchage);
			this.Panel4.Controls.Add(this.lbOchoos);
			this.Panel4.Controls.Add(this.cbOsimselect);
			this.Panel4.Font = new Font("Tahoma", 12F);
			this.Panel4.Location = new Point(357, 46);
			this.Panel4.Name = "Panel4";
			this.Panel4.Size = new Size(365, 215);
			this.Panel4.TabIndex = 23;
			this.Panel4.Visible = false;
			//
			// btOcancel
			//
			this.btOcancel.Location = new Point(168, 164);
			this.btOcancel.Name = "btOcancel";
			this.btOcancel.Size = new Size(84, 29);
			this.btOcancel.TabIndex = 13;
			this.btOcancel.Text = "Cancel";
			this.btOcancel.UseVisualStyleBackColor = true;
			this.btOcancel.Click += new EventHandler(this.btOcancel_Click);
			//
			// btOadd
			//
			this.btOadd.Location = new Point(269, 164);
			this.btOadd.Name = "btOadd";
			this.btOadd.Size = new Size(84, 29);
			this.btOadd.TabIndex = 12;
			this.btOadd.Text = "Apply";
			this.btOadd.UseVisualStyleBackColor = true;
			this.btOadd.Click += new EventHandler(this.btOadd_Click);
			//
			// lbOgender
			//
			this.lbOgender.AutoSize = true;
			this.lbOgender.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbOgender.Location = new Point(120, 114);
			this.lbOgender.Name = "lbOgender";
			this.lbOgender.Size = new Size(60, 19);
			this.lbOgender.TabIndex = 11;
			this.lbOgender.Text = "Gender";
			//
			// lbOage
			//
			this.lbOage.AutoSize = true;
			this.lbOage.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbOage.Location = new Point(143, 80);
			this.lbOage.Name = "lbOage";
			this.lbOage.Size = new Size(37, 19);
			this.lbOage.TabIndex = 10;
			this.lbOage.Text = "Age";
			//
			// tbOgender
			//
			this.tbOgender.Location = new Point(186, 110);
			this.tbOgender.Name = "tbOgender";
			this.tbOgender.Size = new Size(167, 27);
			this.tbOgender.TabIndex = 3;
			//
			// tbOchage
			//
			this.tbOchage.Location = new Point(186, 76);
			this.tbOchage.Name = "tbOchage";
			this.tbOchage.Size = new Size(167, 27);
			this.tbOchage.TabIndex = 2;
			//
			// lbOchoos
			//
			this.lbOchoos.AutoSize = true;
			this.lbOchoos.Location = new Point(22, 19);
			this.lbOchoos.Name = "lbOchoos";
			this.lbOchoos.Size = new Size(106, 19);
			this.lbOchoos.TabIndex = 1;
			this.lbOchoos.Text = "Choose a Sim";
			//
			// cbOsimselect
			//
			this.cbOsimselect.FormattingEnabled = true;
			this.cbOsimselect.Location = new Point(134, 15);
			this.cbOsimselect.Name = "cbOsimselect";
			this.cbOsimselect.Size = new Size(219, 27);
			this.cbOsimselect.TabIndex = 0;
			this.cbOsimselect.SelectedIndexChanged += new EventHandler(
				this.cbOsimselect_SelectedIndexChanged
			);

			//
			// lbadvice
			//
			this.lbadvice.AutoSize = true;
			this.lbadvice.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbadvice.ForeColor = System.Drawing.Color.DarkRed;
			this.lbadvice.Location = new Point(466, 4);
			this.lbadvice.Name = "lbadvice";
			this.lbadvice.Size = new Size(77, 19);
			this.lbadvice.TabIndex = 21;
			this.lbadvice.Text = "Warning";
			this.lbadvice.Visible = false;
			//
			// btdelety
			//
			this.btdelety.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.btdelety.Location = new Point(625, 291);
			this.btdelety.Name = "btdelety";
			this.btdelety.Size = new Size(75, 23);
			this.btdelety.TabIndex = 20;
			this.btdelety.Text = "Remove";
			this.toolTip1.SetToolTip(
				this.btdelety,
				"This just removes the employee\r\nfrom here. You may need to open\r\nthe sim\'s SDSC f"
					+ "ile and clear any\r\nbusiness data and career."
			);
			this.btdelety.UseVisualStyleBackColor = true;
			this.btdelety.Click += new EventHandler(this.btdelety_Click);
			//
			// pbox
			//
			this.pbox.Location = new Point(646, 32);
			this.pbox.Name = "pbox";
			this.pbox.Size = new Size(40, 41);
			this.pbox.TabIndex = 19;
			this.pbox.TabStop = false;
			//
			// lbaward
			//
			this.lbaward.AutoSize = true;
			this.lbaward.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbaward.Location = new Point(388, 156);
			this.lbaward.Name = "lbaward";
			this.lbaward.Size = new Size(65, 19);
			this.lbaward.TabIndex = 18;
			this.lbaward.Text = "Fair Pay";
			//
			// tbunknown
			//
			this.tbunknown.BackColor = System.Drawing.SystemColors.Window;
			this.tbunknown.Font = new Font("Tahoma", 12F);
			this.tbunknown.Location = new Point(459, 152);
			this.tbunknown.Name = "tbunknown";
			this.tbunknown.Size = new Size(167, 27);
			this.tbunknown.TabIndex = 17;
			this.toolTip1.SetToolTip(
				this.tbunknown,
				resources.GetString("tbunknown.ToolTip")
			);
			this.tbunknown.TextChanged += new EventHandler(
				this.tbunknown_TextChanged
			);
			//
			// lbpay
			//
			this.lbpay.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbpay.Location = new Point(374, 283);
			this.lbpay.Name = "lbpay";
			this.lbpay.Size = new Size(227, 19);
			this.lbpay.TabIndex = 16;
			this.lbpay.Text = "Fairly Paid (100%)";
			this.lbpay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			//
			// pbpay
			//
			this.pbpay.BackColor = System.Drawing.Color.Transparent;
			this.pbpay.Font = new Font(
				"Tahoma",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.pbpay.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.pbpay.Location = new Point(363, 245);
			this.pbpay.Maximum = 7;
			this.pbpay.Name = "pbpay";
			this.pbpay.Padding = new Padding(0, 0, 0, 2);
			this.pbpay.SelectedColor = System.Drawing.Color.Gold;
			this.pbpay.Size = new Size(261, 35);
			this.pbpay.TabIndex = 15;
			this.pbpay.TokenCount = 7;
			this.pbpay.UnselectedColor = System.Drawing.Color.Black;
			this.pbpay.Value = 4;
			//
			// tbassi
			//
			this.tbassi.BackColor = System.Drawing.SystemColors.Window;
			this.tbassi.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbassi.Location = new Point(459, 212);
			this.tbassi.Name = "tbassi";
			this.tbassi.ReadOnly = true;
			this.tbassi.Size = new Size(167, 27);
			this.tbassi.TabIndex = 14;
			//
			// lbassi
			//
			this.lbassi.AutoSize = true;
			this.lbassi.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbassi.Location = new Point(361, 216);
			this.lbassi.Name = "lbassi";
			this.lbassi.Size = new Size(92, 19);
			this.lbassi.TabIndex = 13;
			this.lbassi.Text = "Assignment";
			//
			// tbwages
			//
			this.tbwages.BackColor = System.Drawing.SystemColors.Window;
			this.tbwages.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbwages.Location = new Point(459, 182);
			this.tbwages.Name = "tbwages";
			this.tbwages.ReadOnly = true;
			this.tbwages.Size = new Size(167, 27);
			this.tbwages.TabIndex = 12;
			//
			// lbwages
			//
			this.lbwages.AutoSize = true;
			this.lbwages.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbwages.Location = new Point(353, 186);
			this.lbwages.Name = "lbwages";
			this.lbwages.Size = new Size(100, 19);
			this.lbwages.TabIndex = 11;
			this.lbwages.Text = "Hourly Wage";
			//
			// tbsgender
			//
			this.tbsgender.BackColor = System.Drawing.SystemColors.Window;
			this.tbsgender.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbsgender.Location = new Point(459, 122);
			this.tbsgender.Name = "tbsgender";
			this.tbsgender.ReadOnly = true;
			this.tbsgender.Size = new Size(167, 27);
			this.tbsgender.TabIndex = 10;
			//
			// lbsgender
			//
			this.lbsgender.AutoSize = true;
			this.lbsgender.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbsgender.Location = new Point(393, 126);
			this.lbsgender.Name = "lbsgender";
			this.lbsgender.Size = new Size(60, 19);
			this.lbsgender.TabIndex = 9;
			this.lbsgender.Text = "Gender";
			//
			// ybsimage
			//
			this.ybsimage.BackColor = System.Drawing.SystemColors.Window;
			this.ybsimage.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.ybsimage.Location = new Point(459, 92);
			this.ybsimage.Name = "ybsimage";
			this.ybsimage.ReadOnly = true;
			this.ybsimage.Size = new Size(167, 27);
			this.ybsimage.TabIndex = 8;
			//
			// lbsimage
			//
			this.lbsimage.AutoSize = true;
			this.lbsimage.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbsimage.Location = new Point(416, 96);
			this.lbsimage.Name = "lbsimage";
			this.lbsimage.Size = new Size(37, 19);
			this.lbsimage.TabIndex = 7;
			this.lbsimage.Text = "Age";
			//
			// lbLaball
			//
			this.lbLaball.AutoSize = true;
			this.lbLaball.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbLaball.Location = new Point(28, 8);
			this.lbLaball.Name = "lbLaball";
			this.lbLaball.Size = new Size(85, 19);
			this.lbLaball.TabIndex = 6;
			this.lbLaball.Text = "Employees";
			//
			// tbright
			//
			this.tbright.BackColor = System.Drawing.SystemColors.Window;
			this.tbright.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbright.Location = new Point(459, 62);
			this.tbright.Name = "tbright";
			this.tbright.ReadOnly = true;
			this.tbright.Size = new Size(167, 27);
			this.tbright.TabIndex = 5;
			//
			// lbright
			//
			this.lbright.AutoSize = true;
			this.lbright.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.lbright.Location = new Point(401, 66);
			this.lbright.Name = "lbright";
			this.lbright.Size = new Size(52, 19);
			this.lbright.TabIndex = 4;
			this.lbright.Text = "Status";
			//
			// tbLeft
			//
			this.tbLeft.BackColor = System.Drawing.SystemColors.Window;
			this.tbLeft.Font = new Font(
				"Tahoma",
				12F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbLeft.Location = new Point(365, 32);
			this.tbLeft.Name = "tbLeft";
			this.tbLeft.ReadOnly = true;
			this.tbLeft.Size = new Size(261, 27);
			this.tbLeft.TabIndex = 3;
			this.tbLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			// lvEmployees
			//
			this.lvEmployees.Anchor = (
				(AnchorStyles)(
					(
						(
							System.Windows.Forms.AnchorStyles.Top
							| System.Windows.Forms.AnchorStyles.Bottom
						) | System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.lvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvEmployees.FullRowSelect = true;
			this.lvEmployees.HideSelection = false;
			this.lvEmployees.LargeImageList = this.ilist;
			this.lvEmployees.Location = new Point(8, 32);
			this.lvEmployees.MultiSelect = false;
			this.lvEmployees.Name = "lvEmployees";
			this.lvEmployees.Size = new Size(330, 270);
			this.lvEmployees.TabIndex = 1;
			this.lvEmployees.UseCompatibleStateImageBehavior = false;
			this.lvEmployees.SelectedIndexChanged += new EventHandler(
				this.lvEmployees_SelectedIndexChanged
			);
			this.lvEmployees.DoubleClick += new EventHandler(
				this.lvEmployees_DoubleClick
			);
			//
			// ilist
			//
			this.ilist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilist.ImageSize = new Size(96, 96);
			this.ilist.TransparentColor = System.Drawing.Color.Transparent;
			//
			// toolTip1
			//
			this.toolTip1.AutoPopDelay = 8000;
			this.toolTip1.InitialDelay = 200;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "Note";
			//
			// btchngeOwn
			//
			this.btchngeOwn.Anchor = (
				(AnchorStyles)(
					(
						System.Windows.Forms.AnchorStyles.Bottom
						| System.Windows.Forms.AnchorStyles.Left
					)
				)
			);
			this.btchngeOwn.Location = new Point(625, 291);
			this.btchngeOwn.Name = "btchngeOwn";
			this.btchngeOwn.Size = new Size(75, 23);
			this.btchngeOwn.TabIndex = 24;
			this.btchngeOwn.Text = "Change";
			this.toolTip1.SetToolTip(
				this.btchngeOwn,
				"Owner information is not held in this BNFO, this changes\r\nthe owner value in the Lot file (LTXT)"
			);
			this.btchngeOwn.UseVisualStyleBackColor = true;
			this.btchngeOwn.Visible = false;
			this.btchngeOwn.Click += new EventHandler(this.btchngeOwn_Click);
			//
			// BnfoUI
			//
			this.BackgroundImageLocation = new Point(760, 54);
			this.BackgroundImageZoomToFit = true;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.toolBar1);
			this.Font = new Font("Microsoft Sans Serif", 8.25F);
			this.HeaderText = "Business Info";
			this.Name = "BnfoUI";
			this.Size = new Size(760, 398);
			this.Controls.SetChildIndex(this.toolBar1, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.toolBar1.ResumeLayout(false);
			this.toolBar1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.Panel3.ResumeLayout(false);
			this.Panel3.PerformLayout();
			this.Panel4.ResumeLayout(false);
			this.Panel4.PerformLayout();
			((ISupportInitialize)(this.pbox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		public Bnfo Bnfo => (Bnfo)Wrapper;

		bool intern;
		bool employees = false;
		int homeb = 1;
		ushort owner = 0xffff;
		ushort famly = 0;

		protected override void RefreshGUI()
		{
			if (intern)
			{
				return;
			}

			System.Resources.ResourceManager resources =
				new System.Resources.ResourceManager(typeof(BnfoUI));
			if (resources.GetString("$this.HeaderText") == null)
			{
				this.HeaderText = "Business Info";
			}
			else
			{
				this.HeaderText = resources.GetString("$this.HeaderText");
			}

			intern = true;
			// Clear Panel2 but it doesn't seem to be needed
			/*
			this.panel2.Visible = false;
			this.panel1.Visible = true;
			this.biWorkers.Text = "Employees";
			employees = false;
			panel2_clear();
			*/
			if (Bnfo != null)
			{
				lv.Items = Bnfo.CustomerItems;
				biMax.Enabled = biReward.Enabled = biWorkers.Enabled = true;
				lbcashf.Visible = gpreven.Visible = gpexpen.Visible = true;
				owner = 0xffff;
				string ltname = SimPe.Localization.GetString("Unknown");

				Interfaces.Files.IPackedFileDescriptor pfd =
					Bnfo.Package.FindFile(
						0x0BF999E7,
						0,
						0xFFFFFFFF,
						Bnfo.FileDescriptor.Instance
					);
				if (pfd != null)
				{
					Ltxt ltx = new Ltxt();
					ltx.ProcessData(pfd, Bnfo.Package);
					owner = (ushort)ltx.OwnerInstance;
					ltname = ltx.LotName;
					if (ltx.Type == Ltxt.LotType.Residential)
					{
						homeb = 0;
					}
					else
					{
						homeb = 1;
					}

					this.lblot.Text = ltname + " (" + ltx.Type.ToString() + " Lot)";
				}
				else
				{
					Interfaces.Providers.ILotItem ili =
						FileTable.ProviderRegistry.LotProvider.FindLot(
							Bnfo.FileDescriptor.Instance
						);
					if (ili != null)
					{
						ltname = ili.LotName;
						owner = (ushort)ili.Owner;
						this.lblot.Text = ltname;
					}
					homeb = 1;
				}

				tbCur.Text = Bnfo.CurrentBusinessState.ToString();
				tbMax.Text = Bnfo.MaxSeenBusinessState.ToString();

				this.HeaderText += ": " + ltname;
				this.lbLaball.Text = ltname + " Employees";
				GetEmployees();
				RefreshGraphs();
				btClearim.Visible = !AllValid();
			}
			else
			{
				lv.Items = null;
				this.lblot.Text = "";
				biMax.Enabled = biReward.Enabled = biWorkers.Enabled = false;
				btClearim.Visible =
					lbcashf.Visible =
					gpreven.Visible =
					gpexpen.Visible =
						false;
			}

			tbMax.Enabled = biMax.Enabled;
			tbCur.Enabled = biMax.Enabled;
			intern = false;
		}

		private void GetEmployees()
		{
			ExtSDesc sdsc;

			sdsc =
				FileTable.ProviderRegistry.SimDescriptionProvider.SimInstance[owner]
				as ExtSDesc;
			if (sdsc != null)
			{
				AddImage(sdsc);
				ListViewItem lvi = new ListViewItem();
				lvi.Text = sdsc.SimName + " : Owner";
				lvi.ImageIndex = ilist.Images.Count - 1;
				lvi.Tag = sdsc;
				lvi.SubItems.Add("3"); // payrate
				lvi.SubItems.Add("-1"); // sim index
				lvi.SubItems.Add("0"); // Catched Fair Pay
				lvEmployees.Items.Add(lvi);
				famly = sdsc.FamilyInstance;
			}
			else
			{
				this.ilist.Images.Add(new Bitmap(SimPe.GetImage.NoOne));
				ListViewItem lvi = new ListViewItem();
				lvi.Text = SimPe.Localization.GetString("Unknown") + " : Owner";
				lvi.ImageIndex = ilist.Images.Count - 1;
				lvi.Tag = null;
				lvi.SubItems.Add("3"); // payrate
				lvi.SubItems.Add("-1"); // sim index
				lvi.SubItems.Add("0"); // Catched Fair Pay
				lvEmployees.Items.Add(lvi);
				famly = 0;
			}

			for (int i = 0; i < Bnfo.EmployeeCount; i++)
			{
				sdsc =
					FileTable.ProviderRegistry.SimDescriptionProvider.SimInstance[
						Bnfo.Employees[i]
					] as ExtSDesc;
				if (sdsc != null)
				{
					AddImage(sdsc);
					ListViewItem lvi = new ListViewItem();
					lvi.Text = sdsc.SimName + " " + sdsc.SimFamilyName;
					lvi.ImageIndex = ilist.Images.Count - 1;
					lvi.Tag = sdsc;
					lvi.SubItems.Add(Convert.ToString(Bnfo.PayRate[i]));
					lvi.SubItems.Add(Convert.ToString(i));
					lvi.SubItems.Add(Convert.ToString(Bnfo.A[i]));
					lvEmployees.Items.Add(lvi);
				}
				else
				{
					this.ilist.Images.Add(new Bitmap(SimPe.GetImage.NoOne));
					ListViewItem lvi = new ListViewItem();
					lvi.Text = SimPe.Localization.GetString("Unknown");
					lvi.ImageIndex = ilist.Images.Count - 1;
					lvi.Tag = null;
					lvi.SubItems.Add(Convert.ToString(Bnfo.PayRate[i]));
					lvi.SubItems.Add(Convert.ToString(i));
					lvi.SubItems.Add(Convert.ToString(Bnfo.A[i]));
					lvEmployees.Items.Add(lvi);
				}
			}
			btdelety.Visible = btchngeOwn.Visible = false;
		}

		private void RefreshGraphs()
		{
			int n = 0;
			int historycount = Bnfo.HistoryCount;
			/*
			 * somehow I have to gleen if home business or not - homeb = 0 for home business else = 1
			*/
			if (historycount > homeb)
			{
				int cflo =
					(Bnfo.Revenue[historycount - 1])
					- (Bnfo.Expences[historycount - 1]);
				Array.Resize(ref rdatas, historycount - homeb);
				Array.Resize(ref edatas, historycount - homeb);
				// set both graphs to same max value so a direct comparison between the bar heights is possible
				double mMax = 0;

				for (int i = homeb; i < historycount; i++)
				{
					/*
					 * expences can be negative
					 * negative values get cut off to zero at the graph as it can't display them
					 * therefore I add (subtract because they are negative) to revenue
					*/
					edatas[n] = Bnfo.Expences[i];
					rdatas[n] = Bnfo.Revenue[i];
					if (Bnfo.Expences[i] < 0)
					{
						rdatas[n] -= Bnfo.Expences[i];
					}

					if (edatas[n] > mMax)
					{
						mMax = edatas[n];
					}

					if (rdatas[n] > mMax)
					{
						mMax = rdatas[n];
					}

					n++;
				}
				lbcashf.Text = "Current Cashflow = " + cflo.ToString("c");
			}
			else
			{
				edatas = new int[] { 0, 0 };
				rdatas = new int[] { 0, 0 };
				lbcashf.Text = "Cashflow = $0";
			}
		}

		private void AddImage(ExtSDesc sdesc)
		{
			Image img = null;
			if (sdesc.HasImage)
			{
				img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(
					sdesc.Image,
					new Point(0, 0),
					Color.Magenta
				);
			}
			else
			{
				img = SimPe.GetImage.NoOne;
			}

			img = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(
				img,
				this.ilist.ImageSize,
				12,
				Color.FromArgb(90, Color.Black),
				SimPe.PackedFiles.Wrapper.SimPoolControl.GetImagePanelColor(sdesc),
				Color.White,
				Color.FromArgb(80, Color.White),
				true,
				4,
				0
			);
			this.ilist.Images.Add(img);
		}

		public override void OnCommit()
		{
			lbadvice.Visible = false;
			Bnfo.SynchronizeUserData(true, false);
		}

		private void biMax_Activate(object sender, EventArgs e)
		{
			if (lv.Items == null)
			{
				return;
			}

			foreach (BnfoCustomerItem item in lv.Items)
			{
				item.LoyaltyScore = 1000;
			}

			lv.Refresh();
		}

		private void biReward_Activate(object sender, EventArgs e)
		{
			if (Bnfo == null)
			{
				return;
			}

			Bnfo.CurrentBusinessState = 0;
			Bnfo.MaxSeenBusinessState = 0;
			panel2_clear();
			RefreshGUI();
		}

		private void tbCur_TextChanged(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (Bnfo == null)
			{
				return;
			}

			Bnfo.CurrentBusinessState = Helper.StringToUInt32(
				tbCur.Text,
				Bnfo.CurrentBusinessState,
				10
			);
		}

		private void tbMax_TextChanged(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (Bnfo == null)
			{
				return;
			}

			Bnfo.MaxSeenBusinessState = Helper.StringToUInt32(
				tbMax.Text,
				Bnfo.MaxSeenBusinessState,
				10
			);
		}

		private void biWorkers_Click(object sender, EventArgs e)
		{
			if (employees)
			{
				this.panel2.Visible = false;
				this.panel1.Visible = true;
				this.biWorkers.Text = "Employees";
				biMax.Enabled = biReward.Enabled = (Bnfo != null);
				this.CanCommit = true;
				employees = false;
			}
			else
			{
				this.panel2.Visible = true;
				this.panel1.Visible = false;
				this.biWorkers.Text = "Customers";
				biMax.Enabled = biReward.Enabled = false;
				this.CanCommit = false;
				employees = true;
			}
		}

		private void lvEmployees_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvEmployees.SelectedItems.Count < 1)
			{
				return;
			}

			intern = true;
			int payr = 3;
			try
			{
				payr = Convert.ToInt32(lvEmployees.SelectedItems[0].SubItems[1].Text);
			}
			catch { }
			if (payr < 0)
			{
				payr = 0;
			}
			else if (payr > 6)
			{
				payr = 6;
			}

			PayBar(payr);
			try
			{
				tbunknown.Text = Convert
					.ToUInt32(lvEmployees.SelectedItems[0].SubItems[3].Text)
					.ToString("C0");
			}
			catch { }
			if (lvEmployees.SelectedItems[0].Tag != null)
			{
				ExtSDesc sdsc =
					lvEmployees.SelectedItems[0].Tag
					as ExtSDesc;
				tbLeft.Text = sdsc.SimName + " " + sdsc.SimFamilyName;
				ybsimage.Text = Enum.GetName(
					typeof(MetaData.LifeSections),
					(ushort)sdsc.CharacterDescription.LifeSection
				);
				if (sdsc.CharacterDescription.Gender == Data.MetaData.Gender.Female)
				{
					tbsgender.Text = "Female";
				}
				else
				{
					tbsgender.Text = "Male";
				}

				if (lvEmployees.SelectedItems[0].Text.Contains(" : Owner"))
				{
					tbright.Text = "Business Owner";
					btdelety.Visible = false;
					pbpay.Visible = pbox.Visible = lbpay.Visible = false;
					btchngeOwn.Visible = true;
				}
				else
				{
					btchngeOwn.Visible = false;
					pbpay.Visible = pbox.Visible = lbpay.Visible = true;
					btdelety.Visible = true;
					if (sdsc.CharacterDescription.GhostFlag.IsGhost)
					{
						tbright.Text = sdsc.SimName + " has Died";
					}
					else if (sdsc.FamilyInstance == famly)
					{
						tbright.Text = "Family Member";
					}
					else if (
						sdsc.CharacterDescription.CareerLevel == 2
						&& sdsc.CharacterDescription.Career
							== MetaData.Careers.OwnedBuss
					)
					{
						tbright.Text = "Manager";
					}
					else
					{
						tbright.Text = "Employee";
					}
				}

				tbwages.Text = sdsc.Business.Salary.ToString("C0");
				tbassi.Text = Localization.GetString(
					"SimPe.PackedFiles.Wrapper.JobAssignment."
						+ Enum.GetName(
							typeof(JobAssignment),
							(ushort)sdsc.Business.Assignment
						)
				);
			}
			else
			{
				tbLeft.Text = "Sim not Found!";
				ybsimage.Text = SimPe.Localization.GetString("Unknown");
				tbsgender.Text = SimPe.Localization.GetString("Unknown");
				if (lvEmployees.SelectedItems[0].Text.Contains(" : Owner"))
				{
					tbright.Text = "Business Owner";
					btdelety.Visible = false;
					pbpay.Visible = pbox.Visible = lbpay.Visible = false;
					btchngeOwn.Visible = true;
				}
				else
				{
					btchngeOwn.Visible = false;
					tbright.Text = "Employee";
					btdelety.Visible = true;
					pbpay.Visible = pbox.Visible = lbpay.Visible = true;
				}
				tbwages.Text = SimPe.Localization.GetString("Unknown");
				tbassi.Text = SimPe.Localization.GetString("Unknown");
			}
			intern = false;
		}

		private void lvEmployees_DoubleClick(object sender, EventArgs e)
		{
			if (lvEmployees.SelectedItems.Count < 1)
			{
				return;
			}

			if (lvEmployees.SelectedItems[0].Tag == null)
			{
				return;
			}

			ExtSDesc sdsc =
				lvEmployees.SelectedItems[0].Tag as ExtSDesc;
			if (sdsc == null)
			{
				return;
			}

			Interfaces.Files.IPackedFileDescriptor pfd;
			try
			{
				if (sdsc.Package == Bnfo.Package)
				{
					pfd = sdsc.Package.NewDescriptor(
						0xAACE2EFB,
						sdsc.FileDescriptor.SubType,
						sdsc.FileDescriptor.Group,
						sdsc.FileDescriptor.Instance
					);
				}
				else
				{
					pfd = fixlowercase(sdsc.Package.FileName)
						.NewDescriptor(
							0xAACE2EFB,
							sdsc.FileDescriptor.SubType,
							sdsc.FileDescriptor.Group,
							sdsc.FileDescriptor.Instance
						);
				}

				pfd = sdsc.Package.FindFile(pfd);
				SimPe.RemoteControl.OpenPackedFile(pfd, sdsc.Package);
			}
			catch { }
		}

		private void PayBar(int PayRate)
		{
			pbpay.Value = PayRate + 1;
			if (PayRate == 0)
			{
				pbpay.SelectedColor = System.Drawing.Color.DarkRed;
				lbpay.Text = "Ridiculously Underpaid (25%)";
			}
			if (PayRate == 1)
			{
				pbpay.SelectedColor = System.Drawing.Color.Red;
				lbpay.Text = "Very Underpaid (50%)";
			}
			if (PayRate == 2)
			{
				pbpay.SelectedColor = System.Drawing.Color.OrangeRed;
				lbpay.Text = "Underpaid (75%)";
			}
			if (PayRate == 3)
			{
				pbpay.SelectedColor = System.Drawing.Color.Gold;
				lbpay.Text = "Fairly Paid (100%)";
			}
			if (PayRate == 4)
			{
				pbpay.SelectedColor = System.Drawing.Color.YellowGreen;
				lbpay.Text = "Overpaid (125%)";
			}
			if (PayRate == 5)
			{
				pbpay.SelectedColor = System.Drawing.Color.LimeGreen;
				lbpay.Text = "Very Overpaid (150%)";
			}
			if (PayRate == 6)
			{
				pbpay.SelectedColor = System.Drawing.Color.Green;
				lbpay.Text = "Ridiculously Overpaid (175%)";
			}
			SetSmilyIcon();
		}

		private void pbpay_ChangedValue(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (pbpay.Value < 1)
			{
				pbpay.Value = 1;
			}
			else if (pbpay.Value > 7)
			{
				pbpay.Value = 7;
			}

			PayBar(pbpay.Value - 1);
			if (lvEmployees.SelectedItems.Count < 1)
			{
				return;
			}

			try
			{
				int indects = Convert.ToInt32(
					lvEmployees.SelectedItems[0].SubItems[2].Text
				);
				if (indects > -1)
				{
					Bnfo.PayRate[indects] = pbpay.Value - 1;
					lvEmployees.SelectedItems[0].SubItems[1].Text = Convert.ToString(
						pbpay.Value - 1
					);
					this.CanCommit = true;
				}
			}
			catch { }
		}

		private void tbunknown_TextChanged(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (lvEmployees.SelectedItems.Count < 1)
			{
				return;
			}

			try
			{
				int indects = Convert.ToInt32(
					lvEmployees.SelectedItems[0].SubItems[2].Text
				);
				if (indects > -1)
				{
					string fp = tbunknown.Text;
					if (fp.StartsWith("$"))
					{
						fp = fp.Substring(1);
					}

					Bnfo.A[indects] = Convert.ToUInt32(fp);
					lvEmployees.SelectedItems[0].SubItems[3].Text = Convert.ToString(
						Bnfo.A[indects]
					);
					this.CanCommit = true;
				}
			}
			catch { }
		}

		private void SetSmilyIcon()
		{
			uint inst = 0xABBA2585;
			if (pbpay.Value == 1)
			{
				inst = 0xABBA2595;
			}

			if (pbpay.Value == 2)
			{
				inst = 0xABBA2591;
			}

			if (pbpay.Value == 3)
			{
				inst = 0xABBA2588;
			}

			if (pbpay.Value == 4)
			{
				inst = 0xABBA2585;
			}

			if (pbpay.Value == 5)
			{
				inst = 0xABBA2582;
			}

			if (pbpay.Value == 6)
			{
				inst = 0xABBA2578;
			}

			if (pbpay.Value == 7)
			{
				inst = 0xABBA2575;
			}

			Packages.File pkg = SimPe.Packages.File.LoadFromFile(
				System.IO.Path.Combine(
					PathProvider.Global.Latest.InstallFolder,
					"TSData\\Res\\UI\\ui.package"
				)
			);
			if (pkg != null)
			{
				Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(
					0x856DDBAC,
					0,
					0x499DB772,
					inst
				);
				if (pfd != null)
				{
					Picture pic =
						new Picture();
					pic.ProcessData(pfd, pkg);
					pbox.Image = pic.Image;
				}
				else
				{
					pbox.Image = null;
				}
			}
			else
			{
				pbox.Image = null;
			}
		}

		private void btClearim_Click(object sender, EventArgs e)
		{
			Collections.BnfoCustomerItems bnff =
				new Collections.BnfoCustomerItems(Bnfo);

			foreach (BnfoCustomerItem item in Bnfo.CustomerItems)
			{
				if (item.SimDescription != null)
				{
					bnff.Add(item);
				}
			}
			Bnfo.CustomerItems.Clear();
			foreach (BnfoCustomerItem item in bnff)
			{
				Bnfo.CustomerItems.Add(item);
			}
			lv.Refresh();
			btClearim.Visible = false;
		}

		private void btdelety_Click(object sender, EventArgs e)
		{
			if (lvEmployees.SelectedItems.Count < 1)
			{
				return;
			}

			int indects = Convert.ToInt32(
				lvEmployees.SelectedItems[0].SubItems[2].Text
			);
			if (indects < 0)
			{
				return;
			}

			if (lvEmployees.SelectedItems[0].Tag != null)
			{
				ExtSDesc sdsc =
					lvEmployees.SelectedItems[0].Tag
					as ExtSDesc;
				if (sdsc != null)
				{
					if (Bnfo.Package == sdsc.Package)
					{
						try
						{
							sdsc.CharacterDescription.Career = MetaData
								.Careers
								.Unemployed;
							sdsc.CharacterDescription.CareerLevel = 0;
							sdsc.Business.Assignment = JobAssignment.Nothing;
							sdsc.Business.LotID = 0;
							sdsc.Business.Salary = 0;
							sdsc.Business.Flags = 0;
							sdsc.SynchronizeUserData();
							lbadvice.Text =
								sdsc.SimName
								+ "'s SDSC was changed, be sure to Commit now";
						}
						catch
						{
							lbadvice.Text =
								"You will need to find and make changes to "
								+ sdsc.SimName
								+ "'s SDSC file";
						}
					}
					else
					{
						lbadvice.Text =
							"You will need to find and make changes to "
							+ sdsc.SimName
							+ "'s SDSC file";
					}

					lbadvice.Visible = true;
				}
			}

			if (Bnfo.EmployeeCount < 2)
			{
				Bnfo.EmployeeCount = 0;
			}
			else
			{
				try
				{
					ushort[] empls = new ushort[Bnfo.EmployeeCount - 1];
					int[] pr = new int[Bnfo.EmployeeCount - 1];
					uint[] a = new uint[Bnfo.EmployeeCount - 1];
					int j = 0;
					for (int i = 0; i < Bnfo.EmployeeCount; i++)
					{
						if (i != indects)
						{
							empls[j] = Bnfo.Employees[i];
							pr[j] = Bnfo.PayRate[i];
							a[j] = Bnfo.A[i];
							j++;
						}
					}

					Bnfo.EmployeeCount--;

					for (int i = 0; i < Bnfo.EmployeeCount; i++)
					{
						Bnfo.Employees[i] = empls[i];
						Bnfo.PayRate[i] = pr[i];
						Bnfo.A[i] = a[i];
					}
				}
				catch { }
			}
			panel2_clear();
			GetEmployees();
		}

		private void panel2_clear()
		{
			this.CanCommit = true;
			PayBar(3);
			tbassi.Text =
				tbwages.Text =
				tbsgender.Text =
				ybsimage.Text =
				tbLeft.Text =
				tbright.Text =
				tbunknown.Text =
					"";
			lvEmployees.Clear();
			ilist.Images.Clear();
		}

		private void btaddim_Click(object sender, EventArgs e)
		{
			if (cbsimselect.Items.Count < 1)
			{
				cbsimselect.Items.Clear();
				cbsimselect.Sorted = false;
				foreach (
					ExtSDesc sdsc in FileTable
						.ProviderRegistry
						.SimDescriptionProvider
						.SimInstance
						.Values
				)
				{
					if (canhire(sdsc))
					{
						Interfaces.IAlias a = new StaticAlias(
							sdsc.SimId,
							sdsc.SimName + " " + sdsc.SimFamilyName,
							new object[] { sdsc }
						);
						this.cbsimselect.Items.Add(a);
					}
				}
				cbsimselect.Sorted = true;
			}
			biWorkers.Enabled = false;
			Panel3.Visible = true;
		}

		private void btchcancel_Click(object sender, EventArgs e)
		{
			Panel3.Visible = false;
			biWorkers.Enabled = true;
		}

		private void btchadd_Click(object sender, EventArgs e)
		{
			if (cbsimselect.SelectedItem == null)
			{
				return;
			}

			Interfaces.IAlias a =
				cbsimselect.SelectedItem as Interfaces.IAlias;
			ExtSDesc s =
				a.Tag[0] as ExtSDesc;
			if (s != null)
			{
				ushort[] empls = new ushort[Bnfo.EmployeeCount + 1];
				int[] pr = new int[Bnfo.EmployeeCount + 1];
				uint[] tit = new uint[Bnfo.EmployeeCount + 1];
				int bpy = 15;
				bpy += s.Skills.Body / 100;
				bpy += s.Skills.Charisma / 100;
				bpy += s.Skills.Cleaning / 100;
				bpy += s.Skills.Cooking / 100;
				bpy += s.Skills.Creativity / 100;
				bpy += s.Skills.Logic / 100;
				bpy += s.Skills.Mechanical / 100;
				bpy += s.Skills.Art / 100;
				bpy += s.Skills.Music / 100;
				for (int i = 0; i < Bnfo.EmployeeCount; i++)
				{
					empls[i] = Bnfo.Employees[i];
					pr[i] = Bnfo.PayRate[i];
				}
				empls[Bnfo.EmployeeCount] = s.Instance;
				pr[Bnfo.EmployeeCount] = 3;
				tit[Bnfo.EmployeeCount] = (uint)bpy;

				Bnfo.EmployeeCount++;
				Bnfo.Employees = empls;
				Bnfo.PayRate = pr;

				if (Bnfo.Package == s.Package)
				{
					try
					{
						s.CharacterDescription.Career = MetaData.Careers.OwnedBuss;
						s.CharacterDescription.CareerLevel = 1;
						s.Business.Assignment = JobAssignment.Nothing;
						s.Business.LotID = (ushort)Bnfo.FileDescriptor.Instance;
						s.Business.Salary = (ushort)bpy;
						s.Business.Flags = 0;
						s.SynchronizeUserData();
						lbadvice.Text =
							s.SimName + "'s SDSC was changed, be sure to Commit now";
					}
					catch
					{
						lbadvice.Text =
							"You will need to find and make changes to "
							+ s.SimName
							+ "'s SDSC file";
					}
				}
				else
				{
					lbadvice.Text =
						"You will need to find and make changes to "
						+ s.SimName
						+ "'s SDSC file";
				}

				biWorkers.Enabled = true;
				lbadvice.Visible = true;
				Panel3.Visible = false;
				panel2_clear();
				GetEmployees();
			}
		}

		private void cbsimselect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (cbsimselect.SelectedItem == null)
			{
				return;
			}

			try
			{
				Interfaces.IAlias a =
					cbsimselect.SelectedItem as Interfaces.IAlias;
				ExtSDesc s =
					a.Tag[0] as ExtSDesc;
				if (s != null)
				{
					tbchage.Text = Enum.GetName(
						typeof(MetaData.LifeSections),
						(ushort)s.CharacterDescription.LifeSection
					);
					if (s.CharacterDescription.Gender == Data.MetaData.Gender.Female)
					{
						tbchgender.Text = "Female";
					}
					else
					{
						tbchgender.Text = "Male";
					}
				}
				else
				{
					tbchage.Text = tbchgender.Text = "";
				}
			}
			catch { }
		}

		private bool canhire(ExtSDesc sdsc)
		{
			foreach (ushort employee in Bnfo.Employees)
			{
				if (sdsc.Instance == employee)
				{
					return false; // already employee
				}
			}

			if (sdsc.CharacterDescription.Realage < 16)
			{
				return false; // younger than teen
			}

			if (sdsc.University.OnCampus == 1)
			{
				return false; // young adult
			}

			if (sdsc.CharacterDescription.GhostFlag.IsGhost)
			{
				return false; // Too dead to work
			}

			if (sdsc.FamilyInstance == famly)
			{
				return false; // same family as owner
			}

			if (sdsc.FamilyInstance == 0)
			{
				return false;
			}

			if (sdsc.FamilyInstance == 0x7FFF)
			{
				return false; // service sim
			}

			if (sdsc.FamilyInstance == 0x7FFD)
			{
				return false; // orphans
			}

			if (sdsc.FamilyInstance == 0x7FE4)
			{
				return false; // Iconic Hobby Sim
			}

			if (sdsc.FamilyInstance == 0x7FF1)
			{
				return false; // Tropical Locals
			}

			if (sdsc.FamilyInstance == 0x7FF2)
			{
				return false; // Mountain Locals
			}

			if (sdsc.FamilyInstance == 0x7FF3)
			{
				return false; // Asian Locals
			}

			if (sdsc.FamilyInstance == 0x7f65)
			{
				return false; // West World Locals
			}

			if (sdsc.FamilyInstance == 0x7f66)
			{
				return false; // Natives (castaway)
			}

			if (sdsc.FamilyInstance == 0x7f67)
			{
				return false; // Tau Ceti Locals
			}

			if (sdsc.FamilyInstance == 0x7f68)
			{
				return false; // Alpine Locals
			}

			if (sdsc.IsNPC)
			{
				return false; // NPC unique
			}
			// not if is NPC repoter - those are in service sim family and already excluded anyway
			return sdsc.Nightlife.IsHuman; // no pets
		}

		private bool AllValid()
		{
			if (lv.Items == null)
			{
				return true;
			}

			foreach (BnfoCustomerItem item in lv.Items)
			{
				if (item.SimDescription == null)
				{
					return false;
				}
			}

			return true;
		}

		private void btchngeOwn_Click(object sender, EventArgs e)
		{
			if (cbOsimselect.Items.Count < 1)
			{
				cbOsimselect.Items.Clear();
				cbOsimselect.Sorted = false;
				foreach (
					ExtSDesc sdsc in FileTable
						.ProviderRegistry
						.SimDescriptionProvider
						.SimInstance
						.Values
				)
				{
					if (canownim(sdsc))
					{
						Interfaces.IAlias a = new StaticAlias(
							sdsc.SimId,
							sdsc.SimName + " " + sdsc.SimFamilyName,
							new object[] { sdsc }
						);
						this.cbOsimselect.Items.Add(a);
					}
				}
				cbOsimselect.Sorted = true;
			}
			biWorkers.Enabled = false;
			Panel4.Visible = true;
		}

		private void cbOsimselect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (intern)
			{
				return;
			}

			if (cbOsimselect.SelectedItem == null)
			{
				return;
			}

			try
			{
				Interfaces.IAlias a =
					cbOsimselect.SelectedItem as Interfaces.IAlias;
				ExtSDesc s =
					a.Tag[0] as ExtSDesc;
				if (s != null)
				{
					tbOchage.Text = Enum.GetName(
						typeof(MetaData.LifeSections),
						(ushort)s.CharacterDescription.LifeSection
					);
					if (s.CharacterDescription.Gender == Data.MetaData.Gender.Female)
					{
						tbOgender.Text = "Female";
					}
					else
					{
						tbOgender.Text = "Male";
					}
				}
				else
				{
					tbOchage.Text = tbOgender.Text = "";
				}
			}
			catch { }
		}

		private void btOcancel_Click(object sender, EventArgs e)
		{
			Panel4.Visible = false;
			biWorkers.Enabled = true;
		}

		private void btOadd_Click(object sender, EventArgs e)
		{
			if (cbOsimselect.SelectedItem == null)
			{
				return;
			}

			Interfaces.IAlias a =
				cbOsimselect.SelectedItem as Interfaces.IAlias;
			ExtSDesc s =
				a.Tag[0] as ExtSDesc;
			if (s != null)
			{
				if (famly > 0 && homeb == 0 && s.FamilyInstance != famly)
				{
					lbadvice.Text =
						"Only a family member can be the owner of a home business";
					lbadvice.Visible = true;
				}
				else
				{
					Interfaces.Files.IPackedFileDescriptor pfd =
						Bnfo.Package.FindFile(
							0x0BF999E7,
							0,
							0xFFFFFFFF,
							Bnfo.FileDescriptor.Instance
						);
					if (pfd != null)
					{
						Ltxt ltx = new Ltxt();
						ltx.ProcessData(pfd, Bnfo.Package);
						ltx.OwnerInstance = (uint)s.Instance;
						ltx.SynchronizeUserData();
					}
					owner = s.Instance;
				}

				biWorkers.Enabled = true;
				Panel4.Visible = false;
				panel2_clear();
				GetEmployees();
			}
		}

		private bool canownim(ExtSDesc sdsc)
		{
			foreach (ushort employee in Bnfo.Employees)
			{
				if (sdsc.Instance == employee)
				{
					return false; // an employee
				}
			}

			if (sdsc.Instance == owner)
			{
				return false; // current Owner
			}

			if (sdsc.CharacterDescription.Realage < 16)
			{
				return false; // younger than teen
			}

			if (sdsc.CharacterDescription.GhostFlag.IsGhost)
			{
				return false; // Too dead to work
			}

			if (sdsc.FamilyInstance == 0 || sdsc.FamilyInstance > 0x7F00)
			{
				return false; // non Playable
			}

			return sdsc.Nightlife.IsHuman; // no pets
		}

		private Packages.File fixlowercase(string filyname)
		{
			if (System.IO.File.Exists(filyname))
			{
				return SimPe.Packages.File.LoadFromFile(filyname);
			}
			return null;
			/*
			 * SimPe often internally changes filenames to lower case and if re-opening a recently opened file
			 * it can load it from memory causing the lower case filename to remain, this causes it to re-open
			 * the file from disk to refresh the filename
			*/
		}
	}
}
