using System.Reflection;
using Audio.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Audio.Information.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string dbConnection = builder.Configuration.GetConnectionString("AudioDatabase")!;
builder.Services.AddDbContext<AudioDbContext>(options => options.UseNpgsql(dbConnection));

builder.Services
	.AddControllers()
	.AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Audio Information API",
		Description = "An Web API for managing song files",
		Contact = new OpenApiContact
		{
			Name = "Poly"
		}
	});

	// using System.Reflection;
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}).AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AudioDbContext>();
	var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	try
	{
		db.Database.Migrate();
		logger.LogInformation("Database successfully migrated");
	}
	catch (Exception e)
	{
		logger.LogCritical(e, "An error occurred while seeding the database.");
		throw;
	}
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapArtistEntityEndpoints();

app.Run();
