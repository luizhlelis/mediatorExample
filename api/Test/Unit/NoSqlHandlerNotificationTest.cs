
using Xunit;
using MediatorExample.Domain.NotificationHandlers;

namespace MediatorExample.Test.Unit
{
    public class NoSqlHandlerNotificationTest
    {
        private readonly INoSqlHandler _noSqlHandler;

        public NoSqlHandlerNotificationTest()
        {
            _noSqlHandler = new NoSqlHandler();
        }

        [Theory(DisplayName = "Should return the current cumulative total added to the new value")]
        [InlineData(50, 10, 60)]
        [InlineData(-10, 10, 0)]
        [InlineData(-20, 10, 0)]
        public void AddToAccumulatedTotalTest(double newValue,
            double accumulatedTotal, double expectedResult)
        {
            accumulatedTotal = _noSqlHandler
                .AddToAccumulatedTotal(accumulatedTotal, newValue);

            Assert.Equal(expectedResult, accumulatedTotal);
        }
    }
}
