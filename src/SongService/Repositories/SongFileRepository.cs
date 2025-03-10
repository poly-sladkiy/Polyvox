using Microsoft.Extensions.Options;
using SongService.Interfaces;
using SongService.Models;
using SongService.Services;

namespace SongService.Repositories;

public class SongFileRepository : BaseRepository<SongFile>, ISongFileRepository
{
    public SongFileRepository(MongoConnectionService connectionService, IOptions<MongoConnectionSettings> settings)
        : base(connectionService, settings.Value.SongCollectionName)
    {
    }
}