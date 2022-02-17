// =============================================================================== 
// 		Class Name: clsTable_CS.cs
// 		Object purpose: The class is designed for manipulating data in a database table. 
// 		It can be used to fetch data via data access component and return as a DataReader 
// 		or DataSet. This component could be fit in the position of middle tier if the system  
// 		is designed as multi-tiers. 
// 
// 		Designed by: Ben Li. 
// 		Written by:  Ben Li. 
//
// 		Codes generated Date: 05/08/2005 2:50:38 PM.
// 		Last Modified Date:   07/02/2014 12:00:00 PM.
// 
// =============================================================================== 
// Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.
// All rights reserved. 
// 
// THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' 
// WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT 
// NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS 
// FOR A PARTICULAR PURPOSE.
// =============================================================================== 

using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using log4net.Config;

namespace mhcb.EntityHelper
{
	/// <summary>
	/// Summary description for clsTable_CS.
	/// </summary>
	public class EntityCs
	{
		public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(EntityCs));

		public string Output { get; set; }

		public EntityCs()
		{
			//BasicConfigurator.Configure();
			XmlConfigurator.Configure();
		}

		public string EntityClassCs(CheckedListBox checkedListBox1, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			for (int j = 0; j < checkedListBox1.CheckedItems.Count; j++)
			{
				string lstrOldTableName = string.Empty;

				StreamWriter sw = null;

				StringBuilder sb = null;
				StringBuilder sbVariableList = null;
				StringBuilder sbAttrbuteList = null;
				StringBuilder sbDataReader = null;
				StringBuilder sbEnumList = null;
				StringBuilder sbSelectCaseList = null;

				string cTableName = "";
				string cFieldName = "";
				string cDotNetDataType = "";
				string cPosition = "";
				string cIsNullable = "";
				string cKeyType = "";
				string cSqlDataType = "";
				string strFstColumn = "";


				cTableName = checkedListBox1.CheckedItems[j].ToString();
				cTableName = cTableName.Substring(0, 1).ToUpper() + cTableName.Substring(1, cTableName.Length - 1);

				SqlDataReader dr = null;
				var oDataTrans = new DataTransSvc();
				dr = oDataTrans.invoke_Tools_getBaseTablesDetails(cTableName);


				while (dr.Read())
				{

					cFieldName = dr.GetString(0);								//dr.GetString(1);
					cDotNetDataType = DataType.ToSystemType_CS(dr.GetString(1));
					cSqlDataType = DataType.ToSqlDataReaderType(dr.GetString(1));
					cPosition = dr.GetInt32(5).ToString(CultureInfo.InvariantCulture);
					cIsNullable = dr.GetString(6);
					cKeyType = dr.GetString(7);

					if (lstrOldTableName != cTableName)
					{
						if (sw != null)
						{
							ConstructorCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());

							if (blnOther)  //blnWcf != true)
							{
								NewCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());
								CompareToCs(sw, lstrOldTableName, strFstColumn);
							}

							GetEntityCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());							
							GetListCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
							if (blnEdm != true)
							{
								GetCollectionCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
							}

							// for end of class
							sb = new StringBuilder("");
							sb.Append("}");

							Output = Output + sb.ToString();
							sw.WriteLine(sb.ToString());

							if (blnEdm != true)
							{
								EnumCs(sw, lstrOldTableName, sbEnumList.ToString());
								CollectionClassCs(sw, lstrOldTableName, sbSelectCaseList.ToString(), blnWcf, blnEdm, blnOther);
							}


							// for end of namespace
							sb = new StringBuilder("");
							sb.Append("}");
							sb.Append(" ");

							Output = Output + sb;
							sw.WriteLine(sb.ToString());

							sw.Flush();
							sw.Close();
						}

						sb = new StringBuilder(cTableName);
						sb.Append(".cs");
						//sb.Append(blnWcf ? "_ws.cs" : ".cs");


						var oFileInfo = new FileInfo(sb.ToString());
						sw = oFileInfo.CreateText();

						// Create top 
						HeadingCs(sw, cTableName, blnWcf, blnEdm, blnOther);


						// initial attribute list
						sbVariableList = new StringBuilder();
						sbAttrbuteList = new StringBuilder();
						sbDataReader = new StringBuilder();
						sbEnumList = new StringBuilder();
						sbSelectCaseList = new StringBuilder();

						strFstColumn = cFieldName;

						PropertyCs(sw, cDotNetDataType, cFieldName, cPosition, cIsNullable, cKeyType, blnWcf, blnEdm, blnOther);


						sbAttrbuteList.AppendFormat("{0} {1}, " + GlobalVar.cTab[2], new object[] { cDotNetDataType, cFieldName });
						sbVariableList.AppendFormat(GlobalVar.cTab[2] + "this._{0} = {0};", new object[] { cFieldName });

						sbDataReader.AppendFormat(GlobalVar.cTab[2] + "if (aReader[{1}{0}{1}] != DBNull.Value)", new object[] { cFieldName, "\"" });
						sbDataReader.AppendFormat(GlobalVar.cTab[3] + "o.{0} = aReader.Get{1}(aReader.GetOrdinal({2}{0}{2}));", new object[] { cFieldName, cSqlDataType, "\"" });

						//if (blnWcf != true)
						//{
						sbEnumList.Append(GlobalVar.cTab[1] + cFieldName + ",");
						//}

						sbSelectCaseList.AppendFormat(GlobalVar.cTab[4] + "default:");
						sbSelectCaseList.AppendFormat(GlobalVar.cTab[5] + "return (({0})(obj2)).{1}.CompareTo((({0})(o)).{1});", new object[] { cTableName, cFieldName });
						//sbSelectCaseList.Append(GlobalVar.cTab[5] + "break;");

						sbSelectCaseList.AppendFormat(GlobalVar.cTab[4] + "case {0}SortTypes.{1}:", new object[] { cTableName, cFieldName });
						sbSelectCaseList.AppendFormat(GlobalVar.cTab[5] + "return (({0})(obj2)).{1}.CompareTo((({0})(o)).{1});", new object[] { cTableName, cFieldName });
						//sbSelectCaseList.Append(GlobalVar.cTab[5] + "break;");
					}
					else
					{
						PropertyCs(sw, cDotNetDataType, cFieldName, cPosition, cIsNullable, cKeyType, blnWcf, blnEdm, blnOther);

						if (sbAttrbuteList != null)
							sbAttrbuteList.AppendFormat("{0} {1}, " + GlobalVar.cTab[2], new object[] { cDotNetDataType, cFieldName });

						if (sbVariableList != null)
							sbVariableList.AppendFormat(GlobalVar.cTab[2] + "this._{0} = {0};", new object[] { cFieldName });

						if (sbDataReader != null)
						{
							sbDataReader.AppendFormat(GlobalVar.cTab[2] + "if (aReader[{1}{0}{1}] != DBNull.Value)", new object[] { cFieldName, "\"" });
							sbDataReader.AppendFormat(GlobalVar.cTab[3] + "o.{0} = aReader.Get{1}(aReader.GetOrdinal({2}{0}{2}));", new object[] { cFieldName, cSqlDataType, "\"" });
						}

						//if (blnWcf != true)
						//{
						if (sbEnumList != null) sbEnumList.Append(GlobalVar.cTab[1] + cFieldName + ",");
						//}

						if (sbSelectCaseList != null)
						{
							sbSelectCaseList.AppendFormat(GlobalVar.cTab[4] + "case {0}SortTypes.{1}:", new object[] { lstrOldTableName, cFieldName });
							sbSelectCaseList.AppendFormat(GlobalVar.cTab[5] + "return (({0})(obj2)).{1}.CompareTo((({0})(o)).{1});", new object[] { lstrOldTableName, cFieldName });
							//sbSelectCaseList.Append(GlobalVar.cTab[5] + "break;");
						}
					}

