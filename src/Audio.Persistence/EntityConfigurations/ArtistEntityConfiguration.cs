using Audio.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audio.Persistence.EntityConfigurations;

public class ArtistEntityConfiguration : IEntityTypeConfiguration<ArtistEntity>
{
	public void Configure(EntityTypeBuilder<ArtistEntity> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id).ValueGeneratedOnAdd();
		
		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(250);
		
		builder.Property(x => x.Genres).IsRequired()
			.HasConversion<string>(
				(genresList) => string.Join(";", genresList),
				(genresString) => genresString.Split(";", StringSplitOptions.RemoveEmptyEntries));
		
		// builder.Property(x => x.Albums).IsRequired();
		// builder.HasMany(x => x.Albums)
		// 	.WithOne()
		// 	.HasForeignKey(x => x.ArtistId);
	}
}