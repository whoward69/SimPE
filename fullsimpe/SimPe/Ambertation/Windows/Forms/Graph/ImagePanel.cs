// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using System;
using System.Drawing;

namespace Ambertation.Windows.Forms.Graph
{
	/// <summary>
	/// This is a Image Panel
	/// </summary>
	public class ImagePanel : RoundedPanel
	{
		public ImagePanel()
			: base()
		{
			tborder = 2;
			txt = "";
			bg = Color.Gray;
		}

		#region public Properties
		Color bg;
		public Color ImagePanelColor
		{
			get => bg;
			set
			{
				if (bg != value)
				{
					bg = value;
					Invalidate();
				}
			}
		}
		Image thumb;
		public Image Image
		{
			get => thumb;
			set
			{
				thumb = value;
				Invalidate();
			}
		}

		string txt;
		public string Text
		{
			get => txt;
			set
			{
				txt = value;
				Invalidate();
			}
		}

		int tborder;
		public int ImageBorderWidth
		{
			get => tborder;
			set
			{
				tborder = value;
				Invalidate();
			}
		}
		#endregion

		#region Basic Draw Methods
		Rectangle ThumbnailRectangle
		{
			get
			{
				int tw = Width - 4 - (2 * tborder);
				int th = Height - 24 - (2 * tborder);
				if (thumb != null)
				{
					tw = thumb.Width;
					th = thumb.Height;
				}

				Rectangle trec = new Rectangle(
					(Width - tw) / 2,
					(Height - 16 - th) / 2,
					tw,
					th
				);
				return trec;
			}
		}

		protected void DrawThumbnail(
			System.Drawing.Graphics gr,
			Rectangle trec,
			int rad
		)
		{
			DrawThumbnail(
				gr,
				trec,
				rad,
				thumb,
				BorderColor,
				ImagePanelColor,
				GradientColor,
				FadeColor,
				Focused,
				tborder,
				tborder
			);
		}

		protected static void DrawThumbnail(
			System.Drawing.Graphics gr,
			Rectangle trec,
			int rad,
			Image thumb,
			Color borderColor,
			Color imagePanelColor,
			Color gradientColor,
			Color fadeColor,
			bool focused,
			int tborderx,
			int tbordery
		)
		{
			Rectangle srect = new Rectangle(
				trec.Left - tborderx,
				trec.Top - tbordery,
				trec.Width + (2 * tborderx),
				trec.Height + (2 * tbordery)
			);
			;
			DrawNiceRoundRectStart(
				gr,
				srect.X,
				srect.Y,
				srect.Width,
				srect.Height,
				rad,
				imagePanelColor,
				borderColor,
				gradientColor,
				fadeColor,
				focused
			);
			DrawNiceRoundRectEnd(
				gr,
				srect.X,
				srect.Y,
				srect.Width,
				srect.Height,
				rad,
				imagePanelColor,
				borderColor,
				gradientColor,
				fadeColor,
				focused
			);
			Drawing.GraphicRoutines.DrawRoundRect(
				gr,
				new Pen(borderColor),
				srect.X - 2,
				srect.Y - 2,
				srect.Width + 3,
				srect.Height + 3,
				rad
			);

			if (thumb != null)
			{
				gr.DrawImage(
					thumb,
					trec,
					new Rectangle(0, 0, thumb.Width, thumb.Height),
					GraphicsUnit.Pixel
				);
			}
		}

		public static Image CreateThumbnail(
			Image img,
			Size sz,
			int rad,
			Color borderColor,
			Color imagePanelColor,
			Color gradientColor,
			Color fadeColor,
			bool focused,
			int tborderx,
			int tbordery
		)
		{
			Bitmap b = new Bitmap(sz.Width, sz.Height);
			Rectangle trec = new Rectangle(
				new Point(tborderx + 2, tbordery + 2),
				new Size(sz.Width - (2 * tborderx) - 4, sz.Height - (2 * tbordery) - 4)
			);
			img = Drawing.GraphicRoutines.ScaleImage(
				img,
				trec.Width,
				trec.Height,
				true
			);
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);
			SetGraphicsMode(g, false);

			DrawThumbnail(
				g,
				trec,
				rad,
				img,
				borderColor,
				imagePanelColor,
				gradientColor,
				fadeColor,
				focused,
				tborderx,
				tbordery
			);

			g.Dispose();
			return b;
		}

