using AccountSystem.WebApi.Services;

namespace AccountSystem.WebApi.Services;

/// <summary>
/// Email verification and normalization
/// </summary>
public class EmailValidationService : IEmailValidationService
{
	/// <summary>
	/// Trim and lowercase email
	/// </summary>
	/// <param name="email"></param>
	/// <returns></returns>
	public string NormalizeEmail(string email) => email.Trim().ToLower();

	/// <summary>
	/// Validate email format
	/// </summary>
	/// <param name="email"></param>
	/// <returns></returns>
	public bool IsEmailValid(string email)
	{
		try
		{
			var emailAddress = new System.Net.Mail.MailAddress(email);
			return true;
		}
		catch { return false; }
	}
}