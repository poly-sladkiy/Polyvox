using AccountSystem.WebApi.Models;

namespace AccountSystem.WebApi.Services;

public interface IPasswordHasher
{
	string HashPassword(string password);
	bool VerifyPassword(string hashedPassword, string password);
}