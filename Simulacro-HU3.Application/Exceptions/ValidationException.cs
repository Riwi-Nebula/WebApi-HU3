
namespace WebApi_HU3.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
        // Puedes extender esto para incluir una lista de errores
    }
}