namespace IdentityProvider.Api.Abstractions;

public interface IAuthService
{
    Task<AuthenticationResult> AuthorizeAsync(string username, string password);
}

public record AuthenticationResult(bool Successful, UserIdentity Identity = null);

public class UserIdentity
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string[] Roles { get; set; }
}
