// =============================================================================== 
// 		Class Name: clsBusinessService_VB.cs
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
	/// Summary description for clsBusinessService_VB.
	/// </summary>
	public class BusinessSvcVb : BusinessSvcBase
	{
		public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(BusinessSvcVb));

		public BusinessSvcVb()
		{
		}

		public BusinessSvcVb(string server, string dbName, string userName, string password) 
			:  base(server, dbName, userName, password)
		{
			//BasicConfigurator.Configure();
			XmlConfigurator.Configure();
		}


		//public string CreateBsClassFilesVb(SqlDataReader dr1)
		//{
		//	string dsNamespace = ConfigurationManager.AppSettings["DSNamespace.VB"];
		//	string entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.VB"];
		//	string dsClassPrefix = ConfigurationManager.AppSettings["DSClassPrefix"];

		//	var oclsData = new DataSvc(_Server, _DbName, _UserName, _Password);

		//	if (dr1 != null)
		//	{
		//		while (dr1.Read())
		//		{
		//			string cSpName = dr1.GetString(0);
		//			cSpName = cSpName.Substring(0, 1).ToUpper() + cSpName.Substring(1, cSpName.Length - 1);

		//			var dr = oclsData.drGetSParams(cSpName);

		//			StreamWriter sw = null;
		//			StringBuilder sb = null;

		//			sb = new StringBuilder(dsClassPrefix + "_" + cSpName);
		//			sb.Append(".vb");

		//			var oFileInfo = new FileInfo(sb.ToString());
		//			sw = oFileInfo.CreateText();

		//			sb = new StringBuilder("");

		//			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
		//			sb.Append(GlobalVar.cTab[0] + "' <autogenerated>");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tClass Name: ");
		//			sb.Append(dsClassPrefix + "_" + cSpName);
		//			sb.Append(".vb");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tObject purpose: The class is designed for manipulating data in a database table. ");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tIt can be used to fetch data via data access component and return as a DataReader ");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tor DataSet. This component could be fit in the position of middle tier if the system  ");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tis designed as multi-tiers. ");
		//			sb.Append(GlobalVar.cTab[0] + "' ");

		//			sb.Append(GlobalVar.cTab[0] + "' \t\tDesigned & developed by: Ben Li. ");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tThe last modified date: 17/02/2006.");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\t");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\t================================================. ");
		//			sb.Append(GlobalVar.cTab[0] + "' \t\tScripts generated date: " + System.DateTime.Now + ".");

		//			sb.Append(GlobalVar.cTab[0] + "' </autogenerated> ");
		//			sb.Append(GlobalVar.cTab[0] + "' ");

		//			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
		//			sb.Append(GlobalVar.cTab[0] + "' Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.");
		//			sb.Append(GlobalVar.cTab[0] + "' All rights reserved. ");
		//			sb.Append(GlobalVar.cTab[0] + "' ");
		//			sb.Append(GlobalVar.cTab[0] + "' THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' ");
		//			sb.Append(GlobalVar.cTab[0] + "' WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT ");
		//			sb.Append(GlobalVar.cTab[0] + "' NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS ");
		//			sb.Append(GlobalVar.cTab[0] + "' FOR A PARTICULAR PURPOSE.");
		//			sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
		//			sb.Append(GlobalVar.cTab[0] + " ");

		//			sb.Append(GlobalVar.cTab[0] + "Imports System");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.Data");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.IO");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.Data.SqlClient");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.Data.OleDb");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.Reflection");
		//			sb.Append(GlobalVar.cTab[0] + "Imports System.EnterpriseServices");

		//			sb.Append(GlobalVar.cTab[0] );
		//			sb.Append(GlobalVar.cTab[0] + "Imports " + entityNamespace + " ");
		//			sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.CmdBuilder");
		//			sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.DataAccess");
		//			sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.Trace");

		//			sb.Append(GlobalVar.cTab[0] );
		//			sb.Append(GlobalVar.cTab[0] + "Namespace " + dsNamespace);		// begin of namespace

		//			sb.Append(GlobalVar.cTab[0] );
		//			sb.Append(GlobalVar.cTab[0] + "<Transaction(TransactionOption.Required)> _");
		//			sb.Append(GlobalVar.cTab[0] + "Public Class " + dsClassPrefix + "_" + cSpName + "_DataSev");
		//			sb.Append(GlobalVar.cTab[1] + "'Inherits COMServiceBase ");

		//			string className = dsClassPrefix + "_" + cSpName + "";
		//			string spName = "\"" + cSpName + "\"";
		//			string methodName = "\"" + "invoke_" + cSpName + "\"";
		//			string databaseName = "\"" + _DbName + "\"";

		//			sb.Append(GlobalVar.cTab[1] + "<SqlCommandMethod(CommandType.StoredProcedure, " + spName + ")> _");

		//			// get a parameter string for the function
		//			CreateParameterList(dr);

		//			if (sb1.ToString().Equals(""))
		//			{
		//				sb.Append(GlobalVar.cTab[1] + "Public Function invoke_" + cSpName + "( ");
		//			}
		//			else
		//			{
		//				sb.Append(GlobalVar.cTab[1] + "Public Function invoke_" + cSpName + "( _ ");
		//			}

		//			sb.Append(sb1);
		//			sb.Append(" ) AS SqlDataReader ");
		//			sb.Append(GlobalVar.cTab[0] );	// begin of method

		//			sb.Append(GlobalVar.cTab[2] + "Dim mtype As Type = GetType(" + className + ")");
		//			sb.Append(GlobalVar.cTab[2] + "Dim info As MethodInfo = CType(mtype.GetMethod(" + methodName + "), MethodInfo)");
		//			sb.Append(GlobalVar.cTab[0] );

		//			sb.Append(GlobalVar.cTab[2] + "Dim oDE As DataExecution = New DataExecution() ");
		//			sb.Append(GlobalVar.cTab[2] + "Dim dbConnection As SqlConnection = New SqlConnection ");
		//			sb.Append(GlobalVar.cTab[2] + "Dim oCommand As SqlCommand ");
		//			sb.Append(GlobalVar.cTab[2] + "Dim dr As SqlDataReader = Nothing ");
		//			sb.Append(GlobalVar.cTab[0] );


		//			//*** set default value for output parameter *** 
		//			sb.Append(sb3);
		//			sb.Append(GlobalVar.cTab[0] );

		//			sb.Append(GlobalVar.cTab[2] + "Try");		// begin of try
		//			sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceStart()");
		//			sb.Append(GlobalVar.cTab[3] + "dbConnection.ConnectionString = oDE.getDefaultConnectionString()");
		//			sb.Append(GlobalVar.cTab[3] + "dbConnection.Open()");
		//			sb.Append(GlobalVar.cTab[0] );


		//			if (sb2.ToString().Equals(""))
		//			{
		//				sb.Append(GlobalVar.cTab[3] + "oCommand = SqlCommandGenerator.GenerateCommand(dbConnection, info, new object() { ");
		//			}
		//			else
		//			{
		//				sb.Append(GlobalVar.cTab[3] + "oCommand = SqlCommandGenerator.GenerateCommand(dbConnection, info, new object() { _ ");
		//			}

		//			sb.Append(sb2);
		//			sb.Append(" })");
		//			sb.Append(GlobalVar.cTab[0] );

		//			sb.Append(GlobalVar.cTab[3] + "dr = oDE.ExecuteDataReader(oCommand, dbConnection)	\t\t' return DataReader");
		//			sb.Append(GlobalVar.cTab[3] + "'ds = oDE.ExecuteDataSet(oCommand, dbConnection)   \t\t' return DataSet");
		//			sb.Append(GlobalVar.cTab[3] + "'oDE.ExecuteNonQuery(oCommand, dbConnection)				\t\t' no return ");
		//			sb.Append(GlobalVar.cTab[3] + "'cRtn = CType(oDE.ExecuteScalar(oCommand, dbConnection), String)				\t\t' return string ");

		//			sb.Append(GlobalVar.cTab[0] );
		//			sb.Append(GlobalVar.cTab[3] + "'ContextUtil.SetComplete ");

		//			sb.Append(GlobalVar.cTab[2] + "Catch e As Exception ");
		//			sb.Append(GlobalVar.cTab[3] + "'ContextUtil.SetAbort ");
		//			sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceException(e)");
		//			sb.Append(GlobalVar.cTab[2] + "Finally ");


		//			sb.Append(GlobalVar.cTab[3] + "oCommand = Nothing ");
		//			sb.Append(GlobalVar.cTab[3] + "dbConnection.Close() ");
		//			sb.Append(GlobalVar.cTab[3] + "dbConnection = Nothing ");
		//			sb.Append(GlobalVar.cTab[3] + "oDE = Nothing ");
		//			sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceEnd()");
		//			sb.Append(GlobalVar.cTab[2] + "End Try");

		//			sb.Append(GlobalVar.cTab[0] );
		//			sb.Append(GlobalVar.cTab[2] + "Return dr");   // end of try

		//			sb.Append(GlobalVar.cTab[1] + "End Function");	// end of method
		//			sb.Append(GlobalVar.cTab[0] + "End Class");				// end of classs

		//			sb.Append(GlobalVar.cTab[0] + "End Namespace");		// end of namespace

		//			output = output + sb;
		//			sw.WriteLine(sb.ToString());

		//			sw.Flush();
		//			sw.Close();

		//		}

		//		return "The vb.net data layer class has been generated and are copied into the clipborad!";
		//	}
		//	else
		//	{
		//		return "The no vb.net data layer class has been generated!";
		//	}
		//}



		public string CreateBsClassFilesVbV2(CheckedListBox checkedListBox1)
		{
			string dsNamespace = ConfigurationManager.AppSettings["DSNamespace.VB"];
			string entityNamespace = ConfigurationManager.AppSettings["EntityNamespace.VB"];
			string dsClassPrefix = ConfigurationManager.AppSettings["DSClassPrefix"];

			var oclsData = new DataSvc(_Server, _DbName, _UserName, _Password);

			for (int j = 0; j < checkedListBox1.CheckedItems.Count; j++)
			{
				string cSpName = checkedListBox1.CheckedItems[j].ToString();
				cSpName = cSpName.Substring(0, 1).ToUpper() + cSpName.Substring(1, cSpName.Length - 1);

				var dr = oclsData.drGetSParams(cSpName);

				StreamWriter sw = null;
				StringBuilder sb = null;

				sb = new StringBuilder(dsClassPrefix + "_" + cSpName);
				sb.Append(".vb");

				var oFileInfo = new FileInfo(sb.ToString());
				sw = oFileInfo.CreateText();

				sb = new StringBuilder("");

				sb.Append(GlobalVar.cTab[0] + "' =============================================================================== ");
				sb.Append(GlobalVar.cTab[0] + "' <autogenerated>");
				sb.Append(GlobalVar.cTab[0] + "' \t\tClass Name: ");
				sb.Append(dsClassPrefix + "_" + cSpName);
				sb.Append(".vb");
				sb.Append(GlobalVar.cTab[0] + "' \t\tObject purpose: The class is designed for manipulating data in a database table. ");
				sb.Append(GlobalVar.cTab[0] + "' \t\tIt can be used to fetch data via data access component and return as a DataReader ");
				sb.Append(GlobalVar.cTab[0] + "' \t\tor DataSet. This component could be fit in the position of middle tier if the system  ");
				sb.Append(GlobalVar.cTab[0] + "' \t\tis designed as multi-tiers. ");
				sb.Append(GlobalVar.cTab[0] + "' ");

				sb.Append(GlobalVar.cTab[0] + "' \t\tDesigned & developed by: Ben Li. ");
				sb.Append(GlobalVar.cTab[0] + "' \t\tThe last modified date: 17/02/2006.");
				sb.Append(GlobalVar.cTab[0] + "' \t\t");
				sb.Append(GlobalVar.cTab[0] + "' \t\t================================================. ");
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
				sb.Append(GlobalVar.cTab[0] + " ");

				sb.Append(GlobalVar.cTab[0] + "Imports System");
				sb.Append(GlobalVar.cTab[0] + "Imports System.Data");
				sb.Append(GlobalVar.cTab[0] + "Imports System.IO");
				sb.Append(GlobalVar.cTab[0] + "Imports System.Data.SqlClient");
				sb.Append(GlobalVar.cTab[0] + "Imports System.Data.OleDb");
				sb.Append(GlobalVar.cTab[0] + "Imports System.Reflection");
				sb.Append(GlobalVar.cTab[0] + "Imports System.EnterpriseServices");

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[0] + "Imports " + entityNamespace + " ");
				sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.CmdBuilder");
				sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.DataAccess");
				sb.Append(GlobalVar.cTab[0] + "Imports mhcb.vb.Shared.Trace");

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[0] + "Namespace " + dsNamespace);		// begin of namespace

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[0] + "<Transaction(TransactionOption.Required)> _");
				sb.Append(GlobalVar.cTab[0] + "Public Class " + dsClassPrefix + "_" + cSpName + "_DataSev");
				sb.Append(GlobalVar.cTab[1] + "'Inherits COMServiceBase ");

				string className = dsClassPrefix + "_" + cSpName + "";
				string spName = "\"" + cSpName + "\"";
				string methodName = "\"" + "invoke_" + cSpName + "\"";
				string databaseName = "\"" + _DbName + "\"";

				sb.Append(GlobalVar.cTab[1] + "<SqlCommandMethod(CommandType.StoredProcedure, " + spName + ")> _");

				// get a parameter string for the function
				CreateParameterList(dr);

				if (sb1.ToString().Equals(""))
				{
					sb.Append(GlobalVar.cTab[1] + "Public Function invoke_" + cSpName + "( ");
				}
				else
				{
					sb.Append(GlobalVar.cTab[1] + "Public Function invoke_" + cSpName + "( _ ");
				}

				sb.Append(sb1);
				sb.Append(" ) AS SqlDataReader ");
				sb.Append(GlobalVar.cTab[0] );	// begin of method

				sb.Append(GlobalVar.cTab[2] + "Dim mtype As Type = GetType(" + className + ")");
				sb.Append(GlobalVar.cTab[2] + "Dim info As MethodInfo = CType(mtype.GetMethod(" + methodName + "), MethodInfo)");
				sb.Append(GlobalVar.cTab[0] );


				sb.Append(GlobalVar.cTab[2] + "Dim oDE As DataExecution = New DataExecution() ");
				sb.Append(GlobalVar.cTab[2] + "Dim dbConnection As SqlConnection = New SqlConnection ");
				sb.Append(GlobalVar.cTab[2] + "Dim oCommand As SqlCommand ");
				sb.Append(GlobalVar.cTab[2] + "Dim dr As SqlDataReader = Nothing ");
				sb.Append(GlobalVar.cTab[0] );


				//*** set default value for output parameter *** 
				sb.Append(sb3);
				sb.Append(GlobalVar.cTab[0] );

				sb.Append(GlobalVar.cTab[2] + " Try");		// begin of try
				sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceStart()");
				sb.Append(GlobalVar.cTab[3] + "dbConnection.ConnectionString = oDE.getDefaultConnectionString()");
				sb.Append(GlobalVar.cTab[3] + "dbConnection.Open()");
				sb.Append(GlobalVar.cTab[0] );

				if (sb2.ToString().Equals(""))
				{
					sb.Append(GlobalVar.cTab[3] + "oCommand = SqlCommandGenerator.GenerateCommand(dbConnection, info, new object() { ");
				}
				else
				{
					sb.Append(GlobalVar.cTab[3] + "oCommand = SqlCommandGenerator.GenerateCommand(dbConnection, info, new object() { _ ");
				}

				sb.Append(sb2);
				sb.Append(" })");
				sb.Append(GlobalVar.cTab[0] );

				sb.Append(GlobalVar.cTab[3] + "dr = oDE.ExecuteDataReader(oCommand, dbConnection)	\t\t' return DataReader");
				sb.Append(GlobalVar.cTab[3] + "'ds = oDE.ExecuteDataSet(oCommand, dbConnection)   \t\t' return DataSet");
				sb.Append(GlobalVar.cTab[3] + "'oDE.ExecuteNonQuery(oCommand, dbConnection)				\t\t' no return ");
				sb.Append(GlobalVar.cTab[3] + "'cRtn = CType(oDE.ExecuteScalar(oCommand, dbConnection), String)				\t\t' return string ");

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[3] + "'ContextUtil.SetComplete ");

				sb.Append(GlobalVar.cTab[2] + "Catch e As Exception ");
				sb.Append(GlobalVar.cTab[3] + "'ContextUtil.SetAbort ");
				sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceException(e)");
				sb.Append(GlobalVar.cTab[2] + "Finally ");

				sb.Append(GlobalVar.cTab[3] + "oCommand = Nothing ");
				sb.Append(GlobalVar.cTab[3] + "dbConnection.Close() ");
				sb.Append(GlobalVar.cTab[3] + "dbConnection = Nothing ");
				sb.Append(GlobalVar.cTab[3] + "oDE = Nothing ");
				sb.Append(GlobalVar.cTab[3] + "'//TraceMaker.TraceEnd()");
				sb.Append(GlobalVar.cTab[2] + "End Try");

				sb.Append(GlobalVar.cTab[0] );
				sb.Append(GlobalVar.cTab[2] + "Return dr");		// end of try

				sb.Append(GlobalVar.cTab[1] + "End Function");	// end of method
				sb.Append(GlobalVar.cTab[0] + "End Class");		// end of classs

				sb.Append(GlobalVar.cTab[0] + "End Namespace");	// end of namespace

				output = output + sb;
				sw.WriteLine(sb.ToString());

				_logWriter.InfoFormat("Created an method {0}_{1} for the stored proceddure {1}. Save it as the file: {0}_{1}.vb",
										dsClassPrefix, cSpName);

				sw.Flush();
				sw.Close();

				if (j >= checkedListBox1.CheckedItems.Count - 1)
					return "The vb.net data layer class has been generated!";
			}

			return "The no vb.net data layer class has been generated!";
		}


		private void CreateParameterList(SqlDataReader dr)
		{
			var i = 0;

			sb1 = new StringBuilder("");
			sb2 = new StringBuilder("");
			sb3 = new StringBuilder("");

			while (dr.Read())
			{
				string cParameterName = dr.GetString(3);
				string cDataTypeName = DataType.ToSystemTypeVb(dr.GetString(15));
				string cSqlDbType = DataType.ToSystemDataSqlDbType(dr.GetString(15));
				short iDataType = dr.GetInt16(9);

				short iParameterType = dr.GetInt16(5);

				int iDataLength = iDataType == 129 ? dr.GetInt32(10) : 0;

				if (cParameterName != "@RETURN_VALUE")
				{
					i += 1;

					if (i == 1)
					{
						if (iParameterType == 2)
						{
							if (iDataLength == 0)
							{
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + " " + ")> " + "ByRef p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
								sb3.Append(GlobalVar.cTab[3] + "p" + i.ToString(CultureInfo.InvariantCulture) + "=0");
							}
							else
							{
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + ", " + iDataLength + ")> " + "ByRef p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
								sb3.Append(GlobalVar.cTab[3] + "p" + i.ToString(CultureInfo.InvariantCulture) + "=0");
							}
						}
						else
						{
							if (iDataLength == 0)
							{
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + " " + ")> " + "ByVal p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
							}
							else
							{
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + ", " + iDataLength + ")> " + "ByVal p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
							}
						}
					}
					else
					{
						if (iParameterType == 2)
						{
							if (iDataLength == 0)
							{
								sb1.Append(", _");
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + " " + ")> " + "ByRef p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
								sb3.Append(GlobalVar.cTab[3] + "p" + i.ToString(CultureInfo.InvariantCulture) + "=0");
							}
							else
							{
								sb1.Append(", _");
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + ", " + iDataLength + ")> " + "ByRef p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
								sb3.Append(GlobalVar.cTab[3] + "p" + i.ToString(CultureInfo.InvariantCulture) + "=0");
							}
						}
						else
						{
							if (iDataLength == 0)
							{
								sb1.Append(", _");
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + " " + ")> " + "ByVal p" + i.ToString(CultureInfo.InvariantCulture) + " As " + cDataTypeName);
							}
							else
							{
								sb1.Append(", _");
								sb1.Append(GlobalVar.cTab[3] + "<SqlParameter(\"" + cParameterName.Substring(1) + "\", SqlDbType." + cSqlDbType + ", " + iDataLength + ")> " + "ByVal p" + i.ToString() + " As " + cDataTypeName);
							}
						}
					}


					if (i == 1)
					{
						sb2.Append(GlobalVar.cTab[4] + "p" + i.ToString(CultureInfo.InvariantCulture));
					}
					else
					{
						sb2.Append(", p" + i.ToString(CultureInfo.InvariantCulture));
					}
				}
			}

		}

	}
}
