using Audio.Persistence.Entities;

namespace Audio.Persistence.Exceptions;

public class EntityNotFoundException<TEntity, TEntityId>(EntityIdentifier<TEntityId> entityId)
	: Exception($"Entity {typeof(TEntity).Name} with Id {entityId.Id} not found.");