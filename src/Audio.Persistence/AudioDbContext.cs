using Audio.Persistence.Entities;
using Audio.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Audio.Persistence;

public class AudioDbContext : DbContext
{
	public DbSet<ArtistEntity> Artists { get; set; }
	public DbSet<AlbumEntity> Albums { get; set; }
	public DbSet<SongEntity> Songs { get; set; }

	public AudioDbContext(DbContextOptions<AudioDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ArtistEntityConfiguration());
		modelBuilder.ApplyConfiguration(new AlbumEntityConfiguration());
		modelBuilder.ApplyConfiguration(new SongEntityConfiguration());
	}
}