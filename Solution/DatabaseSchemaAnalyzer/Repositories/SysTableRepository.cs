using System;
using Dapper;
using DatabaseSchemaAnalyzer.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DatabaseSchemaAnalyzer.Repositories
{
    public class SysTableRepository
    {
        SqlConnection _sqlConnection;
        public SysTableRepository(SqlConnection connection)
        {
            _sqlConnection = connection;
        }

        public IEnumerable<Table> GetTables ()
        {
            if (this._sqlConnection == null)
                throw new ApplicationException("Connection to database is null");

            string connectionString = _sqlConnection.ConnectionString;
            string sql = @"SELECT name AS Name,
                            create_date AS CreateDate,
                            modify_date AS ModifyDate
                        FROM sys.tables
                        WHERE is_ms_shipped = 0";
            return _sqlConnection.Query<Table>(sql);
        }
    }
}
