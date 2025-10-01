using System;
using System.Collections.Generic;
using Dapper;
using DatabaseSchemaAnalyzer.Models;
using Microsoft.Data.SqlClient;

namespace DatabaseSchemaAnalyzer.Repositories
{
    public class SysColumnsRepository
    {
        SqlConnection _sqlConnection;
        public SysColumnsRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public IEnumerable<Column> GetColumns (string tableName)
        {
            if (this._sqlConnection == null)
                new ArgumentException("Connection to database is null");

            if (tableName == null)
                throw new ArgumentNullException(nameof(tableName));

            if (tableName == "")
                throw new ArgumentException("Parameter can't be empty string", nameof(tableName));

            string sql = @$"DECLARE @TableName NVARCHAR(128) = '{tableName}';
                            SELECT 
                                c.name AS ColumnName,
                                ty.name AS DataType,
                                CASE 
                                    WHEN ty.name IN ('char', 'nchar', 'varchar', 'nvarchar') THEN c.max_length
                                    ELSE NULL
                                END AS Length,
                                c.is_nullable AS IsNullable,
                                c.is_identity AS IsIdentity
                            FROM 
                                sys.columns c
                            INNER JOIN 
                                sys.tables t ON c.object_id = t.object_id
                            INNER JOIN
                                sys.types ty ON c.user_type_id = ty.user_type_id
                            WHERE 
                                t.name = @TableName
                            ORDER BY 
                                c.column_id;";

            return _sqlConnection.Query<Column>(sql);
        }
    }
}
