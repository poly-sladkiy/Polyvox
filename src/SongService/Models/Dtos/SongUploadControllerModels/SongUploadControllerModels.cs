namespace SongService.Models.Dtos.SongUploadControllerModels;

public class SongFileUploadDto
{
    public required string Title { get; set; }
    public required string Artist { get; set; }
    public required string Album { get; set; }
}