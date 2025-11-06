using WebApi_HU3.Infraestructure.Data;

namespace WebApi_HU3.Infraestructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdStudentAsync(int id)
    {
        await _context.Students.FindAsync(id);
    }

    public async Task<IEnumerable<Student>> GetAllStudentAsync()
    {
        await _context.Students.ToListAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}