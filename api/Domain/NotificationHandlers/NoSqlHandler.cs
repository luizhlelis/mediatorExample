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
    public class NoSqlHandler : INotificationHandler<EmployeeNoSqlNotification>
    {
        private readonly IMongoGenericRepository<Office> _officeRepository;

        public NoSqlHandler(IMongoGenericRepository<Office> officeRepository)
        {
            _officeRepository = officeRepository;
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

            newcomerEmployeeOffice.MonthlySalaryExpenses = GetNewMonthlySalaryExpenses(
                newcomerEmployeeOffice.MonthlySalaryExpenses,
                notification.Salary,
                notification.Type);

            _officeRepository.Update(newcomerEmployeeOffice);
        }

        public double GetNewMonthlySalaryExpenses(
            double total,
            double newValue,
            EventAction commandType)
        {
            double newTotalValue;

            newTotalValue =
                commandType == EventAction.Delete ?
                total -= newValue :
                total += newValue;
            
            return newTotalValue < 0 ? 0 : newTotalValue;
        }
    }
}
