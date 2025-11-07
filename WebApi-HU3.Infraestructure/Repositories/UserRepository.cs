using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;
using WebApi_HU3.Infraestructure.Data;

namespace WebApi_HU3.Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    //inyeccion de contexto base de datos 'DbContext'
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    //se obtiene lista completa de los usuarios registrados
    public async Task<IEnumerable<User>> GetAllUser()
    {
        return await _context.Users.ToListAsync();
    }
    
    //se obtiene un usuario por id
    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    //se agregar un nuevo usuario
    public async Task<User> AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    //actualiza informacion de un usuario existente
    public async Task<User> UpdateUser(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    //elimina un usuario por id
    public async Task<User> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return user;
    }

    // 🔹 nuevo método: obtener usuario por email
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}