using System;
using MediatR;

namespace MediatorExample.Domain.Entities.Commands
{
    public class EmployeeCreateCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
    }
}
