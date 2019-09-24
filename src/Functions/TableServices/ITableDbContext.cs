using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Functions.TableServices
{
    public interface ITableDbContext
    {
        Task CreateTableAsync();
        Task<TableEntity> InsertOrMergeEntityAsync(TableEntity entity);
    }
    
}