using Result;
using SongService.Models;
using SongService.Models.Dtos.SongUploadControllerModels;

namespace SongService.Interfaces;

public interface ISongFileStorageService
{
    Task<Result<SongFile>> StoreSongFile(SongFileUploadRequestDto songFile);
}