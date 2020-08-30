using MenuService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Persistence.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);

        Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<Guid> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default);

        Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        Task ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);

        Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
    }
}