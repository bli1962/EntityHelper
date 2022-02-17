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



namespace mhcb.EntityHelper
{
    /// <summary>
    /// Summary description for clsDataType.
    /// </summary>
    public class DataType
    {
        public DataType()
        {
        }


        /// <summary>
        /// Get (.net) System type from SQL server schema columns data type
        /// </summary>
        public static string ToSystemType_CS(string tstrSqlType)
        {
            string _Type = string.Empty;

            switch (tstrSqlType)
            {
                case "bit":
                    _Type = "bool";
                    break;
                case "tinyint":
                    _Type = "byte";
                    break;
                case "smallint":
                    _Type = "Int16";
                    break;
                case "int":
                    _Type = "Int32";
                    break;
                case "biginit":
                    _Type = "Int64";
                    break;
                case "real":
                    _Type = "Single";
                    break;
                case "float":
                    _Type = "double";
                    break;
                case "decimal":
                    _Type = "decimal";
                    break;
                case "numeric":
                    _Type = "decimal";
                    break;
                case "money":
                case "smallmoney":
                    _Type = "decimal";
                    break;
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    _Type = "System.DateTime";
                    break;
                case "sql_variant":
                    _Type = "object";
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    _Type = "string";
                    break;
                case "binary":
                    _Type = "byte";
                    break;
                case "varbinary":
                    _Type = "byte[]";
                    break;
                case "image":
                    //_Type = "System.Drawing.Image";
                    _Type = "byte";
                    break;
                case "uniqueidentifier":
                    _Type = "System.Guid";
                    break;
                default:
                    _Type = "unknown";
                    break;
            }
            return _Type;
        }

        /// <summary>
        /// Get (.net) System type from SQL server schema columns data type
        /// </summary>
        public static string ToSystemTypeVb(string tstrSqlType)
        {
            string _Type = string.Empty;

            switch (tstrSqlType)
            {
                case "bit":
                    _Type = "Boolean";
                    break;
                case "tinyint":
                    _Type = "Byte";
                    break;
                case "smallint":
                    _Type = "int16";
                    break;
                case "int":
                    _Type = "int32";
                    break;
                case "biginit":
                    _Type = "int64";
                    break;
                case "real":
                    _Type = "Single";
                    break;
                case "float":
                    _Type = "Double";
                    break;            
                case "decimal":
                case "numeric":
                    _Type = "Decimal";
                    break;
                case "money":
                case "smallmoney":
                    _Type = "Decimal";
                    break;
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    _Type = "Date";
                    break;
                case "sql_variant":
                    _Type = "Object";
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    _Type = "String";
                    break;
                case "binary":
                    _Type = "Byte";
                    break;
                case "varbinary":
                    _Type = "Byte[]";
                    break;
                case "image":
                    _Type = "System.Drawing.Image";
                    break;
                case "uniqueidentifier":
                    _Type = "System.Guid";
                    break;
                default:
                    _Type = "unknown";
                    break;
            }
            return _Type;
        }


        /// <summary>
        /// From schema columns data type to SqlDataReader data type
        /// </summary>
        public static string ToSqlDataReaderType(string tstrSqlType)
        {
            string _Type = string.Empty;

            switch (tstrSqlType)
            {
                case "bit":
                    _Type = "Boolean";
                    break;
                case "tinyint":
                    _Type = "Byte";
                    break;
                case "smallint":
                    _Type = "Int16";
                    break;
                case "int":
                    _Type = "Int32";
                    break;
                case "biginit":
                    _Type = "Int64";
                    break;
                case "real":
                    _Type = "Single";
                    break;
                case "float":
                    _Type = "Double";
                    break;
                case "decimal":
                case "numeric":
                    _Type = "Decimal";
                    break;
                case "money":
                case "smallmoney":
                    _Type = "Decimal";
                    break;
                case "datetime":
                case "smalldatetime":
                    _Type = "DateTime";
                    break;
                case "timestamp":
                    _Type = "DateTime";
                    break;
                case "char":
                case "image":
                    _Type = "Byte[]";
                    break;
                case "uniqueidentifier":
                    _Type = "Guid";
                    break;
                //				case "sql_variant":
                //				{
                //					_Type = "Object";
                //				}break;
                default:
                    _Type = "String";
                    break;
            }
            return _Type;
        }


