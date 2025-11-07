using Microsoft.Extensions.Options;
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Application.Security;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Application.Services;

public class AuthServices : IAuthService
{
     private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly int _tokenExpiryMinutes;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _tokenExpiryMinutes = jwtSettings.Value.ExpiresMinutes;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existing = await _userRepository.GetByUsernameAsync(request.Usernam);
            if (existing != null)
                throw new InvalidOperationException("El username ya existe.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _userRepository.AddUser(user);

            var token = _jwtService.GenerateToken(user);
            return new AuthResponseDto{ Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(_tokenExpiryMinutes) };
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse { Token = token, ExpiresAt = DateTime.UtcNow.AddMinutes(_tokenExpiryMinutes) };
        }
}