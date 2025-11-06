// Ruta: WebApi-HU3.Application/Services/StudentService.cs
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Application.Exceptions; // Crearemos esta excepción
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> CreateAsync(StudentCreateDto createDto)
        {
            // 1. Mapear DTO a Entidad
            var student = new Student
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email
            };

            // 2. Usar repositorio
            var newStudent = await _studentRepository.AddStudent(student);

            // 3. Mapear Entidad a DTO para respuesta
            return MapStudentToDto(newStudent);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            
            if (student == null)
            {
                throw new NotFoundException("Estudiante no encontrado");
            }

            return MapStudentToDto(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllStudents();
            return students.Select(MapStudentToDto).ToList();
        }

        public async Task UpdateAsync(int id, StudentUpdateDto updateDto)
        {
            var student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                throw new NotFoundException("Estudiante no encontrado");
            }

            // Actualizar propiedades
            student.FirstName = updateDto.FirstName;
            student.LastName = updateDto.LastName;
            student.Email = updateDto.Email;

            await _studentRepository.UpdateStudent(student);
        }

        public async Task DeleteAsync(int id)
        {
            var deletedStudent = await _studentRepository.DeleteStudent(id);
            if (deletedStudent == null)
            {
                throw new NotFoundException("Estudiante no encontrado para eliminar");
            }
        }

        // --- Helper de Mapeo ---
        private StudentDto MapStudentToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };
        }
    }
}