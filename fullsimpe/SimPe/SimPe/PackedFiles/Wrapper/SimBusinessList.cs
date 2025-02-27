using System;
using System.Windows.Forms;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Summary description for SimBusinessList.
	/// </summary>
	public class SimBusinessList : UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SimBusinessList()
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

			if (!DesignMode)
			{
				SetContent();
			}
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
			this.cb = new ComboBox();
			this.SuspendLayout();
			//
			// cb
			//
			this.cb.Dock = System.Windows.Forms.DockStyle.Top;
			this.cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb.Location = new System.Drawing.Point(0, 0);
			this.cb.Name = "cb";
			this.cb.Size = new System.Drawing.Size(150, 21);
			this.cb.TabIndex = 0;
			this.cb.SelectedIndexChanged += new EventHandler(
				this.cb_SelectedIndexChanged
			);
			//
			// SimBusinessList
			//
			this.Controls.Add(this.cb);
			this.Font = new System.Drawing.Font(
				"Tahoma",
				8.25F,
				System.Drawing.FontStyle.Regular,
				System.Drawing.GraphicsUnit.Point,
				((byte)(0))
			);
			this.Name = "SimBusinessList";
			this.Size = new System.Drawing.Size(150, 24);
			this.ResumeLayout(false);
		}
		#endregion

		private ComboBox cb;

		bool loaded;
		LinkedSDesc sdsc;
		public Interfaces.Wrapper.ISDesc SimDescription
		{
			get
			{
				return sdsc;
			}
			set
			{
				sdsc = value as LinkedSDesc;
				SetContent();
			}
		}

		void SetContent()
		{
			loaded = Visible;
			if (!loaded)
			{
				return;
			}

			cb.Items.Clear();
			if (sdsc != null)
			{
				Interfaces.Providers.ILotItem[] items = sdsc.BusinessList;
				foreach (Interfaces.Providers.ILotItem item in items)
				{
					cb.Items.Add(item);
				}

				if (cb.Items.Count > 0)
				{
					cb.SelectedIndex = 0;
				}
				else if (SelectedBusinessChanged != null)
				{
					SelectedBusinessChanged(this, new EventArgs());
				}
			}
		}

		public event EventHandler SelectedBusinessChanged;

		public Interfaces.Providers.ILotItem SelectedBusiness => cb.SelectedItem as Interfaces.Providers.ILotItem;

		private void cb_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedBusinessChanged != null)
			{
				SelectedBusinessChanged(this, new EventArgs());
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!loaded)
			{
				SetContent();
			}
		}
	}
}
