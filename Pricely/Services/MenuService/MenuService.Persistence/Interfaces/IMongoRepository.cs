using MenuService.Domain.Interfaces;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Persistence.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IMongoQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Guid> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default);
        Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        Task ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default);
        Task ReplaceManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        Task<Guid> UpsertOneAsync(TDocument document, CancellationToken cancellationToken = default);
        Task UpsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        Task UpdateOneAsync(TDocument document, CancellationToken cancellationToken = default);
        Task UpdateManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}