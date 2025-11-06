using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Domain.Interfaces;

public interface IStudentRepository
{
    public Task<IEnumerable<Student>> GetAllStudents(); // Get All Students
    public Task<Student> GetStudentById(int id); // Get Student By ID
    public Task<Student> AddStudent(Student student); // Add Student
    public Task<Student> UpdateStudent(Student student); // Update Student
    public Task<Student> DeleteStudent(int id); // Delete Student
}