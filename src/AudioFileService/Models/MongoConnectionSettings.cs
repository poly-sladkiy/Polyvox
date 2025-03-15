namespace AudioFileService.Models;

public class MongoConnectionSettings
{
    public const string Position = nameof(MongoConnectionSettings);

    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SongCollectionName { get; set; } = null!;
}