using MongoDB.Bson;

namespace MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities
{
    public interface IIdentifiableMongoDocument
    {
        ObjectId Id { get; }
    }
}
