using Common.Models;
using DataAccess.Sql.Interfaces;
using DataAccess.Sql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Sql.Repositories
{
    internal class GenericEfRepository<TEntity> : IGenericEfRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IApplicationDbContext _context;
        private protected readonly DbSet<TEntity> Entities;

        public GenericEfRepository(IApplicationDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }


        public async Task<IEnumerable<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var entities = Entities.AsQueryable();

            if (disableTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (include != null)
            {
                entities = include(entities);
            }

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (sortBy != null)
            {
                entities = sortBy(entities);
            }

            return await entities.ToListAsync(cancellationToken);
        }


        public async Task<PagedList<TEntity>> GetPaged(
            int page,
            int pageSize = 20,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var entities = Entities.AsQueryable();

            if (disableTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (include != null)
            {
                entities = include(entities);
            }

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (sortBy != null)
            {
                entities = sortBy(entities);
            }

            var totalItems = await entities.CountAsync(cancellationToken);
            var pagedEntities = await entities
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<TEntity>(pagedEntities, totalItems, page, pageSize);
        }

        public async Task<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> sortBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default)
        {
            var entities = Entities.AsQueryable();

            if (disableTracking)
            {
                entities = entities.AsNoTracking();
            }

            if (include != null)
            {
                entities = include(entities);
            }

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (sortBy != null)
            {
                entities = sortBy(entities);
            }

            return await entities.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await Entities.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty guid", nameof(id));

            var entity = await Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
