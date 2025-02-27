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
	/// Summary description for NgbhItemsListViewItem.
	/// </summary>
	public class NgbhItemsListView : UserControl
	{
		private IContainer components;
		private Panel panel1;
		private ComboBox cbadd;
		private LinkLabel lladd;
		private LinkLabel lldel;
		private Button btUp;
		private Button btDown;
		private ToolStripMenuItem miCopy;
		private ToolStripMenuItem miPaste;
		private ContextMenuStrip menu;
		private ToolStripMenuItem miPasteGossip;
		private ToolStripMenuItem miClone;
		private ToolStripMenuItem miDelCascade;
		private ToolStripSeparator toolStripMenuItem2;
		private ListView lv;
		private CheckBox cbnogoss;

		ThemeManager tm;

		public NgbhItemsListView()
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

			SmallImageList = new ImageList();
			SmallImageList.ImageSize = new Size(NgbhItem.ICON_SIZE, NgbhItem.ICON_SIZE);
			SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

			lv.SelectedIndexChanged += new EventHandler(lv_SelectedIndexChanged);

			SlotType = Data.NeighborhoodSlots.Sims;

			tm = ThemeManager.Global.CreateChild();
			tm.AddControl(menu);
			InitTheo();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (tm != null)
				{
					tm.Clear();
					tm.Parent = null;
					tm = null;
				}
				if (clipboard != null)
				{
					clipboard.Clear();
				}

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
				new ComponentResourceManager(
					typeof(NgbhItemsListView)
				);
			this.lv = new ListView();
			this.menu = new ContextMenuStrip(this.components);
			this.miCopy = new ToolStripMenuItem();
			this.miPaste = new ToolStripMenuItem();
			this.miPasteGossip = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.miClone = new ToolStripMenuItem();
			this.miDelCascade = new ToolStripMenuItem();
			this.panel1 = new Panel();
			this.cbnogoss = new CheckBox();
			this.lladd = new LinkLabel();
			this.cbadd = new ComboBox();
			this.lldel = new LinkLabel();
			this.btUp = new Button();
			this.btDown = new Button();
			this.menu.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			//
			// lv
			//
			this.lv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lv.ContextMenuStrip = this.menu;
			resources.ApplyResources(this.lv, "lv");
			this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv.HideSelection = false;
			this.lv.Name = "lv";
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.List;
			this.lv.SelectedIndexChanged += new EventHandler(
				this.lv_SelectedIndexChanged_1
			);
			//
			// menu
			//
			this.menu.Items.AddRange(
				new ToolStripItem[]
				{
					this.miCopy,
					this.miPaste,
					this.miPasteGossip,
					this.toolStripMenuItem2,
					this.miClone,
					this.miDelCascade,
				}
			);
			this.menu.Name = "menu";
			resources.ApplyResources(this.menu, "menu");
			this.menu.VisibleChanged += new EventHandler(
				this.menu_VisibleChanged
			);
			//
			// miCopy
			//
			resources.ApplyResources(this.miCopy, "miCopy");
			this.miCopy.Name = "miCopy";
			this.miCopy.Click += new EventHandler(this.CopyItems);
			//
			// miPaste
			//
			resources.ApplyResources(this.miPaste, "miPaste");
			this.miPaste.Name = "miPaste";
			this.miPaste.Click += new EventHandler(this.PasteItems);
			//
			// miPasteGossip
			//
			this.miPasteGossip.Name = "miPasteGossip";
			resources.ApplyResources(this.miPasteGossip, "miPasteGossip");
			this.miPasteGossip.Click += new EventHandler(
				this.PasteItemsAsGossip
			);
			//
			// toolStripMenuItem2
			//
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
			//
			// miClone
			//
			this.miClone.Name = "miClone";
			resources.ApplyResources(this.miClone, "miClone");
			this.miClone.Click += new EventHandler(this.CloneItem);
			//
			// miDelCascade
			//
			resources.ApplyResources(this.miDelCascade, "miDelCascade");
			this.miDelCascade.Name = "miDelCascade";
			this.miDelCascade.Click += new EventHandler(this.DeleteCascadeItems);
			//
			// panel1
			//
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.cbnogoss);
			this.panel1.Controls.Add(this.lladd);
			this.panel1.Controls.Add(this.cbadd);
			this.panel1.Controls.Add(this.lldel);
			this.panel1.Controls.Add(this.btUp);
			this.panel1.Controls.Add(this.btDown);
			this.panel1.Name = "panel1";
			//
			// cbnogoss
			//
			resources.ApplyResources(this.cbnogoss, "cbnogoss");
			this.cbnogoss.Name = "cbnogoss";
			this.cbnogoss.UseVisualStyleBackColor = true;
			this.cbnogoss.CheckedChanged += new EventHandler(
				this.cbnogoss_CheckedChanged
			);
			//
			// lladd
			//
			resources.ApplyResources(this.lladd, "lladd");
			this.lladd.Name = "lladd";
			this.lladd.TabStop = true;
			this.lladd.UseCompatibleTextRendering = true;
			this.lladd.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.lladd_LinkClicked
				);
			//
			// cbadd
			//
			resources.ApplyResources(this.cbadd, "cbadd");
			this.cbadd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbadd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cbadd.Name = "cbadd";
			this.cbadd.SelectedIndexChanged += new EventHandler(
				this.cbadd_SelectedIndexChanged
			);
			//
			// lldel
			//
			resources.ApplyResources(this.lldel, "lldel");
			this.lldel.Name = "lldel";
			this.lldel.TabStop = true;
			this.lldel.LinkClicked +=
				new LinkLabelLinkClickedEventHandler(
					this.lldel_LinkClicked
				);
			//
			// btUp
			//
			resources.ApplyResources(this.btUp, "btUp");
			this.btUp.Name = "btUp";
			this.btUp.Click += new EventHandler(this.btUp_Click);
			//
			// btDown
			//
			resources.ApplyResources(this.btDown, "btDown");
			this.btDown.Name = "btDown";
			this.btDown.Click += new EventHandler(this.btDown_Click);
			//
			// NgbhItemsListView
			//
			this.Controls.Add(this.lv);
			this.Controls.Add(this.panel1);
			resources.ApplyResources(this, "$this");
			this.Name = "NgbhItemsListView";
			this.menu.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}

		#endregion

		Data.NeighborhoodSlots st;
		public Data.NeighborhoodSlots SlotType
		{
			get
			{
				return st;
			}
			set
			{
				if (st != value)
				{
					st = value;
					SetContent();
				}
			}
		}

		bool cc = false;

		[Category("Appearance")]
		[DefaultValue(typeof(bool), "false")]
		[Browsable(true)]
		public bool ShowGossip
		{
			get
			{
				return cc;
			}
			set
			{
				cc = value;
				this.cbnogoss.Visible = cc;
			}
		}

		public NgbhSlotList Slot
		{
			get
			{
				if (NgbhItems == null)
				{
					return null;
				}
				else
				{
					return NgbhItems.Parent;
				}
			}
			set
			{
				if (value != null)
				{
					NgbhItems = value.GetItems(this.SlotType);
				}
				else
				{
					NgbhItems = null;
				}
			}
		}

		Collections.NgbhItems items;

		[Browsable(false)]
		public Collections.NgbhItems NgbhItems
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
				SetContent();
			}
		}

		void SetContent()
		{
			this.Clear();
			this.cbadd.Items.Clear();

			if (items != null)
			{
				lv.BeginUpdate();
				foreach (NgbhItem i in items)
				{
					if (cbnogoss.Checked)
					{
						if (!i.IsGossip)
						{
							AddItemToList(i);
						}
					}
					else
					{
						AddItemToList(i);
					}
				}
				lv.EndUpdate();

				SetAvailableAddTypes();
			}
		}

		public void Refresh(bool full)
		{
			if (full)
			{
				SetContent();
			}

			base.Refresh();
		}

		public new void Refresh()
		{
			Refresh(true);
		}

		void AddItemToList(NgbhItem item)
		{
			if (item == null)
			{
				return;
			}

			NgbhItemsListViewItem lvi = new NgbhItemsListViewItem(this, item);
		}

		void InsertItemToList(int index, NgbhItem item)
		{
			if (item == null)
			{
				return;
			}

			NgbhItemsListViewItem lvi = new NgbhItemsListViewItem(this, item, false);
			lv.Items.Insert(index, lvi);
		}

		void SetAvailableAddTypes()
		{
			string prefix =
				typeof(SimMemoryType).Namespace
				+ "."
				+ typeof(SimMemoryType).Name
				+ ".";
			SimMemoryType[] sts = Ngbh.AllowedMemoryTypes(st);
			foreach (SimMemoryType mst in sts)
			{
				cbadd.Items.Add(
					new Data.Alias(
						(uint)mst,
						SimPe.Localization.GetString(prefix + mst.ToString()),
						"{name}"
					)
				);
			}

			if (cbadd.Items.Count > 0)
			{
				cbadd.SelectedIndex = 0;
			}
		}

		public void Clear()
		{
			lv.Clear();
			if (lv.SmallImageList != null)
			{
				lv.SmallImageList.Images.Clear();
			}
		}

		[Browsable(false)]
		public NgbhItemsListViewItem SelectedItem
		{
			get
			{
				if (lv.SelectedItems.Count == 0)
				{
					return null;
				}

				if (lv.FocusedItem != null)
				{
					if (lv.FocusedItem.Selected)
					{
						return lv.FocusedItem as NgbhItemsListViewItem;
					}
				}

				return lv.SelectedItems[0] as NgbhItemsListViewItem;
			}
		}

		[Browsable(false)]
		public NgbhItem SelectedNgbhItem
		{
			get
			{
				if (SelectedItem == null)
				{
					return null;
				}

				return SelectedItem.Item;
			}
		}

		[Browsable(false)]
		public Collections.NgbhItems SelectedNgbhItems
		{
			get
			{
				NgbhSlotList parent = null;
				if (items != null)
				{
					parent = items.Parent;
				}

				Collections.NgbhItems ret = new Collections.NgbhItems(parent);
				foreach (NgbhItemsListViewItem lvi in lv.SelectedItems)
				{
					ret.Add(lvi.Item);
				}

				return ret;
			}
		}

		[Browsable(false)]
		public bool SelectedMultiple => lv.SelectedItems.Count > 1;

		internal void UpdateSelected(NgbhItem item)
		{
			if (item == null)
			{
				return;
			}

			if (SelectedItem == null)
			{
				return;
			}

			SelectedItem.Update();
			this.Refresh(false);
		}

		public ListView.ListViewItemCollection Items => lv.Items;

		ImageList sil;
		public ImageList SmallImageList
		{
			get
			{
				return sil;
			}
			set
			{
				lv.SmallImageList = value;
				sil = value;
			}
		}

		public event EventHandler SelectedIndexChanged;

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, e);
			}
		}

		private void cbadd_SelectedIndexChanged(object sender, EventArgs e)
		{
			lladd.Enabled = cbadd.SelectedIndex >= 0 && items != null;
		}

		private void lv_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			lldel.Enabled = (lv.SelectedItems.Count > 0 && items != null);
			if (lv.Items.Count == 0 || lv.SelectedItems.Count != 1 || items == null)
			{
				btUp.Enabled = false;
				btDown.Enabled = false;
			}
			else
			{
				btUp.Enabled = lldel.Enabled && !lv.Items[0].Selected;
				btDown.Enabled =
					lldel.Enabled && !lv.Items[lv.Items.Count - 1].Selected;
			}
		}

		private void lladd_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			if (items == null || cbadd.SelectedIndex < 0)
			{
				return;
			}

			Data.Alias a = cbadd.SelectedItem as Data.Alias;
			SimMemoryType smt = (SimMemoryType)a.Id;

			int index = this.NextItemIndex(true);
			NgbhItem item = items.InsertNew(index, smt);
			item.SetInitialGuid(smt);
			InsertItemToList(index, item);

			lv.Items[index].Selected = true;
			lv.Items[index].EnsureVisible();
		}

		private void lldel_LinkClicked(
			object sender,
			LinkLabelLinkClickedEventArgs e
		)
		{
			if (lv.SelectedItems.Count == 0 || items == null)
			{
				return;
			}
			//NgbhItemsListViewItem item = this.SelectedItem;
			Collections.NgbhItems nitems = this.SelectedNgbhItems;
			items.Remove(nitems);

			for (int i = lv.SelectedItems.Count; i > 0; i--)
			{
				lv.Items.Remove(lv.SelectedItems[0]);
			}
		}

		void SwapListViewItem(int i1, int i2)
		{
			if (i1 < 0 || i2 < 0 || i1 >= lv.Items.Count || i2 >= lv.Items.Count)
			{
				return;
			}

			ListViewItem o1 = lv.Items[i1];
			ListViewItem o2 = lv.Items[i2];

			lv.Items[i1] = new ListViewItem();
			lv.Items[i2] = o1;
			lv.Items[i1] = o2;
		}

		int SelectedIndex
		{
			get
			{
				if (lv.SelectedIndices.Count == 0)
				{
					return -1;
				}

				return lv.SelectedIndices[0];
			}
		}

		private void btUp_Click(object sender, EventArgs e)
		{
			int index = SelectedIndex;
			items.Swap(index, index - 1);
			SwapListViewItem(index, index - 1);
		}

		private void btDown_Click(object sender, EventArgs e)
		{
			int index = SelectedIndex;
			items.Swap(index, index + 1);
			SwapListViewItem(index, index + 1);
		}

		private void cbnogoss_CheckedChanged(object sender, EventArgs e)
		{
			SetContent();
		}

		#region Extensions by Theo
		Queue clipboard;

		void InitTheo()
		{
			clipboard = new Queue();
		}

		void CopyItems(object sender, EventArgs e)
		{
			CopyItems();
		}

		void CopyItems()
		{
			Collections.NgbhItems selitems = SelectedNgbhItems;
			if (selitems.Count > 0)
			{
				this.Cursor = Cursors.WaitCursor;
				try
				{
					clipboard.Clear();
					foreach (NgbhItem item in selitems)
					{
						clipboard.Enqueue(item);
					}
				}
				catch (Exception exception1)
				{
					this.Cursor = Cursors.Default;
					Helper.ExceptionMessage(
						Localization.Manager.GetString("errconvert"),
						exception1
					);
				}
				this.Cursor = Cursors.Default;
			}
		}

		void PasteItems(object sender, EventArgs e)
		{
			PasteItems(false);
		}

		void PasteItemsAsGossip(object sender, EventArgs e)
		{
			PasteItems(true);
		}

		void PasteItems(bool asgossip)
		{
			int itemIndex = this.NextItemIndex(false);
			this.Cursor = Cursors.WaitCursor;
			Queue newq = new Queue();
			try
			{
				while (clipboard.Count > 0)
				{
					NgbhItem item = clipboard.Dequeue() as NgbhItem;
					newq.Enqueue(item);

					if (item != null)
					{
						item = item.Clone(this.Slot);

						if (item.IsMemory && item.OwnerInstance > 0 && !asgossip)
						{
							item.OwnerInstance = (ushort)items.Parent.SlotID;
						}

						if (asgossip)
						{
							item.Flags.IsVisible = false;
						}

						AddItemAfterSelected(item);
					}
				}
			}
			catch (Exception exception1)
			{
				this.Cursor = Cursors.Default;
				Helper.ExceptionMessage(
					Localization.Manager.GetString("errconvert"),
					exception1
				);
			}

			clipboard.Clear();
			clipboard = newq;
			this.Cursor = Cursors.Default;
		}

		void AddItemAfterSelected(NgbhItem item)
		{
			//if (this.lv.SelectedItems.Count > 0)
			{
				this.Cursor = Cursors.WaitCursor;
				try
				{
					int selectedIndex = this.NextItemIndex(true);

					NgbhItems.Insert(selectedIndex, item);
					this.AddItemAt(item, selectedIndex);
					this.lv.Items[selectedIndex].Selected = true;
					this.lv.Items[selectedIndex].EnsureVisible();
				}
				catch (Exception exception1)
				{
					this.Cursor = Cursors.Default;
					Helper.ExceptionMessage(
						Localization.Manager.GetString("errconvert"),
						exception1
					);
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void AddItemAt(NgbhItem item, int index)
		{
			this.InsertItemToList(index, item);
			this.lv.SelectedItems.Clear();
			this.lv.Items[index].Selected = true;
			this.lv.Items[index].EnsureVisible();
			if (this.SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, new EventArgs());
			}
		}

		int NextItemIndex(bool clearSelection)
		{
			int selectedIndex = this.lv.Items.Count - 1;

			// get index of the last selected item (if any)
			if (this.lv.SelectedIndices.Count > 0)
			{
				selectedIndex = this.lv.SelectedIndices[
					this.lv.SelectedIndices.Count - 1
				];
			}

			// deselect previous (if applicable)
			if (clearSelection)
			{
				this.lv.SelectedItems.Clear();
			}

			// should not exceed the number of items (?)
			selectedIndex = Math.Min(++selectedIndex, this.lv.Items.Count);

			return selectedIndex;
		}

		void CloneItem(object sender, EventArgs e)
		{
			CloneItem();
		}

		void CloneItem()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				this.Cursor = Cursors.WaitCursor;
				try
				{
					// this command operates on a single item only;
					// to avoid ambiguity, use the focused item
					NgbhItem item = this.GetFocusedItem();
					if (item != null)
					{
						int itemIndex = this.lv.FocusedItem.Index + 1;
						NgbhItem item1 = item.Clone();

						items.Insert(itemIndex, item1);
						this.AddItemAt(item1, itemIndex);

						this.lv.FocusedItem.Focused = false;
					}
				}
				catch (Exception exception1)
				{
					this.Cursor = Cursors.Default;
					Helper.ExceptionMessage(
						Localization.Manager.GetString("errconvert"),
						exception1
					);
				}
				this.Cursor = Cursors.Default;
			}
		}

		NgbhItem GetFocusedItem()
		{
			NgbhItemsListViewItem li = this.SelectedItem;
			if (li == null)
			{
				return null;
			}

			return li.Item;
		}

		public void SelectMemoriesByGuid(Collections.NgbhItems items)
		{
			if (items.Length > 0)
			{
				this.lv.Enabled = false;

				ArrayList guidList = new ArrayList();
				foreach (NgbhItem item in items)
				{
					if (!guidList.Contains(item.Guid))
					{
						guidList.Add(item.Guid);
					}
				}

				foreach (ListViewItem li in this.lv.Items)
				{
					NgbhItem item = li.Tag as NgbhItem;
					if (guidList.Contains(item.Guid))
					{
						li.Selected = true;
					}
				}

				this.lv.Enabled = true;
			}
		}

		void DeleteItems(object sender, EventArgs e)
		{
			this.DeleteItems(false);
		}

		void DeleteCascadeItems(object sender, EventArgs e)
		{
			this.DeleteItems(true);
		}

		void DeleteItems(bool cascade)
		{
			if (lv.SelectedItems.Count != 0)
			{
				this.Cursor = Cursors.WaitCursor;
				try
				{
					ArrayList items = new ArrayList();
					foreach (ListViewItem li in lv.SelectedItems)
					{
						items.Add(li);
					}

					Collections.NgbhItems memoryItems = this.SelectedNgbhItems;

					if (cascade)
					{
						((EnhancedNgbh)Slot.Parent).DeleteMemoryEchoes(
							memoryItems,
							Slot.SlotID
						);
					}

					memoryItems[0].ParentSlot.ItemsB.Remove(memoryItems);

					foreach (ListViewItem li in items)
					{
						lv.Items.Remove(li);
					}

					lv.SelectedItems.Clear();
				}
				catch (Exception exception1)
				{
					this.Cursor = Cursors.Default;
					Helper.ExceptionMessage(
						Localization.Manager.GetString("errconvert"),
						exception1
					);
				}
				this.Cursor = Cursors.Default;
			}
		}

		void menu_VisibleChanged(object sender, EventArgs e)
		{
			try
			{
				miCopy.Enabled = lv.SelectedItems.Count > 0;
				miClone.Enabled = miCopy.Enabled;
				miPaste.Enabled = clipboard.Count > 0;

				if (
					((NgbhSlot)items.Parent).Type == Data.NeighborhoodSlots.Sims
					|| ((NgbhSlot)items.Parent).Type
						== Data.NeighborhoodSlots.SimsIntern
				)
				{
					miDelCascade.Enabled = miCopy.Enabled;
					miPasteGossip.Enabled = miPaste.Enabled;
				}
				else
				{
					miDelCascade.Enabled = false;
					miPasteGossip.Enabled = false;
				}
			}
			catch
			{
				miCopy.Enabled =
					miPaste.Enabled =
					miPasteGossip.Enabled =
					miClone.Enabled =
					miDelCascade.Enabled =
						false;
			}
		}
		#endregion
	}
}
