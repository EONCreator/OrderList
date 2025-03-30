using MongoDB.Bson;
using MongoDB.Driver;

namespace OrderList.Infrastructure.Database;

public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        
        CreateCollectionIfNotExists("Orders");
    }

    public IMongoCollection<BsonDocument> Records => _database.GetCollection<BsonDocument>("Orders");
    
    private void CreateCollectionIfNotExists(string collectionName)
    {
        var collectionList = _database.ListCollectionNames().ToList();
        if (!collectionList.Contains(collectionName))
        {
            _database.CreateCollection(collectionName);
        }
    }
}