using Minio;
using SongService.Interfaces;
using SongService.Models;
using SongService.Repositories;
using SongService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<MongoConnectionSettings>(
    builder.Configuration.GetSection(MongoConnectionSettings.Position));

builder.Services.AddSingleton<MinioService>();
builder.Services.AddSingleton<MongoConnectionService>();

builder.Services.AddScoped<ISongFileRepository, SongFileRepository>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

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