
using Xunit;
using MediatorExample.Domain.NotificationHandlers;
using Moq;
using MediatorExample.Domain.Entities.Enums;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Entities.Model;

namespace MediatorExample.Test.Unit
{
    public class NoSqlHandlerNotificationTest
    {
        [Theory(DisplayName = "Should return sum")]
        [InlineData(50, 10, 60)]
        [InlineData(-10, 10, 0)]
        [InlineData(-20, 10, 0)]
        public void ShoudReturnSum(
            double newValue,
            double total,
            double expectedResult)
        {
            Mock<IMongoGenericRepository<Office>> mockedOfficeRepo =
                new Mock<IMongoGenericRepository<Office>>();

            NoSqlHandler notifyHandler = new NoSqlHandler(mockedOfficeRepo.Object);
            double afterUpdatedTotal = total;

            total = notifyHandler
                .GetNewMonthlySalaryExpenses(total, newValue, CommandType.Add);

            afterUpdatedTotal = notifyHandler
                .GetNewMonthlySalaryExpenses(afterUpdatedTotal, newValue, CommandType.Update);

            Assert.Equal(expectedResult, total);
            Assert.Equal(total, afterUpdatedTotal);
        }

        [Theory(DisplayName = "Should return subtraction")]
        [InlineData(10, 50, 40)]
        [InlineData(-10, 10, 20)]
        [InlineData(10, -20, 0)]
        [InlineData(10, 10, 0)]
        public void ShoudReturn(
            double newValue,
            double total,
            double expectedResult)
        {
            Mock<IMongoGenericRepository<Office>> mockedOfficeRepo =
                new Mock<IMongoGenericRepository<Office>>();

            NoSqlHandler notifyHandler = new NoSqlHandler(mockedOfficeRepo.Object);

            total = notifyHandler
                .GetNewMonthlySalaryExpenses(total, newValue, CommandType.Delete);

            Assert.Equal(expectedResult, total);
        }
    }
}
