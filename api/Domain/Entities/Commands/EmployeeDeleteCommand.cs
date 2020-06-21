using System;
using MediatR;

namespace MediatorExample.Domain.Entities.Commands
{
    public class EmployeeDeleteCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
