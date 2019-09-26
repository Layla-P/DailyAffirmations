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
            throw new NotImplementedException();
        }
    }
}