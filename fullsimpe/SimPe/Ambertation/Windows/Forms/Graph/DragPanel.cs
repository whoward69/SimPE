// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ambertation.Windows.Forms.Graph
{
	/// <summary>
	/// This is a Dragable Panel
	/// </summary>
	public abstract class DragPanel : GraphPanelElement
	{
		public DragPanel()
		{
			fnt = new Font(
				"Verdana",
				8.25F,
				FontStyle.Regular,
				GraphicsUnit.Point,
				0
			);
			//SetStyle(ControlStyles.Selectable, true);
			Down = false;
			lk = true;
			Focused = false;
		}

		#region Properties

		/// <summary>
		/// True, if the Mouse is currently down
		/// </summary>
		[Browsable(false)]
		public bool Down
		{
			get; private set;
		}

		public bool Focused
		{
			get; private set;
		}

		bool lk;
		public bool Movable
		{
			get => lk;
			set
			{
				if (lk != value)
				{
					lk = value;
					Invalidate();
				}
			}
		}

		Font fnt;
		public Font Font
		{
			get => fnt;
			set
			{
				if (fnt != value)
				{
					fnt = value;
					Invalidate();
				}
			}
		}
		#endregion


		Point lastpos;

		void SetMousePos(int x, int y)
		{
			lastpos = new Point(x, y);
		}

		#region Event Override
		protected MouseEventArgs FixMouseEventArgs(MouseEventArgs e)
		{
			return new MouseEventArgs(
				e.Button,
				e.Clicks,
				e.X - Left,
				e.Y - Top,
				e.Delta
			);
		}

		internal bool OnMouseDown(MouseEventArgs e)
		{
			if (!BoundingRectangle.Contains(e.X, e.Y))
			{
				return false;
			}

			if (e.Clicks == 1 && Click != null)
			{
				Click(this, new EventArgs());
			}
			else if (e.Clicks == 2 && DoubleClick != null)
			{
				DoubleClick(this, new EventArgs());
				return true;
			}
			else if (MouseDown != null)
			{
				MouseDown(this, FixMouseEventArgs(e));
			}

			if (!lk)
			{
				return true;
			}

			e = FixMouseEventArgs(e);

			if (e.Button == MouseButtons.Left)
			{
				Down = true;
				SetMousePos(e.X, e.Y);
			}

			return true;
		}

		internal bool OnMouseUp(MouseEventArgs e)
		{
			if (!BoundingRectangle.Contains(e.X, e.Y) && !Down)
			{
				return false;
			}

			if (MouseUp != null)
			{
				MouseUp(this, FixMouseEventArgs(e));
			}

			e = FixMouseEventArgs(e);
			Down = false;

			return true;
		}

		internal bool OnMouseMove(MouseEventArgs e)
		{
			if (!BoundingRectangle.Contains(e.X, e.Y) && !Down)
			{
				return false;
			}

			if (MouseMove != null)
			{
				MouseMove(this, FixMouseEventArgs(e));
			}

			if (!lk)
			{
				return true;
			}

			if (!Down)
			{
				return true;
			}

			e = FixMouseEventArgs(e);

			Point delta = new Point(Left + e.X - lastpos.X, Top + e.Y - lastpos.Y);

			SetBounds(delta.X, delta.Y, Width, Height);
			return true;
		}

		internal override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Down = false;
		}

		internal override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			SendToFront();
		}

		#endregion

		#region Events
		public event MouseEventHandler MouseMove;
		public event MouseEventHandler MouseUp;
		public event MouseEventHandler MouseDown;
		public event EventHandler DoubleClick;
		public event EventHandler Click;
		#endregion

		internal void SetFocus(bool val)
		{
			if (Focused != val)
			{
				Focused = val;
				if (Focused)
				{
					OnGotFocus(new EventArgs());
				}
				else
				{
					OnLostFocus(new EventArgs());
				}
			}
		}

		internal override void ChangedParent()
		{
			base.ChangedParent();
			if (Parent != null)
			{
				Movable = !Parent.LockItems;
			}
		}
	}
}
