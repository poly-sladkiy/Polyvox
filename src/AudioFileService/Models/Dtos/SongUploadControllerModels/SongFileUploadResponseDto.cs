namespace AudioFileService.Models.Dtos.SongUploadControllerModels;

public record SongFileUploadResponseDto(
    string Id,
    string Title,
    string FilePath);