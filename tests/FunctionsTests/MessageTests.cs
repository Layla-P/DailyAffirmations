using System.Collections.Generic;
using FluentAssertions;
using Functions.Models;
using Xunit;

namespace FunctionsTests
{
    public class MessageTests
    {
        [Fact]
        public void FormData_Should_ReturnDictionary()
        {
            var formData = "ToCountry=GB&Body=%22Hey+there%22&From=%2B447723445678";
            var expected = new Dictionary<string, string>
            {
                {"ToCountry", "GB"}, {"Body", "\"Hey there\""}, {"From", "+447723445678"}
            };
            var message = new Message();
            var sut = message.FormData(formData);
            sut.Should().Equal(expected);
        }
        
        [Fact]
        public void Message_Should_PopulateProperties()
        {
            var formData = "ToCountry=GB&Body=%22Hey+there%22&From=%2B447723445678";
            var message = new Message(formData);
            
            message.From.Should().Match("+447723445678");
            message.Body.Should().Match("\"Hey there\"");
        }
    }
}