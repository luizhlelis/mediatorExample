using System;

namespace MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities
{
    public interface IIdentifiableEntity
    {
        Guid Id { get; }
    }
}
