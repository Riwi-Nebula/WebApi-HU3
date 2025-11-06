using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Infraestructure.Repositories;

public class StudentRepository : IStudentRepository
{
    //Implementer Logia y Connection con la Base de datos
    
    public Task<IEnumerable<Student>> GetAllStudents()
    {
        throw new NotImplementedException();
    }

    public Task<Student> GetStudentById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> AddStudent(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> UpdateStudent(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> DeleteStudent(int id)
    {
        throw new NotImplementedException();
    }
}