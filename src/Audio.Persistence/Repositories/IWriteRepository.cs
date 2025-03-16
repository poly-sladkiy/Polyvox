using Audio.Persistence.Entities;
using Result;

namespace Audio.Persistence.Repositories;

public interface IWriteRepository<TEntity, TEntityId> : IReadRepository<TEntity, TEntityId>
{
	Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task<Result<List<TEntity>>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
	Task<Result<TEntity>> UpdateAsync(EntityIdentifier<TEntityId> id, TEntity entity, CancellationToken cancellationToken = default);
	Task<Result<bool>> DeleteAsync(EntityIdentifier<TEntityId> id, CancellationToken cancellationToken = default);
}