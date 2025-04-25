using System.ComponentModel.DataAnnotations;

namespace AccountSystem.WebApi.Models.Controllers;

/// <summary>
/// Create new account 
/// </summary>
/// <param name="Email"></param>
/// <param name="UserName"></param>
/// <param name="Password"></param>
public record CreateAccountDtoRequest([EmailAddress] string Email, [Required] string UserName, [Required] string Password);
