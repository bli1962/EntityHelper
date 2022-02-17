// =============================================================================== 
// 		Class Name: clsSPGen.cs
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
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using log4net.Config;


#region	 dbo.sp_getBaseTablesDetails
//	CREATE PROCEDURE dbo.sp_getBaseTablesDetails
//
//	@table_name	as varchar(100)
//
//	AS 
//
//	SELECT column_name,	data_type, 
//	case data_type
//	when 'binary'	then character_maximum_length
//	when 'char'	then character_maximum_length
//	when 'nchar' then	character_maximum_length
//	when 'nvarchar'then	character_maximum_length
//	when 'varbinary' then	character_maximum_length
//	when 'varchar' then	character_maximum_length
//	else 0
//	end	as Length
//	FROM information_schema.columns
//	WHERE	table_name in
//	(
//	Select table_name
//	From Information_Schema.Tables
//	Where	Table_Type='Base Table'
//	)
//	and	 table_name	=	@table_name	
//
//	GO
#endregion


namespace mhcb.EntityHelper
{
	///	Summary	description	for	clsSPGen.
	public class SPGen : BusinessSvcBase
	{
		public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(SPGen));

		// default constructor
		public SPGen()
		{

		}
	  
		// comstom constructor
		public SPGen(string server, string dbName, string userName, string password)
			 : base(server, dbName, userName, password)
		{
			//BasicConfigurator.Configure();
			XmlConfigurator.Configure();
		}


		public enum StoredProcedureTypes
		{
			UPDATE,
			INSERT
		}


		//public string CreateSPModelFiles(SqlDataReader drTableName, StoredProcedureTypes cSPType)
		//{
		//	_logWriter.InfoFormat("Create Stored Procedure of USP_{0}_{1}_{2}", _DbName, cSPType, drTableName);

		//	var spPrefix = ConfigurationManager.AppSettings["SPPrefix"];

		//	if (drTableName != null)
		//	{
		//		var oclsData = new DataSvc(_Server, _DbName, _UserName, _Password);

		//		while (drTableName.Read())
		//		{
		//			string cTableName = drTableName.GetString(0);

		//			var drTableColumns = oclsData.drGetTableFields(cTableName);

		//			StreamWriter sw = null;
		//			System.Text.StringBuilder sb = null;

		//			// **	update
		//			sb = new System.Text.StringBuilder("");
		//			sb.AppendFormat("{0}_{1}_{2}.sql", new string[] { spPrefix, cSPType.ToString(), cTableName });
				   
		//			var oFileInfo = new FileInfo(sb.ToString());
		//			sw = oFileInfo.CreateText();

		//			sb = new System.Text.StringBuilder("");
		//			//sb.Append( GenerateSPScript(StoredProcedureTypes.UPDATE, cTableName, drTableColumns));
		//			sb.Append(GenerateSPScript(cSPType, cTableName, drTableColumns));

		//			output = output + sb.ToString();
		//			sw.WriteLine(sb.ToString());

		//			sw.Flush();
		//			sw.Close();
		//		}

		//		_logWriter.InfoFormat("Stored Procedures have been generated!");
		//		return "Stored Procedures have been generated!";
		//	}

		//	_logWriter.InfoFormat("The	No any Stored Procedureshas	been generated!");
		//	return "The	No any Stored Procedureshas	been generated!";
		//}


		public string CreateSPModelFilesV2(CheckedListBox checkedListBox1, StoredProcedureTypes cSPType)
		{
		   
			string spPrefix = ConfigurationManager.AppSettings["SPPrefix"];
			var oclsData = new DataSvc(_Server, _DbName, _UserName, _Password);

			for (int j = 0; j < checkedListBox1.CheckedItems.Count; j++)
			{

				string cTableName = checkedListBox1.CheckedItems[j].ToString();
				var drTableColumns = oclsData.drGetTableFields(cTableName);

				StreamWriter sw = null;
				StringBuilder sb = null;

				// **	update
				sb = new StringBuilder("");
				sb.AppendFormat("{0}_{1}_{2}.sql", new[] { spPrefix, cSPType.ToString(), cTableName });
			   
				var oFileInfo = new FileInfo(sb.ToString());
				sw = oFileInfo.CreateText();

				sb = new StringBuilder("");
				//sb.Append( GenerateSPScript(StoredProcedureTypes.UPDATE, cTableName, drTableColumns));
				sb.Append(GenerateSPScript(cSPType, cTableName, drTableColumns));

				output = output + sb;
				sw.WriteLine(sb.ToString());

				_logWriter.InfoFormat("Created and saved a Stored Procedure as the file: {0}_{1}_{2}_{3}.sql", spPrefix, _DbName, cSPType, cTableName);

				sw.Flush();
				sw.Close();

				if (j >= checkedListBox1.CheckedItems.Count - 1)
				{
					_logWriter.InfoFormat("All Stored Procedures have been generated!");
					return "Stored Procedures have been generated!";
				} 
			}

			_logWriter.InfoFormat("No Stored Procedureshas been generated!");
			return "The	No any Stored Procedureshas	been generated!";
		}


