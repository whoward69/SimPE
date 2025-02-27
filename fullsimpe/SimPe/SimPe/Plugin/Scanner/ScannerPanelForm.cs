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

namespace SimPe.Plugin.Scanner
{
	/// <summary>
	/// Summary description for ScannerPanelForm.
	/// </summary>
	internal class ScannerPanelForm : Form
	{
		private TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		internal Panel pncloth;
		private Label label1;
		private Label label2;
		private CheckBox cbswim;
		private CheckBox cbact;
		private CheckBox cbskin;
		private CheckBox cbformal;
		private CheckBox cbpreg;
		private CheckBox cbundies;
		private CheckBox cbpj;
		private CheckBox cbevery;
		private CheckBox cbelder;
		private CheckBox cbadult;
		private CheckBox cbyoung;
		private CheckBox cbteen;
		private CheckBox cbchild;
		private CheckBox cbtoddler;
		private CheckBox cbbaby;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		internal CheckBox[] cbages = new CheckBox[7];
		internal CheckBox[] cbsexes = new CheckBox[2];
		private LinkLabel llsetage;
		private LinkLabel llsetcat;
		private System.Windows.Forms.TabPage tabPage2;
		internal Panel pnep;
		private LinkLabel visualStyleLinkLabel1;
		private TextBox tbname;
		private Label label3;
		private System.Windows.Forms.TabPage tabPage3;
		internal Panel pnskin;
		private Label label4;
		private LinkLabel visualStyleLinkLabel2;
		private ComboBox cbskins;
		private SaveFileDialog sfd;
		private CheckBox cbtxmt;
		private CheckBox cbtxtr;
		private CheckBox cbref;
		private System.Windows.Forms.TabPage tabPage4;
		internal Panel pnShelve;
		private Label label5;
		internal Ambertation.Windows.Forms.EnumComboBox cbshelve;
		private LinkLabel llShelve;
		private CheckBox cbout;
		private LinkLabel llsetsex;
		private Label label6;
		private CheckBox cbmale;
		private CheckBox cbfemale;
		internal CheckBox[] cbcategories = new CheckBox[9];

		public ScannerPanelForm()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();

			cbages[0] = cbbaby;
			cbbaby.Tag = Data.Ages.Baby;
			cbages[1] = cbtoddler;
			cbtoddler.Tag = Data.Ages.Toddler;
			cbages[2] = cbchild;
			cbchild.Tag = Data.Ages.Child;
			cbages[3] = cbteen;
			cbteen.Tag = Data.Ages.Teen;
			cbages[4] = cbyoung;
			cbyoung.Tag = Data.Ages.YoungAdult;
			cbages[5] = cbadult;
			cbadult.Tag = Data.Ages.Adult;
			cbages[6] = cbelder;
			cbelder.Tag = Data.Ages.Elder;

			cbcategories[0] = cbact;
			cbact.Tag = Data.OutfitCats.Gym;
			cbcategories[1] = cbevery;
			cbevery.Tag = Data.OutfitCats.Everyday;
			cbcategories[2] = cbformal;
			cbformal.Tag = Data.OutfitCats.Formal;
			cbcategories[3] = cbpj;
			cbpj.Tag = Data.OutfitCats.Pyjamas;
			cbcategories[4] = cbpreg;
			cbpreg.Tag = Data.OutfitCats.Maternity;
			cbcategories[5] = cbskin;
			cbskin.Tag = Data.OutfitCats.Skin;
			cbcategories[6] = cbswim;
			cbswim.Tag = Data.OutfitCats.Swimsuit;
			cbcategories[7] = cbundies;
			cbundies.Tag = Data.OutfitCats.Underwear;
			cbcategories[8] = cbout;
			cbout.Tag = Data.OutfitCats.WinterWear;

			cbsexes[0] = cbmale;
			cbmale.Tag = Data.Sex.Male;
			cbsexes[1] = cbfemale;
			cbfemale.Tag = Data.Sex.Female;

			if (Helper.WindowsRegistry.Username.Trim() != "")
			{
				this.tbname.Text = Helper.WindowsRegistry.Username + "-";
			}

