using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Commands;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Entities.Model;
using MediatR;

namespace MediatorExample.Domain.CommandHandlers
{
    public class EmployeeHandler :
        IRequestHandler<EmployeeCreateCommand, string>,
        IRequestHandler<EmployeeDeleteCommand, string>,
        IRequestHandler<EmployeeUpdateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IEntityRepository<Employee> _employeeRepository;

        public Task<string> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            _employeeRepository.Add(new
                Employee(
                    request.Id,
                    request.Name,
                    request.Email,
                    request.Phone,
                    request.Salary
                )
            );
            throw new NotImplementedException();
        }

        public Task<string> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
