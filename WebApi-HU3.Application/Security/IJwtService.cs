using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Application.Security;

public interface IJwtService
{
    string GenerateToken(User user);
}