
using System.ComponentModel;

namespace MediatorExample.Domain.Entities.Enums
{
    public enum LogEventType
    {
        [Description("Employee Command")]
        EmployeeCommand,
        [Description("Employee Query")]
        EmployeeQuery,
        CompanyCommand,
        CompanyQuery,
        SectorCommand,
        SectorQuery,
        SubsidiaryCommand,
        SubsidiaryQuery,
    }
}
