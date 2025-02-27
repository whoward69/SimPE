using System;
using System.Collections;

using SimPe.Data;
using SimPe.Interfaces.Files;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	/// <summary>
	/// Performs the Sim Surgery
	/// </summary>
	public class PlasticSurgery
	{
		IPackageFile patient;
		IPackageFile archetype;
		IPackageFile ngbh;

		SDesc spatient;
		SDesc sarchetype;

		bool fromTemplate = false;

		/// <summary>
		/// Init the Plastic Surgery
		/// </summary>
		/// <param name="ngbh">The Neighborhood the Sim lives in (needed for DNA)</param>
		/// <param name="patient">The Sim that should be changed</param>
		/// <param name="archetype">The Template Sime</param>
		public PlasticSurgery(
			IPackageFile ngbh,
			IPackageFile patient,
			SDesc spatient,
			IPackageFile archetype,
			SDesc sarchetype
		)
		{
			this.patient = patient;
			this.archetype = archetype;
			this.ngbh = ngbh;

			this.spatient = spatient;
			this.sarchetype = sarchetype;
			this.fromTemplate = (this.sarchetype == null);
		}

		/// <summary>
		/// Returns the Hasvalue used for the Patient
		/// </summary>
		/// <returns></returns>
		uint GetPatientHash()
		{
			Random rn = new Random();
			uint hashgroup = (uint)((uint)rn.Next(0xffffff) | 0xff000000);
			foreach (IPackedFileDescriptor pfd in patient.Index)
			{
				///This is a scenegraph Resource so get the Hash from there!
				if (MetaData.RcolList.Contains(pfd.Type))
				{
					Rcol rcol = new GenericRcol(null, false);
					rcol.ProcessData(pfd, patient);
					hashgroup = Hashes.GroupHash(rcol.FileName);
					break;
				}
			}
			return hashgroup;
		}

		#region simple Clone
		/// <summary>
		/// Create a cloned Sim
		/// </summary>
		/// <returns>the new Package for the patient Sim</returns>
		public Packages.GeneratableFile CloneSim()
		{
			Packages.GeneratableFile ret =
				Packages.File.LoadFromFile((string)null);

			ArrayList list = new ArrayList
			{
				(uint)0xAC506764, //3IDR
				MetaData.GZPS, //GZPS, Property Set
				(uint)0xAC598EAC, //AGED
				(uint)0xCCCEF852, //LxNR, Face
				(uint)0x856DDBAC, //IMG
				(uint)0x534C4F54 //SLOT
			};
			list.AddRange(MetaData.RcolList);

			uint hashgroup = this.GetPatientHash();

			foreach (IPackedFileDescriptor pfd in archetype.Index)
			{
				if (list.Contains(pfd.Type))
				{
					IPackedFile fl = archetype.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);

					///This is a scenegraph Resource and needs a new Hash
					if (MetaData.RcolList.Contains(pfd.Type))
					{
						Rcol rcol = new GenericRcol(null, false);
						rcol.ProcessData(newpfd, ret);

						rcol.FileName =
							"#0x"
							+ Helper.HexString(hashgroup)
							+ "!"
							+ Hashes.StripHashFromName(rcol.FileName);

						switch (pfd.Type)
						{
							case MetaData.SHPE:
							{
								Shape shp = (Shape)rcol.Blocks[0];
								foreach (ShapeItem i in shp.Items)
								{
									i.FileName =
										"#0x"
										+ Helper.HexString(hashgroup)
										+ "!"
										+ Hashes.StripHashFromName(i.FileName);
								}
								break;
							}
						}
						rcol.SynchronizeUserData();
					}
				}
			}

			list.Add((uint)0xE86B1EEF); //make sure the compressed Directory won't be copied!
			if (this.fromTemplate)
			{
				list.Remove(0x534C4F54u); //SLOT file must remain
				list.Remove(0x856DDBACu); // same with IMG
			}

			foreach (IPackedFileDescriptor pfd in patient.Index)
			{
				if (!list.Contains(pfd.Type))
				{
					IPackedFile fl = patient.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);
				}
			}

			//Copy DNA File (if applicable)

			// TODO: Generate new DNA
			/*
			* The game somehow needs the DNA file for the skintone to match the
			* archetype's skintone, or else the patient's skintone will remain.
			* A way to to this in the absence of a SDesc instance is to scan
			* the archetype's PropertySet for the skintone/hair/eyecolor entries
			* and create a DNA file from scratch.
			* (The eyecolor actually resides in the AGED file (0xAC598EAC)
			*
			*/
			IPackedFileDescriptor dna = null;
			if (!this.fromTemplate)
			{
				dna = ngbh.FindFile(
					0xEBFEE33F,
					0,
					MetaData.LOCAL_GROUP,
					sarchetype.Instance
				);
			}
			else
			{
				string skintone = this.GetCpfProperty(
					this.archetype,
					MetaData.GZPS,
					"skintone"
				);
				string hairtone = this.GetCpfProperty(
					this.archetype,
					MetaData.GZPS,
					"hairtone"
				);
				string eyecolor = this.GetCpfProperty(
					this.archetype,
					0xAC598EAC,
					"eyecolor"
				);

				dna = ngbh.NewDescriptor(
					0xEBFEE33F,
					0,
					MetaData.LOCAL_GROUP,
					spatient.Instance
				);

				Cpf cpf = new Cpf();
				cpf.ProcessData(dna, ngbh);

				this.AddCpfItem(cpf, "2", skintone);
				this.AddCpfItem(cpf, "6", skintone);
				this.AddCpfItem(cpf, "268435462", skintone);
				this.AddCpfItem(cpf, "268435458", skintone);

				this.AddCpfItem(cpf, "1", hairtone);
				this.AddCpfItem(cpf, "268435457", hairtone);

				this.AddCpfItem(cpf, "3", eyecolor);
				this.AddCpfItem(cpf, "268435459", eyecolor);

				cpf.SynchronizeUserData();
			}

			if (dna != null)
			{
				IPackedFileDescriptor tna = ngbh.FindFile(
					0xEBFEE33F,
					0,
					MetaData.LOCAL_GROUP,
					spatient.Instance
				);
				if (tna == null)
				{
					tna = ngbh.NewDescriptor(
						0xEBFEE33F,
						0,
						MetaData.LOCAL_GROUP,
						spatient.Instance
					);
					tna.Changed = true;
					ngbh.Add(tna);
				}

				IPackedFile fl = ngbh.Read(dna);
				tna.UserData = fl.UncompressedData;
			}

			UpdateFaceStructure(ret);
			return ret;
		}

		void AddCpfItem(Cpf cpf, string name, string value)
		{
			CpfItem item = new CpfItem();
			item.Name = name;
			item.StringValue = value;
			cpf.AddItem(item);
		}

		#endregion

		/// <summary>
		/// This function will compile the clothing in use by the patient sim,
		/// and add the corresponding resource links to the archetype's reference file.
		/// </summary>
		void ProcessClothing(IPackageFile patient, IPackageFile archetype)
		{
			IPackedFileDescriptor patSourceRef = GetClothing3IDREntry(patient);
			IPackedFileDescriptor arcTargetRef = GetClothing3IDREntry(archetype);

			if (arcTargetRef != null && patSourceRef != null)
			{
				RefFileItem[] pcItems = this.GetClothingItems(patSourceRef, patient);
				if (pcItems == null || pcItems.Length == 0)
				{
					// cascade fatal error
					throw new ApplicationException(
						"Cannot resolve clothing data on the patient sim"
					);
				}

				// next, find the clothing references in the patient package.
				// copy them to the arcTargetRef items
				using (RefFile arcRef = new RefFile())
				{
					arcRef.ProcessData(arcTargetRef, archetype, false);

					ArrayList items = new ArrayList(arcRef.Items);
					ArrayList inUse = new ArrayList(); // SkinCategories

					foreach (IPackedFileDescriptor pfd in items)
					{
						if ((pfd is RefFileItem) && ((RefFileItem)pfd).Skin != null)
						{
							inUse.Add(((RefFileItem)pfd).Skin.Category);
						}
					}

					foreach (RefFileItem item in pcItems)
					{
						if (item.Skin != null)
						{
							// a better matching condition must be found!!!
							if (!inUse.Contains(item.Skin.Category))
							{
								items.Add(item);
							}
						}
					}
					arcRef.Items = (IPackedFileDescriptor[])
						items.ToArray(typeof(IPackedFileDescriptor));
					arcRef.SynchronizeUserData();
				}
			}
		}

		RefFileItem[] GetClothingItems(IPackedFileDescriptor pfd, IPackageFile file)
		{
			ArrayList ret = new ArrayList();

			RefFile refFile = new RefFile();
			refFile.ProcessData(pfd, file, true); // <-- ERROR is here!
			if (refFile.Items.Length > 0)
			{
				foreach (IPackedFileDescriptor ptr in refFile.Items)
				{
					if (ptr is RefFileItem)
					{
						RefFileItem item = ptr as RefFileItem;
						if (item.Skin != null)
						{
							SkinCategories cat = (SkinCategories)item.Skin.Category;

							// we don't want skin pointers
							if ((cat & SkinCategories.Skin) == SkinCategories.Skin)
							{
								continue;
							}

							ret.Add(item);
						}
					}
				}
			}
			return (RefFileItem[])ret.ToArray(typeof(RefFileItem));
		}

		IPackedFileDescriptor GetClothing3IDREntry(IPackageFile file)
		{
			foreach (IPackedFileDescriptor pfd in file.Index)
			{
				if (pfd.Type == MetaData.REF_FILE && pfd.Instance == 0x01)
				{
					return pfd;
				}
			}

			return null;
		}

		#region SkinTone only
		/// <summary>
		/// Set by the CloneSkintone Methode during calls to UpdateSkinTone
		/// </summary>
		uint patientgender = 1;

		/// <summary>
		/// Returns the Hasvalue used for the Patient
		/// </summary>
		/// <returns></returns>
		string GetSkintone(IPackageFile pkg)
		{
			foreach (IPackedFileDescriptor pfd in pkg.Index)
			{
				///This is a scenegraph Resource so get the Hash from there!
				if (pfd.Type == MetaData.GZPS)
				{
					Cpf cpf = new Cpf();
					cpf.ProcessData(pfd, pkg);
					return cpf.GetSaveItem("skintone").StringValue;
				}
			}
			return "";
		}

		string GetCpfProperty(
			IPackageFile pkg,
			uint type,
			string key
		)
		{
			foreach (IPackedFileDescriptor pfd in pkg.Index)
			{
				if (pfd.Type == type)
				{
					using (Cpf cpf = new Cpf())
					{
						cpf.ProcessData(pfd, pkg);
						CpfItem item = cpf.GetItem(key);
						if (item != null)
						{
							return item.StringValue;
						}
					}
				}
			}

			return "";
		}

		/// <summary>
		/// If this is a skinFile it will be relinked to a property Set for the passed skintone
		/// </summary>
		/// <param name="skinfile">a PropertySet</param>
		/// <param name="skin">the new skintone</param>
		/// <param name="skinfiles">a Hashtable listing al Proerty Sets for each available skintone (key=skintone string, value= ArrayList of Cpf Objects)</param>
		/// <returns>FileDescriptor for the new SkinFile</returns>
		IPackedFileDescriptor UpdateSkintone(
			Cpf skinfile,
			string skin,
			Hashtable skinfiles
		)
		{
			IPackedFileDescriptor ret = skinfile.FileDescriptor;

			//this is a skin!
			if (
				(
					skinfile.GetSaveItem("category").UIntegerValue
					& (uint)SkinCategories.Skin
				) == (uint)SkinCategories.Skin
			)
			{
				//the values that are checked for equality to find a matching Property Set in the target skintone
				Hashtable props = new Hashtable
				{
					{
						"fitness",
						skinfile.GetSaveItem("fitness").StringValue.Trim().ToLower()
					},
					{
						"gender",
						skinfile.GetSaveItem("gender").StringValue.Trim().ToLower()
					},
					{
						"outfit",
						skinfile.GetSaveItem("outfit").StringValue.Trim().ToLower()
					},
					{
						"override0subset",
						skinfile.GetSaveItem("override0subset").StringValue.Trim().ToLower()
					}
				};

				foreach (Cpf newcpf in (ArrayList)skinfiles[skin])
				{
					if (
						(
							(
								skinfile.GetSaveItem("age").UIntegerValue
								& newcpf.GetSaveItem("age").UIntegerValue
							) != 0
						)
					)
					{
						bool use = true;
						foreach (string k in props.Keys)
						{
							if (
								newcpf.GetSaveItem(k).StringValue.Trim().ToLower()
								!= (string)props[k]
							)
							{
								patientgender = skinfile
									.GetSaveItem("gender")
									.UIntegerValue;
								use = false;
								break;
							}
						}
						if (use)
						{
							ret = newcpf.FileDescriptor;
							return ret;
						}
					}
				} //foreach
			}

			return ret;
		}

		/// <summary>
		/// Updates the SkinTone References in the 3IDR Files
		/// </summary>
		/// <param name="reffile">the 3IDR File</param><param name="skin">the new skintone</param>
		/// <param name="skinfiles">a Hashtable listing al Proerty Sets for each available skintone (key=skintone string, value= ArrayList of Cpf Objects)</param>
		void UpdateSkintone(
			RefFile reffile,
			string skin,
			Hashtable skinfiles
		)
		{
			if (reffile == null)
			{
				return;
			}

			if (reffile.Items == null)
			{
				return;
			}

			if (reffile.Package == null)
			{
				return;
			}

			for (int i = 0; i < reffile.Items.Length; i++)
			{
				IPackedFileDescriptor pfd =
					(IPackedFileDescriptor)reffile.Items[i];
				if (pfd == null)
				{
					continue;
				}

				if (pfd.Type == MetaData.GZPS)
				{
					Interfaces.Scenegraph.IScenegraphFileIndexItem[] fii =
						FileTableBase.FileIndex.FindFile(pfd, reffile.Package);
					if (fii.Length > 0)
					{
						Cpf skinfile = new Cpf();
						skinfile.ProcessData(fii[0]);

						reffile.Items[i] = UpdateSkintone(skinfile, skin, skinfiles);
					}
				}
			}

			reffile.SynchronizeUserData();
		}

		string FindTxtrName(string name)
		{
			Interfaces.Scenegraph.IScenegraphFileIndexItem item =
				FileTableBase.FileIndex.FindFileByName(
					name,
					MetaData.TXTR,
					0xffffffff,
					true
				);
			if (item != null)
			{
				Rcol txtr = new GenericRcol(null, false);
				txtr.ProcessData(item);
				name = txtr.FileName.Trim();
				if (name.ToLower().EndsWith("_txtr"))
				{
					name = name.Substring(0, name.Length - 5);
				}

				if (name.StartsWith("#"))
				{
					name = "_" + name;
				}
			}
			name = name.Replace("-", "_");

			return name;
		}

		/// <summary>
		/// Updates the SkinTone References in the 3IDR Files
		/// </summary>
		/// <param name="md">The Metreial Definition</param>
		/// <param name="skinfiles">a Hashtable listing al Proerty Sets for each available skintone (key=skintone string, value= ArrayList of Cpf Objects)</param>
		/// <param name="sourceskin">the hash for the source skin</param>
		/// <param name="targetskin">the hash for the target skin</param>
		void UpdateSkintone(
			MaterialDefinition md,
			string targetskin,
			Hashtable skinfiles
		)
		{
			uint age = (uint)
				MetaData.AgeTranslation(
					(MetaData.LifeSections)spatient.CharacterDescription.Age
				);
			try
			{
				age = (uint)
					Math.Pow(2, Convert.ToInt32(md.FindProperty("paramAge").Value));
			}
			catch { }
			try
			{
				patientgender = Convert.ToUInt32(md.FindProperty("paramGender").Value);
			}
			catch { }

			if (skinfiles[targetskin] == null)
			{
				return;
			}

			foreach (Cpf newcpf in (ArrayList)skinfiles[targetskin])
			{
				if (
					newcpf.GetSaveItem("override0subset").StringValue.Trim().ToLower()
					== "face"
				)
				{
					if ((newcpf.GetSaveItem("age").UIntegerValue & age) == age)
					{
						if (
							(newcpf.GetSaveItem("gender").UIntegerValue & patientgender)
							== patientgender
						)
						{
							SkinChain sc = new SkinChain(newcpf);
							IPackedFileDescriptor[] pfds =
								newcpf.Package.FindFile(
									0xAC506764,
									newcpf.FileDescriptor.SubType,
									newcpf.FileDescriptor.Instance
								);

							Rcol txmt = sc.TXMT;
							Rcol txtr = sc.TXTR;
							if (txtr != null && txmt != null)
							{
								string txmtname = txmt.FileName.Trim();
								if (txmtname.ToLower().EndsWith("_txmt"))
								{
									txmtname = txmtname.Substring(
										0,
										txmtname.Length - 5
									);
								}

								string basename = txtr.FileName.Trim();
								if (basename.ToLower().EndsWith("_txtr"))
								{
									basename = basename.Substring(
										0,
										basename.Length - 5
									);
								}

								if (txmtname.IndexOf("#") == 0)
								{
									txmtname = "_" + txmtname;
								}

								int count = 0;
								try
								{
									count = Convert.ToInt32(
										md.FindProperty("numTexturesToComposite").Value
									);
								}
								catch { }

								if (count > 0)
								{
									md.FindProperty("baseTexture0").Value = basename;
									md.FindProperty("stdMatBaseTextureName").Value =
										basename;

									for (int i = 1; i < count; i++)
									{
										string name = md.FindProperty(
												"baseTexture" + i.ToString()
											)
											.Value.Trim();
										if (!name.ToLower().EndsWith("_txtr"))
										{
											name += "_txtr";
										}

										name = this.FindTxtrName(name);

										if (i != 0)
										{
											txmtname += "_";
										}

										txmtname += name;
									}

									md.FindProperty("compositeBaseTextureName").Value =
										txmtname;
									string[] list = new string[1];
									list[0] = txmtname;
									md.Listing = list;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Change the SkinTone of a Sim (to the one of the archetype)
		/// </summary>
		/// <param name="skinfiles">a Hashtable listing al Proerty Sets for each available skintone (key=skintone string, value= ArrayList of Cpf Objects)</param>
		/// <returns>the new Package for the patient Sim</returns>
		public Packages.GeneratableFile CloneSkinTone(Hashtable skinfiles)
		{
			string askin = GetSkintone(this.archetype);
			return CloneSkinTone(askin, skinfiles);
		}

		/// <summary>
		/// Change the SkinTone of a Sim
		/// </summary>
		/// <param name="skin">the new skintone</param>
		/// param name="skinfiles">a Hashtable listing al Proerty Sets for each available skintone (key=skintone string, value= ArrayList of Cpf Objects)</param>
		/// <returns>the new Package for the patient Sim</returns>
		public Packages.GeneratableFile CloneSkinTone(
			string skin,
			Hashtable skinfiles
		)
		{
			Packages.GeneratableFile ret =
				Packages.File.LoadFromFile((string)null);
			string pskin = GetSkintone(this.patient);

			ArrayList list = new ArrayList
			{
				(uint)0xE86B1EEF //make sure the compressed Directory won't be copied!
			};
			foreach (IPackedFileDescriptor pfd in patient.Index)
			{
				if (!list.Contains(pfd.Type))
				{
					IPackedFile fl = patient.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);

					switch (newpfd.Type)
					{
						case (uint)0xAC598EAC: //AGED
						{
							Cpf cpf = new Cpf();
							cpf.ProcessData(newpfd, ret);
							cpf.GetSaveItem("skincolor").StringValue = skin;

							cpf.SynchronizeUserData();
							break;
						}
						case MetaData.GZPS:
						{
							Cpf cpf = new Cpf();
							cpf.ProcessData(newpfd, ret);
							cpf.GetSaveItem("skintone").StringValue = skin;

							cpf.SynchronizeUserData();
							break;
						}
						case MetaData.TXMT:
						{
							Rcol rcol = new GenericRcol(null, false);
							rcol.ProcessData(newpfd, ret);
							MaterialDefinition txmt = (MaterialDefinition)
								rcol.Blocks[0];
							txmt.FindProperty("cafSkinTone").Value = skin;

							rcol.SynchronizeUserData();
							break;
						}
					}
				}
			}

			//Update DNA File
			IPackedFileDescriptor dna = ngbh.FindFile(
				0xEBFEE33F,
				0,
				MetaData.LOCAL_GROUP,
				spatient.Instance
			);
			if (dna != null)
			{
				Cpf cpf = new Cpf();
				cpf.ProcessData(dna, ngbh);
				cpf.GetSaveItem("2").StringValue = skin;
				cpf.GetSaveItem("6").StringValue = skin;

				cpf.SynchronizeUserData();
			}

			//Update 3IDR Files
			IPackedFileDescriptor[] pfds = ret.FindFiles(
				0xAC506764
			);
			foreach (IPackedFileDescriptor pfd in pfds)
			{
				RefFile reffile = new RefFile();
				reffile.ProcessData(pfd, ret);

				UpdateSkintone(reffile, skin, skinfiles);
			}

			//Update TXMT Files for the Face
			pfds = ret.FindFiles(MetaData.TXMT);
			foreach (IPackedFileDescriptor pfd in pfds)
			{
				Rcol rcol = new GenericRcol(null, false);
				rcol.ProcessData(pfd, ret);

				MaterialDefinition md = (MaterialDefinition)rcol.Blocks[0];
				this.UpdateSkintone(md, skin, skinfiles);

				rcol.SynchronizeUserData();
			}

			return ret;
		}

		#endregion

		#region Face Only
		/// <summary>
		/// Clone the Face of a Sim
		/// </summary>
		/// <returns>the new Package for the patient Sim</returns>
		public Packages.GeneratableFile CloneFace()
		{
			Packages.GeneratableFile ret =
				Packages.File.LoadFromFile((string)null);

			ArrayList list = new ArrayList
			{
				(uint)0xCCCEF852 //LxNR, Face
			};

			uint hashgroup = this.GetPatientHash();

			foreach (IPackedFileDescriptor pfd in archetype.Index)
			{
				if (list.Contains(pfd.Type))
				{
					IPackedFile fl = archetype.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);
				}
			}

			list.Add((uint)0xE86B1EEF); //make sure the compressed Directory won't be copied!
			foreach (IPackedFileDescriptor pfd in patient.Index)
			{
				if (!list.Contains(pfd.Type))
				{
					IPackedFile fl = patient.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);
				}
			}

			UpdateFaceStructure(ret);
			return ret;
		}

		/// <summary>
		/// Make sure the correct Face Structure is used (as desribed by Nukael)
		/// </summary>
		/// <remarks>http://www.modthesims2.com/showthread.php?t=56241</remarks>
		/// <param name="pkg">The package with the Face Data</param>
		public void UpdateFaceStructure(Packages.GeneratableFile pkg)
		{
			IPackedFileDescriptor[] pfds = pkg.FindFiles(
				(uint)0xCCCEF852
			); //LxNR, Face
			IPackedFileDescriptor oldpfd = null;
			IPackedFileDescriptor newpfd = null;

			uint oi = 1;
			uint ni = 2;
			foreach (IPackedFileDescriptor pfd in pfds)
			{
				if (pfd.Instance <= oi)
				{
					oldpfd = pfd;
					oi = pfd.Instance;
				}
				if (pfd.Instance >= ni)
				{
					newpfd = pfd;
					ni = pfd.Instance;
				}
			}

			if (oldpfd != null && newpfd != null)
			{
				IPackedFile pf = pkg.Read(newpfd);
				oldpfd.UserData = pf.UncompressedData;
				oldpfd.Changed = true;
			}
		}
		#endregion

		#region Makeup Only
		/// <summary>
		/// Updates the SkinTone References in the 3IDR Files
		/// </summary>
		/// <param name="md">The Metreial Definition</param>
		/// <param name="eyecolor">true, if you want to alter the eyecolor</param>
		/// <param name="makeups">true, if you want to alter the makeup</param>
		void UpdateMakeup(MaterialDefinition md, bool eyecolor, bool makeups)
		{
			string age = md.FindProperty("paramAge").Value;
			string gender = md.FindProperty("paramGender").Value;

			//find a matching Package in the arechtype
			IPackedFileDescriptor[] pfds = this.archetype.FindFiles(
				MetaData.TXMT
			);
			Rcol atxmt = new GenericRcol(null, false);
			MaterialDefinition amd = null;

			foreach (IPackedFileDescriptor pfd in pfds)
			{
				atxmt.ProcessData(pfd, this.archetype);

				amd = (MaterialDefinition)atxmt.Blocks[0];
				if (
					(amd.FindProperty("paramAge").Value == age)
					&& (amd.FindProperty("paramGender").Value == gender)
				)
				{
					break;
				}
			}

			if (amd != null)
			{
				int count = 0;
				md.Add(amd.FindProperty("numTexturesToComposite"));
				try
				{
					count = Convert.ToInt32(
						md.FindProperty("numTexturesToComposite").Value
					);
				}
				catch { }

				string txmtname = "";
				for (int i = 0; i < count; i++)
				{
					MaterialDefinitionProperty val = amd.FindProperty(
						"baseTexture" + i.ToString()
					);
					if (i != 0)
					{
						md.Add(val);
					}

					if (i == 1)
					{
						if (eyecolor)
						{
							md.Add(val);
						}
						else if (makeups)
						{
							md.Add(val);
						}
					}

					string name = val.Value.Trim();
					if (!name.ToLower().EndsWith("_txtr"))
					{
						name += "_txtr";
					}

					name = this.FindTxtrName(name);

					if (i != 0)
					{
						txmtname += "_";
					}

					txmtname += name;
				}

				md.FindProperty("compositeBaseTextureName").Value = txmtname;
				string[] list = new string[1];
				list[0] = txmtname;
				md.Listing = list;

				if (makeups)
				{
					count = 0;
					md.Add(amd.FindProperty("cafNumOverlays"));
					try
					{
						count = Convert.ToInt32(
							md.FindProperty("cafNumOverlays").Value
						);
					}
					catch { }

					for (int i = 0; i < count; i++)
					{
						MaterialDefinitionProperty val = amd.FindProperty(
							"cafOverlay" + i.ToString()
						);
						md.Add(val);
					}
				}
			}
		}

		/// <summary>
		/// Clone the Makeup of a Sim
		/// </summary>
		/// <returns>the new Package for the patient Sim</returns>
		/// <param name="eyecolor">true, if you want to alter the eyecolor</param>
		/// <param name="makeups">true, if you want to alter the makeup</param>
		public Packages.GeneratableFile CloneMakeup(bool eyecolor, bool makeups)
		{
			Packages.GeneratableFile ret =
				Packages.File.LoadFromFile((string)null);

			ArrayList list = new ArrayList
			{
				(uint)0xE86B1EEF //make sure the compressed Directory won't be copied!
			};
			foreach (IPackedFileDescriptor pfd in patient.Index)
			{
				if (!list.Contains(pfd.Type))
				{
					IPackedFile fl = patient.Read(pfd);

					IPackedFileDescriptor newpfd = ret.NewDescriptor(
						pfd.Type,
						pfd.SubType,
						pfd.Group,
						pfd.Instance
					);
					newpfd.UserData = fl.UncompressedData;
					ret.Add(newpfd);
				}
			}

			//Update TXMT Files for the Face
			IPackedFileDescriptor[] pfds = ret.FindFiles(
				MetaData.TXMT
			);
			foreach (IPackedFileDescriptor pfd in pfds)
			{
				Rcol rcol = new GenericRcol(null, false);
				rcol.ProcessData(pfd, ret);

				MaterialDefinition md = (MaterialDefinition)rcol.Blocks[0];
				this.UpdateMakeup(md, eyecolor, makeups);

				rcol.SynchronizeUserData();
			}

			if (eyecolor)
			{
				string eyecolorGuid1 = null;
				string eyecolorGuid2 = null;
				if (!this.fromTemplate)
				{
					IPackedFileDescriptor adna = ngbh.FindFile(
						0xEBFEE33F,
						0,
						MetaData.LOCAL_GROUP,
						sarchetype.Instance
					);
					using (Cpf cpf = new Cpf())
					{
						cpf.ProcessData(adna, ngbh);
						eyecolorGuid1 = cpf.GetSaveItem("3").StringValue;
						eyecolorGuid2 = cpf.GetSaveItem("268435459").StringValue;
					}
				}
				else
				{
					eyecolorGuid1 = this.GetCpfProperty(
						this.archetype,
						0xAC598EAC,
						"eyecolor"
					);
					eyecolorGuid2 = eyecolorGuid1;
				}

				//Update DNA File
				// (if applicable)
				if (eyecolorGuid1 != null && eyecolorGuid1.Length > 0)
				{
					IPackedFileDescriptor dna = ngbh.FindFile(
						0xEBFEE33F,
						0,
						MetaData.LOCAL_GROUP,
						spatient.Instance
					);
					if (dna != null)
					{
						Cpf cpf = new Cpf();
						cpf.ProcessData(dna, ngbh);

						cpf.GetSaveItem("3").StringValue = eyecolorGuid1;
						cpf.GetSaveItem("268435459").StringValue = eyecolorGuid2;

						cpf.SynchronizeUserData();
					}
				}
			}
			return ret;
		}
		#endregion
	}
}
