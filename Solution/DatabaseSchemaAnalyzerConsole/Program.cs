using System;
using DatabaseSchemaAnalyzer;
using DatabaseSchemaAnalyzer.Repositories;
using DatabaseSchemaAnalyzer.Models;
using System.Collections;
using System.Linq;
DapperDbContext dbContext = new DapperDbContext("(localdb)\\MSSQLLocalDB", "ITVDNdb");
if (dbContext.TestConnection(out string status))
{
    using (var connection = dbContext.CreateConnection())
    {
        connection.Open();
        SysTableRepository tableRepository = new SysTableRepository(connection);
        SysColumnsRepository columnsRepository = new SysColumnsRepository(connection);

        var tables = tableRepository.GetTables();

        foreach (var table in tables)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Name: {0}", table.Name);
            Console.WriteLine("Create Date: {0}", table.CreateDate);
            Console.WriteLine("Modify Date: {0}", table.ModifyDate);
            Console.WriteLine("Columns: ");

            foreach (Column col in columnsRepository.GetColumns(table.Name))
            {
                string columnType = col.Length.HasValue
                    ? string.Format("{0}({1})", col.DataType, col.Length)
                    : col.DataType;

                string isNull = col.IsNullable ? "NULL" : "NOT NULL";
                string isIdentity = col.IsIdentity ? "IDENTITY" : "";

                Console.WriteLine("{0} {1} {2} {3}", col.ColumnName, columnType, isNull, isIdentity);
            }
            Console.WriteLine("---------------");
        }
    }

} else 
    Console.WriteLine(status);

Console.ReadLine();
