using System.Threading.Tasks;

namespace WebApi_HU3.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Autentica un usuario y devuelve un token JWT si las credenciales son correctas.
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>JWT token o null si las credenciales son incorrectas</returns>
        Task<string?> Authenticate(string email, string password);
    }
}