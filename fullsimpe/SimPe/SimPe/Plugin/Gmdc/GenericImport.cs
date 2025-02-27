using System.Drawing;
using System.Windows.Forms;

namespace SimPe.Plugin.Gmdc
{
	/// <summary>
	/// Summary description for GenericImport.
	/// </summary>
	class GenericImportForm : Form
	{
		private SteepValley.Windows.Forms.XPGradientPanel Gradientpanel1;
		private System.ComponentModel.IContainer components;
		private ListViewEx lvmesh;
		private ImageList imageList1;
		private ColumnHeader chMeshName;
		private ColumnHeader chMeshAction;
		private ColumnHeader chMeshTarget;
		private ColumnHeader chFaces;
		private ColumnHeader chVertices;
		private ColumnHeader chImportEnvelope;
		private SteepValley.Windows.Forms.XPLine xpLine1;
		private Label label1;
		private ColumnHeader chJointCount;
		private Label label2;
		private SteepValley.Windows.Forms.XPLine xpLine2;
		private ListViewEx lvbones;
		private ColumnHeader clBoneName;
		private ColumnHeader clBoneAction;
		private ColumnHeader clImportBone;
		private ColumnHeader clAssignedVertices;
		private Label label3;
		private SteepValley.Windows.Forms.XPLine xpLine3;
		private Panel panel1;
		private Button button1;
		private CheckBox cbClear;

		GenericImportForm()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();

