using System.Collections.ObjectModel;

namespace Audio.Core.Models;

public class Artist
{
	public string Name { get; private set; }
	public string[] Genres { get; private set; }
	
	private List<Album> _albums;

	public ReadOnlyCollection<Album> Albums
	{
		get => _albums.AsReadOnly(); 
		private set => _albums = _albums.ToList();
	}

	public Artist(string name, string[] genres, List<Album> albums)
	{
		Name = name;
		Genres = genres;
		_albums = albums;
	}
}