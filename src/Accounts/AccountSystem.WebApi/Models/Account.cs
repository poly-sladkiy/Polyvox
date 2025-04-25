namespace AccountSystem.WebApi.Models;

/// <summary>
/// Acount model
/// </summary>
public class Account
{
	public Guid AcountId { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public string Role { get; set; }

	public Account(Guid accountId, string userName, string email, string role)
	{
		AcountId = accountId;
		UserName = userName;
		Email = email;
		CreatedAt = DateTimeOffset.Now;
		Role = role;
	}
}