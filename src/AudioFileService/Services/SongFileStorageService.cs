using AudioFileService.Exceptions;
using AudioFileService.Interfaces;
using AudioFileService.Models;
using AudioFileService.Models.Dtos.SongUploadControllerModels;
using Result;

namespace AudioFileService.Services;

public class SongFileStorageService : ISongFileStorageService
{
    private readonly MinioService _minioService;
    private readonly ISongFileRepository _songFileRepository;

    public SongFileStorageService(ISongFileRepository songFileRepository, MinioService minioService)
    {
        _songFileRepository = songFileRepository;
        _minioService = minioService;
    }


    public async Task<Result<SongFile>> StoreSongFile(SongFileUploadRequestDto songFile)
    {
        var fileExtension = Path.GetExtension(songFile.File.FileName).ToLower();
        if (fileExtension != ".flac" && fileExtension != ".wav" && fileExtension != ".mp3" && fileExtension != ".alac")
            return Result<SongFile>.Fail(new NotAllowedSongFileExtension());

        var objectName = $"{Guid.NewGuid()}{fileExtension}";
        await using var stream = songFile.File.OpenReadStream();
        await _minioService.UploadFileAsync(objectName, stream, songFile.File.ContentType);

        var filePath = objectName;

        var musicFile = new SongFile
        {
            Title = songFile.Title,
            Artist = songFile.Artist,
            Album = songFile.Album,
            FilePath = filePath,
            Size = songFile.File.Length,
            Format = fileExtension
        };

        await _songFileRepository.CreateAsync(musicFile);
        return musicFile;
    }
}