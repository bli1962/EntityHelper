// =============================================================================== 
// 		Class Name: clsData.cs
// 		Object purpose: The class is designed for manipulating data in a database table. 
// 		It can be used to fetch data via data access component and return as a DataReader 
// 		or DataSet. This component could be fit in the position of middle tier if the system  
// 		is designed as multi-tiers. 
// 
// 		Designed by: Ben Li. 
// 		Written by:  Ben Li. 
//
// 		Codes generated Date: 05/08/2005 2:50:38 PM.
// 		Last Modified Date:   22/01/2014 12:22:00 PM.
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
using System.Data;
using System.Data.SqlClient;
//using mhcb.cs.Shared.Trace;
using log4net;
using log4net.Config;


namespace mhcb.EntityHelper
{
	/// <summary>
	/// Summary description for clsData.
	/// </summary>
	public class DataSvc
	{
		public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(DataSvc));

		private string SQL_CONN_STRING = string.Empty;

		public DataSvc()
		{
			
		}

		public DataSvc(string Server, string DbName, string UserName, string Password)
		{
			//BasicConfigurator.Configure();
			XmlConfigurator.Configure();
			CreateConnectionString(Server, DbName, UserName, Password);
		}

		private void CreateConnectionString(string BoxServer, string textBoxDbName, string textBoxUserName, string textBoxPassword)
		{
			try
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				//sb.AppendFormat("data source={0};initial catalog={1};integrated security=false;persist security info=True;User ID={2};Password={3}", new object[]{textBoxServer.Text, textBoxDbName.Text, textBoxUserName.Text, textBoxPassword.Text});
				sb.AppendFormat("data source={0};initial catalog={1};integrated security=false;persist security info=True;User ID={2};Password={3}", new object[] { BoxServer, textBoxDbName, textBoxUserName, textBoxPassword });
				SQL_CONN_STRING = sb.ToString();
				//return true;
				_logWriter.InfoFormat("Connecting database: {0}", textBoxDbName);
			}
			catch (Exception e)
			{
				////TraceMaker.TraceException(e);
				_logWriter.ErrorFormat("Error: Message in CreateConnectionString : {0}", e.Message);
			}
		}



		public DataSet getSchemaTables_ds()
		{
			DataSet ds = new DataSet();

		   
			DataTransSvc oDataTrans = new DataTransSvc();
			try
			{
				ds = oDataTrans.invoke_getSchemaTables_ds();
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				ds = null;
			}
			finally
			{
				oDataTrans  =null;
			}
			return ds;
		}


		public SqlDataReader getSchemaTables_dr()
		{
			DataTransSvc oDataTrans = new DataTransSvc();
			SqlDataReader dr = null;
			try
			{
				dr = oDataTrans.invoke_getSchemaTables_dr();
				
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				dr = null;
			}
			finally
			{
				oDataTrans = null;
			}
			return dr;
		}


		public SqlDataReader getSchemaViews_dr()
		{
			DataTransSvc oDataTrans = new DataTransSvc();
			SqlDataReader dr = null;
			try
			{
				dr = oDataTrans.invoke_getSchemaViews_dr();

			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				dr = null;
			}
			finally
			{
				oDataTrans = null;
			}
			return dr;
		}

		/// Get the SqlDataReader object
		/// <returns>SqlDataReader</returns>
		public SqlDataReader drStoredProcs()
		{
			DataTransSvc oDataTrans = new DataTransSvc();
			try
			{
				SqlDataReader dr = oDataTrans.invoke_getStoredProcs();

				if (dr.HasRows)
					return dr;
				else
					return null;
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				return null;
			}
			finally
			{
				oDataTrans = null;
			}
		}

		public SqlDataReader drGetSParams(string strSP)
		{
			DataTransSvc oDataTrans = new DataTransSvc();
			try
			{
				SqlDataReader dr = oDataTrans.invoke_Tools_getSParams(strSP);

				if (dr.HasRows)
					return dr;
				else
					return null;
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				return null;
			}
			finally
			{
				oDataTrans = null;
			}
		}


		public SqlDataReader drUserTables()
		{
			DataTransSvc oDataTrans = new DataTransSvc();
			try
			{
				SqlDataReader dr = oDataTrans.invoke_getSchemaTables_dr();		//oDataTrans.invoke_getBaseTables();

				if (dr.HasRows)
					return dr;
				else
					return null;
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				return null;
			}
			finally
			{
				oDataTrans = null;
			}
		}


		public SqlDataReader drGetTableFields(string strTable)
		{

			DataTransSvc oDataTrans = new DataTransSvc();
			try
			{               
				SqlDataReader dr = oDataTrans.invoke_Tools_getBaseTablesDetails(strTable);

				if (dr.HasRows)
					return dr;
				else
					return null;
			}
			catch (Exception e)
			{
				//TraceMaker.TraceException(e);
				_logWriter.ErrorFormat(string.Format("Exception Caught:\r\n{0}", e));
				return null;
			}
			finally
			{
				oDataTrans = null;
			}
		}
	}
}
