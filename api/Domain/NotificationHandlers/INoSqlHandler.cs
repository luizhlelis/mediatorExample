
namespace MediatorExample.Domain.NotificationHandlers
{
    public interface INoSqlHandler
    {
        double AddToAccumulatedTotal(double accumulatedTotal, double newValue);
    }
}
