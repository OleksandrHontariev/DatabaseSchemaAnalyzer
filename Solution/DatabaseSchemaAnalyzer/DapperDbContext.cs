using System;
using Microsoft.Data.SqlClient;

namespace DatabaseSchemaAnalyzer
{
    public class DapperDbContext
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString
        {
            get => @$"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }
        public DapperDbContext() { }
        public DapperDbContext (string serverName, string databaseName)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
        }

        public SqlConnection CreateConnection ()
        {
            if (string.IsNullOrWhiteSpace(ServerName))
                throw new InvalidOperationException("ServerName is required to create a connection.");

            if (string.IsNullOrWhiteSpace(DatabaseName))
                throw new InvalidOperationException("DatabaseName is required to create a connection.");

            return new SqlConnection(ConnectionString);
        }

        public bool TestConnection (out string statusMessage)
        {
            try
            {
                using var connection = CreateConnection();
                connection.Open();

                statusMessage = "Connection successful!";
                return true;
            } catch (Exception ex)
            {
                statusMessage = $"Connection failed: {ex.Message}";
                return false;
            }
        }
    }
}
