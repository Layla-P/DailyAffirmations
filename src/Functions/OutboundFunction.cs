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
            //The following link is great for creating an Azure cron
            //https://codehollow.com/2017/02/azure-functions-time-trigger-cron-cheat-sheet/
            
            TwilioClient.Init(
                Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
            );
            
             //collect all the telephone numbers of our users from the table storage
            var numbers = await _tableContext.ListNumbersAsync();

            //get a random quote from the database
            var quoteEntity = await _tableContext.GetRandomEntityAsync();

            foreach (var number in numbers)
            {
                var message =  MessageResource.Create(
                    from: new PhoneNumber(_phoneNumber),
                    to: new PhoneNumber(number),
                    body: quoteEntity.Quote );

                log.LogInformation($"Message-SID: {message.Sid} sent to number: {number}");
            }
        }
    }
}

//https://stackoverflow.com/questions/47541981/azure-functions-trigger-timer-trigger-locally

//https://markheath.net/post/deploying-azure-functions-with-azure-cli