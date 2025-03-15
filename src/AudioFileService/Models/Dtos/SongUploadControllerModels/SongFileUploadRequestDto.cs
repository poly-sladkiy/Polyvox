namespace AudioFileService.Models.Dtos.SongUploadControllerModels;

/// <summary>
///     Модель информации об аудио записи
/// </summary>
public record SongFileUploadRequestDto
{
    public required IFormFile File { get; set; }

    /// <summary>
    ///     Название трека
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Имя исполнителя/ей
    /// </summary>
    public required string Artist { get; set; }

    /// <summary>
    ///     Название альбома
    /// </summary>
    public required string Album { get; set; }

    /// <summary>
    ///     Дополнительная информация об аудио, пример.: "year=2018;genre=alternative"
    /// </summary>
    public string? AdditionalData { get; set; }
}