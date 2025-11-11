using WebApi_HU3.Application.DTOs;

namespace WebApi_HU3.Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto> RegisterAsync(UserRegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, UserDto userDto); // Usamos UserDto para actualizar
        Task DeleteAsync(int id);
    }
}