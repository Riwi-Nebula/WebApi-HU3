using WebApi_HU3.Application.DTOs;

namespace WebApi_HU3.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> CreateAsync(StudentCreateDto createDto);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, StudentUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}