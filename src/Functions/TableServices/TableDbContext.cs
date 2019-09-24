using System;
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
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString();

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            _log.LogInformation("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            _table = tableClient.GetTableReference(_tableConfiguration.TableName);
            
            if (await _table.CreateIfNotExistsAsync())
            {
                _log.LogInformation("Created Table named: {0}", _tableConfiguration.TableName);
            }
            else
            {
                _log.LogInformation("Table {0} already exists", _tableConfiguration.TableName);
            }

        }
        
        public  async Task<TableEntity> InsertOrMergeEntityAsync(TableEntity entity)
        {
            if (_table is null)
            {
                await CreateTableAsync();
            }
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await _table.ExecuteAsync(insertOrMergeOperation);
                QuoteEntity insertedQuote = result.Result as QuoteEntity;

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
                if (result.RequestCharge.HasValue)
                {
                    _log.LogInformation("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedQuote;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                throw;
            }
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
    }
}