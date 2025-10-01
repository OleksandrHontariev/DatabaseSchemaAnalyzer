using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSchemaAnalyzer.Models
{
    public class Column
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int? Length { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
    }
}
