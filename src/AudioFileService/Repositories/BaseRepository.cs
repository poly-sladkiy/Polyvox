﻿using AudioFileService.Interfaces;
using AudioFileService.Models;
using AudioFileService.Services;
using MongoDB.Driver;

namespace AudioFileService.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly MongoConnectionService _connectionService;
    private readonly IMongoCollection<TEntity> _songCollection;

    public BaseRepository(MongoConnectionService connectionService, string collectionName)
    {
        _connectionService = connectionService;
        _songCollection = _connectionService.GetCollection<TEntity>(collectionName);
    }

    public virtual async Task<List<TEntity>> GetAsync()
    {
        return await _songCollection.Find(_ => true).ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAsync(int count)
    {
        return await _songCollection.Find(_ => true).ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(string id)
    {
        return await _songCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task CreateAsync(TEntity newEntity)
    {
        await _songCollection.InsertOneAsync(newEntity);
    }

    public virtual async Task CreateAsync(IEnumerable<TEntity> newEntity)
    {
        await _songCollection.InsertManyAsync(newEntity);
    }

    public virtual async Task UpdateAsync(string id, TEntity updateEntity)
    {
        await _songCollection.ReplaceOneAsync(x => x.Id == id, updateEntity);
    }

    public virtual async Task RemoveAsync(string id)
    {
        await _songCollection.DeleteOneAsync(x => x.Id == id);
    }
}