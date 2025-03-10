using Microsoft.AspNetCore.Mvc;
using SongService.Interfaces;
using SongService.Services;

namespace SongService.Controllers;

[Route("/api/[controller]/[action]")]
[ApiController]
public class SongStreamController : ControllerBase
{
    private readonly MinioService _minioService;
    private readonly ISongFileRepository _songFileRepository;

    public SongStreamController(MinioService minioService, ISongFileRepository songFileRepository)
    {
        _minioService = minioService;
        _songFileRepository = songFileRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> StreamAudio(string id)
    {
        var musicFile = await _songFileRepository.GetAsync(id);
        if (musicFile == null) return NotFound("Audio file not found.");

        var stream = await _minioService.GetFileStreamAsync(musicFile.FilePath);
        if (stream.Length == 0) return NotFound("Could not retrieve audio file.");

        var fileName = $"{musicFile.Artist} - {musicFile.Title}{musicFile.Format}";
        var contentType = GetContentType(musicFile.Format);

        return File(stream, contentType, fileName, true);
    }

    private string GetContentType(string format)
    {
        return format switch
        {
            ".mp3" => "audio/mpeg",
            ".wav" => "audio/wav",
            ".flac" => "audio/flac",
            _ => "application/octet-stream"
        };
    }
}