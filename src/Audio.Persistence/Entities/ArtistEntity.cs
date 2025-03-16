namespace Audio.Persistence.Entities;

public class ArtistEntity : EntityIdentifier<Guid>
{
	private ArtistEntity() { }
	
	public ArtistEntity(string name, string[] genres)
	{
		Name = name;
		Genres = genres;
	}

	public string Name { get; set; }
	public string[] Genres { get; set; }
	public virtual List<AlbumEntity> Albums { get; set; }
	public virtual List<SongEntity> Songs { get; set; }
}