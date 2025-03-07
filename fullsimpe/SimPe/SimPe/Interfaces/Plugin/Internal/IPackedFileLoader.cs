// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later
using SimPe.Interfaces.Files;

namespace SimPe.Interfaces.Plugin.Internal
{
	/// <summary>
	/// This Interface Implements Methods that must be provided by a PackedFile Wrapper
	/// </summary>
	/// <remarks>If you want to Implement a Wrapper you must use the SimPe.Interfaces.Plugin.IFileWrapper</remarks>
	public interface IPackedFileWrapper : IWrapper, IPackedFileName, System.IDisposable
	{
		/// <summary>
		/// Returns the Package where this File is assigned to (can be null)
		/// </summary>
		IPackageFile Package
		{
			get;
			//set;
		}

		/// <summary>
		/// Returns the FileDescriptor Associated with the File
		/// </summary>
		/// <remarks>
		/// When the Descriptor is returned, make sure that the userdata is not out of Data;
		/// </remarks>
		IPackedFileDescriptor FileDescriptor
		{
			get;
		}

		/// <summary>
		/// Processe the Data stored in the sent File
		/// </summary>
		/// <param name="item">Contains a Scenegraph Item (which combines a FileDescriptor with a Package)</param>
		void ProcessData(Scenegraph.IScenegraphFileIndexItem item);

		/// <summary>
		/// Processe the Data stored in the sent File
		/// </summary>
		/// <param name="pfd">Description of the sent File</param>
		/// <param name="package">The Package containing the File</param>
		/// <param name="file">The data of the file (if null, the Method must try to read it from the packge!)</param>
		void ProcessData(
			IPackedFileDescriptor pfd,
			IPackageFile package,
			IPackedFile file
		);

		/// <summary>
		/// Processe the Data stored in the described File
		/// </summary>
		/// <param name="pfd">Description of the sent File</param>
		/// <param name="package">The Package containing the File</param>
		/// <remarks>The Files's data must be read from the package</remarks>
		void ProcessData(IPackedFileDescriptor pfd, IPackageFile package);

		/// <summary>
		/// Processe the Data stored in the sent File
		/// </summary>
		/// <param name="item">Contains a Scenegraph Item (which combines a FileDescriptor with a Package)</param>
		/// <param name="catchex">true, if the Method should handle all occuring Exceptions</param>
		void ProcessData(
			Scenegraph.IScenegraphFileIndexItem item,
			bool catchex
		);

		/// <summary>
		/// Processe the Data stored in the sent File
		/// </summary>
		/// <param name="pfd">Description of the sent File</param>
		/// <param name="package">The Package containing the File</param>
		/// <param name="file">The data of the file (if null, the Method must try to read it from the packge!)</param>
		/// <param name="catchex">true, if the Method should handle all occuring Exceptions</param>
		void ProcessData(
			IPackedFileDescriptor pfd,
			IPackageFile package,
			IPackedFile file,
			bool catchex
		);

		/// <summary>
		/// Processe the Data stored in the described File
		/// </summary>
		/// <param name="pfd">Description of the sent File</param>
		/// <param name="package">The Package containing the File</param>
		/// <remarks>The Files's data must be read from the package</remarks>
		/// <param name="catchex">true, if the Method should handle all occuring Exceptions</param>
		void ProcessData(IPackedFileDescriptor pfd, IPackageFile package, bool catchex);

		/// <summary>
		/// Will return current Data
		/// </summary>
		System.IO.BinaryReader StoredData
		{
			get;
		}

		/// <summary>
		/// Returns / Sets the assigned UI Handler (can be null!)
		/// </summary>
		/// <remarks>If you set this value to null, it will Return the Default UIHandler or null if no default exists</remarks>
		IPackedFileUI UIHandler
		{
			get; set;
		}

		/// <summary>
		/// Processes the stored Data again and Sends an Update Request to the assigned UI Handler (if not null)
		/// </summary>
		void Refresh();

		/// <summary>
		/// Sends an Update Request to the assigned UI Handler (if not null)
		/// </summary>
		void RefreshUI();

		/// <summary>
		/// Initializes the GUI for this Wrapper, and Updates the it's content
		/// </summary>
		void LoadUI();

		/// <summary>
		/// Tries to correct Possible Errors.
		/// </summary>
		/// <param name="registry">The Wrapper Registry</param>
		void Fix(IWrapperRegistry registry);

		/// <summary>
		/// Return the content of the Files
		/// </summary>
		System.IO.MemoryStream Content
		{
			get;
		}

		/// <summary>
		/// Returns the default Extension for this File
		/// </summary>
		string FileExtension
		{
			get;
		}

		/// <summary>
		/// Allways returns <see cref="this"/> Object.
		/// </summary>
		/// <returns>this Object</returns>
		/// <remarks>
		/// This Method is Important, when a Wrapper implements <see cref="IMultiplePackedFileWrapper"/>, as
		/// it will create a new Instance of the Class it was called from.
		/// </remarks>
		IFileWrapper Activate();
	}
}
