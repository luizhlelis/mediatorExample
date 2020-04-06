using MediatorExample.Domain.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;

namespace MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic
{
    public interface IEntityRepository<TEntity>
        where TEntity : class, IIdentifiableEntity, new()
    {
        #region Write

        #region Add

        /// <summary>
        /// Add: Adds entity to database without nested entity properties.
        /// </summary>
        /// <param name="entity">The entity that will be inserted into the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entity.</returns>
        TEntity Add(TEntity entity);
        /// <summary>
        /// AddWithNestedProperties: Adds entity to database with nested entity properties.
        /// </summary>
        /// <param name="entity">The entity that will be inserted into the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entity.</returns>
        TEntity AddWithNestedProperties(TEntity entity);
        /// <summary>
        /// AddRange: Adds a range of entities to database without nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entities.</returns>
        void AddRange(IEnumerable<TEntity> entityList);
        /// <summary>
        /// AddRangeWithNestedProperties: Adds a range of entities to database with nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The added entities.</returns>
        void AddRangeWithNestedProperties(IEnumerable<TEntity> entityList);
        /// <summary>
        /// BulkAddAll: Adds a range of entities to database via a bulk operation.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="allowNotNullSelfReferences">When AllowNotNullSelfReferences is set to true, entities with self referencing 
        /// foreign keys declared as NOT NULL will be properly inserted. But, this will only work if the database user has the 
        /// required privileges to execute ALTER TABLE table name NOCHECK CONSTRAINT ALL and ALTER TABLE table name CHECK CONSTRAINT ALL.</param>
        /// <param name="commandTimeoutInMinutes">Command Timeout In Minutes.</param>
        /// <param name="enableRecursiveInsert">When Recursive is set to true the entire entity hierarchy will be inserted.</param>
        /// <param name="sortUsingClusteredIndex">When SortUsingClusteredIndex is set to true the entities will be sorted according to the clustered index 
        /// of the target table.</param>
        /// <param name="updateStatistics">When UpdateStatistics is set the command "UPDATE STATISTICS tablename WITH ALL" will be executed after the insert.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        //void BulkAddAll(IList<TEntity> entityList);

        #endregion

        #region Remove

        /// <summary>
        /// Remove: Removes entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be removed from the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The enity removed.</returns>
        TEntity Remove(TEntity entity);
        /// <summary>
        /// Remove: Removes entity from database by entity's id.
        /// </summary>
        /// <param name="id">The entity id that will be removed from the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The enity removed.</returns>
        TEntity Remove(int id);
        /// <summary>
        /// RemoveRange: Removes a range of entities from database.
        /// </summary>
        /// <param name="entityList">The list of entities that will be removed from the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entityList);
        /// <summary>
        /// RemoveRangeWithoutFetchData: Removes a range of entities from database with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        void RemoveRangeWithoutFetchData(IEnumerable<string> clauses = null);
        /// RemoveRangeWithoutFetchData: Removes a range of entities from database with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        void RemoveRangeWithoutFetchData(params Expression<Func<TEntity, bool>>[] clauses);
        /// <summary>
        /// BulkDeleteAll: Deletes a range of entities to database via a bulk operation.
        /// </summary>
        /// <param name="commandTimeoutInMinutes">Command Timeout In Minutes.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        //void BulkDeleteAll(int commandTimeoutInMinutes = 10,
        //    params Expression<Func<TEntity, bool>>[] clauses);

        #endregion

        #region Update

        /// <summary>
        /// Update: Updates entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be updated in the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The updated entity.</returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// UpdateWithNestedProperties: Updates entity from database.
        /// </summary>
        /// <param name="entity">The entity that will be updated in the database.</param>
        /// <param name="operation">Operation to audit.</param>
        /// <returns>The updated entity.</returns>
        TEntity UpdateWithNestedProperties(TEntity entity);
        /// <summary>
        /// UpdateRange: Updates a range of entities from database without nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be updated in the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entityList);
        /// <summary>
        /// UpdateRange: Updates a range of entities from database with nested entity properties.
        /// </summary>
        /// <param name="entityList">The list of entities that will be updated in the database.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        IEnumerable<TEntity> UpdateRangeWithNestedProperties(IEnumerable<TEntity> entityList);
        /// <summary>
        /// BulkUpdateAll: Updates a range of entities to database via a bulk operation.
        /// </summary>
        /// <param name="entityList">The list of entities that will be inserted into the database.</param>
        /// <param name="updatedColumnNames">If UpdatedColumnNames is an empty list all non-key mapped columns will be updated, 
        /// otherwise only the columns specified.</param>
        /// <param name="keyPropertyNames">If KeyMemberNames is an empty list the primary key columns will be used to select which 
        /// rows to update, otherwise the columns specified will be used.</param>
        /// <param name="insertIfNew">Insert if new.</param>
        /// <param name="isAudited">Is audited.</param>
        /// <param name="operation">Operation to audit.</param>
        //void BulkUpdateAll(IList entityList, string[] updatedColumnNames, string[] keyPropertyNames, int commandTimeoutInMinutes = 10,
        //ß    bool insertIfNew = false);

        #endregion

        #endregion

        #region Read

        #region FindAll

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table.
        /// </summary>
        /// <returns>The retrieved entities from the database table.</returns>
        IEnumerable<TEntity> FindAll();
        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering.</returns>
        IEnumerable<TEntity> FindAll(IEnumerable<string> clauses);
        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering.</returns>
        IEnumerable<TEntity> FindAll(params Expression<Func<TEntity, bool>>[] clauses);
        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <returns>The retrieved entities from the database table with pagination and sorting.</returns>
        IEnumerable<TEntity> FindAll(QueryLimits queryLimits);
        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAll(QueryLimits queryLimits, IEnumerable<string> clauses);

        /// <summary>
        /// FindAll: This function retrieves all the entities from the database table with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The retrieved entities from the database table with filtering, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAll(QueryLimits queryLimits, params Expression<Func<TEntity, bool>>[] clauses);

        #endregion

        #region FindAllIncluding

        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties.
        /// </summary>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties.</returns>
        IEnumerable<TEntity> FindAllIncluding(
          params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(IEnumerable<string> clauses,
          params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clause">Condition that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(Expression<Func<TEntity, bool>> clause,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties and filtering.
        /// </summary>
        /// <param name="clauses">Conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(List<Expression<Func<TEntity, bool>>> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
          params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
          IEnumerable<string> clauses,
          params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clause">Condition that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            Expression<Func<TEntity, bool>> clause,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table with nested entity properties, filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">Conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with nested entity properties, filtering, pagination and sorting.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
            List<Expression<Func<TEntity, bool>>> clauses,
            params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too).
        /// </summary>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with the properties in includeProperties.</returns>
        IEnumerable<TEntity> FindAllIncluding(params string[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities according to the clauses conditions and with the properties in includeProperties.</returns>
        IEnumerable<TEntity> FindAllIncluding(IEnumerable<string> clauses,
          params string[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities with the properties in includeProperties and the selected pagination information.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
          params string[] includeProperties);
        /// <summary>
        /// FindAllIncluding: This function retrieves all the entities from the database table including nested entity 
        /// properties (that can have nested entity properties too) and with filtering, pagination and sorting.
        /// </summary>
        /// <param name="queryLimits">The limit parameters that will be applied to the pagination and sorting actions.</param>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entities according to the clauses conditions, with the properties in includeProperties and the 
        /// selected pagination information.</returns>
        IEnumerable<TEntity> FindAllIncluding(QueryLimits queryLimits,
          IEnumerable<string> clauses,
          params string[] includeProperties);

        #endregion

        #region Find

        #region FindMax

        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property.
        /// </summary>
        /// <param name="orderProperty">The property that the max value will be retrieved.</param>
        /// <returns>The retrieved entity with the max value property.</returns>
        TEntity FindMax(Expression<Func<TEntity, object>> orderProperty);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property that the max value will be retrieved.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses, Expression<Func<TEntity, object>> orderProperty);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <returns>The retrieved entity with the max value property.</returns>
        TEntity FindMax(string orderProperty);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses, string orderProperty);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        TEntity FindMax(Expression<Func<TEntity, object>> orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves that has the max value property with entity nested properties filtering and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses,
           Expression<Func<TEntity, object>> orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        TEntity FindMax(string orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses,
           string orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        TEntity FindMax(Expression<Func<TEntity, object>> orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses,
           Expression<Func<TEntity, object>> orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the max value with entity nested properties.</retunrs>
        TEntity FindMax(string orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMax: This function retrieves the entity that has the max value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the max value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the max value according to the clauses.</returns>
        TEntity FindMax(IEnumerable<string> clauses,
           string orderProperty,
           params string[] includeProperties);

        #endregion

        #region FindMin

        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property.
        /// </summary>
        /// <param name="orderProperty">The property that the min value will be retrieved.</param>
        /// <returns>The retrieved entity with the min value property.</returns>
        TEntity FindMin(Expression<Func<TEntity, object>> orderProperty);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property that the min value will be retrieved.</param>
        /// <reutnr>The retrieved entity that has the min value according with the filter.</reutnr>
        TEntity FindMin(IEnumerable<string> clauses, Expression<Func<TEntity, object>> orderProperty);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <returns>The retrieved entity with the min value property.</returns>
        TEntity FindMin(string orderProperty);
        /// <summary>
        /// FindMin: This funtion retrieves the entity that has the min value property with filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        TEntity FindMin(IEnumerable<string> clauses, string orderProperty);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        TEntity FindMin(Expression<Func<TEntity, object>> orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties filtering 
        /// and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        TEntity FindMin(IEnumerable<string> clauses,
           Expression<Func<TEntity, object>> orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties.
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        TEntity FindMin(string orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        TEntity FindMin(IEnumerable<string> clauses,
           string orderProperty,
           params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        TEntity FindMin(Expression<Func<TEntity, object>> orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        TEntity FindMin(IEnumerable<string> clauses,
           Expression<Func<TEntity, object>> orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too).
        /// </summary>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <retunrs>The retrieved entity that has the min value with entity nested properties.</retunrs>
        TEntity FindMin(string orderProperty,
           params string[] includeProperties);
        /// <summary>
        /// FindMin: This function retrieves the entity that has the min value property with entity nested properties 
        /// (that can have nested entity properties too) and filtering.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <param name="orderProperty">The property name that the min value will be retrieved.</param>
        /// <param name="includeProperties">The list of entity properties that will be included into the action.</param>
        /// <returns>The retrieved entity that has the min value according to the clauses.</returns>
        TEntity FindMin(IEnumerable<string> clauses,
           string orderProperty,
           params string[] includeProperties);

        #endregion

        /// <summary>
        /// Find: This function retrieves any entity in the table type by id.
        /// </summary>
        /// <param name="id">The entity's id that will be retrieved.</param>
        /// <returns>The found entities.</returns>
        TEntity Find(int id);

        #endregion

        #endregion

        #region Count

        /// <summary>
        /// Count: This function counts the number of registers in the database entity.
        /// </summary>
        /// <returns>The number of registers in the database entity.</returns>
        int Count();
        /// <summary>
        /// Count: This function counts the number of registers in the database entity, according with the clauses.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The number of registers in the database entity.</returns>
        int Count(IEnumerable<string> clauses);
        /// <summary>
        /// Count: This function counts the number of registers in the database entity, according with the clauses.
        /// </summary>
        /// <param name="clauses">The list of conditions that will be applied to the action.</param>
        /// <returns>The number of registers in the database entity.</returns>
        int Count(params Expression<Func<TEntity, bool>>[] clauses);

        #endregion

        /// <summary>
        /// ExecuteQuery: This function executes any SQL query at the database.
        /// </summary>
        /// <param name="query">The query that will be executed.</param>
        /// <returns>The values returned by the query.</returns>
        int ExecuteQuery(string query);
    }
}