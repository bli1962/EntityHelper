
CREATE PROCEDURE dbo.tools_getBaseTables

AS 

SELECT table_name, column_name, data_type
FROM information_schema.columns
WHERE table_name in
  (
   Select table_name
   From Information_Schema.Tables
   Where Table_Type='Base Table'
  ) 
ORDER BY table_name
GO



CREATE PROCEDURE dbo.tools_getBaseTablesDetails

@table_name as varchar(100)

AS 

	SELECT column_name, data_type, 
	case data_type
	when 'binary' then character_maximum_length
	when 'char' then character_maximum_length
	when 'nchar' then character_maximum_length
	when 'nvarchar'then character_maximum_length
	when 'varbinary' then character_maximum_length
	when 'varchar' then character_maximum_length
	else 0
	end as Length,

	numeric_precision as Precise, 
	numeric_scale as Scale

	FROM information_schema.columns
	WHERE table_name in
	(
	Select table_name
	From Information_Schema.Tables
	Where Table_Type='Base Table'
	)
	and  table_name = @table_name
GO



CREATE PROCEDURE dbo.tools_getSParams
	@SpName as varchar(100)
As

exec sp_procedure_params_rowset  @SpName

GO