        /// <summary>
        /// From schema columns data type to System.Data.SqlDbType data type
        /// </summary>
        public static string ToSystemDataSqlDbType(string tstrSqlType)
        {
            string _Type = string.Empty;

            switch (tstrSqlType)
            {
                case "bit":
                    _Type = "Bit";
                    break;
                case "tinyint":
                    _Type = "TinyInt";
                    break;
                case "smallint":
                    _Type = "SmallInt";
                    break;
                case "int":
                    _Type = "Int";
                    break;
                case "bigint":
                    _Type = "BigInt";
                    break;
                case "real":
                    _Type = "Real";
                    break;
                case "float":
                    _Type = "Float";
                    break;
                case "decimal":
                    _Type = "Decimal";
                    break;
                case "numeric":
                    _Type = "Numeric";
                    break;
                case "datetime":
                    _Type = "DateTime";
                    break;
                case "smalldatetime":
                    _Type = "SmallDateTime";
                    break;
                case "money":
                    _Type = "Money";
                    break;
                case "smallmoney":
                    _Type = "SmallMoney";
                    break;
                case "char":
                    _Type = "Char";
                    break;
                case "nchar":
                    _Type = "NChar";
                    break;
                case "varchar":
                    _Type = "VarChar";
                    break;
                case "nvarchar":
                    _Type = "NVarChar";
                    break;
                case "text":
                    _Type = "Text";
                    break;
                case "ntext":
                    _Type = "NText";
                    break;
                case "binary":
                    _Type = "Binary";
                    break;
                case "varbinary":
                    _Type = "VarBinary";
                    break;
                default:
                    _Type = "unknown";
                    break;
            }
            return _Type;
        }


        //public static string GetGetSqlXXX(string tstrSqlType)
        //{
        //    string _Type = string.Empty;

        //    switch (tstrSqlType)
        //    {
        //        case "bit":
        //            {
        //                _Type = "SqlBoolean";
        //            } break;
        //        case "tinyint":
        //            {
        //                _Type = "SqlByte";
        //            } break;
        //        case "smallint":
        //            {
        //                _Type = "SqlInt16";
        //            } break;
        //        case "int":
        //            {
        //                _Type = "SqlInt32";
        //            } break;
        //        case "biginit":
        //            {
        //                _Type = "SqlInt64";
        //            } break;
        //        case "real":
        //            {
        //                _Type = "SqlSingle";
        //            } break;
        //        case "float":
        //            {
        //                _Type = "SqlDouble";
        //            } break;
        //        case "decimal":
        //        case "numeric":
        //            {
        //                _Type = "SqlDecimal";
        //            } break;
        //        case "money":
        //        case "smallmoney":
        //            {
        //                _Type = "SqlMoney";
        //            } break;
        //        case "datetime":
        //        case "smalldatetime":
        //            {
        //                _Type = "SqlDateTime";
        //            } break;
        //        case "char":
        //        case "nchar":
        //        case "varchar":
        //        case "nvarchar":
        //        case "text":
        //        case "ntext":
        //            {
        //                _Type = "SqlString";
        //            } break;
        //        case "binary":
        //        case "varbinary":
        //        case "timestamp":
        //            {
        //                _Type = "SqlBinary";
        //            } break;
        //        case "image":
        //            {
        //                _Type = "SqlBinary";
        //            } break;
        //        case "uniqueidentifier":
        //            {
        //                _Type = "SqlGuid";
        //            } break;
        //        //				case "sql_variant":
        //        //				{
        //        //					_Type = "Object";
        //        //				}break;
        //        default:
        //            {
        //                _Type = "SqlString";
        //            } break;
        //    }
        //    return _Type;
        //}

    }
}
