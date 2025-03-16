using Audio.Persistence.Entities;
using Result;

namespace Audio.Persistence.Repositories;

public interface IReadRepository<TEntity, TEntityId>
{
	Task<Result<List<TEntity>>> GetAllAsync();
	Task<Result<TEntity>> GetById(EntityIdentifier<TEntityId> id);
}