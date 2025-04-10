﻿using AudioFileService.Interfaces;
using AudioFileService.Models;
using AudioFileService.Models.Dtos.SongUploadControllerModels;
using Microsoft.AspNetCore.Mvc;

namespace AudioFileService.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class SongFileStorageController : ControllerBase
{
    private readonly ISongFileRepository _songFileRepository;
    private readonly ISongFileStorageService _songFileStorageService;

    public SongFileStorageController(ISongFileRepository songFileRepository,
        ISongFileStorageService songFileStorageService)
    {
        _songFileRepository = songFileRepository;
        _songFileStorageService = songFileStorageService;
    }

    /// <summary>
    ///     Получить файлы аудиозаписей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<SongFile>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songFileRepository.GetAsync();

        return Ok(songs);
    }

    /// <summary>
    ///     Загрузить файл
    /// </summary>
    /// <param name="songFile">Аудио файл (Only FLAC, WAV, ALAC, and MP3) с информацией о нем</param>
    /// <returns></returns>
    [HttpPost("upload")]
    [ProducesResponseType<SongFileUploadResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile([FromForm] SongFileUploadRequestDto songFile)
    {
        if (songFile.File.Length == 0)
            return BadRequest("No file uploaded.");

        var musicFileResult = await _songFileStorageService.StoreSongFile(songFile);

        if (musicFileResult.IsSuccess)
            return Ok(new SongFileUploadResponseDto(
                musicFileResult.Value.Id,
                musicFileResult.Value.Title,
                musicFileResult.Value.FilePath));

        return ValidationProblem(new ValidationProblemDetails
        {
            Detail = musicFileResult.Error.Message
        });
    }
}