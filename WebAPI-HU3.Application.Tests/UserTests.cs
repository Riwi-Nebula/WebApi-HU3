using Microsoft.AspNetCore.Identity;
using WebApi_HU3.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Should_Hash_And_Verify_Password()
    {
        var hasher = new PasswordHasher<User>();
        var user = new User { Username = "testuser" };
        var password = "Contraseña";

        var hashed = hasher.HashPassword(user, password);
        var result = hasher.VerifyHashedPassword(user, hashed, password);
        
        Assert.Equal(PasswordVerificationResult.Success, result);
    }
}