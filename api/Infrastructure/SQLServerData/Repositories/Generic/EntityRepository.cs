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

/*
    This repository was based on DynamicLinqRepository project and adapted to
    EFCore: github.com/RodrigoBernardino/DynamicLinqRepository
*/

namespace NexaDb.Infra.SqlServerData.Repositories.Generic
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IIdentifiableEntity, new()
    {
        public EntityRepository(IConfiguration configuration)
        {
            ContextCreator = () =>
            {
                DbContextOptionsBuilder<SqlServerContext> builder =
                    new DbContextOptionsBuilder<SqlServerContext>();

                builder.UseSqlServer(
                    configuration
                            .GetSection("ConnectionSettings:SqlServer:ConnectionString")
                            .Value
                );

                SqlServerContext context = new SqlServerContext(builder.Options);

                return context;
            };
        }

        public Func<SqlServerContext> ContextCreator { get; private set; }

        #region Write

        #region Add

        /// <summary>
        /// Add: Adds entity to database without nested entity properties.
        /// </summary>
        /// <param name="entity">The entity that will be inserted into the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entity.</returns>
        public virtual TEntity Add(TEntity entity)
        {
            entity = entity.DeleteNestedProperties();

            using (var entityContext = ContextCreator())
            {
                EntityEntry<TEntity> addedEntity = entityContext.Set<TEntity>().Add(entity);
                entityContext.SaveChanges();

                return addedEntity.Entity;
            }
        }

        /// <summary>
        /// AddWithNestedProperties: Adds entity to database with nested entity properties.
        /// </summary>
        /// <param name="entity">The entity that will be inserted into the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entity.</returns>
        public virtual TEntity AddWithNestedProperties(TEntity entity)
        {
            using (var entityContext = ContextCreator())
            {
                EntityEntry<TEntity> addedEntity = entityContext.Set<TEntity>().Add(entity);
                entityContext.SaveChanges();

                return addedEntity.Entity;
            }
        }

        /// <summary>
        /// AddRange: Adds a range of entities to database without nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entities.</returns>
        public virtual void AddRange(IEnumerable<TEntity> entityList)
        {
            entityList = entityList.Select(entity =>
            {
                entity = entity.DeleteNestedProperties();
                return entity;
            });

            using (var entityContext = ContextCreator())
            {
                entityContext.Set<TEntity>().AddRange(entityList);

                entityContext.SaveChanges();
            }
        }

        /// <summary>
        /// AddRangeWithNestedProperties: Adds a range of entities to database with nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entities.</returns>
        public virtual void AddRangeWithNestedProperties(IEnumerable<TEntity> entityList)
        {
            using (var entityContext = ContextCreator())
            {
                entityContext.Set<TEntity>().AddRange(entityList);

                entityContext.SaveChanges();
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove: Removes entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be removed from the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The enity removed.</returns>
        public virtual TEntity Remove(TEntity entity)
        {
            using (var entityContext = ContextCreator())
            {
                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();

                return entity;
            }
        }

        /// <summary>
        /// Remove: Removes entity from database by entity's id.
        /// </summary>
        /// <param name="id">The entity id that will be removed from the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The enity removed.</returns>
        public virtual TEntity Remove(int id)
        {
            using (var entityContext = ContextCreator())
            {
                TEntity entity = entityContext.Set<TEntity>().Find(id);
                if (entity == null)
                    return new TEntity();

                entityContext.Entry(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();

                return entity;
            }
        }

        /// <summary>
        /// RemoveRange: Removes a range of entities from database.
        /// </summary>
        /// <param name="entityList">The list of entities that will be removed from the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entityList)
        {
            using (var entityContext = ContextCreator())
            {
                foreach (var entity in entityList)
                {
                    entityContext.Entry(entity).State = EntityState.Deleted;
                }

                entityContext.SaveChanges();

                return entityList;
            }
        }

        /// <summary>
        /// RemoveRangeWithoutFetchData: Removes a range of entities from database with filtering.
        /// </summary>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        public virtual void RemoveRangeWithoutFetchData(IEnumerable<string> clauses = null)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();

                if (clauses != null)
                    temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                entityContext.Set<TEntity>().RemoveRange(temporaryQuery);
                entityContext.SaveChanges();
            }
        }

        /// <summary>
        /// RemoveRangeWithoutFetchData: Removes a range of entities from database with filtering.
        /// </summary>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        public virtual void RemoveRangeWithoutFetchData(params Expression<Func<TEntity, bool>>[] clauses)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();

                if (clauses != null)
                    temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                entityContext.Set<TEntity>().RemoveRange(temporaryQuery);
                entityContext.SaveChanges();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update: Updates entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be updated in the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The updated entity.</returns>
        public virtual TEntity Update(TEntity entity)
        {
            entity = entity.DeleteNestedProperties();

            using (var entityContext = ContextCreator())
            {
                TEntity existingEntity =
                    entityContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);

                if (existingEntity == null)
                    return new TEntity();

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        /// <summary>
        /// UpdateWithNestedProperties: Updates entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be updated in the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The updated entity.</returns>
        public virtual TEntity UpdateWithNestedProperties(TEntity entity)
        {
            using (var entityContext = ContextCreator())
            {
                TEntity existingEntity =
                    entityContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);

                if (existingEntity == null)
                    return new TEntity();

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        /// <summary>
        /// UpdateRange: Updates a range of entities from database without nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be updated in the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entityList)
        {
            using (var entityContext = ContextCreator())
            {
                List<TEntity> updatedEntities = new List<TEntity>();

                foreach (var entity in entityList)
                {
                    var pureEntity = entity.DeleteNestedProperties();

                    TEntity existingEntity =
                        entityContext.Set<TEntity>().FirstOrDefault(e => e.Id == pureEntity.Id);

                    if (existingEntity != null)
                        SimpleMapper.PropertyMap(pureEntity, existingEntity);

                    updatedEntities.Add(existingEntity);
                }

                entityContext.SaveChanges();

                return updatedEntities;
            }
        }

        /// <summary>
        /// UpdateRange: Updates a range of entities from database with nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be updated in the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        public virtual IEnumerable<TEntity> UpdateRangeWithNestedProperties(IEnumerable<TEntity> entityList)
        {
            using (var entityContext = ContextCreator())
            {
                List<TEntity> updatedEntities = new List<TEntity>();

                foreach (var entity in entityList)
                {
                    TEntity existingEntity =
                        entityContext.Set<TEntity>().FirstOrDefault(e => e.Id == entity.Id);

                    if (existingEntity != null)
                        SimpleMapper.PropertyMap(entity, existingEntity);

                    updatedEntities.Add(existingEntity);
                }

                entityContext.SaveChanges();

                return updatedEntities;
            }
        }

        #endregion

        #endregion

        #region Read

        #region FindAll

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table.
        /// </summary>
        /// <returns>The retrieved entities from the database table.</returns>
        public virtual IEnumerable<TEntity> FindAll()
        {
            using (var entityContext = ContextCreator())
            {
                return entityContext.Set<TEntity>().ToList();
            }
        }

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering.</returns>
        public virtual IEnumerable<TEntity> FindAll(IEnumerable<string> clauses)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                return temporaryQuery.ToList();
            }
        }

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering.</returns>
        public virtual IEnumerable<TEntity> FindAll(params Expression<Func<TEntity, bool>>[] clauses)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> query = entityContext.Set<TEntity>();
                query = clauses.Aggregate(query,
                    (current, property) => current.Where(property));

                return query.ToList();
            }
        }

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <returns>The retrieved entities from the database table with pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAll(QueryLimits queryLimits)
        {
            using (var entityContext = ContextCreator())
            {
                return entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                    .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                    .Take(queryLimits.Limit)
                    .ToList();
            }
        }

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAll(QueryLimits queryLimits, IEnumerable<string> clauses)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                foreach (var clause in clauses)
                    temporaryQuery = temporaryQuery.Where(clause);
                return temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                    .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                    .Take(queryLimits.Limit)
                    .ToList();
            }
        }

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAll(QueryLimits queryLimits, params Expression<Func<TEntity, bool>>[] clauses)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery,
                    (current, property) => current.Where(property));
                return temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                    .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                    .Take(queryLimits.Limit)
                    .ToList();
            }
        }

        #endregion

        #region FindAllIncluding

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties.
        /// </summary>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var query = entityContext.Set<TEntity>().AsQueryable();
                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(IEnumerable<string> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clause">Condition that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(Expression<Func<TEntity, bool>> clause,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = temporaryQuery.Where(clause);
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clauses">Conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(List<Expression<Func<TEntity, bool>>> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> query;

                query = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            IEnumerable<string> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var query = temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clause">Condition that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            Expression<Func<TEntity, bool>> clause,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = temporaryQuery.Where(clause);

                var query = temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">Conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            List<Expression<Func<TEntity, bool>>> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var query = temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too).
        /// </summary>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with the properties in includeProperties.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var query = entityContext.Set<TEntity>().AsQueryable();
                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities according to the clauses conditions and with the properties in includeProperties.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(IEnumerable<string> clauses,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with the properties in includeProperties and the selected pagination information.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> query;

                query = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities according to the clauses conditions, with the properties in includeProperties and the 
        /// selected pagination information.</returns>
        public virtual IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            IEnumerable<string> clauses,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var query = temporaryQuery.OrderBy(String.Format("{0} {1}", queryLimits.Order, queryLimits.Orientation))
                        .Skip(queryLimits.Limit * (queryLimits.Page - 1))
                        .Take(queryLimits.Limit);

                query = includeProperties.Aggregate(query,
                    (current, property) => current.Include(property));
                return query.ToList();
            }
        }

        #endregion

        #region Find

        #region FindMax

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property.
        /// </summary>
        /// <param name="orderProperty">The property that the max value will be retrieved.</param>
        /// <returns>The retrieved entity with the max value property.</returns>
        public virtual TEntity FindMax(Expression<Func<TEntity, object>> orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);
                var entity = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property that the max value will be retrieved.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses, Expression<Func<TEntity, object>> orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var propertyName = PropertyService.GetLambdaPropName(orderProperty);
                var entity = temporaryQuery.OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <returns>The retrieved entity with the max value property.</returns>
        public virtual TEntity FindMax(string orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                var entity = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses, string orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var entity = temporaryQuery.OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        public virtual TEntity FindMax(Expression<Func<TEntity, object>> orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves that has the max value property with entity nested properties filtering and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses,
            Expression<Func<TEntity, object>> orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        public virtual TEntity FindMax(string orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses,
            string orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        public virtual TEntity FindMax(Expression<Func<TEntity, object>> orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses,
            Expression<Func<TEntity, object>> orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        public virtual TEntity FindMax(string orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        public virtual TEntity FindMax(IEnumerable<string> clauses,
            string orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "DESC")).FirstOrDefault();

                return entity;
            }
        }

        #endregion

        #region FindMin

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property.
        /// </summary>
        /// <param name="orderProperty">The property that the min value will be retrieved.</param>
        /// <returns>The retrieved entity with the min value property.</returns>
        public virtual TEntity FindMin(Expression<Func<TEntity, object>> orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);
                var entity = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property that the min value will be retrieved.</param>
        /// <reutnr>The retrieved entity that has the min value according with the filter.</reutnr>
        public virtual TEntity FindMin(IEnumerable<string> clauses, Expression<Func<TEntity, object>> orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var propertyName = PropertyService.GetLambdaPropName(orderProperty);
                var entity = temporaryQuery.OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <returns>The retrieved entity with the min value property.</returns>
        public virtual TEntity FindMin(string orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                var entity = entityContext.Set<TEntity>().OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This funtion retrieves the entity that has the min value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        public virtual TEntity FindMin(IEnumerable<string> clauses, string orderProperty)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));

                var entity = temporaryQuery.OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        public virtual TEntity FindMin(Expression<Func<TEntity, object>> orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties filtering 
        /// and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        public virtual TEntity FindMin(IEnumerable<string> clauses,
            Expression<Func<TEntity, object>> orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        public virtual TEntity FindMin(string orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        public virtual TEntity FindMin(IEnumerable<string> clauses,
            string orderProperty,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        public virtual TEntity FindMin(Expression<Func<TEntity, object>> orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        public virtual TEntity FindMin(IEnumerable<string> clauses,
            Expression<Func<TEntity, object>> orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                var propertyName = PropertyService.GetLambdaPropName(orderProperty);

                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", propertyName, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        public virtual TEntity FindMin(string orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        public virtual TEntity FindMin(IEnumerable<string> clauses,
            string orderProperty,
            params string[] includeProperties)
        {
            using (var entityContext = ContextCreator())
            {
                IQueryable<TEntity> temporaryQuery = entityContext.Set<TEntity>();
                temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                var query = includeProperties.Aggregate(temporaryQuery,
                    (current, property) => current.Include(property));

                var entity = query.OrderBy(String.Format("{0} {1}", orderProperty, "ASC")).FirstOrDefault();

                return entity;
            }
        }

        #endregion

        /// <summary>
        /// Find: This function retrieves any entity in the table type by id.
        /// </summary>
        /// <param name="id">The entity's id that will be retrieved.</param>
        /// <returns>The found entities.</returns>
        public virtual TEntity Find(int id)
        {
            using (var entityContext = ContextCreator())
            {
                var entity = entityContext.Set<TEntity>().Find(id);

                return entity;
            }
        }

        #endregion

        #endregion

        #region Count

        /// <summary>
        /// Count: This function counts the number of registers in the database entity.
        /// </summary>
        /// <returns>The number of registers in the database entity.</returns>
        public virtual int Count()
        {
            using (var context = ContextCreator())
            {
                try
                {
                    return context.Set<TEntity>().Count();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                        String.Format("It was not possible to find the data: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Count: This function counts the number of registers in the database entity, according with the clauses.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The number of registers in the database entity.</returns>
        public virtual int Count(IEnumerable<string> clauses)
        {
            using (var context = ContextCreator())
            {
                try
                {
                    IQueryable<TEntity> temporaryQuery = context.Set<TEntity>();
                    temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                    return temporaryQuery.Count();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                        String.Format("It was not possible to find the data: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Count: This function counts the number of registers in the database entity, according with the clauses.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The number of registers in the database entity.</returns>
        public virtual int Count(params Expression<Func<TEntity, bool>>[] clauses)
        {
            using (var context = ContextCreator())
            {
                try
                {
                    IQueryable<TEntity> temporaryQuery = context.Set<TEntity>();
                    temporaryQuery = clauses.Aggregate(temporaryQuery, (current, clause) => current.Where(clause));
                    return temporaryQuery.Count();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                        String.Format("It was not possible to find the data: {0}", ex.Message));
                }
            }
        }

        #endregion

        /// <summary>
        /// ExecuteQuery: This function executes any SQL query at the database.
        /// </summary>
        /// <param name="query">The query that will be executed.</param>
        /// <returns>The values returned by the query.</returns>
        public virtual int ExecuteQuery(string query)
        {
            using (var entityContext = ContextCreator())
            {
                var rowsAffected = entityContext.Database.ExecuteSqlCommand(query);

                return rowsAffected;
            }
        }
    }
}