using System;
using Functions;
using Xunit;

namespace FunctionsTests
{
    public class OutboundFunctionTests
    {
        [Fact]
        public void GivenNullTableContext_ShouldThrow_NewArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new OutboundFunction(null));
        }
    }
}