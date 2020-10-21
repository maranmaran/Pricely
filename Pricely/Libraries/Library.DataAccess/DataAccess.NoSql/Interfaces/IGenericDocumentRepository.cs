using DataAccess.NoSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.NoSql.Interfaces
{
    /// <summary>
    /// Handles basic CRUD interactions for document databases
    /// </summary>
    /// <typeparam name="TDocument">The type of the document.</typeparam>
    public interface IGenericDocumentRepository<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Returns document collection as queryable.
        /// </summary>
        IQueryable<TDocument> AsQueryable();

        /// <summary>
        /// Gets all documents.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<IEnumerable<TDocument>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Filters the by given filter expression
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<IEnumerable<TDocument>> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Filters the by given filter expression and projects data
        /// </summary>
        /// <typeparam name="TProjected">The type of the projected.</typeparam>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="projectionExpression">The projection expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Data projection</returns>
        Task<IEnumerable<TProjected>> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds single document in async fashion based on filter expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds single document in async fashion by the given GUID identifier
        /// TODO: Support generic identifiers
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<TDocument> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts one document asynchronously
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<Guid> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts many documents asynchronously
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replaces one document asynchronously
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replaces many documents asynchronously
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task ReplaceManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upsert one document asynchronously
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<Guid> UpsertOneAsync(TDocument document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upserts many documents asynchronously
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update one document asynchronously
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpdateOneAsync(TDocument document, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates many documents asynchronously
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpdateManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update one document asynchronously by given filter expression result
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update one document asynchronously by ID
        /// </summary>
        /// TODO Support generic identifiers
        /// <param name="id">Identifier</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes many documents asynchronously
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default);
    }
}