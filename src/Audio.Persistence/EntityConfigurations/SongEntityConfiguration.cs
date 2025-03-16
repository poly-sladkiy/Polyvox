using Audio.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audio.Persistence.EntityConfigurations;

public class SongEntityConfiguration : IEntityTypeConfiguration<SongEntity>
{
	public void Configure(EntityTypeBuilder<SongEntity> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		
		builder.Property(x => x.Title)
			.IsRequired()
			.HasMaxLength(250);
		
		builder.Property(x => x.AlbumId).IsRequired();
		builder.HasOne(x => x.Album)
			.WithMany(x => x.Songs)
			.HasForeignKey(x => x.AlbumId);
		
		builder.Property(x => x.ArtistId).IsRequired();
		builder.HasOne(x => x.Artist)
			.WithMany(x => x.Songs)
			.HasForeignKey(x => x.AlbumId);
	}
}