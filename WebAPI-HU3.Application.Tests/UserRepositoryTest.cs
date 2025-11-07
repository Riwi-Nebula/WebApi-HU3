using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Infraestructure.Data;

namespace WebAPI_HU3.Application.Tests;

public class UserRepositoryTest
{
    private AppDbContext GetDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("UpdateDb_" + Guid.NewGuid())
            .Options;
        return new AppDbContext(options);
    }
    
    [Fact]
    public async Task Should_Update_User_Email()
    {
        // Arrange
        var db = GetDb();
        var user = new User { Username = "ana", Email = "old@email.com" };
        db.Users.Add(user);
        await db.SaveChangesAsync();

        // Act
        user.Email = "new@email.com";
        db.Users.Update(user);
        await db.SaveChangesAsync();

        // Assert
        var updated = await db.Users.FirstOrDefaultAsync(u => u.Username == "ana");
        Assert.Equal("new@email.com", updated.Email);
    }
}