			this.cbskins.SelectedIndex = 0;
			sfd.InitialDirectory = PathProvider.SimSavegameFolder;

			cbshelve.Enum = typeof(PackedFiles.Wrapper.ShelveDimension);
			cbshelve.ResourceManager = Localization.Manager;
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

		static ScannerPanelForm form;
		public static ScannerPanelForm Form
		{
			get
			{
				if (form == null)
				{
					form = new ScannerPanelForm();
				}

				return form;
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.pnep = new Panel();
			this.tbname = new TextBox();
			this.label3 = new Label();
			this.visualStyleLinkLabel1 = new LinkLabel();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.pncloth = new Panel();
			this.label6 = new Label();
			this.cbmale = new CheckBox();
			this.cbfemale = new CheckBox();
			this.llsetsex = new LinkLabel();
			this.cbout = new CheckBox();
			this.llsetcat = new LinkLabel();
			this.llsetage = new LinkLabel();
			this.cbswim = new CheckBox();
			this.cbact = new CheckBox();
			this.cbskin = new CheckBox();
			this.cbformal = new CheckBox();
			this.cbpreg = new CheckBox();
			this.cbundies = new CheckBox();
			this.cbpj = new CheckBox();
			this.cbevery = new CheckBox();
			this.cbelder = new CheckBox();
			this.cbadult = new CheckBox();
			this.cbyoung = new CheckBox();
			this.cbteen = new CheckBox();
			this.cbchild = new CheckBox();
			this.cbtoddler = new CheckBox();
			this.cbbaby = new CheckBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.pnShelve = new Panel();
			this.cbshelve = new Ambertation.Windows.Forms.EnumComboBox();
			this.label5 = new Label();
			this.llShelve = new LinkLabel();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.pnskin = new Panel();
			this.cbtxtr = new CheckBox();
			this.cbtxmt = new CheckBox();
			this.cbskins = new ComboBox();
			this.label4 = new Label();
			this.visualStyleLinkLabel2 = new LinkLabel();
			this.cbref = new CheckBox();
			this.sfd = new SaveFileDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.pnep.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.pncloth.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.pnShelve.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.pnskin.SuspendLayout();
			this.SuspendLayout();
			//
			// tabControl1
			//
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(432, 284);
			this.tabControl1.TabIndex = 0;
			//
			// tabPage2
			//
			this.tabPage2.Controls.Add(this.pnep);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(424, 258);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "EP-Ready?";
			//
			// pnep
			//
			this.pnep.Controls.Add(this.tbname);
			this.pnep.Controls.Add(this.label3);
			this.pnep.Controls.Add(this.visualStyleLinkLabel1);
			this.pnep.Location = new System.Drawing.Point(24, 8);
			this.pnep.Name = "pnep";
			this.pnep.Size = new System.Drawing.Size(289, 72);
			this.pnep.TabIndex = 0;
			//
			// tbname
			//
			this.tbname.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.tbname.Location = new System.Drawing.Point(23, 24);
			this.tbname.Name = "tbname";
			this.tbname.Size = new System.Drawing.Size(190, 21);
			this.tbname.TabIndex = 40;
			this.tbname.Text = "SimPe-";
			//
			// label3
			//
			this.label3.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label3.Location = new System.Drawing.Point(7, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 39;
			this.label3.Text = "Name Prefix:";
			//
			// visualStyleLinkLabel1
			//
			this.visualStyleLinkLabel1.AutoSize = true;
			this.visualStyleLinkLabel1.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.visualStyleLinkLabel1.Location = new System.Drawing.Point(0, 56);
			this.visualStyleLinkLabel1.Name = "visualStyleLinkLabel1";
			this.visualStyleLinkLabel1.Size = new System.Drawing.Size(160, 13);
			this.visualStyleLinkLabel1.TabIndex = 38;
			this.visualStyleLinkLabel1.TabStop = true;
			this.visualStyleLinkLabel1.Text = "make University-Ready";
			this.visualStyleLinkLabel1.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.MakeEPReady
				);
			//
			// tabPage1
			//
			this.tabPage1.Controls.Add(this.pncloth);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(424, 258);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Clothes";
			//
			// pncloth
			//
			this.pncloth.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.pncloth.Controls.Add(this.label6);
			this.pncloth.Controls.Add(this.cbmale);
			this.pncloth.Controls.Add(this.cbfemale);
			this.pncloth.Controls.Add(this.llsetsex);
			this.pncloth.Controls.Add(this.cbout);
			this.pncloth.Controls.Add(this.llsetcat);
			this.pncloth.Controls.Add(this.llsetage);
			this.pncloth.Controls.Add(this.cbswim);
			this.pncloth.Controls.Add(this.cbact);
			this.pncloth.Controls.Add(this.cbskin);
			this.pncloth.Controls.Add(this.cbformal);
			this.pncloth.Controls.Add(this.cbpreg);
			this.pncloth.Controls.Add(this.cbundies);
			this.pncloth.Controls.Add(this.cbpj);
			this.pncloth.Controls.Add(this.cbevery);
			this.pncloth.Controls.Add(this.cbelder);
			this.pncloth.Controls.Add(this.cbadult);
			this.pncloth.Controls.Add(this.cbyoung);
			this.pncloth.Controls.Add(this.cbteen);
			this.pncloth.Controls.Add(this.cbchild);
			this.pncloth.Controls.Add(this.cbtoddler);
			this.pncloth.Controls.Add(this.cbbaby);
			this.pncloth.Controls.Add(this.label2);
			this.pncloth.Controls.Add(this.label1);
			this.pncloth.Location = new System.Drawing.Point(24, 8);
			this.pncloth.Name = "pncloth";
			this.pncloth.Size = new System.Drawing.Size(386, 184);
			this.pncloth.TabIndex = 0;
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label6.Location = new System.Drawing.Point(24, 139);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(58, 13);
			this.label6.TabIndex = 43;
			this.label6.Text = "Gender:";
			//
			// cbmale
			//
			this.cbmale.FlatStyle = FlatStyle.System;
			this.cbmale.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbmale.Location = new System.Drawing.Point(104, 155);
			this.cbmale.Name = "cbmale";
			this.cbmale.Size = new System.Drawing.Size(80, 24);
			this.cbmale.TabIndex = 42;
			this.cbmale.Text = "Male";
			//
			// cbfemale
			//
			this.cbfemale.FlatStyle = FlatStyle.System;
			this.cbfemale.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbfemale.Location = new System.Drawing.Point(16, 155);
			this.cbfemale.Name = "cbfemale";
			this.cbfemale.Size = new System.Drawing.Size(80, 24);
			this.cbfemale.TabIndex = 41;
			this.cbfemale.Text = "Female";
			//
			// llsetsex
			//
			this.llsetsex.AutoSize = true;
			this.llsetsex.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.llsetsex.Location = new System.Drawing.Point(0, 139);
			this.llsetsex.Name = "llsetsex";
			this.llsetsex.Size = new System.Drawing.Size(27, 13);
			this.llsetsex.TabIndex = 40;
			this.llsetsex.TabStop = true;
			this.llsetsex.Text = "set";
			this.llsetsex.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(this.setSex);
			//
			// cbout
			//
			this.cbout.FlatStyle = FlatStyle.System;
			this.cbout.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbout.Location = new System.Drawing.Point(256, 112);
			this.cbout.Name = "cbout";
			this.cbout.Size = new System.Drawing.Size(101, 24);
			this.cbout.TabIndex = 39;
			this.cbout.Text = "Winter Wear";
			//
			// llsetcat
			//
			this.llsetcat.AutoSize = true;
			this.llsetcat.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.llsetcat.Location = new System.Drawing.Point(0, 72);
			this.llsetcat.Name = "llsetcat";
			this.llsetcat.Size = new System.Drawing.Size(27, 13);
			this.llsetcat.TabIndex = 38;
			this.llsetcat.TabStop = true;
			this.llsetcat.Text = "set";
			this.llsetcat.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(this.SetCat);
			//
			// llsetage
			//
			this.llsetage.AutoSize = true;
			this.llsetage.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.llsetage.Location = new System.Drawing.Point(0, 8);
			this.llsetage.Name = "llsetage";
			this.llsetage.Size = new System.Drawing.Size(27, 13);
			this.llsetage.TabIndex = 37;
			this.llsetage.TabStop = true;
			this.llsetage.Text = "set";
			this.llsetage.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(this.setAge);
			//
			// cbswim
			//
			this.cbswim.FlatStyle = FlatStyle.System;
			this.cbswim.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbswim.Location = new System.Drawing.Point(16, 112);
			this.cbswim.Name = "cbswim";
			this.cbswim.Size = new System.Drawing.Size(80, 24);
			this.cbswim.TabIndex = 36;
			this.cbswim.Text = "Swim Suit";
			//
			// cbact
			//
			this.cbact.FlatStyle = FlatStyle.System;
			this.cbact.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbact.Location = new System.Drawing.Point(192, 112);
			this.cbact.Name = "cbact";
			this.cbact.Size = new System.Drawing.Size(50, 24);
			this.cbact.TabIndex = 35;
			this.cbact.Text = "Gym";
			this.cbact.CheckedChanged += new EventHandler(
				this.cbact_CheckedChanged
			);
			//
			// cbskin
			//
			this.cbskin.FlatStyle = FlatStyle.System;
			this.cbskin.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbskin.Location = new System.Drawing.Point(256, 136);
			this.cbskin.Name = "cbskin";
			this.cbskin.Size = new System.Drawing.Size(56, 24);
			this.cbskin.TabIndex = 34;
			this.cbskin.Text = "Skin";
			//
			// cbformal
			//
			this.cbformal.FlatStyle = FlatStyle.System;
			this.cbformal.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbformal.Location = new System.Drawing.Point(182, 88);
			this.cbformal.Name = "cbformal";
			this.cbformal.Size = new System.Drawing.Size(64, 24);
			this.cbformal.TabIndex = 33;
			this.cbformal.Text = "Formal";
			//
			// cbpreg
			//
			this.cbpreg.FlatStyle = FlatStyle.System;
			this.cbpreg.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbpreg.Location = new System.Drawing.Point(256, 88);
			this.cbpreg.Name = "cbpreg";
			this.cbpreg.Size = new System.Drawing.Size(75, 24);
			this.cbpreg.TabIndex = 32;
			this.cbpreg.Text = "Maternity";
			//
			// cbundies
			//
			this.cbundies.FlatStyle = FlatStyle.System;
			this.cbundies.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbundies.Location = new System.Drawing.Point(104, 112);
			this.cbundies.Name = "cbundies";
			this.cbundies.Size = new System.Drawing.Size(82, 24);
			this.cbundies.TabIndex = 31;
			this.cbundies.Text = "Underwear";
			//
			// cbpj
			//
			this.cbpj.FlatStyle = FlatStyle.System;
			this.cbpj.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbpj.Location = new System.Drawing.Point(104, 88);
			this.cbpj.Name = "cbpj";
			this.cbpj.Size = new System.Drawing.Size(72, 24);
			this.cbpj.TabIndex = 30;
			this.cbpj.Text = "Pyjamas";
			//
			// cbevery
			//
			this.cbevery.FlatStyle = FlatStyle.System;
			this.cbevery.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbevery.Location = new System.Drawing.Point(16, 88);
			this.cbevery.Name = "cbevery";
			this.cbevery.Size = new System.Drawing.Size(80, 24);
			this.cbevery.TabIndex = 29;
			this.cbevery.Text = "Everyday";
			//
			// cbelder
			//
			this.cbelder.FlatStyle = FlatStyle.System;
			this.cbelder.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbelder.Location = new System.Drawing.Point(182, 48);
			this.cbelder.Name = "cbelder";
			this.cbelder.Size = new System.Drawing.Size(64, 24);
			this.cbelder.TabIndex = 28;
			this.cbelder.Text = "Elder";
			//
			// cbadult
			//
			this.cbadult.FlatStyle = FlatStyle.System;
			this.cbadult.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbadult.Location = new System.Drawing.Point(112, 48);
			this.cbadult.Name = "cbadult";
			this.cbadult.Size = new System.Drawing.Size(64, 24);
			this.cbadult.TabIndex = 27;
			this.cbadult.Text = "Adult";
			//
			// cbyoung
			//
			this.cbyoung.FlatStyle = FlatStyle.System;
			this.cbyoung.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbyoung.Location = new System.Drawing.Point(16, 48);
			this.cbyoung.Name = "cbyoung";
			this.cbyoung.Size = new System.Drawing.Size(88, 24);
			this.cbyoung.TabIndex = 26;
			this.cbyoung.Text = "young Adult";
			//
			// cbteen
			//
			this.cbteen.FlatStyle = FlatStyle.System;
			this.cbteen.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbteen.Location = new System.Drawing.Point(256, 24);
			this.cbteen.Name = "cbteen";
			this.cbteen.Size = new System.Drawing.Size(80, 24);
			this.cbteen.TabIndex = 25;
			this.cbteen.Text = "Teenager";
			//
			// cbchild
			//
			this.cbchild.FlatStyle = FlatStyle.System;
			this.cbchild.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbchild.Location = new System.Drawing.Point(182, 24);
			this.cbchild.Name = "cbchild";
			this.cbchild.Size = new System.Drawing.Size(64, 24);
			this.cbchild.TabIndex = 24;
			this.cbchild.Text = "Child";
			//
			// cbtoddler
			//
			this.cbtoddler.FlatStyle = FlatStyle.System;
			this.cbtoddler.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbtoddler.Location = new System.Drawing.Point(104, 24);
			this.cbtoddler.Name = "cbtoddler";
			this.cbtoddler.Size = new System.Drawing.Size(64, 24);
			this.cbtoddler.TabIndex = 23;
			this.cbtoddler.Text = "Toddler";
			//
			// cbbaby
			//
			this.cbbaby.FlatStyle = FlatStyle.System;
			this.cbbaby.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.cbbaby.Location = new System.Drawing.Point(16, 24);
			this.cbbaby.Name = "cbbaby";
			this.cbbaby.Size = new System.Drawing.Size(64, 24);
			this.cbbaby.TabIndex = 22;
			this.cbbaby.Text = "Baby";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label2.Location = new System.Drawing.Point(24, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Categories:";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label1.Location = new System.Drawing.Point(24, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Ages:";
			//
			// tabPage4
			//
			this.tabPage4.Controls.Add(this.pnShelve);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(424, 258);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "SheleveReady?";
			//
			// pnShelve
			//
			this.pnShelve.Controls.Add(this.cbshelve);
			this.pnShelve.Controls.Add(this.label5);
			this.pnShelve.Controls.Add(this.llShelve);
			this.pnShelve.Location = new System.Drawing.Point(8, 8);
			this.pnShelve.Name = "pnShelve";
			this.pnShelve.Size = new System.Drawing.Size(361, 72);
			this.pnShelve.TabIndex = 1;
			//
			// cbshelve
			//
			this.cbshelve.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.cbshelve.DropDownStyle =
				ComboBoxStyle
				.DropDownList;
			this.cbshelve.Enum = null;
			this.cbshelve.Location = new System.Drawing.Point(16, 24);
			this.cbshelve.Name = "cbshelve";
			this.cbshelve.ResourceManager = null;
			this.cbshelve.Size = new System.Drawing.Size(329, 21);
			this.cbshelve.TabIndex = 40;
			this.cbshelve.SelectedIndexChanged += new EventHandler(
				this.cbshelve_SelectedIndexChanged
			);
			//
			// label5
			//
			this.label5.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label5.Location = new System.Drawing.Point(0, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 16);
			this.label5.TabIndex = 39;
			this.label5.Text = "Dimension:";
			//
			// llShelve
			//
			this.llShelve.AutoSize = true;
			this.llShelve.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.llShelve.Location = new System.Drawing.Point(0, 56);
			this.llShelve.Name = "llShelve";
			this.llShelve.Size = new System.Drawing.Size(147, 13);
			this.llShelve.TabIndex = 38;
			this.llShelve.TabStop = true;
			this.llShelve.Text = "set Shelve Dimension";
			this.llShelve.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.visualStyleLinkLabel3_LinkClicked
				);
			//
			// tabPage3
			//
			this.tabPage3.Controls.Add(this.pnskin);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(424, 258);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Skin";
			//
			// pnskin
			//
			this.pnskin.Controls.Add(this.cbtxtr);
			this.pnskin.Controls.Add(this.cbtxmt);
			this.pnskin.Controls.Add(this.cbskins);
			this.pnskin.Controls.Add(this.label4);
			this.pnskin.Controls.Add(this.visualStyleLinkLabel2);
			this.pnskin.Controls.Add(this.cbref);
			this.pnskin.Location = new System.Drawing.Point(24, 8);
			this.pnskin.Name = "pnskin";
			this.pnskin.Size = new System.Drawing.Size(343, 120);
			this.pnskin.TabIndex = 1;
			//
			// cbtxtr
			//
			this.cbtxtr.Checked = true;
			this.cbtxtr.CheckState = CheckState.Checked;
			this.cbtxtr.FlatStyle = FlatStyle.System;
			this.cbtxtr.Location = new System.Drawing.Point(136, 48);
			this.cbtxtr.Name = "cbtxtr";
			this.cbtxtr.Size = new System.Drawing.Size(104, 24);
			this.cbtxtr.TabIndex = 42;
			this.cbtxtr.Text = "override TXTR";
			//
			// cbtxmt
			//
			this.cbtxmt.Checked = true;
			this.cbtxmt.CheckState = CheckState.Checked;
			this.cbtxmt.FlatStyle = FlatStyle.System;
			this.cbtxmt.Location = new System.Drawing.Point(16, 48);
			this.cbtxmt.Name = "cbtxmt";
			this.cbtxmt.Size = new System.Drawing.Size(112, 24);
			this.cbtxmt.TabIndex = 41;
			this.cbtxmt.Text = "override TXMT";
			//
			// cbskins
			//
			this.cbskins.DropDownStyle =
				ComboBoxStyle
				.DropDownList;
			this.cbskins.Items.AddRange(
				new object[]
				{
					"Light",
					"Normal",
					"Medium",
					"Dark",
					"Alien",
					"Zombie",
					"Mannequin",
					"CAS Mannequin",
					"Vampire",
				}
			);
			this.cbskins.Location = new System.Drawing.Point(16, 24);
			this.cbskins.Name = "cbskins";
			this.cbskins.Size = new System.Drawing.Size(256, 21);
			this.cbskins.TabIndex = 40;
			//
			// label4
			//
			this.label4.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				(
					(System.Drawing.FontStyle)(
						(
							System.Drawing.FontStyle.Bold
							| System.Drawing.FontStyle.Italic
						)
					)
				),
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.label4.Location = new System.Drawing.Point(0, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 39;
			this.label4.Text = "Base Skin:";
			//
			// visualStyleLinkLabel2
			//
			this.visualStyleLinkLabel2.AutoSize = true;
			this.visualStyleLinkLabel2.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Bold,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.visualStyleLinkLabel2.Location = new System.Drawing.Point(0, 96);
			this.visualStyleLinkLabel2.Name = "visualStyleLinkLabel2";
			this.visualStyleLinkLabel2.Size = new System.Drawing.Size(191, 13);
			this.visualStyleLinkLabel2.TabIndex = 38;
			this.visualStyleLinkLabel2.TabStop = true;
			this.visualStyleLinkLabel2.Text = "create default Skin override";
			this.visualStyleLinkLabel2.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.CreateSkinOverride
				);
			//
			// cbref
			//
			this.cbref.FlatStyle = FlatStyle.System;
			this.cbref.Location = new System.Drawing.Point(16, 68);
			this.cbref.Name = "cbref";
			this.cbref.Size = new System.Drawing.Size(136, 24);
			this.cbref.TabIndex = 43;
			this.cbref.Text = "override Reference";
			//
			// sfd
			//
			this.sfd.Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*";
			this.sfd.Title = "Skin Override";
			//
			// ScannerPanelForm
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(736, 357);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font(
				"Verdana",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.Name = "ScannerPanelForm";
			this.Text = "ScannerPanelForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.pnep.ResumeLayout(false);
			this.pnep.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.pncloth.ResumeLayout(false);
			this.pncloth.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.pnShelve.ResumeLayout(false);
			this.pnShelve.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.pnskin.ResumeLayout(false);
			this.pnskin.PerformLayout();
			this.ResumeLayout(false);
		}
		#endregion



		private void SetCat(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			ClothingScanner cs = (ClothingScanner)pncloth.Tag;
			cs.SetCategory();
		}

		private void setAge(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			ClothingScanner cs = (ClothingScanner)pncloth.Tag;
			cs.SetAge();
		}

		private void setSex(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ClothingScanner cs = (ClothingScanner)pncloth.Tag;
			cs.SetSex();
		}

		private void MakeEPReady(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			EPReadyScanner cs = (EPReadyScanner)pnep.Tag;
			cs.Fix(this.tbname.Text);
		}

		private void CreateSkinOverride(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			if (!cbtxtr.Checked && !cbtxmt.Checked && !cbref.Checked)
			{
				MessageBox.Show("Please select at least one Checkbox!");
				return;
			}

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				string skintone = "";
				string family = "";
				if (cbskins.SelectedIndex < 4)
				{
					skintone =
						"0000000"
						+ (cbskins.SelectedIndex + 1).ToString()
						+ "-0000-0000-0000-000000000000";
				}
				else if (cbskins.SelectedIndex == 4)
				{
					skintone = "6baf064a-85ad-4e37-8d81-a987e9f8da46"; //Alien Skin
				}
				else if (cbskins.SelectedIndex == 5)
				{
					skintone = "b6ee1dbc-5bb3-4146-8315-02bd64eda707"; //Zombie Skin
				}
				else if (cbskins.SelectedIndex == 6)
				{
					skintone = "b9a94827-7544-450c-a8f4-6f643ae89a71"; //Mannequin Skin
				}
				else if (cbskins.SelectedIndex == 7)
				{
					skintone = "6eea47c7-8a35-4be7-9242-dcd082f53b55"; //CAS Mannequin Skin
				}
				else if (cbskins.SelectedIndex == 8)
				{
					skintone = "00000000-0000-0000-0000-000000000000"; //Vampire
				}

				if (cbskins.SelectedIndex < 4)
				{
					family = "21afb87c-e872-4f4c-af3c-c3685ed4e220";
				}
				else if (cbskins.SelectedIndex == 4)
				{
					family = "ad5da337-bdd1-4593-acdd-19001595cbbb"; //Alien Skin
				}
				else if (cbskins.SelectedIndex == 5)
				{
					family = "b6ee1dbc-5bb3-4146-8315-02bd64eda707"; //Zombie Skin
				}
				else if (cbskins.SelectedIndex == 6)
				{
					family = "59621330-1005-4b88-b4f2-77deb751fcf3"; //Mannequin Skin
				}
				else if (cbskins.SelectedIndex == 7)
				{
					family = "59621330-1005-4b88-b4f2-77deb751fcf3"; //CAS Mannequin Skin
				}
				else if (cbskins.SelectedIndex == 8)
				{
					family = "13ae91e7-b825-4559-82a3-0ead8e8dd7fd"; //Vampire
				}

				SkinScanner cs = (SkinScanner)pnskin.Tag;
				cs.CreateOverride(
					skintone,
					family,
					sfd.FileName,
					cbtxmt.Checked,
					cbtxtr.Checked,
					cbref.Checked
				);
			}
		}

		private void visualStyleLinkLabel3_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			PackedFiles.Wrapper.ShelveDimension sd =
				(PackedFiles.Wrapper.ShelveDimension)cbshelve.SelectedValue;
			ShelveScanner cs = (ShelveScanner)this.pnShelve.Tag;
			cs.Set(sd);
		}

		private void cbshelve_SelectedIndexChanged(object sender, EventArgs e)
		{
			PackedFiles.Wrapper.ShelveDimension sd =
				(PackedFiles.Wrapper.ShelveDimension)cbshelve.SelectedValue;
			llShelve.Enabled = (
				sd != PackedFiles.Wrapper.ShelveDimension.Indetermined
				&& sd != PackedFiles.Wrapper.ShelveDimension.Multitile
				&& sd != PackedFiles.Wrapper.ShelveDimension.Unknown1
				&& sd != PackedFiles.Wrapper.ShelveDimension.Unknown2
			);
		}

		private void cbact_CheckedChanged(object sender, EventArgs e)
		{
		}
	}
}
