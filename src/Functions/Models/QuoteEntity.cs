using Microsoft.Azure.Cosmos.Table;

namespace Functions.Models
{
        public class QuoteEntity : TableEntity
        {
            public QuoteEntity()
            {
            }

            public QuoteEntity(string phoneNumber, string quote)
            {
                PartitionKey = "Quotes";
                RowKey = phoneNumber;
                UserPhoneNumber = phoneNumber;
                Quote = quote;
            }

            public string UserPhoneNumber { get; set; }
            public string Quote { get; set; }
        }

}