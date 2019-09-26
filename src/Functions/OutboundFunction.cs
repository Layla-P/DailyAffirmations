using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functions.Models;
using Functions.TableServices;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Functions
{
    public class OutboundFunction
    {
    
        private readonly ITableDbContext _tableContext;
        private readonly string _phoneNumber;

        public OutboundFunction(ITableDbContext tableDb)
        {
            _tableContext = tableDb ?? throw new ArgumentException();
            _phoneNumber = Environment.GetEnvironmentVariable("PhoneNumber");
        }
        
        [FunctionName("OutboundFunction")]
        public async Task Run([TimerTrigger("0 0 10 * * *")] TimerInfo myTimer, ILogger log)
        {
            throw new NotImplementedException();
        }
    }
}
