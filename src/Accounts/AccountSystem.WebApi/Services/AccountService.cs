using System.ComponentModel.DataAnnotations;
using AccountSystem.WebApi.Models;
using Result;

namespace AccountSystem.WebApi.Services;

/// <summary>
/// Account management
/// </summary>
public class AccountService : IAccountService
{
	private readonly IEmailValidationService _emailValidationService;

	public AccountService(IEmailValidationService emailValidationService)
	{
		_emailValidationService = emailValidationService;
	}

	public Result<Account> Create(string? email, string? userName, string? password)
	{
		if (email == null || userName == null || password == null)
			return Result<Account>.Fail(new ValidationException($"Value cannot be null: {nameof(email)}, {nameof(userName)}, {nameof(password)}"));
		
		if (_emailValidationService.IsEmailValid(email) is false)
			return Result<Account>.Fail(new ValidationException($"Email is not valid: {email}"));
		
		var accountId = Guid.NewGuid();
		var normalizedEmail = _emailValidationService.NormalizeEmail(email);
		var account = new Account(accountId, normalizedEmail, userName, "user");
		
		return Result<Account>.Success(account);
	}
}