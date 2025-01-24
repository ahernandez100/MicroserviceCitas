using AuthMicroservice.Repositories;
using AuthMicroservice.Utils;
using System.Security.Cryptography;


namespace AuthMicroservice.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;

        public AuthenticationService()
        {
            _userRepository = new UserRepository();
        }

        public bool ValidateUser(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
                return false;

            // Verificar la contraseña usando la nueva implementación
            return PasswordHasher.VerifyPassword(user.PasswordHash, password);
        }
    }
}