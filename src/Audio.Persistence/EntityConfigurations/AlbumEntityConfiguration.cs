using Audio.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audio.Persistence.EntityConfigurations;

public class AlbumEntityConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
	public void Configure(EntityTypeBuilder<AlbumEntity> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		
		builder.Property(x => x.Title).IsRequired();

		builder.Property(x => x.ArtistId).IsRequired();
		builder.HasOne<ArtistEntity>()
			.WithMany(x => x.Albums)
			.HasForeignKey(x => x.ArtistId);
		
		// builder.Property(x => x.Songs).IsRequired();
		// builder.HasMany(x => x.Songs)
		// 	.WithOne()
		// 	.HasForeignKey(x => x.AlbumId);
	}
}