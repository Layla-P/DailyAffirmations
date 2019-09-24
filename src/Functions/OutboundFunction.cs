using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functions.TableServices;
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

        public OutboundFunction(ITableDbContext tableDb)
        {
            _tableContext = tableDb ?? throw new ArgumentException();
        }
        
        [FunctionName("OutboundFunction")]
        public  async Task Run([TimerTrigger("* */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
            
//            TwilioClient.Init(
//                Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
//                Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
//            );
            
            // collect all the telephone numbers of our users from the table storage
            var numbers = await _tableContext.ListNumbersAsync();

            foreach (var number in numbers)
            {
//                var message =  MessageResource.Create(
//                    from: new PhoneNumber("+14155238886"),
//                    to: new PhoneNumber(number),
//                    body: "Your daily dog!");
//
                log.LogInformation($"number");
            }
        }
    }
}