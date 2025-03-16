namespace Audio.Persistence.Entities;

public class SongEntity : EntityIdentifier<Guid>
{
	private SongEntity() { }
	
	public SongEntity(string title, string? lyrics, Guid albumId, Guid artistId)
	{
		Title = title;
		Lyrics = lyrics;
		AlbumId = albumId;
		ArtistId = artistId;
	}

	public string Title { get; set; }
	public string? Lyrics { get; set; }
	public Guid AlbumId { get; set; }
	public virtual AlbumEntity Album { get; set; }
	public Guid ArtistId { get; set; }
	public virtual ArtistEntity Artist { get; set; }
}