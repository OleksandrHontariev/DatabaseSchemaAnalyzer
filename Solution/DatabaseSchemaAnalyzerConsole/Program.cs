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
        SysTableRepository repo = new SysTableRepository(connection);
        var tables = repo.GetTables() ?? Enumerable.Empty<Table>();

        foreach (var table in tables)
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Name: {0}", table.Name);
            Console.WriteLine("Create Date: {0}", table.CreateDate);
            Console.WriteLine("Modify Date: {0}", table.ModifyDate);
        }
    }

} else 
    Console.WriteLine(status);

Console.ReadLine();
