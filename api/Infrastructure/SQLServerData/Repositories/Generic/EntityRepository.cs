using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;
using MediatorExample.Domain.Utils;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using MediatorExample.Infrastructure.SqlServerData.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MediatorExample.Infra.SqlServerData.Repositories.Generic
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IIdentifiableEntity, new()
    {
        public Func<SqlServerContext> ContextCreator { get; private set; }

        public EntityRepository(IConfiguration configuration)
        {
            ContextCreator = () =>
            {
                DbContextOptionsBuilder<SqlServerContext> builder =
                    new DbContextOptionsBuilder<SqlServerContext>();

                builder.UseSqlServer(
                    configuration
                            .GetSection("ConnectionSettings")
                            .GetSection("SqlServer")
                            .GetSection("ConnectionString")
                            .Value
                );

                SqlServerContext context = new SqlServerContext(builder.Options);

                return context;
            };
        }

        public virtual void Add(TEntity entity)
        {
            entity = entity.DeleteNestedProperties();

            using (SqlServerContext entityContext = ContextCreator())
            {
                entityContext.Set<TEntity>().AddAsync(entity);
                entityContext.SaveChanges();
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> entityList)
        {
            entityList = entityList.Select(entity =>
            {
                entity = entity.DeleteNestedProperties();
                return entity;
            });

            using (SqlServerContext entityContext = ContextCreator())
            {
                entityContext.Set<TEntity>().AddRange(entityList);

                entityContext.SaveChanges();
            }
        }

        public virtual void Remove(Guid id)
        {
            using (SqlServerContext entityContext = ContextCreator())
            {
                TEntity entity = entityContext.Set<TEntity>().Find(id);
                if (entity == null)
                    return;

                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity)
        {
            entity = entity.DeleteNestedProperties();

            using (SqlServerContext entityContext = ContextCreator())
            {
                TEntity existingEntity =
                    entityContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);

                if (existingEntity == null)
                    return;

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll()
        {
            using (SqlServerContext entityContext = ContextCreator())
            {
                return await entityContext.Set<TEntity>().ToListAsync();
            }
        }

        public virtual async Task<int> Count()
        {
            using (SqlServerContext context = ContextCreator())
            {
                try
                {
                    return await context.Set<TEntity>().CountAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                        String.Format("It was not possible to find the data: {0}", ex.Message));
                }
            }
        }
    }
}