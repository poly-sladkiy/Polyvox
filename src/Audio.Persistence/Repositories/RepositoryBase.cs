using Audio.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Result;

namespace Audio.Persistence.Repositories;

public class RepositoryBase<TEntity, TEntityId> : ReadRepository<TEntity, TEntityId>, IWriteRepository<TEntity, TEntityId> 
	where TEntity : EntityIdentifier<TEntityId>, new()
{
	private readonly AudioDbContext _context;

	public RepositoryBase(AudioDbContext context) : base(context)
	{
		_context = context;
	}

	public virtual async Task<Result<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
		_context.SaveChanges();
		
		return entity;
	}

	public virtual async Task<Result<List<TEntity>>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
		_context.SaveChanges();
		
		return Result<List<TEntity>>.Success(entities);
	}

	public virtual async Task<Result<TEntity>> UpdateAsync(EntityIdentifier<TEntityId> id, TEntity entity, CancellationToken cancellationToken = default)
	{
		entity.Id = id.Id;
		var result = _context.Set<TEntity>().Update(entity);
		
		await _context.SaveChangesAsync(cancellationToken);

		return entity;
	}

	public async Task<Result<bool>> DeleteAsync(EntityIdentifier<TEntityId> id, CancellationToken cancellationToken = default)
	{
		var track = _context.Attach(new TEntity() { Id = id.Id });
		track.State = EntityState.Deleted;
		var result = await _context.SaveChangesAsync(cancellationToken);

		return result == 0;
	}
}