using DataAccess.NoSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.NoSql.Interfaces
{
    public interface IGenericDocumentRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        Task<IEnumerable<TDocument>> GetAll(CancellationToken cancellationToken = default);

        Task<IEnumerable<TDocument>> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default);


        Task<IEnumerable<TProjected>> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression,
            CancellationToken cancellationToken = default);

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