using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;
using WebApi_HU3.Infraestructure.Data;

namespace WebApi_HU3.Infraestructure.Repositories;

//inyeccion de contexto base de datos 'DbContext'
public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    
    
    //se obtiene lista completa de los estudiantes registrados
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    //se obtiene lista completa de los estudiantes registrados
    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        return await _context.Students.ToListAsync();
    }
    
    //se obtiene un estudiante por id
    public async Task<Student?> GetStudentById(int id)
    {
        return await _context.Students.FindAsync(id);
    }
    
    //se agregar un nuevo estudiante
    public async Task<Student> AddStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    //actualiza informacion de un estudiante existente
    public async Task<Student> UpdateStudent(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }

    //elimina un estudiante por id
    public async Task<Student> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return student;
    }
}