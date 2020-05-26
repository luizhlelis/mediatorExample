using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Model;
using MediatR;

namespace MediatorExample.Domain.NotificationHandlers
{
    public class NoSqlHandler : INotificationHandler<Employee>, INoSqlHandler
    {
        public NoSqlHandler()
        {
        }

        public Task Handle(Employee notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public int AddToAccumulatedTotal(int accumulatedTotal, int newValue)
        {
            accumulatedTotal += newValue;
            return accumulatedTotal < 0 ? 0 : accumulatedTotal;
        }
    }
}
