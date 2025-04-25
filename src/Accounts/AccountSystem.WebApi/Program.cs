using System.Net;
using System.Reflection;
using AccountSystem.WebApi.Services;
using Cassandra;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmailValidationService, EmailValidationService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSingleton<Cluster>((_) => Cluster.Builder()
	.AddContactPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9042))
	.WithCredentials("cassandra", "cassandra")
	.Build()
);
builder.Services.AddScoped<Cassandra.ISession>((serviceProvider) => serviceProvider.GetService<Cluster>()!.Connect());

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Accounts API",
		Description = "An Web API for managing user`s Accounts",
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