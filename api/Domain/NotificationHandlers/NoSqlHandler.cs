using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Model;
using MediatR;

namespace Domain.NotificationHandlers
{
    public class NoSqlHandler : INotificationHandler<Employee>
    {
        public NoSqlHandler()
        {
        }

        public Task Handle(Employee notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
