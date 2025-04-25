using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AccountSystem.WebApi.Services;

public class PasswordHasher : IPasswordHasher
{
	private const int SaltSize = 16;
	private const int HashSize = 32;
	private const int HashIterations = 100_000;
	
	private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
	
	public string HashPassword(string password)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, Algorithm, HashSize);

		return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
	}

	public bool VerifyPassword(string hashedPassword, string password)
	{
		string[] parts = hashedPassword.Split('-');

		byte[] hash = Convert.FromHexString(parts[0]);
		byte[] salt = Convert.FromHexString(parts[1]);
		
		byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashIterations, Algorithm, HashSize);
		
		return CryptographicOperations.FixedTimeEquals(hash, inputHash);
	}
}