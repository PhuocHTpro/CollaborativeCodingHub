using CollaborativeCodingServer.Models.Entities;
using CollaborativeCodingServer.Repositories;

namespace CollaborativeCodingServer.Services
{
    public class AuthService
    {
        private readonly UserRepository repository = new UserRepository();

        public bool Register(string username, string password)
        {
            if (repository.UserExists(username))
                return false;

            User user = new User
            {
                Username = username,
                Password = password
            };

            return repository.Register(user);
        }

        public User Login(string username, string password)
        {
            return repository.Login(username, password);
        }
    }
}