		protected void DrawCaption(
			System.Drawing.Graphics gr,
			Rectangle r,
			Font f,
			bool center
		)
		{
			StringFormat sf = new StringFormat
			{
				FormatFlags = StringFormatFlags.NoWrap
			};

			string tx = Text;
			SizeF sz = gr.MeasureString(tx, Font);
			if (sz.Width > Width - 8 && Text.Length > 4)
			{
				int len = Text.Length - 4;
				while ((sz.Width > r.Width - 8) && Text.Length > len && len > 0)
				{
					tx = Text.Substring(0, len) + "...";
					sz = gr.MeasureString(tx, Font);
					len--;
				}
			}
			int shift = (int)((r.Height - sz.Height) / 2);
			int lshift = (int)((r.Width - 8 - sz.Width) / 2);
			if (!center)
			{
				lshift = 0;
			}

			gr.DrawString(
				tx,
				f,
				new Pen(ForeColor).Brush,
				new RectangleF(
					new PointF(r.Left + 4 + lshift, r.Top + shift + 1),
					new SizeF(
						Math.Min(r.Width - 8, r.Width - 8 - (2 * lshift)),
						Math.Min(r.Height, r.Height - (2 * shift))
					)
				),
				sf
			);
		}

		protected override void UserDraw(System.Drawing.Graphics gr)
		{
			Rectangle trec = ThumbnailRectangle;
			int rad = Math.Min(Math.Min(8, trec.Height / 2), trec.Width / 2);

			DrawThumbnail(gr, trec, rad);

			DrawNiceRoundRect(gr, 0, Height - 16, Width, 16, 8, PanelColor);
			DrawCaption(gr, new Rectangle(0, Height - 16, Width, 16), Font, true);
		}

		#endregion

		public Size BestSize(Size imgsize)
		{
			return BestSize(imgsize.Width, imgsize.Height);
		}

		public Size BestSize(int imgwidth, int imgheight)
		{
			return new Size(
				imgwidth + (2 * tborder) + 5,
				imgheight + (2 * tborder) + 5 + 19
			);
		}

		protected override void InitDocks()
		{
			if (docks == null)
			{
				docks = new DockPoint[10];

				docks[0] = new DockPoint(0, 0, LinkControlType.MiddleLeft);
				docks[1] = new DockPoint(0, 0, LinkControlType.MiddleRight);

				docks[2] = new DockPoint(0, 0, LinkControlType.TopCenter);
				docks[3] = new DockPoint(0, 0, LinkControlType.TopLeft);
				docks[4] = new DockPoint(0, 0, LinkControlType.TopRight);

				docks[5] = new DockPoint(0, 0, LinkControlType.BottomCenter);
				docks[6] = new DockPoint(0, 0, LinkControlType.BottomLeft);
				docks[7] = new DockPoint(0, 0, LinkControlType.BottomRight);

				docks[8] = new DockPoint(0, 0, LinkControlType.MiddleLeft);
				docks[9] = new DockPoint(0, 0, LinkControlType.MiddleRight);
			}
		}

		protected override void SetupDocks()
		{
			if (docks == null)
			{
				InitDocks();
			}

			Rectangle trec = ThumbnailRectangle;
			trec = new Rectangle(
				trec.Left - tborder - 2,
				trec.Top - tborder - 2,
				trec.Width + (2 * tborder) + 4,
				trec.Height + (2 * tborder) + 4
			);
			;
			Rectangle prec = new Rectangle(0, Height - 16, Width, 16);

			docks[0].X = Left + prec.Left;
			docks[0].Y = Top + prec.Top + (prec.Height / 2);
			docks[1].X = Left + prec.Left + prec.Width;
			docks[1].Y = Top + prec.Top + (prec.Height / 2);

			docks[2].X = Left + trec.Left + (trec.Width / 2);
			docks[2].Y = Top + trec.Top;
			docks[3].X = Left + trec.Left;
			docks[3].Y = Top + trec.Top;
			docks[4].X = Left + trec.Left + trec.Width;
			docks[4].Y = Top + trec.Top;

			docks[5].X = Left + prec.Left + (prec.Width / 2);
			docks[5].Y = Top + prec.Bottom;
			docks[6].X = Left + prec.Left;
			docks[6].Y = Top + prec.Bottom;
			docks[7].X = Left + prec.Left + prec.Width;
			docks[7].Y = Top + prec.Bottom;

			docks[8].X = Left + trec.Left;
			docks[8].Y = Top + trec.Top + (trec.Height / 2);
			docks[9].X = Left + trec.Left + trec.Width;
			docks[9].Y = Top + trec.Top + (trec.Height / 2);
		}
	}
}
