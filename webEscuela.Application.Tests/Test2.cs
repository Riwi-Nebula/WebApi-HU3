using Microsoft.Extensions.Options;
using Moq;
using WebApi_HU3.Application.Security;
using WebApi_HU3.Application.Services;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace webEscuela.Application.Tests;

public class Test2
{
    [Fact]
    public async Task Register_Throws_WhenUsernameExists()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var existing = new User { Id = Guid.NewGuid(), Username = "maria" };
        mockRepo.Setup(r => r.GetByUsernameAsync("maria")).ReturnsAsync(existing);

        var jwtSettings = new JwtSettings
        {
            Secret = "ClaveDePruebaMuyLarga1234567890",
            Issuer = "test",
            Audience = "test",
            ExpiresMinutes = 60
        };

        var passwordHasher = new PasswordHasher<User>();
        var jwtService = new JwtService(Options.Create(jwtSettings));
        var authService = new AuthServices(mockRepo.Object, jwtService, passwordHasher, Options.Create(jwtSettings));

        var request = new RegisterRequest { Username = "maria", Email = "maria@example.com", Password = "x" };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => authService.RegisterAsync(request));
    }
}
