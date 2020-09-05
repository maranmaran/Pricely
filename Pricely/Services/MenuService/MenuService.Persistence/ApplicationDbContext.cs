using MenuService.Domain.Entities;
using MenuService.Persistence.Seed;
using MongoDB.Driver;

namespace MenuService.Persistence
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database = null;

        public ApplicationDbContext(DatabaseSettings _settings)
        {
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.Database);
        }

        public IMongoCollection<Menu> Menu => _database.GetCollection<Menu>(typeof(Menu).GetCollectionName());
    }
}

