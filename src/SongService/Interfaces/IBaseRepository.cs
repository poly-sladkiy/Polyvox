namespace SongService.Interfaces;

public interface IBaseRepository<TEntity>
{
    public Task<List<TEntity>> GetAsync();
    
    public Task<List<TEntity>> GetAsync(int count);

    public Task<TEntity?> GetAsync(string id);

    public Task CreateAsync(TEntity newEntity);
    
    public Task CreateAsync(IEnumerable<TEntity> newEntities);

    public Task UpdateAsync(string id, TEntity updateEntity);

    public Task RemoveAsync(string id);
}