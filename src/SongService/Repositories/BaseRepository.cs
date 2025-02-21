using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SongService.Interfaces;
using SongService.Models;
using SongService.Services;

namespace SongService.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly MongoConnectionService _connectionService;
    private readonly IMongoCollection<TEntity> _songCollection;

    public BaseRepository(MongoConnectionService connectionService, string collectionName)
    {
        _connectionService = connectionService;
        _songCollection = _connectionService.GetCollection<TEntity>(collectionName);
    }

    public async Task<List<TEntity>> GetAsync() =>
        await _songCollection.Find(_ => true).ToListAsync();
    
    public async Task<List<TEntity>> GetAsync(int count) =>
        await _songCollection.Find(_ => true).ToListAsync();

    public async Task<TEntity?> GetAsync(string id) =>
        await _songCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TEntity newEntity) =>
        await _songCollection.InsertOneAsync(newEntity);
    
    public async Task CreateAsync(IEnumerable<TEntity> newEntity) =>
        await _songCollection.InsertManyAsync(newEntity);

    public async Task UpdateAsync(string id, TEntity updateEntity) =>
        await _songCollection.ReplaceOneAsync(x => x.Id == id, updateEntity);

    public async Task RemoveAsync(string id) =>
        await _songCollection.DeleteOneAsync(x => x.Id == id);
}