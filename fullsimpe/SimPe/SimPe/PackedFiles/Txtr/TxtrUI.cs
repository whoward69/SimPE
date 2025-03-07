// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using SimPe.Interfaces.Plugin;
using SimPe.Plugin;

namespace SimPe.PackedFiles.Txtr
{
	/// <summary>
	/// This class is used to fill the UI for this FileType with Data
	/// </summary>
	public class TxtrUI : IPackedFileUI
	{
		#region Code to Startup the UI

		/// <summary>
		/// Holds a reference to the Form containing the UI Panel
		/// </summary>
		internal TxtrForm form;

		/// <summary>
		/// Constructor for the Class
		/// </summary>
		public TxtrUI()
		{
			form = new TxtrForm();

			form.cbformats.Items.Add(ImageLoader.TxtrFormats.Unknown);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.ExtRaw8Bit);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.Raw8Bit);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.Raw24Bit);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.ExtRaw24Bit);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.Raw32Bit);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.DXT1Format);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.DXT3Format);
			form.cbformats.Items.Add(ImageLoader.TxtrFormats.DXT5Format);
		}
		#endregion

		#region IPackedFileUI Member

		/// <summary>
		/// Returns the Panel that will be displayed within SimPe
		/// </summary>
		public System.Windows.Forms.Control GUIHandle => form.txtrPanel;

		/// <summary>
		/// Is called by SimPe (through the Wrapper) when the Panel is going to be displayed, so
		/// you should updatet the Data displayed by the Panel with the Attributes stored in the
		/// passed Wrapper.
		/// </summary>
		/// <param name="wrapper">The Attributes of this Wrapper have to be displayed</param>
		public void UpdateGUI(IFileWrapper wrapper)
		{
			Txtr wrp = (Txtr)wrapper;
			if (wrp.Blocks.Length == 0)
			{
				wrp.Blocks = new Interfaces.Scenegraph.IRcolBlock[1];
				wrp.Blocks[0] = new ImageData(wrp);
			}
			form.wrapper = wrp;

			form.btex.Enabled = false;
			form.lbimg.Items.Clear();
			form.cbitem.Items.Clear();
			form.cbmipmaps.Items.Clear();
			form.lldel.Enabled = false;

			foreach (ImageData id in wrp.Blocks)
			{
				form.cbitem.Items.Add(id);
			}

			if (form.cbitem.Items.Count > 0)
			{
				form.cbitem.SelectedIndex = 0;
			}
			//if (form.lbimg.Items.Count>0) form.lbimg.SelectedIndex = 0;
		}

		#endregion

		#region IDisposable Member
		public virtual void Dispose()
		{
			form.Dispose();
		}
		#endregion
	}
}
