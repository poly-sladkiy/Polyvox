using System.Reflection;
using Audio.Persistence;
using AudioFileService.Interfaces;
using AudioFileService.Models;
using AudioFileService.Repositories;
using AudioFileService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Minio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("AudioDatabase")!;
builder.Services.AddDbContext<AudioDbContext>(options => options.UseNpgsql(connection));

builder.Services.Configure<MongoConnectionSettings>(
    builder.Configuration.GetSection(MongoConnectionSettings.Position));

builder.Services.AddSingleton<MinioService>();
builder.Services.AddSingleton<MongoConnectionService>();

builder.Services.AddScoped<ISongFileRepository, SongFileRepository>();
builder.Services.AddScoped<ISongFileStorageService, SongFileStorageService>();

var minioSection = builder.Configuration.GetSection("Minio");

var minioEndpoint = minioSection.GetValue<string>("Endpoint");
var minioAccessKey = minioSection.GetValue<string>("AccessKey")!;
var minioSecretKey = minioSection.GetValue<string>("SecretKey")!;

// Add Minio using the custom endpoint and configure additional settings for default MinioClient initialization
builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(minioEndpoint)
    .WithCredentials(minioAccessKey, minioSecretKey)
    .WithSSL(false)
    .Build());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SongFile API",
        Description = "An Web API for managing song files",
        Contact = new OpenApiContact
        {
            Name = "Poly"
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AudioDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        db.Database.Migrate();
        logger.LogInformation("Migrated DB");
    }
    catch (Exception e)
    {
        logger.LogCritical(e, "An error occurred while seeding the database.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();