namespace Audio.Persistence.Entities;

public class AlbumEntity : EntityIdentifier<Guid>
{
	private AlbumEntity() { }
	
	public AlbumEntity(string title)
	{
		Title = title;
	}

	public Guid ArtistId { get; set; }
	public string Title { get; set; }
	public DateOnly ReleaseDate { get; set; }
	public virtual List<SongEntity> Songs { get; set; }
}