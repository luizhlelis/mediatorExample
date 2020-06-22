using System;
using MediatorExample.Domain.Entities.Enums;
using MediatR;

namespace MediatorExample.Domain.Entities.Notifications
{
    public class LogNotification : INotification
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public EventType EventType { get; set; }
        public EventAction EventAction { get; set; }
    }
}
