using AudioFileService.Models;
using AudioFileService.Models.Dtos.SongUploadControllerModels;
using Result;

namespace AudioFileService.Interfaces;

public interface ISongFileStorageService
{
    Task<Result<SongFile>> StoreSongFile(SongFileUploadRequestDto songFile);
}