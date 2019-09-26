using System;
using Functions;
using Functions.Models;
using Functions.TableServices;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FunctionsTests
{
    public class TableContextTests
    {
        private ITableDbContext _sut;
        private Mock<ILoggerFactory> _log;
        private Mock<ITableConfiguration> _tableConfiguration;
        public TableContextTests()
        {
            _sut = new TableDbContext(_log.Object, _tableConfiguration.Object);
        }

        [Fact]
        public void ListNumbersAsync_Should_ReturnListWithoutDuplicates()
        {
           //todo
        }
    }
}