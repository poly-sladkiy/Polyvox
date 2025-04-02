using System.Collections.ObjectModel;

namespace Audio.Core.Models;

public class Album
{
	public string Name { get; private set; }

	private List<Song> _songs;

	public ReadOnlyCollection<Song> Songs
	{
		get => _songs.AsReadOnly();
		private set => _songs = value.ToList();
	}

	public DateOnly ReleaseDate { get; private set; }

	public Album(string name, DateOnly releaseDate)
	{
		Name = name;
		ReleaseDate = releaseDate;
	}
}