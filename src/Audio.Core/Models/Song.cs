namespace Audio.Core.Models;

public class Song
{
	public string Title { get; private set; }
	public string[] Genres { get; private set; }
	public string? Lyrics { get; private set; }

	public Song(string title, string[] genres, string? lyrics = null)
	{
		Title = title;
		Genres = genres;
		Lyrics = lyrics;
	}
}