			ComboBox cb = new ComboBox();
			this.imageList1.ImageSize = new Size(1, cb.Height + 2);
			cb.Dispose();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources =
				new System.Resources.ResourceManager(typeof(GenericImportForm));
			this.Gradientpanel1 = new SteepValley.Windows.Forms.XPGradientPanel();
			this.panel1 = new Panel();
			this.cbClear = new CheckBox();
			this.button1 = new Button();
			this.label3 = new Label();
			this.xpLine3 = new SteepValley.Windows.Forms.XPLine();
			this.lvbones = new ListViewEx();
			this.clBoneName = new ColumnHeader();
			this.clBoneAction = new ColumnHeader();
			this.clImportBone = new ColumnHeader();
			this.clAssignedVertices = new ColumnHeader();
			this.imageList1 = new ImageList(this.components);
			this.label2 = new Label();
			this.xpLine2 = new SteepValley.Windows.Forms.XPLine();
			this.label1 = new Label();
			this.xpLine1 = new SteepValley.Windows.Forms.XPLine();
			this.lvmesh = new ListViewEx();
			this.chMeshName = new ColumnHeader();
			this.chMeshAction = new ColumnHeader();
			this.chMeshTarget = new ColumnHeader();
			this.chFaces = new ColumnHeader();
			this.chVertices = new ColumnHeader();
			this.chImportEnvelope = new ColumnHeader();
			this.chJointCount = new ColumnHeader();
			this.Gradientpanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// Gradientpanel1
			//
			this.Gradientpanel1.BackColor = Color.Transparent;
			this.Gradientpanel1.Controls.Add(this.panel1);
			this.Gradientpanel1.Controls.Add(this.label3);
			this.Gradientpanel1.Controls.Add(this.xpLine3);
			this.Gradientpanel1.Controls.Add(this.lvbones);
			this.Gradientpanel1.Controls.Add(this.label2);
			this.Gradientpanel1.Controls.Add(this.xpLine2);
			this.Gradientpanel1.Controls.Add(this.label1);
			this.Gradientpanel1.Controls.Add(this.xpLine1);
			this.Gradientpanel1.Controls.Add(this.lvmesh);
			this.Gradientpanel1.Dock = DockStyle.Fill;
			this.Gradientpanel1.Location = new Point(0, 0);
			this.Gradientpanel1.Name = "Gradientpanel1";
			this.Gradientpanel1.Size = new Size(752, 486);
			this.Gradientpanel1.TabIndex = 0;
			//
			// panel1
			//
			this.panel1.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Bottom
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.panel1.BackColor = Color.Transparent;
			this.panel1.Controls.Add(this.cbClear);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Location = new Point(0, 384);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(752, 100);
			this.panel1.TabIndex = 10;
			//
			// cbClear
			//
			this.cbClear.Location = new Point(8, 8);
			this.cbClear.Name = "cbClear";
			this.cbClear.Size = new Size(192, 24);
			this.cbClear.TabIndex = 1;
			this.cbClear.Text = "Clear Meshgroups before Import";
			//
			// button1
			//
			this.button1.FlatStyle = FlatStyle.System;
			this.button1.Location = new Point(672, 72);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Import";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// label3
			//
			this.label3.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Bottom
						| AnchorStyles.Right
					)
				)
			);
			this.label3.BackColor = Color.Transparent;
			this.label3.Font = new Font(
				"Tahoma",
				8.25F,
				FontStyle.Bold,
				GraphicsUnit.Point,
				((System.Byte)(0))
			);
			this.label3.ForeColor = Color.FromArgb(
				((System.Byte)(64)),
				((System.Byte)(64)),
				((System.Byte)(64))
			);
			this.label3.Location = new Point(648, 352);
			this.label3.Name = "label3";
			this.label3.TabIndex = 9;
			this.label3.Text = "Options";
			this.label3.TextAlign = ContentAlignment.BottomRight;
			//
			// xpLine3
			//
			this.xpLine3.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Bottom
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.xpLine3.BackColor = Color.Transparent;
			this.xpLine3.Location = new Point(9, 376);
			this.xpLine3.Name = "xpLine3";
			this.xpLine3.Size = new Size(740, 4);
			this.xpLine3.TabIndex = 8;
			//
			// lvbones
			//
			this.lvbones.Anchor = (
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
			this.lvbones.BorderStyle = BorderStyle.None;
			this.lvbones.Columns.AddRange(
				new ColumnHeader[]
				{
					this.clBoneName,
					this.clBoneAction,
					this.clImportBone,
					this.clAssignedVertices,
				}
			);
			this.lvbones.FullRowSelect = true;
			this.lvbones.HeaderStyle =
				ColumnHeaderStyle
				.Nonclickable;
			this.lvbones.HideSelection = false;
			this.lvbones.Location = new Point(8, 216);
			this.lvbones.Name = "lvbones";
			this.lvbones.Size = new Size(736, 128);
			this.lvbones.SmallImageList = this.imageList1;
			this.lvbones.TabIndex = 7;
			this.lvbones.View = View.Details;
			//
			// clBoneName
			//
			this.clBoneName.Text = "Name";
			this.clBoneName.Width = 106;
			//
			// clBoneAction
			//
			this.clBoneAction.Text = "";
			this.clBoneAction.Width = 102;
			//
			// clImportBone
			//
			this.clImportBone.Text = "Import as";
			this.clImportBone.Width = 277;
			//
			// clAssignedVertices
			//
			this.clAssignedVertices.Text = "Vertices";
			this.clAssignedVertices.Width = 67;
			//
			// imageList1
			//
			this.imageList1.ImageSize = new Size(1, 16);
			this.imageList1.TransparentColor = Color.Transparent;
			//
			// label2
			//
			this.label2.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.label2.BackColor = Color.Transparent;
			this.label2.Font = new Font(
				"Tahoma",
				8.25F,
				FontStyle.Bold,
				GraphicsUnit.Point,
				((System.Byte)(0))
			);
			this.label2.ForeColor = Color.FromArgb(
				((System.Byte)(64)),
				((System.Byte)(64)),
				((System.Byte)(64))
			);
			this.label2.Location = new Point(648, 184);
			this.label2.Name = "label2";
			this.label2.TabIndex = 6;
			this.label2.Text = "Skeleton";
			this.label2.TextAlign = ContentAlignment.BottomRight;
			//
			// xpLine2
			//
			this.xpLine2.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.xpLine2.BackColor = Color.Transparent;
			this.xpLine2.Location = new Point(9, 208);
			this.xpLine2.Name = "xpLine2";
			this.xpLine2.Size = new Size(740, 4);
			this.xpLine2.TabIndex = 5;
			//
			// label1
			//
			this.label1.Anchor = (
				(AnchorStyles)(
					(
						AnchorStyles.Top
						| AnchorStyles.Right
					)
				)
			);
			this.label1.BackColor = Color.Transparent;
			this.label1.Font = new Font(
				"Tahoma",
				8.25F,
				FontStyle.Bold,
				GraphicsUnit.Point,
				((System.Byte)(0))
			);
			this.label1.ForeColor = Color.FromArgb(
				((System.Byte)(64)),
				((System.Byte)(64)),
				((System.Byte)(64))
			);
			this.label1.Location = new Point(648, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 4;
			this.label1.Text = "Mesh Groups";
			this.label1.TextAlign = ContentAlignment.BottomRight;
			//
			// xpLine1
			//
			this.xpLine1.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.xpLine1.BackColor = Color.Transparent;
			this.xpLine1.Location = new Point(9, 32);
			this.xpLine1.Name = "xpLine1";
			this.xpLine1.Size = new Size(740, 4);
			this.xpLine1.TabIndex = 3;
			//
			// lvmesh
			//
			this.lvmesh.Anchor = (
				(AnchorStyles)(
					(
						(
							AnchorStyles.Top
							| AnchorStyles.Left
						) | AnchorStyles.Right
					)
				)
			);
			this.lvmesh.BorderStyle = BorderStyle.None;
			this.lvmesh.Columns.AddRange(
				new ColumnHeader[]
				{
					this.chMeshName,
					this.chMeshAction,
					this.chMeshTarget,
					this.chFaces,
					this.chVertices,
					this.chImportEnvelope,
					this.chJointCount,
				}
			);
			this.lvmesh.FullRowSelect = true;
			this.lvmesh.HeaderStyle =
				ColumnHeaderStyle
				.Nonclickable;
			this.lvmesh.HideSelection = false;
			this.lvmesh.Location = new Point(8, 40);
			this.lvmesh.Name = "lvmesh";
			this.lvmesh.Size = new Size(736, 136);
			this.lvmesh.SmallImageList = this.imageList1;
			this.lvmesh.TabIndex = 2;
			this.lvmesh.View = View.Details;
			//
			// chMeshName
			//
			this.chMeshName.Text = "Name";
			this.chMeshName.Width = 106;
			//
			// chMeshAction
			//
			this.chMeshAction.Text = "";
			this.chMeshAction.Width = 102;
			//
			// chMeshTarget
			//
			this.chMeshTarget.Text = "Import as";
			this.chMeshTarget.Width = 277;
			//
			// chFaces
			//
			this.chFaces.Text = "Faces";
			this.chFaces.Width = 67;
			//
			// chVertices
			//
			this.chVertices.Text = "Vertices";
			this.chVertices.Width = 67;
			//
			// chImportEnvelope
			//
			this.chImportEnvelope.Text = "Boneweight Import";
			this.chImportEnvelope.Width = 20;
			//
			// chJointCount
			//
			this.chJointCount.Text = "Joint Count";
			this.chJointCount.Width = 79;
			//
			// GenericImportForm
			//
			this.AutoScaleBaseSize = new Size(5, 14);
			this.ClientSize = new Size(752, 486);
			this.Controls.Add(this.Gradientpanel1);
			this.Font = new Font(
				"Tahoma",
				8.25F,
				FontStyle.Regular,
				GraphicsUnit.Point,
				((System.Byte)(0))
			);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GenericImportForm";
			this.ShowInTaskbar = false;
			this.Text = "Mesh Import";
			this.Gradientpanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion



		GenericMeshImport gmi;

		public static void Execute(GenericMeshImport gmi)
		{
			GenericImportForm f = new GenericImportForm();
			f.gmi = gmi;
			f.Setup();
			f.ShowDialog();
			f.Dispose();
		}

		MeshListViewItem.ActionChangedEvent chg;
		BoneListViewItem.ActionChangedEvent bonechg;

		void Setup()
		{
			this.cbClear.Checked = gmi.ClearGroupsOnImport;
			if (chg == null)
			{
				chg = new MeshListViewItem.ActionChangedEvent(ActionChangedEvent);
			}

			if (bonechg == null)
			{
				bonechg = new BoneListViewItem.ActionChangedEvent(
					BoneActionChangedEvent
				);
			}

			foreach (Ambertation.Scenes.Mesh m in gmi.Scene.MeshCollection)
			{
				new MeshListViewItemExt(lvmesh, m, gmi, chg);
			}

			foreach (Ambertation.Scenes.Joint j in gmi.Scene.JointCollection)
			{
				new BoneListViewItemExt(this.lvbones, j, gmi, bonechg);
			}
		}

		bool ignore = false;

		void ActionChangedEvent(MeshListViewItem sender)
		{
			if (ignore)
			{
				return;
			}

			ignore = true;
			foreach (MeshListViewItem mlvi in lvmesh.SelectedItems)
			{
				if (mlvi == sender)
				{
					continue;
				}

				mlvi.Action = sender.Action;
			}
			ignore = false;
		}

		void BoneActionChangedEvent(BoneListViewItem sender)
		{
			if (ignore)
			{
				return;
			}

			ignore = true;
			foreach (BoneListViewItem blvi in lvbones.SelectedItems)
			{
				if (blvi == sender)
				{
					continue;
				}

				blvi.Action = sender.Action;
			}
			ignore = false;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			MeshListViewItemExt[] meshes = new MeshListViewItemExt[lvmesh.Items.Count];
			lvmesh.Items.CopyTo(meshes, 0);
			gmi.SetMeshList(meshes);

			BoneListViewItemExt[] bones = new BoneListViewItemExt[lvbones.Items.Count];
			lvbones.Items.CopyTo(bones, 0);
			gmi.SetBoneList(bones);

			gmi.ClearGroupsOnImport = this.cbClear.Checked;

			Close();
		}
	}
}
