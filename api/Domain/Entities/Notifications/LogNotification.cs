using System;
using MediatorExample.Domain.Entities.Enums;
using MediatR;

namespace MediatorExample.Domain.Entities.Notifications
{
    public class LogNotification : INotification
    {
        public Guid Id { get; set; }
        public CommandType CommandType { get; set; }
        public LogEventType EventType { get; set; }
    }
}
