using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimPe
{
	/// <summary>
	/// Summary description for FileTableItemForm.
	/// </summary>
	public class FileTableItemForm : System.Windows.Forms.Form
	{
		private SteepValley.Windows.Forms.XPGradientPanel xpGradientPanel1;

		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox cbRec;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.TextBox tbRoot;
		private System.Windows.Forms.ComboBox cbEpVer;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private System.Windows.Forms.OpenFileDialog ofd;

		public FileTableItemForm()
		{
			InitializeComponent();

			ofd.Filter = SimPe.ExtensionProvider.BuildFilterString(
				new SimPe.ExtensionType[]
				{
					SimPe.ExtensionType.Package,
					SimPe.ExtensionType.AllFiles,
				}
			);

			this.cbEpVer.Items.Clear();
			cbEpVer.Items.Add(SimPe.Localization.GetString("All"));
			foreach (ExpansionItem ei in PathProvider.Global.Expansions)
			{
				cbEpVer.Items.Add(ei.Name);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			System.ComponentModel.ComponentResourceManager resources =
				new System.ComponentModel.ComponentResourceManager(
					typeof(FileTableItemForm)
				);
			this.xpGradientPanel1 = new SteepValley.Windows.Forms.XPGradientPanel();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cbEpVer = new System.Windows.Forms.ComboBox();
			this.tbRoot = new System.Windows.Forms.TextBox();
			this.tbName = new System.Windows.Forms.TextBox();
			this.cbRec = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.xpGradientPanel1.SuspendLayout();
			this.SuspendLayout();
			//
			// xpGradientPanel1
			//
			this.xpGradientPanel1.BackColor = System.Drawing.Color.Transparent;
			this.xpGradientPanel1.Controls.Add(this.button3);
			this.xpGradientPanel1.Controls.Add(this.button2);
			this.xpGradientPanel1.Controls.Add(this.button1);
			this.xpGradientPanel1.Controls.Add(this.cbEpVer);
			this.xpGradientPanel1.Controls.Add(this.tbRoot);
			this.xpGradientPanel1.Controls.Add(this.tbName);
			this.xpGradientPanel1.Controls.Add(this.cbRec);
			this.xpGradientPanel1.Controls.Add(this.label3);
			this.xpGradientPanel1.Controls.Add(this.label2);
			this.xpGradientPanel1.Controls.Add(this.label1);
			resources.ApplyResources(this.xpGradientPanel1, "xpGradientPanel1");
			this.xpGradientPanel1.Name = "xpGradientPanel1";
			//
			// button3
			//
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			//
			// button2
			//
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			//
			// button1
			//
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			//
			// cbEpVer
			//
			this.cbEpVer.DropDownStyle = System
				.Windows
				.Forms
				.ComboBoxStyle
				.DropDownList;
			resources.ApplyResources(this.cbEpVer, "cbEpVer");
			this.cbEpVer.Name = "cbEpVer";
			//
			// tbRoot
			//
			resources.ApplyResources(this.tbRoot, "tbRoot");
			this.tbRoot.Name = "tbRoot";
			this.tbRoot.ReadOnly = true;
			//
			// tbName
			//
			resources.ApplyResources(this.tbName, "tbName");
			this.tbName.Name = "tbName";
			this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
			//
			// cbRec
			//
			resources.ApplyResources(this.cbRec, "cbRec");
			this.cbRec.Name = "cbRec";
			this.cbRec.UseVisualStyleBackColor = false;
			//
			// label3
			//
			this.label3.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			//
			// label2
			//
			this.label2.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			//
			// label1
			//
			this.label1.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			//
			// FileTableItemForm
			//
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.xpGradientPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FileTableItemForm";
			this.xpGradientPanel1.ResumeLayout(false);
			this.xpGradientPanel1.PerformLayout();
			this.ResumeLayout(false);
		}
		#endregion

		bool ok;
		bool file;

		public static FileTableItem Execute()
		{
			FileTableItem fti = new FileTableItem("", false, false);

			if (Execute(fti))
				return fti;
			else
				return null;
		}

		public static bool Execute(FileTableItem fti)
		{
			FileTableItemForm f = new FileTableItemForm();
			f.tbName.Text = fti.Name;
			f.tbRoot.Text = fti.Type.ToString();
			if (fti.EpVersion + 1 < f.cbEpVer.Items.Count)
				f.cbEpVer.SelectedIndex = fti.EpVersion + 1;
			else
			{
				ExpansionItem ei = PathProvider.Global[fti.EpVersion];
				for (int i = 0; i < f.cbEpVer.Items.Count; i++)
				{
					if (f.cbEpVer.Items[i].ToString() == ei.Name)
					{
						f.cbEpVer.SelectedIndex = i;
						break;
					}
				}
			}
			f.cbRec.Checked = fti.IsRecursive;
			f.ok = false;
			f.file = fti.IsFile;
			f.UpdateRec();

			f.ShowDialog();

			if (f.ok)
			{
				fti.Type = FileTablePaths.Absolute;
				fti.Name = f.tbName.Text.Trim();
				fti.IsRecursive = f.cbRec.Checked;
				string epname = f.cbEpVer.Text;
				foreach (ExpansionItem ei in PathProvider.Global.Expansions)
					if (ei.Name == epname)
						fti.EpVersion = ei.Version;

				fti.IsFile = f.file;

				return true;
			}

			return false;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			ok = true;
			Close();
		}

		void UpdateType()
		{
			FileTableItem fti = new FileTableItem(tbName.Text, false, file);
			fti.Name = tbName.Text;

			this.tbRoot.Text = fti.Type.ToString();
		}

		void UpdateRec()
		{
			this.cbRec.Enabled = !file;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				file = false;
				tbName.Text = fbd.SelectedPath;

				UpdateType();
				UpdateRec();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				file = true;
				tbName.Text = ofd.FileName;

				UpdateType();
				UpdateRec();
			}
		}

		private void tbName_TextChanged(object sender, System.EventArgs e)
		{
		}
	}
}
