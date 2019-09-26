using System.Collections.Generic;
using System.Threading.Tasks;
using Functions.Models;
using Microsoft.Azure.Cosmos.Table;

namespace Functions.TableServices
{
    public interface ITableDbContext
    {
        Task CreateTableAsync();
        Task<QuoteEntity> InsertOrMergeEntityAsync(TableEntity entity);
        
        Task<QuoteEntity> GetRandomEntityAsync();
        
        Task<IEnumerable<string>> ListNumbersAsync();
    }
    
}