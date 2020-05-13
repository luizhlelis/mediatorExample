using System;
using MediatR;

namespace MediatorExample.Domain.Commands
{
    public class EmployeeDeleteCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
