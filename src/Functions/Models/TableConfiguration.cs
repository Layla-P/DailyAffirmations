using Functions.Models;

namespace AddQuoteFunction.Models
{
    public class TableConfiguration : ITableConfiguration
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }
}