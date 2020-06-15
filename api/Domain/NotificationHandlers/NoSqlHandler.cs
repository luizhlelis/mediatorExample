using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Model;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Entities.Enums;
using MediatorExample.Domain.Entities.Notifications;
using MediatR;

namespace MediatorExample.Domain.NotificationHandlers
{
    public class NoSqlHandler : INotificationHandler<EmployeeNoSqlNotification>, INoSqlHandler
    {
        private readonly IMongoGenericRepository<Office> _officeRepository;

        public NoSqlHandler()
        {
        }

        public async Task Handle(
            EmployeeNoSqlNotification notification,
            CancellationToken cancellationToken)
        {
            Office newcomerEmployeeOffice = await _officeRepository
                .GetById(notification.OfficeId);

            newcomerEmployeeOffice.EmployeeList
                .Add(new EmployeeSummary
                {
                    Id = notification.Id,
                    Name = notification.Name
                });

            AddToAccumulatedTotal(
                newcomerEmployeeOffice.MonthlyEmployeeExpenses,
                notification.Salary);

            _officeRepository.Update(newcomerEmployeeOffice);
        }

        public double AddToAccumulatedTotal(double accumulatedTotal, double newValue)
        {
            accumulatedTotal += newValue;
            return accumulatedTotal < 0 ? 0 : accumulatedTotal;
        }
    }
}
