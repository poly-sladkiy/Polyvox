namespace AccountSystem.WebApi.Services;

public interface IEmailValidationService
{
	/// <summary>
	/// Trim and lowercase email
	/// </summary>
	/// <param name="email"></param>
	/// <returns></returns>
	string NormalizeEmail(string email);

	/// <summary>
	/// Validate email format
	/// </summary>
	/// <param name="email"></param>
	/// <returns></returns>
	bool IsEmailValid(string email);
}