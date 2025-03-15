using MongoDB.Bson.Serialization.Attributes;

namespace AudioFileService.Models;

[BsonIgnoreExtraElements]
public class SongFile : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long Size { get; set; }
    public string Format { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}