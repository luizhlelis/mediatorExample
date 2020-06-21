using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;

namespace MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic
{
    public interface IMongoGenericRepository<TDocument>
        where TDocument : class, IIdentifiableMongoDocument, new()
    {
        void Add(TDocument document);
        Task<TDocument> GetById(Guid id);
        Task<IEnumerable<TDocument>> GetAll();
        void Update(TDocument document);
        void Remove(Guid id);
        void Dispose();
    }
}
