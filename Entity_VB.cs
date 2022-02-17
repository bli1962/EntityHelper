// =============================================================================== 
// 		Class Name: clsTable_VB.cs
// 		Object purpose: The class is designed for manipulating data in a database table. 
// 		It can be used to fetch data via data access component and return as a DataReader 
// 		or DataSet. This component could be fit in the position of middle tier if the system  
// 		is designed as multi-tiers. 
// 
// 		Designed by: Ben Li. 
// 		Written by:  Ben Li. 
//
// 		Codes generated Date: 5/08/2005 2:50:38 PM.
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
	/// Summary description for clsTable_VB.
	/// </summary>
	public class EntityVb
	{
		public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(EntityVb));

		public string output { get; set; }

		public EntityVb()
		{
			//BasicConfigurator.Configure();
			XmlConfigurator.Configure();
		}

		public string EntityClassVb(CheckedListBox checkedListBox1, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			for (int j = 0; j < checkedListBox1.CheckedItems.Count; j++)
			{
				var lstrOldTableName = string.Empty;
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

				cTableName = checkedListBox1.CheckedItems[j].ToString();		//dr.GetString(0);
				cTableName = cTableName.Substring(0, 1).ToUpper() + cTableName.Substring(1, cTableName.Length - 1);

				SqlDataReader dr = null;
				var oDataTrans = new DataTransSvc();
				dr = oDataTrans.invoke_Tools_getBaseTablesDetails(cTableName);

				int i = 0;
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
						i += 1;

						if (sw != null)
						{
							ConstructorVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());

							if (blnOther)  //blnWcf != true)
							{
								NewVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());
								CompareToVb(sw, lstrOldTableName, strFstColumn);
							}

							GetEntityVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
							GetListVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
			
							if (blnEdm != true)
							{
								GetCollectionVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
							}


							// for end of Clase 
							sb = new StringBuilder("");
							sb.Append(GlobalVar.cTab[0] + "End Class");
							sb.Append("");

							output = output + sb.ToString();
							sw.WriteLine(sb.ToString());

							if (blnEdm != true)
							{
								EnumVb(sw, lstrOldTableName, sbEnumList.ToString());
								CollectionClassVb(sw, lstrOldTableName, sbSelectCaseList.ToString(), blnWcf, blnEdm, blnOther);
							}

							// for end of namespace
							sb = new StringBuilder("");
							sb.Append(GlobalVar.cTab[0] + "End Namespace ");

							output = output + sb.ToString();
							sw.WriteLine(sb.ToString());

							sw.Flush();
							sw.Close();
							i = 1;
						}

						sb = new StringBuilder(cTableName);
						//sb.Append(blnWcf ? "_ws.VB" : ".VB");
						sb.Append(".VB");


						var oFileInfo = new FileInfo(sb.ToString());
						sw = oFileInfo.CreateText();

						HeadingVb(sw, cTableName, blnWcf, blnEdm, blnOther);
						

						// initial attribute list
						sbVariableList = new StringBuilder();
						sbAttrbuteList = new StringBuilder();
						sbDataReader = new StringBuilder();
						sbEnumList = new StringBuilder();
						sbSelectCaseList = new StringBuilder();

						strFstColumn = cFieldName;

						PropertyVb(sw, cDotNetDataType, cFieldName, cPosition, cIsNullable, cKeyType, blnWcf, blnEdm, blnOther);
						
						sbAttrbuteList.AppendFormat("ByVal {1} As {0}, _ " + GlobalVar.cTab[2], new object[] { cDotNetDataType, cFieldName });
						sbVariableList.AppendFormat(GlobalVar.cTab[2] + "Me._{0} = {0}", new object[] { cFieldName });

						sbDataReader.AppendFormat(GlobalVar.cTab[2] + "If Not (aReader({1}{0}{1}) Is DBNull.Value) Then", new object[] { cFieldName, "\"" });
						sbDataReader.AppendFormat(GlobalVar.cTab[3] + "o.{0} = aReader.Get{1}(aReader.GetOrdinal({2}{0}{2}))", new object[] { cFieldName, cSqlDataType, "\"" });
						sbDataReader.AppendFormat(GlobalVar.cTab[2] + "End If");

						//if (blnWcf != true)
						//{
						sbEnumList.Append(GlobalVar.cTab[1] + cFieldName);
						//}

						sbSelectCaseList.AppendFormat(GlobalVar.cTab[4] + "Case {0}SortTypes.{1}", new object[] { cTableName, cFieldName });
						sbSelectCaseList.AppendFormat(GlobalVar.cTab[5] + "Return CType(obj2, {0}).{1}.CompareTo(CType(o, {0}).{1})", new object[] { cTableName, cFieldName });
					}
					else
					{
						PropertyVb(sw, cDotNetDataType, cFieldName, cPosition, cIsNullable, cKeyType, blnWcf, blnEdm, blnOther);

						if (sbAttrbuteList != null)
							sbAttrbuteList.AppendFormat("ByVal {1} As {0}, _ " + GlobalVar.cTab[2], new object[] { cDotNetDataType, cFieldName });

						if (sbVariableList != null)
							sbVariableList.AppendFormat(GlobalVar.cTab[2] + "Me._{0} = {0}", new object[] { cFieldName });

						if (sbDataReader != null)
						{
							sbDataReader.AppendFormat(GlobalVar.cTab[2] + "If Not (aReader({1}{0}{1}) Is DBNull.Value) Then", new object[] { cFieldName, "\"" });
							sbDataReader.AppendFormat(GlobalVar.cTab[3] + "o.{0} = aReader.Get{1}(aReader.GetOrdinal({2}{0}{2}))", new object[] { cFieldName, cSqlDataType, "\"" });
							sbDataReader.AppendFormat(GlobalVar.cTab[2] + "End If");
						}

						//if (blnWcf != true)
						//{
						if (sbEnumList != null) 
							sbEnumList.Append(GlobalVar.cTab[1] + cFieldName);
						//}

						if (sbSelectCaseList != null)
						{
							sbSelectCaseList.AppendFormat(GlobalVar.cTab[4] + "Case {0}SortTypes.{1}", new object[] { lstrOldTableName, cFieldName });
							sbSelectCaseList.AppendFormat(GlobalVar.cTab[5] + "Return CType(obj2, {0}).{1}.CompareTo(CType(o, {0}).{1})", new object[] { lstrOldTableName, cFieldName });
						}
					}

					lstrOldTableName = cTableName;
				}


				// after at end of recordset, we still need to append parts of bottom and collecton
				//sb.Append(sbAttrbuteList.ToString());	
				if (sbAttrbuteList != null)
				{
					ConstructorVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());
					if (blnWcf != true)
					{
						NewVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbVariableList.ToString());
					
						CompareToVb(sw, lstrOldTableName, strFstColumn);
					}
					GetEntityVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
					GetCollectionVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
					GetListVb(sw, lstrOldTableName, sbAttrbuteList.ToString(), sbDataReader.ToString());
				}

				// for end of Clase 
				sb = new StringBuilder("");
				sb.Append(GlobalVar.cTab[0] + "End Class");
				sb.Append("");

				output = output + sb.ToString();
				sw.WriteLine(sb.ToString());

				//if (blnWcf != true)
				//{
				if (sbEnumList != null) 
					EnumVb(sw, lstrOldTableName, sbEnumList.ToString());
				//}

				if (sbSelectCaseList != null)
					CollectionClassVb(sw, lstrOldTableName, sbSelectCaseList.ToString(), blnWcf, blnEdm, blnOther);

				// for end of namespace
				sb = new StringBuilder("");
				sb.Append(GlobalVar.cTab[0] + "End Namespace ");

				output = output + sb.ToString();
				sw.WriteLine(sb.ToString());

				_logWriter.InfoFormat("Created an entity for table {0} and saved it as the file: {0}.vb", cTableName);

				sw.Flush();
				sw.Close();

				if (j >= checkedListBox1.CheckedItems.Count - 1)
					return "The vb.net entities have been generated!";
			}

			return "No vb.net entitiy has been generated!";
		}


		public void HeadingVb(StreamWriter sw, string tstrClassName, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			var entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.VB"];

			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
			sb.Append(GlobalVar.cTab[0] + "' <autogenerated>");
			sb.Append(GlobalVar.cTab[0] + "' \t\tClass Name: ");
			sb.Append(tstrClassName +".vb");
			sb.Append(GlobalVar.cTab[0] + "' \t\tObject purpose: The class is designed for an encapsulation data of a database. ");
			sb.Append(GlobalVar.cTab[0] + "' \t\ttable. The class should be used in both client and server sides, if your system ");
			sb.Append(GlobalVar.cTab[0] + "' \t\tis designed as a multi-tier one.");
			sb.Append(GlobalVar.cTab[0] + "' ");

			sb.Append(GlobalVar.cTab[0] + "' \t\tDesigned & developed by: Ben Li. ");
			sb.Append(GlobalVar.cTab[0] + "' \t\tThe last modified date: 17/02/2006.");
			sb.Append(GlobalVar.cTab[0] + "' \t\t==================================.");
			sb.Append(GlobalVar.cTab[0] + "' \t\tScripts generated date: " + System.DateTime.Now + ".");
			sb.Append(GlobalVar.cTab[0] + "' </autogenerated> ");
			sb.Append(GlobalVar.cTab[0] + "' ");

			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
			sb.Append(GlobalVar.cTab[0] + "' Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.");
			sb.Append(GlobalVar.cTab[0] + "' All rights reserved. ");
			sb.Append(GlobalVar.cTab[0] + "' ");
			sb.Append(GlobalVar.cTab[0] + "' THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' ");
			sb.Append(GlobalVar.cTab[0] + "' WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT ");
			sb.Append(GlobalVar.cTab[0] + "' NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS ");
			sb.Append(GlobalVar.cTab[0] + "' FOR A PARTICULAR PURPOSE.");
			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
			sb.Append(GlobalVar.cTab[1] + " ");

			sb.Append(GlobalVar.cTab[0] + " ");
			sb.Append(GlobalVar.cTab[0] + "Imports System ");
			sb.Append(GlobalVar.cTab[0] + "Imports System.Collections ");
			sb.Append(GlobalVar.cTab[0] + "Imports System.Collections.Generic ");
			sb.Append(GlobalVar.cTab[0] + "Imports System.Data.SqlClient ");
			sb.Append(GlobalVar.cTab[0] + "Imports System.Data.Linq.Mapping ");
			sb.Append(GlobalVar.cTab[0] + "Imports System.Runtime.Serialization ");
			sb.Append(GlobalVar.cTab[0] + "Imports GUIDE.Library.VB ");


			sb.Append(GlobalVar.cTab[0] + " ");
			sb.Append(blnWcf ? GlobalVar.cTab[0] + "Namespace " + entityNamespace + ".DataContract" : GlobalVar.cTab[0] + "Namespace " + entityNamespace);
			
			sb.Append(GlobalVar.cTab[0] + " ");
			sb.Append(GlobalVar.cTab[0] );

			if (blnEdm)
			{
				sb.Append(GlobalVar.cTab[0] + "<Table(Name = \"" + tstrClassName + "\")>");
			}
			else if (blnWcf)
			{
				sb.Append("<DataContract(Namespace =\"" + entityNamespace + "\")>");
			}


			sb.Append(GlobalVar.cTab[0] + "Public Class ");
			sb.Append(tstrClassName);
			if (blnOther)
			{
				sb.Append(GlobalVar.cTab[1] + "Inherits LibraryBase");
				sb.Append(GlobalVar.cTab[1] + "Implements System.IComparable");
			}
			sb.Append(GlobalVar.cTab[0] );

			if (blnWcf)
			{
				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[1] + "<OnSerializing>");
				sb.Append(GlobalVar.cTab[1] + "Sub OnSerializing(StreamingContext context)");
				sb.Append(GlobalVar.cTab[2] + "'call something()");
				sb.Append(GlobalVar.cTab[1] + "End Sub");
				sb.Append(GlobalVar.cTab[0] );

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[1] + "<OnSerialized>");
				sb.Append(GlobalVar.cTab[1] + "Sub OnSerialized(StreamingContext context)");
				sb.Append(GlobalVar.cTab[2] + "'call something()");
				sb.Append(GlobalVar.cTab[1] + "End Sub");
				sb.Append(GlobalVar.cTab[0] );

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[1] + "<OnDeserializing>");
				sb.Append(GlobalVar.cTab[1] + "Sub OnDeserializing(StreamingContext context)");
				sb.Append(GlobalVar.cTab[2] + "'call something()");
				sb.Append(GlobalVar.cTab[1] + "End Sub");
				sb.Append(GlobalVar.cTab[0] );

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[1] + "<OnDeserialized>");
				sb.Append(GlobalVar.cTab[1] + "Sub OnDeserialized(StreamingContext context)");
				sb.Append(GlobalVar.cTab[2] + "'call something()");
				sb.Append(GlobalVar.cTab[1] + "End Sub");
				sb.Append(GlobalVar.cTab[0] );
			}
			else if (blnOther)
			{
				sb.Append(GlobalVar.cTab[1] + "Public Delegate Sub DlgPropertyChangeHandler(ByVal o As " + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[1] + "Public Shared Event OnPropertyChanged As DlgPropertyChangeHandler");
				sb.Append(GlobalVar.cTab[1] + "Public Shared Event OnPropertyChanged As OnPropertyNew");
				sb.Append(GlobalVar.cTab[1] + "Public Shared Event OnPropertyChanged As OnPropertyDeleted");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "Public Sub SomethingChanged(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsDirty = True");
				sb.Append(GlobalVar.cTab[1] + "End Sub");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "Public Sub SomethingNew(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsNew = True");
				sb.Append(GlobalVar.cTab[1] + "End Sub");

				sb.Append(GlobalVar.cTab[0]);
				sb.Append(GlobalVar.cTab[1] + "Public Sub SomethingDeleted(" + tstrClassName + " o" + tstrClassName + ")");
				sb.Append(GlobalVar.cTab[2] + "o" + tstrClassName + ".IsDeleted = True");
				sb.Append(GlobalVar.cTab[1] + "End Sub");
				sb.Append(GlobalVar.cTab[0]);
			}

			output = output + sb.ToString();
			sw.WriteLine(sb.ToString());
		}


		public void PropertyVb(StreamWriter sw, string tstrAttributeType, string tstrAttributeName,
								string lstrAttriPosition, string IsNullable, string cKeyType,
								Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			var sb = new StringBuilder("");


			// // Property attributes
			if (blnEdm)
			{
				//sb.AppendFormat(GlobalVar.cTab[1] + "<Column> _");
				sb.AppendFormat(GlobalVar.cTab[1] + "<Column(Name=\"{0}\", IsPrimaryKey={1}, CanBeNull={2})> _",
													new object[] { tstrAttributeName, 
													cKeyType == "PRIMARY KEY" ? "true" : "false", 
													IsNullable == "YES" ? "true" : "false"});


			}
			else if (blnWcf)
			{
				//sb.AppendFormat(GlobalVar.cTab[1] + "<DataMember(Name = \"" + tstrAttributeName + "\")> _");

				sb.AppendFormat(GlobalVar.cTab[1] + "<DataMember(Name =\"" + tstrAttributeName + "\", Order = " + lstrAttriPosition + ")> _");
				sb.AppendFormat(GlobalVar.cTab[1] + "<Column(Name=\"{0}\", IsPrimaryKey={1}, CanBeNull={2})> _",
											new object[] { tstrAttributeName, 
															cKeyType == "PRIMARY KEY" ? "true" : "false", 
															IsNullable == "YES" ? "true" : "false"});
			}


			// Property values
			if (blnEdm)
			{
				if (IsNullable == "NO" || tstrAttributeType == "string")
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "Public Property {1}() As {0} ", new object[] { tstrAttributeType, tstrAttributeName });
				}
				else
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "Public Property {1}() As Nullable<{0}> ", new object[] { tstrAttributeType, tstrAttributeName });
				}
			}
			else
			{
				sb.AppendFormat(GlobalVar.cTab[1] + "Public Property {1}() As {0} ", new object[] { tstrAttributeType, tstrAttributeName });
			}

			sb.Append(GlobalVar.cTab[2] + "Get");
			sb.Append(GlobalVar.cTab[3] + "Return _" + tstrAttributeName);
			sb.Append(GlobalVar.cTab[2] + "End Get");

			sb.Append(GlobalVar.cTab[2] + "Set(ByVal Value As " + tstrAttributeType + ")");
			if (blnOther)
			{
				sb.Append(GlobalVar.cTab[3] + "If (Me._" + tstrAttributeName + " <> Value) Then RaiseEvent OnPropertyChanged(Me)");
			}

			sb.Append(GlobalVar.cTab[3] + "Me._" + tstrAttributeName + "= value");
			sb.Append(GlobalVar.cTab[2] + "End Set");
			sb.Append(GlobalVar.cTab[1] + "End Property");

			if (blnEdm)
			{
				if (IsNullable == "NO" || tstrAttributeType == "string")
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "Private _{1} As {0}", new object[] { tstrAttributeType, tstrAttributeName });
				}
				else
				{
					sb.AppendFormat(GlobalVar.cTab[1] + "Private _{1} As Nullable<{0}>", new object[] { tstrAttributeType, tstrAttributeName });
				}
			} 
			else
			{
				sb.AppendFormat(GlobalVar.cTab[1] + "Private _{1} As {0}", new object[] { tstrAttributeType, tstrAttributeName });	
			}

			
			sb.Append(GlobalVar.cTab[0] );

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void EndingVb(StreamWriter sw, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(tstrAttrbuteList + ")");
			sb.Append("\t" + tstrVariableList);
			sb.Append(GlobalVar.cTab[1] + "End Sub");
			sb.Append(GlobalVar.cTab[0] + "End Class");
			sb.Append(" ");

			output = output + sb.ToString();
			sw.WriteLine(sb.ToString());
		}

		public void ConstructorVb(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[1] + "' <summary>Default Constructor</summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Sub New" + "()");
			sb.Append(GlobalVar.cTab[2] + "'Please append default values here");
			sb.Append(GlobalVar.cTab[1] + "End Sub");

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[1] + "' <summary>User defined Constructor</summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Sub "+ "New " + "(");
			sb.Append(tstrAttrbuteList.TrimEnd(new char[] { ',', ' ', '\r', '\t', '\n', '_' }) + ")");
			sb.Append("\t" + tstrVariableList);
			sb.Append(GlobalVar.cTab[1] + "End Sub");
			sb.Append(GlobalVar.cTab[0]);

			output = output + sb.ToString();
			sw.WriteLine(sb.ToString());
		}

		public void EnumVb(StreamWriter sw, string lstrTableName, string tstrVariableList)
		{

			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0]);
			sb.Append(GlobalVar.cTab[0] + "' <summary>Create Enum </summary>");
			sb.Append(GlobalVar.cTab[0] + "Public Enum " + lstrTableName + "SortTypes");

			sb.Append(tstrVariableList);
			sb.Append(GlobalVar.cTab[0] + "End Enum");
			sb.Append(GlobalVar.cTab[0]);

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}

		public void CompareToVb(StreamWriter sw, string lstrTableName, string tstrFstColumn)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[1] + "'<summary>Implements CompareTo </summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo");
			sb.Append(GlobalVar.cTab[2] + "Dim o As " + lstrTableName);
			sb.Append(GlobalVar.cTab[2] + "If obj Is Nothing Then Return 1");
			sb.Append(GlobalVar.cTab[2] + "o = CType(obj, " + lstrTableName + ")");
			sb.Append(GlobalVar.cTab[2] + "Return Me." + tstrFstColumn + ".CompareTo(o." + tstrFstColumn + ")");
			sb.Append(GlobalVar.cTab[1] + "End Function");
			sb.Append(GlobalVar.cTab[0]);

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}

		public string BuildSubClassVb(StreamWriter sw, string lstrTableName, string cSelectCaseList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[1] + "'<summary>Create Internal Class</summary>");
			sb.Append(GlobalVar.cTab[1] + "Friend Class " + lstrTableName + "Sort");
			sb.Append(GlobalVar.cTab[2] + "Implements IComparer");

			sb.Append(GlobalVar.cTab[2] + "Private _sort As " + lstrTableName + "SortTypes");
			sb.Append(GlobalVar.cTab[2] + "Private _asc As Boolean");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[2] + "Public Sub New(ByVal sort As " + lstrTableName + "SortTypes)");
			sb.Append(GlobalVar.cTab[3] + "_sort = sort");
			sb.Append(GlobalVar.cTab[3] + "_asc = True");
			sb.Append(GlobalVar.cTab[2] + "End Sub");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[2] + "Public Sub New(ByVal sort As " + lstrTableName + "SortTypes, ByVal asc As Boolean)");
			sb.Append(GlobalVar.cTab[3] + "_sort = sort");
			sb.Append(GlobalVar.cTab[3] + "_asc = asc");
			sb.Append(GlobalVar.cTab[2] + "End Sub");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[2] + "Public Function Compare(ByVal obj1 As Object, ByVal obj2 As Object) As Integer Implements IComparer.Compare");
			sb.Append(GlobalVar.cTab[3] + "Dim x As Object");
			sb.Append(GlobalVar.cTab[3] + "If _asc Then");
			sb.Append(GlobalVar.cTab[4] + "x = obj2 : obj2 = obj1 : obj1 = x");
			sb.Append(GlobalVar.cTab[3] + "End If");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[3] + "Dim o As " + lstrTableName);
			sb.Append(GlobalVar.cTab[3] + "If obj1 Is Nothing Then Return 1");
			sb.Append(GlobalVar.cTab[3] + "o = CType(obj1, " + lstrTableName + ")");
			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[3] + "Select Case _sort");
			sb.Append(GlobalVar.cTab[4] + cSelectCaseList);
			sb.Append(GlobalVar.cTab[3] + "End Select");
			sb.Append(GlobalVar.cTab[2] + "End Function");
			sb.Append(GlobalVar.cTab[1] + "End Class");

			return sb.ToString();
		}

		public void NewVb(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[1] + "' <summary> Get new instance of class </summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Shared Function");
			sb.Append(" CreateNew" + lstrTableName);
			sb.Append("(");
			sb.Append(tstrAttrbuteList.TrimEnd(new char[] { ',', ' ', '\r', '\t', '\n', '_' }) + ") As " + lstrTableName);

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[2] + "Dim o As New " + lstrTableName);

			sb.Append(GlobalVar.cTab[1] + tstrVariableList.Replace("Me._", "o."));
			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[2] + "Return o");

			sb.Append(GlobalVar.cTab[1] + "End Function");

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void GetEntityVb(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[1] + "' <summary> Get an instance of class </summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Shared Function");
			sb.Append(" Get_" + lstrTableName);
			sb.Append("(");

			sb.Append("ByVal aReader As SqlDataReader) As " + lstrTableName);

			sb.Append(GlobalVar.cTab[2] + "Dim o As New " + lstrTableName);
			sb.Append(GlobalVar.cTab[1] + tstrVariableList);

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[2] + "Return o");

			sb.Append(GlobalVar.cTab[1] + "End Function");

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void GetCollectionVb(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[1] + "' <summary> Get an collection of class </summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Shared Function GetCollection_" + lstrTableName + "(");
			sb.Append("ByVal aReader As SqlDataReader) As " + lstrTableName + "_Collection");

			sb.Append(GlobalVar.cTab[2] + "Dim oo As New " + lstrTableName + "_Collection");
			//sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[2] + "While aReader.Read");
			sb.Append(GlobalVar.cTab[3] + "oo.Add(Get_" + lstrTableName + "(aReader))");
			sb.Append(GlobalVar.cTab[2] + "End While");
			sb.Append(GlobalVar.cTab[2] + "Return oo");

			sb.Append(GlobalVar.cTab[1] + "End Function");

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void GetListVb(StreamWriter sw, string lstrTableName, string tstrAttrbuteList, string tstrVariableList)
		{
			var sb = new StringBuilder("");

			sb.Append(GlobalVar.cTab[1] + "' <summary> Get an List of class </summary>");
			sb.Append(GlobalVar.cTab[1] + "Public Shared Function GetList_" + lstrTableName + "(");
			sb.Append("ByVal aReader As SqlDataReader) As IList(Of " + lstrTableName + ")");

			sb.Append(GlobalVar.cTab[2] + "Dim oo As IList(Of " + lstrTableName + ") = New List(Of " + lstrTableName + ")()");
			sb.Append(GlobalVar.cTab[2] + "While aReader.Read");
			sb.Append(GlobalVar.cTab[3] + "oo.Add(Get_" + lstrTableName + "(aReader))");
			sb.Append(GlobalVar.cTab[2] + "End While");
			sb.Append(GlobalVar.cTab[2] + "Return oo");

			sb.Append(GlobalVar.cTab[1] + "End Function");

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}


		public void CollectionClassVb(StreamWriter sw, string strTableName, string cSelectCaseList, Boolean blnWcf, Boolean blnEdm, Boolean blnOther)
		{
			var entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.VB"];

			var sb = new StringBuilder("");


			sb.Append(GlobalVar.cTab[0] );
			if (blnWcf)
			{
				sb.Append(GlobalVar.cTab[0] + "<CollectionDataContract(Namespace =\"" + entityNamespace + "\")>");
			}
			
			sb.Append(GlobalVar.cTab[0] + "Public Class " + strTableName + "_Collection");
			sb.Append(GlobalVar.cTab[1] + "Inherits mhcbCollectionBase ");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[1] + "Public Sub New()");
			sb.Append(GlobalVar.cTab[1] + "End Sub ");
			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[1] + "Public Overloads Sub Sort(ByVal oSortType As " + strTableName + "SortTypes)");
			sb.Append(GlobalVar.cTab[2] + "Dim comp As New " + strTableName + "Sort(oSortType)");
			sb.Append(GlobalVar.cTab[2] + "Me.InnerList.Sort(comp)");
			sb.Append(GlobalVar.cTab[1] + "End Sub ");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(GlobalVar.cTab[1] + "Public Overloads Sub Sort(ByVal oSortType As " + strTableName + "SortTypes, ByVal asc As Boolean)");
			sb.Append(GlobalVar.cTab[2] + "Dim comp As New " + strTableName + "Sort(oSortType, asc)");
			sb.Append(GlobalVar.cTab[2] + "Me.InnerList.Sort(comp)");
			sb.Append(GlobalVar.cTab[1] + "End Sub ");
			sb.Append(GlobalVar.cTab[0] );

			sb.Append(BuildSubClassVb(sw, strTableName, cSelectCaseList));

			sb.Append(GlobalVar.cTab[0] );
			sb.Append(GlobalVar.cTab[0] + "End Class ");

			sb.Append(GlobalVar.cTab[0] );

			output = output + sb;
			sw.WriteLine(sb.ToString());
		}

	}
}
