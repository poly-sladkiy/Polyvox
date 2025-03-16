using Audio.Persistence.Entities;
using Audio.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;
using Result;

namespace Audio.Persistence.Repositories;

public class ReadRepository<TEntity, TEntityId> : IReadRepository<TEntity, TEntityId> where TEntity : EntityIdentifier<TEntityId>
{
	private readonly AudioDbContext _context;

	public ReadRepository(AudioDbContext context)
	{
		_context = context;
	}

	public virtual async Task<Result<List<TEntity>>> GetAllAsync()
	{
		var entities = await _context.Set<TEntity>().ToListAsync();
		return Result<List<TEntity>>.Success(entities);
	}

	public virtual async Task<Result<TEntity>> GetById(EntityIdentifier<TEntityId> id)
	{
		var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(_ => _.Id.Equals(id));
		return entity ?? Result<TEntity>.Fail(new EntityNotFoundException<TEntity, TEntityId>(id));
	}
}