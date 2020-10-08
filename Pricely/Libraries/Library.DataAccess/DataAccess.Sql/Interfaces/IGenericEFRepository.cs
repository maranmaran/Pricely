using Common.Models;
using DataAccess.Sql.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Sql.Interfaces
{
    /// <summary>
    /// Generic repository for Entity framework core
    /// This provides all basic CRUD operations along with sorting, filtering and pagination
    /// </summary>
    /// <typeparam name="TEntity">Database entity that inherits IEntity interface provided by library</typeparam>
    public interface IGenericEfRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <param name="filter">Filter predicate - when called returns filtered input (reduces data set result size)</param>
        /// <param name="sortBy">Sort by statement - when called returns sorted input</param>
        /// <param name="include">Include statement - when called includes additional navigation props / transforms input</param>
        /// <param name="disableTracking">Disables tracking from EF for more optimized query</param>
        /// <param name="cancellationToken">Stops request when requested</param>
        Task<IEnumerable<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets paged list of entities
        /// </summary>
        /// <param name="page">Wanted page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="filter">Filter predicate - when called returns filtered input (reduces data set result size)</param>
        /// <param name="sortBy">Sort by statement - when called returns sorted input</param>
        /// <param name="include">Include statement - when called includes additional navigation props / transforms input</param>
        /// <param name="disableTracking">Disables tracking from EF for more optimized query</param>
        /// <param name="cancellationToken">Stops request when requested</param>
        /// <returns></returns>
        Task<PagedList<TEntity>> GetPaged(
            int page,
            int pageSize = 20,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets single entity by ID
        /// </summary>
        /// <param name="filter">Filter predicate</param>
        /// <param name="sortBy">Order by statement</param>
        /// <param name="include">Include statement</param>
        /// <param name="disableTracking">Disables tracking</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// Inserts single entity
        /// </summary>
        Task<Guid> Insert(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates single entity
        /// </summary>
        Task Update(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes single entity
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Generic repository for Entity framework core with support for projection to DTO models
    /// This provides all basic CRUD operations along with sorting, filtering and pagination
    /// </summary>
    /// <typeparam name="TEntity">Database entity that inherits IEntity interface provided by library</typeparam>
    /// <typeparam name="TProjection">DTO reflection for database entity that extends EntityDtoBase</typeparam>
    public interface IGenericEfRepository<TEntity, TProjection>
        : IGenericEfRepository<TEntity>
        where TEntity : EntityBase
        where TProjection : EntityDtoBase
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <param name="filter">Filter predicate - when called returns filtered input (reduces data set result size)</param>
        /// <param name="sortBy">Sort by statement - when called returns sorted input</param>
        /// <param name="include">Include statement - when called includes additional navigation props / transforms input</param>
        /// <param name="disableTracking">Disables tracking from EF for more optimized query</param>
        /// <param name="cancellationToken">Stops request when requested</param>
        new Task<IEnumerable<TProjection>> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets paged list of entities
        /// </summary>
        /// <param name="page">Wanted page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="filter">Filter predicate - when called returns filtered input (reduces data set result size)</param>
        /// <param name="sortBy">Sort by statement - when called returns sorted input</param>
        /// <param name="include">Include statement - when called includes additional navigation props / transforms input</param>
        /// <param name="disableTracking">Disables tracking from EF for more optimized query</param>
        /// <param name="cancellationToken">Stops request when requested</param>
        /// <returns></returns>
        new Task<PagedList<TProjection>> GetPaged(
            int page,
            int pageSize = 20,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets single entity by ID
        /// </summary>
        /// <param name="filter">Filter predicate</param>
        /// <param name="sortBy">Order by statement</param>
        /// <param name="include">Include statement</param>
        /// <param name="disableTracking">Disables tracking</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        new Task<TProjection> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default);
    }
}