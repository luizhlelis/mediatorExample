using System;
using MediatorExample.Domain.Entities.Enums;
using MediatR;

namespace MediatorExample.Domain.Entities.Notifications
{
    public class EmployeeNoSqlNotification : INotification
    {
        public Guid Id { get; set; }
        public Guid OfficeId { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public EventAction Type { get; set; }
    }
}
