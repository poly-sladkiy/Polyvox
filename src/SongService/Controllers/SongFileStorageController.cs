using Microsoft.AspNetCore.Mvc;
using SongService.Interfaces;
using SongService.Models;
using SongService.Models.Dtos.SongUploadControllerModels;
using SongService.Services;

namespace SongService.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class SongFileStorageController : ControllerBase
{
    private readonly MinioService _minioService;
    private readonly ISongFileRepository _songFileRepository;

    public SongFileStorageController(MinioService minioService, ISongFileRepository songFileRepository)
    {
        _minioService = minioService;
        _songFileRepository = songFileRepository;
    }
    
    /// <summary>
    /// Получить файлы аудиозаписей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<List<SongFile>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songFileRepository.GetAsync();

        return Ok(songs);
    }

    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="file">Аудио файл (Only FLAC, WAV, ALAC, and MP3)</param>
    /// <param name="songFileInfo">Информация об исполнителе и песне</param>
    /// <returns></returns>
    [HttpPost("upload")]
    [ProducesResponseType<List<SongFile>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadFile(IFormFile? file, [FromQuery] SongFileUploadDto songFileInfo)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (fileExtension != ".flac" && fileExtension != ".wav" && fileExtension != ".mp3" && fileExtension != ".alac")
            return BadRequest("Only FLAC, WAV, ALAC, and MP3 files are allowed.");

        var objectName = $"{Guid.NewGuid()}{fileExtension}";
        await using var stream = file.OpenReadStream();
        await _minioService.UploadFileAsync(objectName, stream, file.ContentType);

        var filePath = objectName; 
        
        var musicFile = new SongFile()
        {
            Title = songFileInfo.Title,
            Artist = songFileInfo.Artist,
            Album = songFileInfo.Album,
            FilePath = filePath,
            Size = file.Length,
            Format = fileExtension
        };

        await _songFileRepository.CreateAsync(musicFile);

        return Ok(new { musicFile.Id, musicFile.Title, musicFile.FilePath });
    }
}