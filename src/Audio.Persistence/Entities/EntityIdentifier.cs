using System.Diagnostics.CodeAnalysis;

namespace Audio.Persistence.Entities;

public class EntityIdentifier<T>
{
	[NotNull]
	public T Id { get; set; }
}