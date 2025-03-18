using Audio.Persistence;
using Audio.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AudioFileService.Controllers.DatabaseControllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ArtistController : ControllerBase
{
	private readonly AudioDbContext _context;

	public ArtistController(AudioDbContext context)
	{
		_context = context;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetArtists()
	{
		return Ok(await _context.Artists.ToListAsync());
	}
	
	[HttpPost]
	public async Task<IActionResult> CreateArtistsAsync(ArtistDto artistDto)
	{
		var artist = new ArtistEntity(artistDto.Name, artistDto.Genres);
		
		await _context.Artists.AddAsync(artist);
		_context.SaveChanges();
		return Ok(artist);
	}
}

public record ArtistDto(string Name, string[] Genres);