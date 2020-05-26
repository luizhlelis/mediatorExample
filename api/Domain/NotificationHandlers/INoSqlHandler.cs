
namespace MediatorExample.Domain.NotificationHandlers
{
    public interface INoSqlHandler
    {
        int AddToAccumulatedTotal(int accumulatedTotal, int newValue);
    }
}
