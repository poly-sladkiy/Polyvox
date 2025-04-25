using System.ComponentModel.DataAnnotations;
using AccountSystem.WebApi.Models;
using Cassandra;
using Result;

namespace AccountSystem.WebApi.Services;

/// <summary>
/// Account management
/// </summary>
public class AccountService : IAccountService
{
	private readonly IEmailValidationService _emailValidationService;
	private readonly IPasswordHasher _passwordHasher;
	private readonly Cassandra.ISession _cassandraSession;

	public AccountService(IEmailValidationService emailValidationService, Cassandra.ISession cassandraSession, IPasswordHasher passwordHasher)
	{
		_emailValidationService = emailValidationService;
		_cassandraSession = cassandraSession;
		_passwordHasher = passwordHasher;
	}

	public async Task<Result<Account>> Create(string? email, string? userName, string? password)
	{
		if (email == null || userName == null || password == null)
			return Result<Account>.Fail(
				new ValidationException(
					$"Value cannot be null: {nameof(email)}, {nameof(userName)}, {nameof(password)}"));

		if (_emailValidationService.IsEmailValid(email) is false)
			return Result<Account>.Fail(new ValidationException($"Email is not valid: {email}"));

		var accountId = Guid.NewGuid();
		var normalizedEmail = _emailValidationService.NormalizeEmail(email);
		var hashPassword = _passwordHasher.HashPassword(password);
		
		var account = new Account(accountId, normalizedEmail, userName, "user");

		var createAccountStatement = _cassandraSession.Prepare(
			"""
			INSERT INTO account_system.accounts
			(account_id, user_name, email, "timestamp")
			VALUES(:account_id, :user_name, :email, :timestamp);
			""");
		var boundedAccountStatement = createAccountStatement.Bind(
			new
			{
				account_id = account.AcountId,
				user_name = account.UserName,
				email = account.Email,
				timestamp = account.CreatedAt
			});

		var cqlCreateAccountByEmail = _cassandraSession.Prepare(
				"""
				INSERT INTO account_system.accounts_by_email
				(email, account_id, user_name, password_hash)
				VALUES(:email, :account_id, :user_name, :password_hash);
				""");
		var boundedCreateAccountByEmail = cqlCreateAccountByEmail.Bind(
			new
			{
				email = account.Email,
				account_id = account.AcountId,
				user_name = account.UserName,
				password_hash = hashPassword,
			});

		var batch = new BatchStatement().SetBatchType(BatchType.Logged)
			.Add(boundedAccountStatement)
			.Add(boundedCreateAccountByEmail);
		
		var res  = await _cassandraSession.ExecuteAsync(batch);
		
		return Result<Account>.Success(account);
	}
}