using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Model;
using MediatR;

namespace MediatorExample.Domain.NotificationHandlers
{
    public class LogHandler : INotificationHandler<Employee>
    {
        public Task Handle(Employee notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
