using MongoDB.Driver;

namespace SwitchAPI.DB
{
    public class MongoContext
    {
        IMongoClient _client;
        IMongoDatabase _database;
        public MongoContext(string ConectionString, string DbName)
        {
            var client = new MongoClient(ConectionString);
            _database = client.GetDatabase(DbName);

        }
        public IMongoCollection<Cars> GetCollection<Cars>(string CollectionName)
        {
            return _database.GetCollection<Cars>(CollectionName);
        }

    }
}
