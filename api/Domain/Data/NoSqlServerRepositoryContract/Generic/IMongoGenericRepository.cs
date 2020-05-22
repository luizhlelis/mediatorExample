using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;

namespace MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic
{
    public interface IMongoGenericRepository<TDocument>
        where TDocument : class, IIdentifiableMongoDocument, new()
    {
    }
}
