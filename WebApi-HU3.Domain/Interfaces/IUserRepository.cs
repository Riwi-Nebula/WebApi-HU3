using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Domain.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUser(); // Get All Users
    public Task<User> GetUserById(int id); // Get User By ID
    public Task<User> AddUser(User user); // Add User
    public Task<User> UpdateUser(User user); // Update User
    public Task<User> DeleteUser(int id); // Delete User
    public Task<User?> GetUserByEmailAsync(string email); //Get User By Email
}