
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Exceptions;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserRegisterDto registerDto)
        {
            // 1. Validar si el email ya existe
            //en esta linea no funciona el GetUserByEmailAsync, creo que no existe
            var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new ValidationException("El email ya está registrado");
            }

            // 2. Parsear el Rol (string a enum)
            if (!Enum.TryParse<UserRole>(registerDto.Role, true, out var userRole))
            {
                userRole = UserRole.User; // Rol por defecto si es inválido
            }

            // 3. Hashear la contraseña
            //El BCryp no se porque no funciona
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                //PasswordHash tambien tiene un error
                PasswordHash = passwordHash, // Usamos el campo corregido
                Role = userRole
            };

            var newUser = await _userRepository.AddUser(user);

            // 4. Crear un DTO de login para generar el token
            var loginDto = new UserLoginDto { Email = registerDto.Email, Password = registerDto.Password };
            return await LoginAsync(loginDto);
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            // 4. Validar usuario y contraseña
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new ValidationException("Credenciales inválidas");
            }

            // 5. Generar Token JWT
            var token = GenerateJwtToken(user);
            var userDto = MapUserToDto(user);

            return new AuthResponseDto { Token = token, User = userDto };
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }
            return MapUserToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllUser();
            return users.Select(MapUserToDto).ToList();
        }

        public async Task UpdateAsync(int id, UserDto userDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            
            if (Enum.TryParse<UserRole>(userDto.Role, true, out var userRole))
            {
                user.Role = userRole;
            }

            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteAsync(int id)
        {
            var deletedUser = await _userRepository.DeleteUser(id);
            if (deletedUser == null)
            {
                throw new NotFoundException("Usuario no encontrado para eliminar");
            }
        }

        // --- Helpers de Mapeo y Token ---

        private UserDto MapUserToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString() // Convertir enum a string
            };
        }

        private string GenerateJwtToken(User user)
        {
            // Leer la configuración desde appsettings.json
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] 
                ?? throw new InvalidOperationException("Configuración JWT 'Key' no encontrada")));
            
            var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("Configuración JWT 'Issuer' no encontrada");
            var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("Configuración JWT 'Audience' no encontrada");

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()) // Rol
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(8), // Duración del token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}