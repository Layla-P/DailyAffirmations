using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functions.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace Functions.TableServices
{
    public class TableDbContext : ITableDbContext
    {
        private readonly ITableConfiguration _tableConfiguration;

        private CloudTable _table;

        private readonly ILogger _log;


        public TableDbContext(ILoggerFactory log, ITableConfiguration tableConfiguration)
        {
            _log = log.CreateLogger<TableDbContext>();
            // https://stackoverflow.com/questions/54876798/how-can-i-use-the-new-di-to-inject-an-ilogger-into-an-azure-function-using-iwebj

            _tableConfiguration = tableConfiguration;
            
        }
        
        public async Task CreateTableAsync()
        {
            throw new NotImplementedException();
        }
        
        public  async Task<QuoteEntity> InsertOrMergeEntityAsync(TableEntity entity)
        {
            throw new NotImplementedException();
           
        }

        public async Task<QuoteEntity> GetRandomEntityAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> ListNumbersAsync()
        {
            throw new NotImplementedException();
        }

        private CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(_tableConfiguration.ConnectionString);
            }
            catch (FormatException)
            {
                _log.LogInformation(
                    "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                _log.LogInformation(
                    "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");

                throw;
            }

            _log.LogInformation("Success!");
            return storageAccount;
        }

        private int GetRandom(IEnumerable<TableEntity> quotes)
        {
            var count = quotes.Count();
            Random r = new Random();
            return r.Next(0, count-1);
        }
    }
}