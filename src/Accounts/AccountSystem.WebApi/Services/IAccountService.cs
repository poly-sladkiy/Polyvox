using AccountSystem.WebApi.Models;
using Result;

namespace AccountSystem.WebApi.Services;

public interface IAccountService
{
	Result<Account> Create(string? email, string? userName, string? password);
}