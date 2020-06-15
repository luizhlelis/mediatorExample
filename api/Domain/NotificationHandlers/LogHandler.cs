using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Enums;
using MediatorExample.Domain.Entities.Model;
using MediatorExample.Domain.Entities.Notifications;
using MediatR;

namespace MediatorExample.Domain.NotificationHandlers
{
    public class LogHandler : INotificationHandler<LogNotification>
    {
        public Task Handle(LogNotification notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
