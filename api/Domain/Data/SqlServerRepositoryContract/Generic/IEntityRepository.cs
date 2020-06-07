using MediatorExample.Domain.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;
using System.Threading.Tasks;

namespace MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic
{
    public interface IEntityRepository<TEntity>
        where TEntity : class, IIdentifiableEntity, new()
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entityList);
        void Remove(Guid id);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> FindAll();
        Task<int> Count();
    }
}