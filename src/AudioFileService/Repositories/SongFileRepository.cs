using AudioFileService.Interfaces;
using AudioFileService.Models;
using AudioFileService.Services;
using Microsoft.Extensions.Options;

namespace AudioFileService.Repositories;

public class SongFileRepository : BaseRepository<SongFile>, ISongFileRepository
{
    public SongFileRepository(MongoConnectionService connectionService, IOptions<MongoConnectionSettings> settings)
        : base(connectionService, settings.Value.SongCollectionName)
    {
    }
}