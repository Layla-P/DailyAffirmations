using System;
using System.IO;
using System.Threading.Tasks;
using Functions.Models;
using Functions.TableServices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public class InboundFunction
    {
        private readonly ITableDbContext _tableContext;

        public InboundFunction(ITableDbContext tableDb)
        {
            _tableContext = tableDb ?? throw new ArgumentException();
        }

        [FunctionName("InboundFunction")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var message = new Message(requestBody);

            var quoteEntity = new QuoteEntity(message.From, message.Body);

            var newQuote = await _tableContext.InsertOrMergeEntityAsync(quoteEntity) as QuoteEntity;

            if (newQuote is null)
            {
                log.LogInformation("A new quote couldn't be created");
            }
            else
            {
                log.LogInformation($"Key: {newQuote.PartitionKey}, quote: {newQuote.Quote}  number: {newQuote.UserPhoneNumber}");
            }
        }
    }
}