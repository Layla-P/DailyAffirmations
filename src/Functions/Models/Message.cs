using System.Collections.Generic;
using System.Net;

namespace Functions.Models
{
 
    public class Message
    {
        public Message(){}

        public Message(string formQuery)
        {
            var formData = FormData(formQuery);

            From = formData["From"];
            Body = formData["Body"];
        }
        public string From { get; set; }
        public string Body  {get; set; }
        
        public Dictionary<string, string> FormData(string formQuery)
        {
            var formDictionary = new Dictionary<string, string>();
            var formArray = formQuery.Split('&');
            foreach (var item in formArray)
            {
                var pair = item.Split('=');
                var key = WebUtility.UrlDecode(pair[0]);
                var value = WebUtility.UrlDecode(pair[1]);
                formDictionary.Add(key, value);
            }

            return formDictionary;
        }
    }
}