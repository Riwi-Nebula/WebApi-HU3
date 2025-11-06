using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    //Implementer Logia y Connection con la Base de datos
    
    public Task<IEnumerable<User>> GetAllUser()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}