using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SongService.Models;

namespace SongService.Services;

public class MongoConnectionService
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoConnectionService(IOptions<MongoConnectionSettings> setting)
    {
        var mongoClient = new MongoClient(setting.Value.ConnectionString);
        _mongoDatabase = mongoClient.GetDatabase(setting.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name, MongoCollectionSettings? settings = null)
    {
        return _mongoDatabase.GetCollection<T>(name, settings);
    }
}