		public string GenerateSPScript(StoredProcedureTypes sptypeGenerate,  string sTableName,  SqlDataReader dr)
		{
			// _logWriter.InfoFormat("Create SP {0} for table {1}", cSPType, sTableName);

			string spPrefix = ConfigurationManager.AppSettings["SPPrefix"];

			_logWriter.InfoFormat("The beginning of writting the {0} Stored Procedure for table {1}", sptypeGenerate, sTableName);

			var sGeneratedCode = new StringBuilder();
			var sParamDeclaration = new StringBuilder();
			var sBody = new StringBuilder();
			var sINSERTValues = new StringBuilder();

			// Setup SP	code,	begining is	the	same no	matter the type
			sGeneratedCode.Append(GlobalVar.cTab[0] + "/*	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	===============================================================================	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	");
			sGeneratedCode.AppendFormat("{2}_{1}_{0}", new string[] { sTableName, sptypeGenerate.ToString(), spPrefix });
			sGeneratedCode.Append(".sql");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	The application designed & written by: Ben Li.");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	The last codes modified date: 3 Feburary 2006.");
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	This sql scripts generated date: " + System.DateTime.Now + ".");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	");

			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	===============================================================================	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	Copyright (C) 2005 Mizuho Corporate Bank, Limited Sydney Branch.");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	All rights reserved. ");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	THIS CODES ARE GENERATED AUTOMATICLLY AND INFORMATION IS PROVIDED 'AS IS' ");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT ");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS ");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	FOR A PARTICULAR PURPOSE.");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "//	===============================================================================");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "*/	");
			sGeneratedCode.Append(GlobalVar.cTab[0] + "");

			sGeneratedCode.AppendFormat("CREATE PROCEDURE dbo.{2}_{1}_{0}", new string[] { sTableName, sptypeGenerate.ToString(), spPrefix });
			sGeneratedCode.Append(Environment.NewLine);

			// Setup body	code,	different	for	UPDATE and INSERT
			switch (sptypeGenerate)
			{
				case StoredProcedureTypes.INSERT:
					sBody.AppendFormat(GlobalVar.cTab[0] + "BEGIN TRANSACTION");
					sBody.Append(Environment.NewLine);

					sBody.AppendFormat("INSERT INTO	[{0}]	(", sTableName);
					sBody.Append(Environment.NewLine);


					sINSERTValues.Append("VALUES (");
					sINSERTValues.Append(Environment.NewLine);
					break;

				case StoredProcedureTypes.UPDATE:
					sBody.AppendFormat("BEGIN TRANSACTION");
					sBody.Append(Environment.NewLine);

					sBody.AppendFormat("UPDATE [{0}]", sTableName);
					sBody.Append(Environment.NewLine);
					sBody.Append("SET");
					sBody.Append(Environment.NewLine);
					break;
			}

			while (dr.Read())
			{
				string cCol = dr.GetString(0);
				string cType = dr.GetString(1);
				int iLen = dr.GetInt32(2);

				// Param Declaration construction
				switch (cType)
				{
					case "decimal":
						byte iPrecise = dr.GetByte(3);
						int iScale = dr.GetInt32(4);
						sParamDeclaration.AppendFormat("\t@{0} {1} ({2}, {3})", new object[] { cCol, cType, iPrecise, iScale });
						break;
					default:
						sParamDeclaration.AppendFormat("\t@{0} {1}", new object[] { cCol, cType });
						break;
				}

				if (iLen != 0)
					sParamDeclaration.AppendFormat("({0})", iLen);

				sParamDeclaration.Append(",");
				sParamDeclaration.Append(Environment.NewLine);

				// Body	construction,	different	for	INSERT and UPDATE
				switch (sptypeGenerate)
				{
					case StoredProcedureTypes.INSERT:
						sINSERTValues.AppendFormat("\t@{0},", cCol);
						sINSERTValues.Append(Environment.NewLine);

						sBody.AppendFormat("\t{0},", cCol);
						sBody.Append(Environment.NewLine);
						break;

					case StoredProcedureTypes.UPDATE:
						sBody.AppendFormat("\t{0}	=	@{0},", new object[] { cCol, });
						sBody.Append(Environment.NewLine);
						break;
				}
			}

			// * new code to return id of a new record
			//sBody.Append("\t@NewId int OUTPUT,");
			//sBody.Append(Environment.NewLine);
			sGeneratedCode.Append(GlobalVar.cTab[1] + "@AffectedID int = 0 OUTPUT,");
			sGeneratedCode.Append(GlobalVar.cTab[1] + "@RowAffected int = 0 OUTPUT,");
			sGeneratedCode.Append(GlobalVar.cTab[1] + "@ValidCode int = 0 OUTPUT,");
			sGeneratedCode.Append(Environment.NewLine);

			// Now stitch	the	body parts together	into the SP	whole			
			sGeneratedCode.Append(sParamDeclaration.Remove(sParamDeclaration.Length - 3, 3));
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append("AS");
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append(sBody.Remove(sBody.Length - 3, 3));


			switch (sptypeGenerate)
			{
				case StoredProcedureTypes.INSERT:
					sGeneratedCode.Append(")");
					sGeneratedCode.Append(Environment.NewLine);
					sGeneratedCode.Append(sINSERTValues.Remove(sINSERTValues.Length - 3, 3));
					sGeneratedCode.Append(")");
					break;

				case StoredProcedureTypes.UPDATE:
					sGeneratedCode.Append(Environment.NewLine);
					sGeneratedCode.Append("/** WHERE Clause	here**/");
					break;
			}

			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append(Environment.NewLine);

			switch (sptypeGenerate)
			{
				case StoredProcedureTypes.INSERT:

					sGeneratedCode.Append("/*****************************************");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "Transaction and error controler");
					sGeneratedCode.Append(GlobalVar.cTab[0] + "******************************************/");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @RowAffected = @@RowCount");

					sGeneratedCode.Append(GlobalVar.cTab[1] + "IF @@Error !=0 GOTO ErrorCondition");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "GOTO Success_COMMIT");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "ErrorCondition:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "ROLLBACK TRANSACTION");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "RAISERROR('An error occurred when trying to INSERT. Table = " + sTableName + ". SProc = " + spPrefix + "_INSERT_" + sTableName + ".', 16, 1)");
					//sGeneratedCode.Append("RAISERROR('An error occurred when trying to INSERT. Table ={0}. SProc ={1}_{0}', 16, 1)", new object[]{sTableName, SP_Prefix});		

					sGeneratedCode.Append(GlobalVar.cTab[1] + "GOTO SProcReturn");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "Success_COMMIT:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "COMMIT TRANSACTION");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "SProcReturn:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @ValidCode = @@Error");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @AffectedID = SCOPE_IDENTITY()");

					sGeneratedCode.Append(Environment.NewLine);
					sGeneratedCode.Append(GlobalVar.cTab[1] + "RETURN @ValidCode");

					sGeneratedCode.Append(Environment.NewLine);

					break;
				case StoredProcedureTypes.UPDATE:
					sGeneratedCode.Append("/*****************************************");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "Transaction and error controler");
					sGeneratedCode.Append(GlobalVar.cTab[0] + "******************************************/");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @RowAffected = @@RowCount");

					sGeneratedCode.Append(GlobalVar.cTab[1] + "IF @@Error !=0 GOTO ErrorCondition");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "GOTO Success_COMMIT");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "ErrorCondition:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "ROLLBACK TRANSACTION");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "RAISERROR('An error occurred when trying to UPDATE. Table = " + sTableName + ". SProc = " + spPrefix + "_UPDATE_" + sTableName + ".', 16, 1)");
					//sGeneratedCode.Append("RAISERROR('An error occurred when trying to UPDATE. Table ={0}. SProc ={1}_{0}', 16, 1)", new object[]{sTableName, SP_Prefix});		

					sGeneratedCode.Append(GlobalVar.cTab[1] + "GOTO SProcReturn");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "Success_COMMIT:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "COMMIT TRANSACTION");
					sGeneratedCode.Append(Environment.NewLine);

					sGeneratedCode.Append(GlobalVar.cTab[0] + "SProcReturn:");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @ValidCode = @@Error");
					sGeneratedCode.Append(GlobalVar.cTab[1] + "SET @AffectedID = SCOPE_IDENTITY()");

					sGeneratedCode.Append(Environment.NewLine);
					sGeneratedCode.Append(GlobalVar.cTab[1] + "RETURN @ValidCode");

					sGeneratedCode.Append(Environment.NewLine);
					break;
			}

			_logWriter.InfoFormat("The end of writting the {0} Stored Procedure for table {1}", sptypeGenerate, sTableName);
			sGeneratedCode.Append("GO");

			return sGeneratedCode.ToString();
		}

	}




}
