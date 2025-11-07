using Microsoft.Extensions.Configuration;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi_HU3.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string?> Authenticate(string email, string password)
    {
        // Obtener usuario por email
        var users = await _userRepository.GetAllUser();
        var user = users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return null;

        // Validar password con hash
        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordValid)
            return null;

        //Generar token claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Create Token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: cred
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}