using System.ComponentModel.DataAnnotations;
using AccountSystem.WebApi.Models;
using AccountSystem.WebApi.Models.Controllers;
using AccountSystem.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountSystem.WebApi.Controllers;

/// <summary>
/// Account management controller
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	/// <summary>
	/// Create new user account
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost]
	[ProducesResponseType<Account>(StatusCodes.Status200OK)]
	[ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
	public IActionResult Create(CreateAccountDtoRequest request)
	{
		var accountCreationResult = _accountService.Create(request.Email, request.UserName, request.Password);
		if (accountCreationResult.IsSuccess)
			return Ok(accountCreationResult.Value);

		var errorDetails = new ValidationProblemDetails() { Detail = accountCreationResult.Error.Message } ;
		
		return BadRequest(errorDetails);
	}
}