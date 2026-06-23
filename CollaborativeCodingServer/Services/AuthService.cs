using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollaborativeCodingServer.Database;
using CollaborativeCodingServer.Models;

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

        public bool Login(string username, string password)
        {
            return repository.Login(username, password);
        }
    }
}
