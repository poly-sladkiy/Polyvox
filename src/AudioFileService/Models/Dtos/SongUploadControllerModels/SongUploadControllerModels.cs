namespace AudioFileService.Models.Dtos.SongUploadControllerModels;

public record SongFileUploadDto
{
    public required string Title { get; set; }
    public required string Artist { get; set; }
    public required string Album { get; set; }
}