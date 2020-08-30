using MenuService.Domain.Attributes;
using MenuService.Domain.Interfaces;
using MenuService.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Persistence.Repositories
{
    internal class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private protected readonly IMongoCollection<TDocument> Collection;

        public MongoRepository(DatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);

            Collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await Collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

            return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Guid> InsertOneAsync(TDocument document, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(document, new InsertOneOptions() { BypassDocumentValidation = false }, cancellationToken);

            return Guid.Parse(document.Id.ToString());
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(documents, new InsertManyOptions(), cancellationToken);
        }

        public async Task ReplaceOneAsync(TDocument document, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);

            await Collection.FindOneAndReplaceAsync(filter, document, new FindOneAndReplaceOptions<TDocument, TDocument>(), cancellationToken);
        }

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            await Collection.FindOneAndDeleteAsync(filterExpression, new FindOneAndDeleteOptions<TDocument, TDocument>(), cancellationToken);
        }

        public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var objectId = new ObjectId(id);

            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);

            await Collection.FindOneAndDeleteAsync(filter, new FindOneAndDeleteOptions<TDocument, TDocument>(), cancellationToken);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(filterExpression, cancellationToken);
        }
    }
}
