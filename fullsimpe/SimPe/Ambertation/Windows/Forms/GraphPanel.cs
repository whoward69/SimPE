// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Ambertation.Windows.Forms.Graph;

namespace Ambertation.Windows.Forms
{
	/// <summary>
	/// This is a Dragable Panel
	/// </summary>
	[ToolboxBitmap(typeof(Panel))]
	public class GraphPanel : Panel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		internal Collections.GraphElements LinkItems => Items;

		public Collections.GraphElements Items
		{
			get;
		}

		public GraphPanel()
		{
			// Required designer variable.
			InitializeComponent();

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
			Items = new Collections.GraphElements();
			Items.ItemsChanged += new EventHandler(li_ItemsChanged);
			BackColor = SystemColors.ControlLightLight;
			lm = LinkControlLineMode.Bezier;
			quality = true;
			savebound = true;
			minwd = 0;
			minhg = 0;
			lk = false;
			update = false;

			autosz = false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();

				if (Items != null)
				{
					while (Items.Count > 0)
					{
						GraphPanelElement l = Items[0];
						Items.RemoveAt(0);
						l.Dispose();
					}
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
			components = new Container();
		}
		#endregion

		#region Properties
		public new Control Parent
		{
			get => base.Parent;
			set
			{
				if (base.Parent != value)
				{
					if (Parent != null)
					{
						Parent.SizeChanged -= new EventHandler(Parent_SizeChanged);
					}

					base.Parent = value;
					if (Parent != null)
					{
						MinWidth = Parent.ClientRectangle.Width;
						MinHeight = Parent.ClientRectangle.Height;
						Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
					}
				}
			}
		}
		bool lk;
		public bool LockItems
		{
			get => lk;
			set
			{
				if (lk != value)
				{
					lk = value;
					SetLocked();
				}
			}
		}
		bool savebound;
		public virtual bool SaveBounds
		{
			get => savebound;
			set => savebound = value;
		}

		bool autosz;
		public override bool AutoSize
		{
			get => autosz;
			set
			{
				autosz = value;
				li_ItemsChanged(Items, null);
				if (autosz)
				{
					Dock = DockStyle.None;
					SetBounds(0, 0, Width, Height);
				}
			}
		}
		LinkControlLineMode lm;
		public LinkControlLineMode LineMode
		{
			get => lm;
			set
			{
				lm = value;
				SetLinkLineMode();
			}
		}

		bool quality;
		public bool Quality
		{
			get => quality;
			set
			{
				quality = value;
				SetLinkQuality();
			}
		}
		int minwd,
			minhg;

		[Browsable(false)]
		public int MinWidth
		{
			get => DesignMode && Parent != null ? Parent.Width : minwd;
			set
			{
				minwd = value;
				Width = Math.Max(Width, minwd);
			}
		}

		[Browsable(false)]
		public int MinHeight
		{
			get => DesignMode && Parent != null ? Parent.Height : minhg;
			set
			{
				minhg = value;
				Height = Math.Max(Height, minhg);
			}
		}

		[Browsable(false)]
		public override bool AutoScroll
		{
			get => base.AutoScroll;
			set
			{
			}
		}

		[Browsable(false)]
		public GraphPanelElement SelectedElement
		{
			get
			{
				foreach (GraphPanelElement gpe in Items)
				{
					if (gpe is DragPanel)
					{
						if (((DragPanel)gpe).Focused)
						{
							return gpe;
						}
					}
				}

				return null;
			}
			set
			{
				if (value == null)
				{
					return;
				}

				if (!(value is DragPanel))
				{
					return;
				}

				if (Items.Contains(value))
				{
					GraphPanelElement[] elements = new GraphPanelElement[Items.Count];
					Items.CopyTo(elements);
					foreach (GraphPanelElement gpe in elements)
					{
						if (gpe is DragPanel)
						{
							((DragPanel)gpe).SetFocus(gpe == value);
							/*if (gpe==value)
							{
								Label lb = new Label();
								lb.Location = gpe.Location;
								lb.Visible = true;
								lb.Parent = this;

								this.ScrollControlIntoView(lb);
								lb.Parent = null;
								lb.Dispose();
							}*/
						}
					}
				}
			}
		}
		#endregion



		#region Event Override
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (update)
			{
				return;
			}

			base.OnPaint(e);
			GraphPanelElement.SetGraphicsMode(e.Graphics, true);
			foreach (GraphPanelElement c in Items)
			{
				c.OnPaint(e);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			bool hit = false;
			GraphPanelElement[] elements = new GraphPanelElement[Items.Count];
			Items.CopyTo(elements);
			for (int i = elements.Length - 1; i >= 0; i--)
			{
				GraphPanelElement c = elements[i];

				if (c is DragPanel)
				{
					if (!hit)
					{
						if (((DragPanel)c).OnMouseDown(e))
						{
							if (e.Button == MouseButtons.Left)
							{
								hit = true;
								((DragPanel)c).SetFocus(true);
								continue;
							}
						}
					}

					if (e.Button == MouseButtons.Left)
					{
						((DragPanel)c).SetFocus(false);
					}
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			for (int i = Items.Count - 1; i >= 0; i--)
			{
				GraphPanelElement c = Items[i];

				if (c is DragPanel)
				{
					if (((DragPanel)c).OnMouseMove(e))
					{
						break;
					}
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			for (int i = Items.Count - 1; i >= 0; i--)
			{
				GraphPanelElement c = Items[i];

				if (c is DragPanel)
				{
					if (((DragPanel)c).OnMouseUp(e))
					{
						break;
					}
				}
			}
		}

		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
		}

		#endregion

		void SetLinkLineMode()
		{
			foreach (GraphPanelElement gpe in Items)
			{
				gpe.ChangedParent();
			}
		}

		void SetLinkQuality()
		{
			foreach (GraphPanelElement gpe in Items)
			{
				gpe.ChangedParent();
			}
		}

		void SetSaveBound()
		{
			foreach (GraphPanelElement gpe in Items)
			{
				gpe.SaveBounds = SaveBounds;
			}
		}

		void SetLocked()
		{
			foreach (GraphPanelElement gpe in Items)
			{
				if (gpe is DragPanel)
				{
					((DragPanel)gpe).Movable = !LockItems;
				}
			}
		}

		private void li_ItemsChanged(object sender, EventArgs e)
		{
			if (!autosz)
			{
				return;
			}

			int mx = 0;
			int my = 0;
			foreach (GraphPanelElement gpe in Items)
			{
				mx = Math.Max(mx, gpe.Right);
				my = Math.Max(my, gpe.Bottom);
			}

			Width = Math.Max(mx, MinWidth);
			Height = Math.Max(my, MinHeight);
		}

		private void Parent_SizeChanged(object sender, EventArgs e)
		{
			MinWidth = Parent.ClientRectangle.Width;
			MinHeight = Parent.ClientRectangle.Height;
		}

		bool update;

		public void BeginUpdate()
		{
			update = true;
		}

		public void EndUpdate()
		{
			update = false;
			Refresh();
		}

		public void Clear()
		{
			while (Items.Count > 0)
			{
				GraphPanelElement l = Items[0];
				Items.RemoveAt(0);
				l.Clear();
				l.Parent = null;
			}

			Refresh();
		}

		/// <summary>
		/// Calculate the Radius of a Circle you can use to place Items on
		/// </summary>
		/// <param name="centersize">The Size of the Item that should be presented in the center of the Cricle</param>
		/// <param name="itemsize">The size of the Items that should sourrpund the circle</param>
		/// <param name="itemcount">The number of items that should surround the Center</param>
		/// <returns>The calculated Radius</returns>
		public static double GetPinCircleRadius(
			Size centersize,
			Size itemsize,
			int itemcount
		)
		{
			double alpha = Math.Max(
				0.01,
				Math.Min(Math.PI / 2, 2 * Math.PI / itemcount)
			);
			double l = Math.Max(itemsize.Width, itemsize.Height) * Math.Sqrt(2);
			double minl =
				(Math.Max(centersize.Width, centersize.Height) * Math.Sqrt(0.5)) + (l / 2);

			return Math.Max(l / (2 * Math.Sin(alpha)), minl);
		}

		/// <summary>
		/// Calculate sthe location of an Item on a Circle
		/// </summary>
		/// <param name="center">The centner of the Circle</param>
		/// <param name="r">The radius (as caluclated in <see cref="GetPinCircleRadius"/>)</param>
		/// <param name="nr">The number of the item on the circle</param>
		/// <param name="itemcount">Maximum Number of Items on the circle</param>
		/// <param name="itemsize">The Size of the Item</param>
		/// <returns>the point for the given Location</returns>
		public static Point GetItemLocationOnPinCricle(
			Point center,
			double r,
			int nr,
			int itemcount,
			Size itemsize
		)
		{
			double alpha = 2 * Math.PI / itemcount * nr;

			return new Point(
				center.X + (int)(Math.Cos(alpha) * r) - (itemsize.Width / 2),
				center.Y + (int)(Math.Sin(alpha) * r) - (itemsize.Height / 2)
			);
		}

		public static Point GetCenterLocationOnPinCircle(
			Point center,
			double r,
			Size itemsize
		)
		{
			return new Point(
				center.X - (itemsize.Width / 2),
				center.Y - (itemsize.Height / 2)
			);
		}
	}
}
