using IdentityProvider.Api.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Api.Infrastructure;

public class FakeAuthService(IPasswordHasher<UserIdentity> passwordHasher) : IAuthService
{
    public Task<AuthenticationResult> AuthorizeAsync(string username, string password)
    {
        var identity = new UserIdentity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Smith",
            UserName = username,
            Email = "john@domain.com",
            Roles = ["director", "developer"]
        };

        // Symulacja (haslo powinno byc zahaszowane przy zapisie do bazy danych)
        var hashedPassword = passwordHasher.HashPassword(identity, "123");       
        
        if (username == "john" 
            && passwordHasher.VerifyHashedPassword(identity, hashedPassword, password)== PasswordVerificationResult.Success)
        {
            return Task.FromResult(new AuthenticationResult(true, identity));
        }

        return Task.FromResult(new AuthenticationResult(false));
    }
}
