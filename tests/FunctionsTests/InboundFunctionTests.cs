using System;
   using Functions;
   using Xunit;
   
   namespace FunctionsTests
   {
       public class InboundFunctionTests
       {
           [Fact]
           public void GivenNullTableContext_ShouldThrow_NewArgumentException()
           {
               Assert.Throws<ArgumentException>(() => new InboundFunction(null));
           }
       }
   }