					lstrOldTableName = cTableName;
				}


				// after at end of recordset, we still need to append parts of bottom and collection
				if (sbAttrbuteList != null)
				{
					ConstructorCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());

					if (blnOther) //blnWcf != true)
					{
						NewCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());
						CompareToCs(sw, lstrOldTableName, strFstColumn);
					}

					GetEntityCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
					GetListCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());

					if (blnEdm != true)
					{
						GetCollectionCs(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
					}
				}


				// for end of class
				sb = new StringBuilder("");
				sb.Append(GlobalVar.cTab[0] + "}");
				sb.Append(GlobalVar.cTab[0]);

				Output = Output + sb;
				sw.WriteLine(sb.ToString());

				if (blnEdm != true)
				{
					if (sbEnumList != null) 
						EnumCs(sw, lstrOldTableName, sbEnumList.ToString());

					if (sbSelectCaseList != null)
						CollectionClassCs(sw, lstrOldTableName, sbSelectCaseList.ToString(), blnWcf, blnEdm, blnOther);
				}


				// for end of namespace
				sb = new StringBuilder("");
				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[0] + "}");
				sb.Append(GlobalVar.cTab[0]);

				Output = Output + sb;
				sw.WriteLine(sb.ToString());

				_logWriter.InfoFormat("Created an entity for table {0} and saved it as the file: {0}.cs", cTableName);

				sw.Flush();
				sw.Close();

				if (j >= checkedListBox1.CheckedItems.Count - 1)
					return "C# entities have been generated and are copied into the Clipboard!";
			}

			return "No C# entity has been generated!";
		}



		public void HeadingCs(StreamWriter sw, string tstrClassName, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			var entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.CS"];

			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0] + "// =============================================================================== ");
			sb.Append(GlobalVar.cTab[0] + "// <autogenerated>");
			sb.Append(GlobalVar.cTab[0] + "// \t\tClass Name: ");
			sb.Append(tstrClassName + ".cs");
			sb.Append(GlobalVar.cTab[0] + "// \t\tObject purpose: The class is designed for an encapsulation data of a database. ");
			sb.Append(GlobalVar.cTab[0] + "// \t\ttable. The class should be used in both client and server sides, if your system ");
			sb.Append(GlobalVar.cTab[0] + "// \t\tis designed as a multi-tier one.");
			sb.Append(GlobalVar.cTab[0] + "// ");

			sb.Append(GlobalVar.cTab[0] + "// \t\tDesigned & developed by: Ben Li. ");
			sb.Append(GlobalVar.cTab[0] + "// \t\tThe last modified date: 17/02/2006.");
			sb.Append(GlobalVar.cTab[0] + "// \t\t==================================");
			sb.Append(GlobalVar.cTab[0] + "// \t\tScripts generated date: " + System.DateTime.Now + ".");
			sb.Append(GlobalVar.cTab[0] + "// </autogenerated> ");
			sb.Append(GlobalVar.cTab[0] + "// ");

			sb.Append(GlobalVar.cTab[0] + "// =============================================================================== ");
			sb.Append(GlobalVar.cTab[0] + "// Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.");
			sb.Append(GlobalVar.cTab[0] + "// All rights reserved. ");
			sb.Append(GlobalVar.cTab[0] + "// ");
			sb.Append(GlobalVar.cTab[0] + "// THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' ");
			sb.Append(GlobalVar.cTab[0] + "// WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT ");
			sb.Append(GlobalVar.cTab[0] + "// NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS ");
			sb.Append(GlobalVar.cTab[0] + "// FOR A PARTICULAR PURPOSE.");
			sb.Append(GlobalVar.cTab[0] + "// =============================================================================== ");
			sb.Append(GlobalVar.cTab[1] + " ");

			sb.Append(GlobalVar.cTab[0] + " ");
			sb.Append(GlobalVar.cTab[0] + "using System; ");
			sb.Append(GlobalVar.cTab[0] + "using System.Collections; ");
			sb.Append(GlobalVar.cTab[0] + "using System.Collections.Generic; ");
			sb.Append(GlobalVar.cTab[0] + "using System.Data.SqlClient; ");
			sb.Append(GlobalVar.cTab[0] + "using System.Data.Linq.Mapping; ");
			sb.Append(GlobalVar.cTab[0] + "using System.Runtime.Serialization; ");
			sb.Append(GlobalVar.cTab[0] + "using GUIDE.Library.CS; ");

			sb.Append(GlobalVar.cTab[0] + " ");
			sb.Append(blnWcf ? GlobalVar.cTab[0] + "namespace " + entityNamespace + ".DataContract" : GlobalVar.cTab[0] + "namespace " + entityNamespace);

			sb.Append(GlobalVar.cTab[0] + "{");
			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[0]);


			if (blnEdm)			  
			{
				sb.Append(GlobalVar.cTab[0] + "[Table(Name = \"" + tstrClassName + "\")]");
			}
			else if (blnWcf)
			{
				sb.Append("[DataContract(Namespace =\"" + entityNamespace + "\")]");
			}


			sb.Append(GlobalVar.cTab[0] + "public class ");
			sb.Append(blnOther ? tstrClassName + " : LibraryBase, System.IComparable" : tstrClassName );
			sb.Append(GlobalVar.cTab[0] + "{ ");

			if (blnWcf)
			{
				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "[OnSerializing]");
				sb.Append(GlobalVar.cTab[1] + "void OnSerializing(StreamingContext context)");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "//call something();");
				sb.Append(GlobalVar.cTab[1] + "}");
				sb.Append(GlobalVar.cTab[0]);

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "[OnSerialized]");
				sb.Append(GlobalVar.cTab[1] + "void OnSerialized(StreamingContext context)");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "//call something();");
				sb.Append(GlobalVar.cTab[1] + "}");
				sb.Append(GlobalVar.cTab[0]);

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "[OnDeserializing]");
				sb.Append(GlobalVar.cTab[1] + "void OnDeserializing(StreamingContext context)");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "//call something();");
				sb.Append(GlobalVar.cTab[1] + "}");
				sb.Append(GlobalVar.cTab[0]);

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "[OnDeserialized]");
				sb.Append(GlobalVar.cTab[1] + "void OnDeserialized(StreamingContext context)");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "//call something();");
				sb.Append(GlobalVar.cTab[1] + "}");
				sb.Append(GlobalVar.cTab[0]);
			}
			else if(blnOther)
			{
				sb.Append(GlobalVar.cTab[1] + "public delegate void DlgPropertyChangeHandler(" + tstrClassName + " o);");
				sb.Append(GlobalVar.cTab[1] + "public static event DlgPropertyChangeHandler OnPropertyChanged;");
				sb.Append(GlobalVar.cTab[1] + "public static event DlgPropertyChangeHandler OnPropertyNew;");
				sb.Append(GlobalVar.cTab[1] + "public static event DlgPropertyChangeHandler OnPropertyDeleted;");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "public void SomethingChanged(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsDirty = true;");
				sb.Append(GlobalVar.cTab[1] + "}");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "public void SomethingNew(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsNew = true;");
				sb.Append(GlobalVar.cTab[1] + "}");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "public void SomethingDeleted(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsDeleted = true;");
				sb.Append(GlobalVar.cTab[1] + "}");
				sb.Append(GlobalVar.cTab[0]);
			}

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void PropertyCs(StreamWriter sw, string tstrAttributeType, string tstrAttributeName, 
								string lstrAttriPosition, string IsNullable, string cKeyType,
								Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			
			var sb = new StringBuilder(GlobalVar.cTab[0] + "");

			// Property attributes
			if (blnEdm)
			{
				
				//sb.AppendFormat(GlobalVar.cTab[1] + "[Column] ");
				//sb.AppendFormat(GlobalVar.cTab[1] + "[Column(Name =\" {0} \")]", new object[] { tstrAttributeName });	
		
				sb.AppendFormat(GlobalVar.cTab[1] + "[Column(Name=\"{0}\", IsPrimaryKey={1}, CanBeNull={2})]", 
											new object[] { tstrAttributeName, 
															cKeyType == "PRIMARY KEY" ? "true" : "false", 
															IsNullable == "YES" ? "true" : "false"});
			}
			else if (blnWcf)
			{
				//sb.AppendFormat(GlobalVar.cTab[1] + "[DataMember(Name =\"" + tstrAttributeName + "\")]");

				sb.AppendFormat(GlobalVar.cTab[1] + "[DataMember(Name =\"" + tstrAttributeName + "\", Order = " + lstrAttriPosition + ")]");
				sb.AppendFormat(GlobalVar.cTab[1] + "[Column(Name=\"{0}\", IsPrimaryKey={1}, CanBeNull={2})]", 
											new object[] { tstrAttributeName, 
															cKeyType == "PRIMARY KEY" ? "true" : "false", 
															IsNullable == "YES" ? "true" : "false"});
			}


			// Property values
			if (blnEdm)
			{
				if (IsNullable == "NO" || tstrAttributeType =="string")
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "public {0} {1}", new object[] { tstrAttributeType, tstrAttributeName });
				}
				else
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "public Nullable<{0}> {1}", new object[] { tstrAttributeType, tstrAttributeName });
				}

				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "get { return _" + tstrAttributeName + ";}");
				sb.Append(GlobalVar.cTab[2] + "set { this._" + tstrAttributeName + "= value;}");
				sb.Append(GlobalVar.cTab[1] + "}");

				if (IsNullable == "NO" || tstrAttributeType =="string")
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "private {0} _{1};", new object[] { tstrAttributeType, tstrAttributeName });
				}
				else
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "private Nullable<{0}> _{1};", new object[] { tstrAttributeType, tstrAttributeName });
				}
			}
			else if (blnWcf)
			{
				
				sb.AppendFormat(GlobalVar.cTab[1] + "public {0} {1}", new object[] { tstrAttributeType, tstrAttributeName });
				
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "get { return _" + tstrAttributeName + ";}");
				sb.Append(GlobalVar.cTab[2] + "set { this._" + tstrAttributeName + "= value;}");
				sb.Append(GlobalVar.cTab[1] + "}");

				sb.AppendFormat(GlobalVar.cTab[1] + "private {0} _{1};", new object[] { tstrAttributeType, tstrAttributeName });
			}
			else
			{
				sb.AppendFormat(GlobalVar.cTab[1] + "public {0} {1}", new object[] { tstrAttributeType, tstrAttributeName });
				sb.Append(GlobalVar.cTab[1] + "{");
				sb.Append(GlobalVar.cTab[2] + "get { return _" + tstrAttributeName + ";}");


				sb.Append(GlobalVar.cTab[2] + "set {");
				sb.Append(GlobalVar.cTab[3] + "if ((_" + tstrAttributeName + " !=value) && (OnPropertyChanged != null)) OnPropertyChanged(this);");
				sb.Append(GlobalVar.cTab[3] + "this._" + tstrAttributeName + "= value;");
				sb.Append(GlobalVar.cTab[2] + "}");

				sb.Append(GlobalVar.cTab[1] + "}");
				sb.AppendFormat(GlobalVar.cTab[1] + "private {0} _{1};", new object[] { tstrAttributeType, tstrAttributeName });
			}

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void EndingCs(StreamWriter sw, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(tstrAttrbuteList + ")");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append("\t" + tstrVariableList);
			sb.Append(GlobalVar.cTab[1] + "}");
			sb.Append(GlobalVar.cTab[0] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void ConstructorCs(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> Default Constructor </summary> ");
			sb.Append(GlobalVar.cTab[1] + "public " + lstrTableName + "()");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append(GlobalVar.cTab[2] + "//Please append default values here");
			sb.Append(GlobalVar.cTab[1] + "}");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> User defined Constructor </summary> ");
			sb.Append(GlobalVar.cTab[1] + "public " + lstrTableName + "(");
			sb.Append(tstrAttrbuteList.TrimEnd(new char[] { ',', ' ', '\r', '\t', '\n' }) + ")");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append("\t" + tstrVariableList);
			sb.Append(GlobalVar.cTab[1] + "}");
			sb.Append(GlobalVar.cTab[0]);

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void EnumCs(StreamWriter sw, string lstrTableName, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[0] + "//<summary>Create Enum </summary>");
			sb.Append(GlobalVar.cTab[0] + "public enum " + lstrTableName + "SortTypes");
			sb.Append(GlobalVar.cTab[0] + "{");
			sb.Append(tstrVariableList.TrimEnd(new char[] { ',', ' ', '\r', '\t', '\n' }) + "");
			sb.Append(GlobalVar.cTab[0] + "}");
			sb.Append(GlobalVar.cTab[0]);

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void CompareToCs(StreamWriter sw, string lstrTableName, string tstrFstColumn)
		{

			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "//<summary>Implements CompareTo </summary>");
			sb.Append(GlobalVar.cTab[1] + "public int CompareTo(object obj)");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append(GlobalVar.cTab[2] + lstrTableName + " o;");
			sb.Append(GlobalVar.cTab[2] + "if (obj == null) return 1;");
			sb.Append(GlobalVar.cTab[2] + "o = (" + lstrTableName + ")(obj);");
			sb.Append(GlobalVar.cTab[2] + "return this." + tstrFstColumn + ".CompareTo(o." + tstrFstColumn + ");");
			sb.Append(GlobalVar.cTab[1] + "}");
			sb.Append(GlobalVar.cTab[0]);

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public string BuildSubClassCs(StreamWriter sw, string lstrTableName, string cSelectCaseList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "//<summary>Create Internal Class</summary>");
			sb.Append(GlobalVar.cTab[1] + "internal class " + lstrTableName + "Sort : System.Collections.IComparer");
			sb.Append(GlobalVar.cTab[1] + "{");

			sb.Append(GlobalVar.cTab[2] + "private " + lstrTableName + "SortTypes _sort;");
			sb.Append(GlobalVar.cTab[2] + "private bool _asc;");
			sb.Append(GlobalVar.cTab[0]);

			sb.Append(GlobalVar.cTab[2] + "public " + lstrTableName + "Sort(" + lstrTableName + "SortTypes sort)");
			sb.Append(GlobalVar.cTab[2] + "{");
			sb.Append(GlobalVar.cTab[3] + "_sort = sort;");
			sb.Append(GlobalVar.cTab[3] + "_asc = true;");
			sb.Append(GlobalVar.cTab[2] + "}");
			sb.Append(GlobalVar.cTab[0]);

			sb.Append(GlobalVar.cTab[2] + "public " + lstrTableName + "Sort(" + lstrTableName + "SortTypes sort, bool asc)");
			sb.Append(GlobalVar.cTab[2] + "{");
			sb.Append(GlobalVar.cTab[3] + "_sort = sort;");
			sb.Append(GlobalVar.cTab[3] + "_asc = asc;");
			sb.Append(GlobalVar.cTab[2] + "}");
			sb.Append(GlobalVar.cTab[0]);


			sb.Append(GlobalVar.cTab[2] + "public int Compare(object obj1, object obj2)");
			sb.Append(GlobalVar.cTab[2] + "{");
			sb.Append(GlobalVar.cTab[3] + "Object x;");
			sb.Append(GlobalVar.cTab[3] + "if ( _asc )");
			sb.Append(GlobalVar.cTab[3] + "{");
			sb.Append(GlobalVar.cTab[4] + "x = obj2; obj2 = obj1; obj1 = x ;");
			sb.Append(GlobalVar.cTab[3] + "}");
			sb.Append(GlobalVar.cTab[0]);

			sb.Append(GlobalVar.cTab[3] + lstrTableName + " o; ");
			sb.Append(GlobalVar.cTab[3] + "if (obj1 == null) return 1;");
			sb.Append(GlobalVar.cTab[3] + "o = ((" + lstrTableName + ")(obj1));");
			sb.Append(GlobalVar.cTab[2] + "");
			sb.Append(GlobalVar.cTab[3] + "switch (_sort) {");
			sb.Append(GlobalVar.cTab[4] + cSelectCaseList);
			sb.Append(GlobalVar.cTab[3] + "}");
			sb.Append(GlobalVar.cTab[2] + "}");
			sb.Append(GlobalVar.cTab[1] + "}");

			return sb.ToString();
		}

		public void NewCs(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> Get new instance of class</summary> ");
			sb.Append(GlobalVar.cTab[1] + "public static ");
			sb.Append(lstrTableName + " CreateNew" + lstrTableName);
			sb.Append("(");

			sb.Append(tstrAttrbuteList.TrimEnd(new char[] { ',', ' ', '\r', '\t', '\n' }) + ")");
			sb.Append(GlobalVar.cTab[1] + "{");

			sb.Append(GlobalVar.cTab[2] + lstrTableName + " o = new " + lstrTableName + "();");

			sb.Append(GlobalVar.cTab[1] + tstrVariableList.Replace("this._", "o."));
			sb.Append(GlobalVar.cTab[2] + "return o;");

			sb.Append(GlobalVar.cTab[1] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}

		public void GetEntityCs(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> Get new instance of class</summary> ");
			sb.Append(GlobalVar.cTab[1] + "public static ");
			sb.Append(lstrTableName + " Get_" + lstrTableName);
			sb.Append("(");

			sb.Append("SqlDataReader aReader)");
			sb.Append(GlobalVar.cTab[1] + "{");

			sb.Append(GlobalVar.cTab[2] + lstrTableName + " o = new " + lstrTableName + "();");
			sb.Append(GlobalVar.cTab[1] + tstrVariableList);

			sb.Append(GlobalVar.cTab[2] + "return o;");

			sb.Append(GlobalVar.cTab[1] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void GetCollectionCs(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> Get new collection of class</summary> ");
			sb.Append(GlobalVar.cTab[1] + "public static ");
			sb.Append(lstrTableName + "_Collection GetCollection_" + lstrTableName + "(SqlDataReader aReader) ");

			sb.Append(GlobalVar.cTab[1] + "{");

			sb.Append(GlobalVar.cTab[2] + lstrTableName + "_Collection oo = new " + lstrTableName + "_Collection();");
			sb.Append(GlobalVar.cTab[2] + "while (aReader.Read())");
			sb.Append(GlobalVar.cTab[2] + "{");

			sb.Append(GlobalVar.cTab[3] + "oo.Add(Get_" + lstrTableName + "(aReader));");

			sb.Append(GlobalVar.cTab[2] + "}");
			sb.Append(GlobalVar.cTab[2] + "return oo;");

			sb.Append(GlobalVar.cTab[1] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void GetListCs(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "// <summary> Get new List of class</summary> ");
			sb.Append(GlobalVar.cTab[1] + "public static IList<" + lstrTableName + "> GetList_" + lstrTableName + "(SqlDataReader aReader) ");

			sb.Append(GlobalVar.cTab[1] + "{");

			sb.Append(GlobalVar.cTab[2] + "IList<" + lstrTableName + "> oo = new List<" + lstrTableName + ">();");
			sb.Append(GlobalVar.cTab[2] + "while (aReader.Read())");
			sb.Append(GlobalVar.cTab[2] + "{");

			sb.Append(GlobalVar.cTab[3] + "oo.Add(Get_" + lstrTableName + "(aReader));");

			sb.Append(GlobalVar.cTab[2] + "}");
			sb.Append(GlobalVar.cTab[2] + "return oo;");

			sb.Append(GlobalVar.cTab[1] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void CollectionClassCs(StreamWriter sw, string strTableName, string cSelectCaseList, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			string entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.CS"];
			var sb = new StringBuilder("");


			sb.Append(GlobalVar.cTab[0]);
			if (blnWcf )
			{
				sb.Append(GlobalVar.cTab[0] + "[CollectionDataContract(Namespace =\"" + entityNamespace + "\")]");
			}

			sb.Append(GlobalVar.cTab[0] + "public class " + strTableName + "_Collection : mhcbCollectionBase");
			sb.Append(GlobalVar.cTab[0] + "{");

			sb.Append(GlobalVar.cTab[1] + "public " + strTableName + "_Collection()");
			sb.Append(GlobalVar.cTab[1] + "{}");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "public void Sort(" + strTableName + "SortTypes oSortType)");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append(GlobalVar.cTab[2] + strTableName + "Sort comp = new " + strTableName + "Sort(oSortType);");
			sb.Append(GlobalVar.cTab[2] + "this.InnerList.Sort(comp);");
			sb.Append(GlobalVar.cTab[1] + "}");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[1] + "public void Sort(" + strTableName + "SortTypes oSortType, bool asc)");
			sb.Append(GlobalVar.cTab[1] + "{");
			sb.Append(GlobalVar.cTab[2] + strTableName + "Sort comp = new " + strTableName + "Sort(oSortType, asc);");
			sb.Append(GlobalVar.cTab[2] + "this.InnerList.Sort(comp);");
			sb.Append(GlobalVar.cTab[1] + "}");

			sb.Append(BuildSubClassCs(sw, strTableName, cSelectCaseList));
			sb.Append(GlobalVar.cTab[0] + "}");

			Output = Output + sb;
			sw.WriteLine(sb.ToString());
		}
	}
}