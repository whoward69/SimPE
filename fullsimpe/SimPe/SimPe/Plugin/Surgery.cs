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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for Sims.
	/// </summary>
	public class Surgery : Form
	{
		private ImageList ilist;
		private ListView lv;
		private Button button1;
		private Label label1;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private PictureBox pbpatient;
		private PictureBox pbarche;
		private LinkLabel llusepatient;
		private LinkLabel llusearche;
		private LinkLabel llexport;
		private SaveFileDialog sfd;
		private ToolTip toolTip1;
		private CheckBox cbskin;
		private ListView lvskin;
		private ImageList iskin;
		private CheckBox cbface;
		private CheckBox cbmakeup;
		private CheckBox cbeye;
		private LinkLabel ctlLoadPackage;
		private OpenFileDialog opd;
		private Label lbUbi;
		private CheckBox cbgals;
		private CheckBox cbmens;
		private CheckBox cbadults;
		private PropertyGrid pgPatientDetails;
		private PropertyGrid pgArchetypeDetails;
		internal CheckBox cbTownie;
		internal CheckBox cbNpc;
		private IContainer components;

		public Surgery()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();

			LoadArchetyp();
			this.cbTownie.Checked = ShowTownies;
			this.cbNpc.Checked = ShowNPCs;
			this.cbadults.Checked = AdultsOnly;
			this.cbgals.Checked = JustGals;
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
				new ComponentResourceManager(typeof(Surgery));
			this.ilist = new ImageList(this.components);
			this.lv = new ListView();
			this.button1 = new Button();
			this.label1 = new Label();
			this.lbUbi = new Label();
			this.groupBox1 = new GroupBox();
			this.pgPatientDetails = new PropertyGrid();
			this.cbeye = new CheckBox();
			this.cbmakeup = new CheckBox();
			this.llexport = new LinkLabel();
			this.pbpatient = new PictureBox();
			this.llusepatient = new LinkLabel();
			this.cbface = new CheckBox();
			this.groupBox2 = new GroupBox();
			this.pgArchetypeDetails = new PropertyGrid();
			this.ctlLoadPackage = new LinkLabel();
			this.llusearche = new LinkLabel();
			this.pbarche = new PictureBox();
			this.sfd = new SaveFileDialog();
			this.toolTip1 = new ToolTip(this.components);
			this.cbskin = new CheckBox();
			this.groupBox3 = new GroupBox();
			this.lvskin = new ListView();
			this.iskin = new ImageList(this.components);
			this.opd = new OpenFileDialog();
			this.cbgals = new CheckBox();
			this.cbmens = new CheckBox();
			this.cbadults = new CheckBox();
			this.cbTownie = new CheckBox();
			this.cbNpc = new CheckBox();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)(this.pbpatient)).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)(this.pbarche)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			//
			// ilist
			//
			this.ilist.ColorDepth = ColorDepth.Depth32Bit;
			resources.ApplyResources(this.ilist, "ilist");
			this.ilist.TransparentColor = Color.Transparent;
			//
			// lv
			//
			resources.ApplyResources(this.lv, "lv");
			this.lv.BorderStyle = BorderStyle.None;
			this.lv.HideSelection = false;
			this.lv.LargeImageList = this.ilist;
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.SmallImageList = this.ilist;
			this.lv.Sorting = SortOrder.Ascending;
			this.lv.StateImageList = this.ilist;
			this.toolTip1.SetToolTip(this.lv, resources.GetString("lv.ToolTip"));
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.SelectedIndexChanged += new EventHandler(this.SelectSim);
			this.lv.DoubleClick += new EventHandler(this.Open);
			//
			// button1
			//
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.toolTip1.SetToolTip(
				this.button1,
				resources.GetString("button1.ToolTip")
			);
			this.button1.Click += new EventHandler(this.Open);
			//
			// label1
			//
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			//
			// lbUbi
			//
			this.lbUbi.BackColor = SystemColors.Window;
			resources.ApplyResources(this.lbUbi, "lbUbi");
			this.lbUbi.ForeColor = Color.Brown;
			this.lbUbi.Name = "lbUbi";
			//
			// groupBox1
			//
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.pgPatientDetails);
			this.groupBox1.Controls.Add(this.cbeye);
			this.groupBox1.Controls.Add(this.cbmakeup);
			this.groupBox1.Controls.Add(this.llexport);
			this.groupBox1.Controls.Add(this.pbpatient);
			this.groupBox1.Controls.Add(this.llusepatient);
			this.groupBox1.Controls.Add(this.cbface);
			this.groupBox1.Name = "groupBox1";
			//
			// pgPatientDetails
			//
			resources.ApplyResources(this.pgPatientDetails, "pgPatientDetails");
			this.pgPatientDetails.Name = "pgPatientDetails";
			this.pgPatientDetails.PropertySort =
				PropertySort
				.Categorized;
			this.pgPatientDetails.ToolbarVisible = false;
			//
			// cbeye
			//
			resources.ApplyResources(this.cbeye, "cbeye");
			this.cbeye.Name = "cbeye";
			this.toolTip1.SetToolTip(this.cbeye, resources.GetString("cbeye.ToolTip"));
			this.cbeye.CheckedChanged += new EventHandler(
				this.cbskin_CheckedChanged
			);
			//
			// cbmakeup
			//
			resources.ApplyResources(this.cbmakeup, "cbmakeup");
			this.cbmakeup.Name = "cbmakeup";
			this.toolTip1.SetToolTip(
				this.cbmakeup,
				resources.GetString("cbmakeup.ToolTip")
			);
			this.cbmakeup.CheckedChanged += new EventHandler(
				this.cbskin_CheckedChanged
			);
			//
			// llexport
			//
			resources.ApplyResources(this.llexport, "llexport");
			this.llexport.Name = "llexport";
			this.llexport.TabStop = true;
			this.toolTip1.SetToolTip(
				this.llexport,
				resources.GetString("llexport.ToolTip")
			);
			this.llexport.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(this.Export);
			//
			// pbpatient
			//
			this.pbpatient.BorderStyle = BorderStyle.FixedSingle;
			resources.ApplyResources(this.pbpatient, "pbpatient");
			this.pbpatient.Name = "pbpatient";
			this.pbpatient.TabStop = false;
			//
			// llusepatient
			//
			resources.ApplyResources(this.llusepatient, "llusepatient");
			this.llusepatient.Name = "llusepatient";
			this.llusepatient.TabStop = true;
			this.toolTip1.SetToolTip(
				this.llusepatient,
				resources.GetString("llusepatient.ToolTip")
			);
			this.llusepatient.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.UsePatient
				);
			//
			// cbface
			//
			resources.ApplyResources(this.cbface, "cbface");
			this.cbface.ForeColor = SystemColors.ControlText;
			this.cbface.Name = "cbface";
			this.toolTip1.SetToolTip(
				this.cbface,
				resources.GetString("cbface.ToolTip")
			);
			this.cbface.CheckedChanged += new EventHandler(
				this.cbskin_CheckedChanged
			);
			//
			// groupBox2
			//
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.pgArchetypeDetails);
			this.groupBox2.Controls.Add(this.ctlLoadPackage);
			this.groupBox2.Controls.Add(this.llusearche);
			this.groupBox2.Controls.Add(this.pbarche);
			this.groupBox2.Name = "groupBox2";
			//
			// pgArchetypeDetails
			//
			resources.ApplyResources(this.pgArchetypeDetails, "pgArchetypeDetails");
			this.pgArchetypeDetails.Name = "pgArchetypeDetails";
			this.pgArchetypeDetails.PropertySort =
				PropertySort
				.Categorized;
			this.pgArchetypeDetails.ToolbarVisible = false;
			//
			// ctlLoadPackage
			//
			resources.ApplyResources(this.ctlLoadPackage, "ctlLoadPackage");
			this.ctlLoadPackage.Name = "ctlLoadPackage";
			this.ctlLoadPackage.TabStop = true;
			this.toolTip1.SetToolTip(
				this.ctlLoadPackage,
				resources.GetString("ctlLoadPackage.ToolTip")
			);
			this.ctlLoadPackage.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.ctlLoadPackage_LinkClicked
				);
			//
			// llusearche
			//
			resources.ApplyResources(this.llusearche, "llusearche");
			this.llusearche.Name = "llusearche";
			this.llusearche.TabStop = true;
			this.toolTip1.SetToolTip(
				this.llusearche,
				resources.GetString("llusearche.ToolTip")
			);
			this.llusearche.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.UseArchetype
				);
			//
			// pbarche
			//
			resources.ApplyResources(this.pbarche, "pbarche");
			this.pbarche.BorderStyle = BorderStyle.FixedSingle;
			this.pbarche.Name = "pbarche";
			this.pbarche.TabStop = false;
			//
			// sfd
			//
			resources.ApplyResources(this.sfd, "sfd");
			//
			// toolTip1
			//
			this.toolTip1.AutoPopDelay = 30000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			//
			// cbskin
			//
			resources.ApplyResources(this.cbskin, "cbskin");
			this.cbskin.Name = "cbskin";
			this.toolTip1.SetToolTip(
				this.cbskin,
				resources.GetString("cbskin.ToolTip")
			);
			this.cbskin.CheckedChanged += new EventHandler(
				this.cbskin_CheckedChanged
			);
			//
			// groupBox3
			//
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.cbskin);
			this.groupBox3.Controls.Add(this.lvskin);
			this.groupBox3.Name = "groupBox3";
			//
			// lvskin
			//
			resources.ApplyResources(this.lvskin, "lvskin");
			this.lvskin.BorderStyle = BorderStyle.None;
			this.lvskin.HideSelection = false;
			this.lvskin.LargeImageList = this.iskin;
			this.lvskin.MultiSelect = false;
			this.lvskin.Name = "lvskin";
			this.lvskin.UseCompatibleStateImageBehavior = false;
			this.lvskin.SelectedIndexChanged += new EventHandler(
				this.lvskin_SelectedIndexChanged
			);
			//
			// iskin
			//
			this.iskin.ColorDepth = ColorDepth.Depth32Bit;
			resources.ApplyResources(this.iskin, "iskin");
			this.iskin.TransparentColor = Color.Transparent;
			//
			// opd
			//
			this.opd.DefaultExt = "package";
			this.opd.FileOk += new CancelEventHandler(
				this.opd_FileOk
			);
			//
			// cbgals
			//
			resources.ApplyResources(this.cbgals, "cbgals");
			this.cbgals.Name = "cbgals";
			this.cbgals.UseVisualStyleBackColor = true;
			this.cbgals.CheckedChanged += new EventHandler(
				this.genderFilter_CheckedChanged
			);
			//
			// cbmens
			//
			resources.ApplyResources(this.cbmens, "cbmens");
			this.cbmens.Name = "cbmens";
			this.cbmens.UseVisualStyleBackColor = true;
			this.cbmens.CheckedChanged += new EventHandler(
				this.genderFilter_CheckedChanged
			);
			//
			// cbadults
			//
			resources.ApplyResources(this.cbadults, "cbadults");
			this.cbadults.Name = "cbadults";
			this.cbadults.UseVisualStyleBackColor = true;
			this.cbadults.CheckedChanged += new EventHandler(
				this.genderFilter_CheckedChanged
			);
			//
			// cbTownie
			//
			resources.ApplyResources(this.cbTownie, "cbTownie");
			this.cbTownie.Name = "cbTownie";
			this.cbTownie.CheckedChanged += new EventHandler(
				this.genderFilter_CheckedChanged
			);
			//
			// cbNpc
			//
			resources.ApplyResources(this.cbNpc, "cbNpc");
			this.cbNpc.Name = "cbNpc";
			this.cbNpc.CheckedChanged += new EventHandler(
				this.genderFilter_CheckedChanged
			);
			//
			// Surgery
			//
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.lbUbi);
			this.Controls.Add(this.cbTownie);
			this.Controls.Add(this.cbNpc);
			this.Controls.Add(this.cbgals);
			this.Controls.Add(this.cbmens);
			this.Controls.Add(this.cbadults);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox3);
			this.FormBorderStyle =
				FormBorderStyle
				.SizableToolWindow;
			this.Name = "Surgery";
			this.SizeGripStyle = SizeGripStyle.Show;
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)(this.pbpatient)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)(this.pbarche)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		protected void AddImage(PackedFiles.Wrapper.ExtSDesc sdesc)
		{
			if (sdesc.HasImage) // if (sdesc.Image != null) -Chris H
			{
				this.ilist.Images.Add(sdesc.Image);
			}
			else
			{
				this.ilist.Images.Add(new Bitmap(GetImage.NoOne));
			}
		}

		private bool realIsNPC(PackedFiles.Wrapper.ExtSDesc sdesc)
		{
			return sdesc.FamilyInstance == 0x7fff;
		}

		private bool realIsTownie(PackedFiles.Wrapper.ExtSDesc sdesc)
		{
			return sdesc.FamilyInstance < 0x7fff && sdesc.FamilyInstance >= 0x7f00;
		}

		protected void AddSim(PackedFiles.Wrapper.ExtSDesc sdesc)
		{
			if (!sdesc.AvailableCharacterData)
			{
				return;
			}

			if (sdesc.Nightlife.Species > 0)
			{
				return;
			}

			if (sdesc.IsNPC)
			{
				return;
			}

			if (
				sdesc.CharacterDescription.ServiceTypes
				== Data.MetaData.ServiceTypes.TinySim
			)
			{
				return;
			}

			if (
				(int)sdesc.Version
					== (int)PackedFiles.Wrapper.SDescVersions.Castaway
				&& sdesc.Castaway.Subspecies > 0
			)
			{
				return;
			}

			if (!this.cbNpc.Checked && realIsNPC(sdesc))
			{
				return;
			}

			if (!this.cbTownie.Checked && realIsTownie(sdesc))
			{
				return;
			}

			if (
				this.cbadults.Checked
				&& sdesc.CharacterDescription.LifeSection
					!= Data.MetaData.LifeSections.Adult
			)
			{
				return;
			}

			if (
				this.cbmens.Checked
				&& sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Female
			)
			{
				return;
			}

			if (
				this.cbgals.Checked
				&& sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Male
			)
			{
				return;
			}

			AddImage(sdesc);
			ListViewItem lvi = new ListViewItem();
			lvi.Text = sdesc.SimName + " " + sdesc.SimFamilyName;
			lvi.ImageIndex = ilist.Images.Count - 1;
			lvi.Tag = sdesc;
			lv.Items.Add(lvi);
		}

		void LoadArchetyp()
		{
			iskin.Images.Add(
				new Bitmap(
					GetImage.SheOne,
					iskin.ImageSize.Width,
					iskin.ImageSize.Height
				)
			);
			ListViewItem lvia = new ListViewItem("From Archetype");
			lvia.ImageIndex = 0;
			this.lvskin.Items.Add(lvia);
			lvia.Selected = true;
		}

		Hashtable skinfiles;

		void LoadSkins()
		{
			WaitingScreen.Wait();
			try
			{
				skinfiles = new Hashtable();
				ArrayList tones = new ArrayList();
				FileTableBase.FileIndex.Load();
				Interfaces.Scenegraph.IScenegraphFileIndexItem[] items =
					FileTableBase.FileIndex.FindFile(Data.MetaData.GZPS, true);
				foreach (
					Interfaces.Scenegraph.IScenegraphFileIndexItem item in items
				)
				{
					PackedFiles.Wrapper.Cpf skin =
						new PackedFiles.Wrapper.Cpf();
					skin.ProcessData(item);
					if (
						(skin.GetSaveItem("type").StringValue == "skin")
						&& (
							(
								skin.GetSaveItem("category").UIntegerValue
								& (uint)Data.SkinCategories.Skin
							) == (uint)Data.SkinCategories.Skin
						)
					)
					{
						//Maintain a List of all availabe SkinsFiles per skintone
						ArrayList files = null;
						string st = skin.GetSaveItem("skintone").StringValue;
						if (skinfiles.ContainsKey(st))
						{
							files = (ArrayList)skinfiles[st];
						}
						else
						{
							files = new ArrayList();
							skinfiles[st] = files;
						}
						files.Add(skin);

						if (
							(
								skin.GetSaveItem("override0subset").StringValue
									== "body"
								|| skin.GetSaveItem("override0subset").StringValue
									== "top"
							)
							&& skin.GetSaveItem("gender").UIntegerValue == 1
							&& skin.GetSaveItem("age").UIntegerValue
								== (uint)Data.Ages.Adult
						)
						{
							WaitingScreen.UpdateMessage(
								skin.GetSaveItem("name").StringValue
							);

							if (tones.Contains(st))
							{
								continue;
							}
							else
							{
								tones.Add(st);
							}

							Interfaces.Scenegraph.IScenegraphFileIndexItem[] idr =
								FileTableBase.FileIndex.FindFile(
									0xAC506764,
									item.FileDescriptor.Group,
									item.FileDescriptor.LongInstance,
									null
								);
							if (idr.Length > 0)
							{
								RefFile reffile = new RefFile();
								reffile.ProcessData(idr[0]);

								ListViewItem lvi = new ListViewItem(
									skin.GetSaveItem("name").StringValue
								);
								if (Helper.WindowsRegistry.HiddenMode)
								{
									lvi.Text +=
										" ("
										+ skin.GetSaveItem("skintone").StringValue
										+ ")";
								}

								lvi.Tag = skin.GetSaveItem("skintone").StringValue;
								foreach (
									Interfaces.Files.IPackedFileDescriptor pfd in reffile.Items
								)
								{
									if (pfd.Type == Data.MetaData.TXMT)
									{
										Interfaces.Scenegraph.IScenegraphFileIndexItem[] txmts =
											FileTableBase.FileIndex.FindFile(pfd, null);
										if (txmts.Length > 0)
										{
											Rcol rcol = new GenericRcol(
												null,
												false
											);
											rcol.ProcessData(txmts[0]);

											MaterialDefinition md = (MaterialDefinition)
												rcol.Blocks[0];
											string txtrname =
												md.FindProperty(
													"stdMatBaseTextureName"
												).Value + "_txtr";

											Interfaces.Scenegraph.IScenegraphFileIndexItem txtri =
												FileTableBase.FileIndex.FindFileByName(
													txtrname,
													Data.MetaData.TXTR,
													Data.MetaData.LOCAL_GROUP,
													true
												);
											if (txtri != null)
											{
												rcol = new GenericRcol(null, false);
												rcol.ProcessData(txtri);

												ImageData id = (ImageData)
													rcol.Blocks[0];
												MipMap mm = id.GetLargestTexture(
													iskin.ImageSize
												);

												if (mm != null)
												{
													iskin.Images.Add(
														ImageLoader.Preview(
															mm.Texture,
															iskin.ImageSize
														)
													);
													lvi.ImageIndex =
														iskin.Images.Count - 1;
												}
											}
										}
									}
								} //foreach reffile.Items

								lvskin.Items.Add(lvi);
							} //if idr
						}
					} // Don't need to process evry item of clothing do we
				} //foreach items
			}
			finally
			{
				WaitingScreen.UpdateImage(null);
				WaitingScreen.Stop();
			}
			Application.UseWaitCursor = false;
		}

		Interfaces.Files.IPackedFileDescriptor pfd;
		Interfaces.IProviderRegistry prov;
		Interfaces.Files.IPackageFile ngbh;

		public Interfaces.Plugin.IToolResult Execute(
			ref Interfaces.Files.IPackedFileDescriptor pfd,
			ref Interfaces.Files.IPackageFile package,
			Interfaces.IProviderRegistry prov
		)
		{
			this.Cursor = Cursors.WaitCursor;

			Idno idno = Idno.FromPackage(package);
			if (idno != null)
			{
				this.lbUbi.Visible = (idno.Type != NeighborhoodType.Normal);
			}

			this.pfd = null;
			this.prov = prov;
			this.ngbh = package;

			this.pbarche.Image = null;
			this.pbpatient.Image = null;

			this.spatient = null;
			this.sarche = null;
			this.tarcheFile = null;

			button1.Enabled = false;

			ilist.Images.Clear();
			lv.Items.Clear();

			Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(
				Data.MetaData.SIM_DESCRIPTION_FILE
			);
			WaitingScreen.Wait();
			try
			{
				foreach (Interfaces.Files.IPackedFileDescriptor spfd in pfds)
				{
					PackedFiles.Wrapper.ExtSDesc sdesc =
						new PackedFiles.Wrapper.ExtSDesc();
					sdesc.ProcessData(spfd, package);
					AddSim(sdesc);
				}

				this.Cursor = Cursors.Default;
				this.llusearche.Enabled = false;
				this.llusepatient.Enabled = false;
				this.llexport.Enabled = false;
				if (lv.Items.Count > 0)
				{
					lv.Items[0].Selected = true;
				}
			}
			finally
			{
				WaitingScreen.Stop(this);
			}
			RemoteControl.ShowSubForm(this);

			if (this.pfd != null)
			{
				pfd = this.pfd;
			}

			return new ToolResult((this.pfd != null), false);
		}

		private void Open(object sender, EventArgs e)
		{
			if (!CanDo())
			{
				return;
			}

			Packages.File patient = Packages.File.LoadFromFile(
				spatient.CharacterFileName
			);
			Packages.File archetype = null;
			if (sarche != null)
			{
				archetype = Packages.File.LoadFromFile(sarche.CharacterFileName);
			}
			else if (this.tarcheFile != null)
			{
				archetype = Packages.File.LoadFromFile(this.tarcheFile);
				if (!this.CheckArchetypeFile(archetype))
				{
					Helper.ExceptionMessage(
						"The selected template file is not valid.",
						new ApplicationException()
					);
					return;
				}
			}
			else
			{
				archetype = Packages.File.LoadFromFile(null);
			}

			Packages.GeneratableFile newpackage = null;
			PlasticSurgery ps = new PlasticSurgery(
				ngbh,
				patient,
				spatient,
				archetype,
				sarche
			);

			if (
				!this.cbskin.Checked
				&& !this.cbface.Checked
				&& !this.cbmakeup.Checked
				&& !this.cbeye.Checked
			)
			{
				newpackage = ps.CloneSim();
			}

			if (this.cbskin.Checked)
			{
				if (lvskin.SelectedItems.Count == 0)
				{
					return;
				}

				string skin = (string)lvskin.SelectedItems[0].Tag;
				if (skin == null)
				{
					newpackage = ps.CloneSkinTone(skinfiles);
				}
				else
				{
					newpackage = ps.CloneSkinTone(skin, skinfiles);
				}
			}

			if (this.cbface.Checked)
			{
				if (this.cbskin.Checked)
				{
					ps = new PlasticSurgery(
						ngbh,
						newpackage,
						spatient,
						archetype,
						sarche
					);
				}

				newpackage = ps.CloneFace();
			}

			if (this.cbmakeup.Checked)
			{
				if ((this.cbskin.Checked) || (this.cbface.Checked))
				{
					ps = new PlasticSurgery(
						ngbh,
						newpackage,
						spatient,
						archetype,
						sarche
					);
				}

				newpackage = ps.CloneMakeup(false, true);
			}

			if (this.cbeye.Checked)
			{
				if (
					(this.cbskin.Checked)
					|| (this.cbface.Checked)
					|| (this.cbmakeup.Checked)
				)
				{
					ps = new PlasticSurgery(
						ngbh,
						newpackage,
						spatient,
						archetype,
						sarche
					);
				}

				newpackage = ps.CloneMakeup(true, false);
			}

			if (newpackage != null)
			{
				newpackage.Save(spatient.CharacterFileName);
				prov.SimNameProvider.StoredData = null;
				Close();
			}
		}

		private void SelectSim(object sender, EventArgs e)
		{
			this.llusearche.Enabled = false;
			this.llusepatient.Enabled = false;
			if (lv.SelectedItems.Count == 0)
			{
				return;
			}

			this.llusearche.Enabled = true;
			this.llusepatient.Enabled = !(
				(PackedFiles.Wrapper.ExtSDesc)lv.SelectedItems[0].Tag
			).IsNPC;
		}

		PackedFiles.Wrapper.SDesc spatient = null;
		PackedFiles.Wrapper.SDesc sarche = null;

		private void UsePatient(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			this.llexport.Enabled = (spatient != null);
			if (lv.SelectedItems.Count == 0)
			{
				return;
			}

			if (lv.SelectedItems[0].ImageIndex >= 0)
			{
				pbpatient.Image = ilist.Images[lv.SelectedItems[0].ImageIndex];
			}

			spatient = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;

			button1.Enabled =
				(pbpatient.Image != null)
				&& (sarche != null || this.tarcheFile != null);
			pfd = (Interfaces.Files.IPackedFileDescriptor)spatient.FileDescriptor;
			this.llexport.Enabled = (spatient != null);
			ShowSimDetails(spatient, pgPatientDetails);
		}

		private void UseArchetype(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			if (lv.SelectedItems.Count == 0)
			{
				return;
			}

			if (lv.SelectedItems[0].ImageIndex >= 0)
			{
				this.pbarche.Image = ilist.Images[lv.SelectedItems[0].ImageIndex];
			}

			iskin.Images[0] = ImageLoader.Preview(pbarche.Image, iskin.ImageSize);
			lvskin.Refresh();

			sarche = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
			button1.Enabled = (pbpatient.Image != null) && (sarche != null);
			ShowSimDetails(sarche, pgArchetypeDetails);
		}

		protected void FaceSurgery()
		{
			try
			{
				Packages.GeneratableFile patient =
					Packages.File.LoadFromFile(
						spatient.CharacterFileName
					);
				Packages.File archetype = null;
				if (sarche != null)
				{
					archetype = Packages.File.LoadFromFile(
						sarche.CharacterFileName
					);
				}
				else if (this.tarcheFile != null)
				{
					archetype = Packages.File.LoadFromFile(this.tarcheFile);
				}

				if (!this.CheckArchetypeFile(archetype))
				{
					Helper.ExceptionMessage(
						"The selected template file is not valid.",
						new ApplicationException()
					);
					return;
				}

				//Load Facial Data
				Interfaces.Files.IPackedFileDescriptor[] apfds = archetype.FindFiles(
					0xCCCEF852
				); //LxNR, Face
				if (apfds.Length == 0)
				{
					return;
				}

				Interfaces.Files.IPackedFile file = archetype.Read(apfds[0]);

				Interfaces.Files.IPackedFileDescriptor[] ppfds = patient.FindFiles(
					0xCCCEF852
				); //LxNR, Face
				if (ppfds.Length == 0)
				{
					return;
				}

				ppfds[0].UserData = file.UncompressedData;

				//System.IO.MemoryStream ms = patient.Build();
				//patient.Reader.Close();
				patient.Save(spatient.CharacterFileName);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(
					"Unable to update the new Character Package.",
					ex
				);
			}
		}

		private void Export(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			if (spatient == null)
			{
				return;
			}

			try
			{
				//list of all Files top copy from the Archetype
				ArrayList list = new ArrayList
				{
					(uint)0xAC506764, //3IDR
					(uint)0xE519C933, //CRES
					(uint)0xEBCF3E27, //GZPS, Property Set
					(uint)0xAC598EAC, //AGED
					(uint)0xCCCEF852, //LxNR, Face
					(uint)0x0C560F39, //BINX
					(uint)0xAC4F8687, //GMDC
					(uint)0x7BA3838C, //GMND
					(uint)0x49596978, //MATD
					(uint)0xFC6EB1F7 //SHPE
				};

				System.IO.BinaryReader br1 = new System.IO.BinaryReader(
					this.GetType()
						.Assembly.GetManifestResourceStream("SimPe.data.3d.simpe")
				);
				System.IO.BinaryReader br2 = new System.IO.BinaryReader(
					this.GetType()
						.Assembly.GetManifestResourceStream("SimPe.data.bin.simpe")
				);

				Packages.PackedFileDescriptor pfd1 =
					new Packages.PackedFileDescriptor();
				pfd1.Group = 0xffffffff;
				pfd1.SubType = 0x00000000;
				pfd1.Instance = 0xFF123456;
				pfd1.Type = 0xAC506764; //3IDR
				pfd1.UserData = br1.ReadBytes((int)br1.BaseStream.Length);

				Packages.PackedFileDescriptor pfd2 =
					new Packages.PackedFileDescriptor();
				pfd2.Group = 0xffffffff;
				pfd2.SubType = 0x00000000;
				pfd2.Instance = 0xFF123456;
				pfd2.Type = 0x0C560F39; //BINX
				pfd2.UserData = br2.ReadBytes((int)br2.BaseStream.Length);

				sfd.InitialDirectory = System.IO.Path.Combine(
					PathProvider.SimSavegameFolder,
					"SavedSims"
				);
				sfd.FileName = System.IO.Path.GetFileNameWithoutExtension(
					spatient.CharacterFileName
				);

				Packages.GeneratableFile source =
					Packages.File.LoadFromFile(
						spatient.CharacterFileName
					);
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					Packages.GeneratableFile patient =
						Packages.File.LoadFromStream(
							(System.IO.BinaryReader)null
						);
					patient.FileName = "";
					patient.Add(pfd1);
					patient.Add(pfd2);

					foreach (Interfaces.Files.IPackedFileDescriptor pfd in source.Index)
					{
						if (list.Contains(pfd.Type))
						{
							Interfaces.Files.IPackedFile file = source.Read(pfd);
							pfd.UserData = file.UncompressedData;
							patient.Add(pfd);

							if (
								(pfd.Type == Data.MetaData.GZPS)
								|| (pfd.Type == 0xAC598EAC)
							) //property set and 3IDR
							{
								PackedFiles.Wrapper.Cpf cpf =
									new PackedFiles.Wrapper.Cpf();
								cpf.ProcessData(pfd, patient);

								PackedFiles.Wrapper.CpfItem ci =
									new PackedFiles.Wrapper.CpfItem();
								ci.Name = "product";
								ci.UIntegerValue = 0;
								cpf.AddItem(ci, false);

								ci = cpf.GetItem("version");
								if (ci == null)
								{
									ci = new PackedFiles.Wrapper.CpfItem();
									ci.Name = "version";
									if (
										(
											cpf.GetSaveItem("age").UIntegerValue
											& (uint)Data.Ages.YoungAdult
										) != 0
									)
									{
										ci.UIntegerValue = 2;
									}
									else
									{
										ci.UIntegerValue = 1;
									}

									cpf.AddItem(ci);
								}

								cpf.SynchronizeUserData();
							}
						}
					}
					patient.Save(sfd.FileName);
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		bool CanDo()
		{
			if (spatient == null)
			{
				return false;
			}

			bool ret = true;
			if (cbskin.Checked)
			{
				ret = (lvskin.SelectedItems.Count == 1);
				if (ret)
				{
					if (
						lvskin.Items[0].Selected
						&& (sarche == null && tarcheFile == null)
					)
					{
						ret = false;
					}
				}
			}

			if (!cbskin.Checked || cbface.Checked || cbmakeup.Checked || cbeye.Checked)
			{
				ret = ret && (sarche != null || tarcheFile != null);
			}

			return ret;
		}

		bool skload = false;

		private void cbskin_CheckedChanged(object sender, EventArgs e) // Fuck
		{
			if (!skload)
			{
				LoadSkins();
			}

			lvskin.Enabled = this.cbskin.Checked;
			button1.Enabled = CanDo();
			skload = true;
		}

		private void lvskin_SelectedIndexChanged(object sender, EventArgs e)
		{
			button1.Enabled = CanDo();
		}

		private void ctlLoadPackage_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			this.opd.InitialDirectory = System.IO.Path.Combine(
				PathProvider.SimSavegameFolder,
				"SavedSims"
			);
			this.opd.ShowDialog();
		}

		private void opd_FileOk(object sender, CancelEventArgs e)
		{
			if (!e.Cancel)
			{
				this.tarcheFile = this.opd.FileName;
				if (
					this.CheckArchetypeFile(
						Packages.File.LoadFromFile(tarcheFile)
					)
				)
				{
					button1.Enabled = (this.spatient != null);
					pbarche.Image = GetImage.NoOne;
					ShowSimDetails(
						Packages.File.LoadFromFile(tarcheFile),
						pgArchetypeDetails
					);
				}
				else
				{
					tarcheFile = null;
					sarche = null;
					button1.Enabled = false;
					pbarche.Image = GetIcon.Fail;
					pgArchetypeDetails.SelectedObject = null;
				}
				iskin.Images[0] = ImageLoader.Preview(pbarche.Image, iskin.ImageSize);
				lvskin.Refresh();
			}
		}

		string tarcheFile = null;

		/// <summary>
		/// Checks if an arbitrary package contains the file types required
		/// for archetype elegibility.
		/// </summary>
		/// <param name="archeFile"></param>
		/// <returns>True if the provided package can be a surgery archetype, otherwise false.</returns>
		bool CheckArchetypeFile(Packages.File archeFile)
		{
			bool ret = false;
			if (archeFile == null)
			{
				return ret;
			}

			// Build a list of required file types.
			// Could this list be static?
			ArrayList list = new ArrayList
			{
				0xAC506764u, //3IDR
				Data.MetaData.GZPS, //GZPS, Property Set
				0xAC598EACu, //AGED
				0xCCCEF852u //LxNR, Face
			};
			// For now we disregard the user options, and consider
			// all these types mandatory.
			for (
				int i = 0;
				i < list.Count && (ret = ContainsType(archeFile.Index, (uint)list[i]));
				i++
			)
			{
				;
			}

			return ret;
		}

		static bool ContainsType(
			Interfaces.Files.IPackedFileDescriptor[] index,
			uint type
		)
		{
			for (int i = 0; i < index.Length; i++)
			{
				if (index[i].Type == type && index[i].Group == 0xffffffff)
				{
					return true;
				}
			}

			return false;
		}

		protected void FillList()
		{
			this.Cursor = Cursors.WaitCursor;

			this.pbarche.Image = null;
			this.pbpatient.Image = null;
			this.pgPatientDetails.SelectedObject = null;
			this.pgArchetypeDetails.SelectedObject = null;

			if (this.cbmens.Checked)
			{
				this.iskin.Images[0] = ImageLoader.Preview(
					GetImage.NoOne,
					iskin.ImageSize
				);
			}
			else
			{
				this.iskin.Images[0] = ImageLoader.Preview(
					GetImage.SheOne,
					iskin.ImageSize
				);
			}

			lvskin.Refresh();

			this.spatient = null;
			this.sarche = null;
			this.tarcheFile = null;

			button1.Enabled = false;
			ilist.Images.Clear();
			lv.Items.Clear();

			Interfaces.Files.IPackedFileDescriptor[] pfds = ngbh.FindFiles(
				Data.MetaData.SIM_DESCRIPTION_FILE
			);
			WaitingScreen.Wait();
			try
			{
				foreach (Interfaces.Files.IPackedFileDescriptor spfd in pfds)
				{
					PackedFiles.Wrapper.ExtSDesc sdesc =
						new PackedFiles.Wrapper.ExtSDesc();
					sdesc.ProcessData(spfd, ngbh);
					AddSim(sdesc);
				}

				this.Cursor = Cursors.Default;
				this.llusearche.Enabled = false;
				this.llusepatient.Enabled = false;
				this.llexport.Enabled = false;
				if (lv.Items.Count > 0)
				{
					lv.Items[0].Selected = true;
				}
			}
			finally
			{
				WaitingScreen.Stop(this);
			}
		}

		private void genderFilter_CheckedChanged(object sender, EventArgs e)
		{
			this.cbgals.Enabled = !this.cbmens.Checked;
			this.cbmens.Enabled = !this.cbgals.Checked;
			if (ngbh != null)
			{
				this.FillList();
			}
		}

		void ShowSimDetails(PackedFiles.Wrapper.SDesc sim, PropertyGrid pg)
		{
			Packages.File package = Packages.File.LoadFromFile(
				sim.CharacterFileName
			);
			if (package != null)
			{
				Interfaces.Files.IPackedFileDescriptor pfdAged = package.FindFile(
					0xAC598EAC,
					0,
					Data.MetaData.LOCAL_GROUP,
					1
				);
				if (pfdAged != null)
				{
					PackedFiles.Wrapper.Cpf aged =
						new PackedFiles.Wrapper.Cpf();
					aged.ProcessData(pfdAged, package, true);

					SimInfo nfo = new SimInfo(
						aged,
						System.IO.Path.GetFileName(sim.CharacterFileName),
						String.Format("{0} {1}", sim.SimName, sim.SimFamilyName)
					);
					pg.SelectedObject = nfo;
				}
			}
		}

		void ShowSimDetails(Packages.File package, PropertyGrid pg)
		{
			Interfaces.Files.IPackedFileDescriptor pfdAged = package.FindFile(
				0xAC598EAC,
				0,
				Data.MetaData.LOCAL_GROUP,
				1
			);
			if (pfdAged != null)
			{
				PackedFiles.Wrapper.Cpf aged =
					new PackedFiles.Wrapper.Cpf();
				aged.ProcessData(pfdAged, package, true);

				SimInfo nfo = new SimInfo(
					aged,
					System.IO.Path.GetFileName(package.FileName),
					null
				);
				pg.SelectedObject = nfo;
			}
		}

		private bool ShowTownies
		{
			get
			{
				XmlRegistryKey rkf =
					Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
				object o = rkf.GetValue("ShowTownies", false);
				return Convert.ToBoolean(o);
			}
		}

		private bool ShowNPCs
		{
			get
			{
				XmlRegistryKey rkf =
					Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
				object o = rkf.GetValue("ShowNPCs", false);
				return Convert.ToBoolean(o);
			}
		}

		private bool UseBigIcons
		{
			get
			{
				XmlRegistryKey rkf =
					Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
				object o = rkf.GetValue("UseBiggerIcons", false);
				return Convert.ToBoolean(o);
			}
		}

		private bool AdultsOnly
		{
			get
			{
				XmlRegistryKey rkf =
					Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
				object o = rkf.GetValue("AdultsOnly", false);
				return Convert.ToBoolean(o);
			}
		}

		private bool JustGals
		{
			get
			{
				XmlRegistryKey rkf =
					Helper.WindowsRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
				object o = rkf.GetValue("JustGals", false);
				return Convert.ToBoolean(o);
			}
		}
	}

	/// <summary>
	/// A simple info object for PropertyGrid presentation purposes.
	/// </summary>
	internal sealed class SimInfo
	{
		PackedFiles.Wrapper.Cpf ageData;

		[Category("General")]
		public Data.Ages Age => (Data.Ages)ageData.GetItem("age").UIntegerValue;

		[Category("General")]
		public Data.SimGender Gender => (Data.SimGender)ageData.GetItem("gender").UIntegerValue;

		[Category("General")]
		public string Name
		{
			get;
		}

		[Category("General")]
		public string Filename
		{
			get;
		}

		[Category("Genetics")]
		public string Hair => ageData.GetItem("haircolor").StringValue;

		[Category("Genetics")]
		public string Eyes => ageData.GetItem("eyecolor").StringValue;

		[Category("Genetics")]
		public string Skin => ageData.GetItem("skincolor").StringValue;

		[Category("Genetics")]
		public string Bodyshape => Data.MetaData.GetBodyName(
					Data.MetaData.GetBodyShapeid(
						ageData.GetItem("skincolor").StringValue
					)
				);

		public SimInfo(PackedFiles.Wrapper.Cpf aged, string filename, string name)
		{
			if (aged == null)
			{
				throw new ArgumentNullException();
			}

			this.ageData = aged;
			this.Filename = filename;
			this.Name = name;
		}
	}
}
