using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace eBaby
{
    public class UserRegistry
    {
        private List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public IReadOnlyList<User> RegisteredUsers => _users;

        public User FindUser(string userName)
        {
            return _users
                .FirstOrDefault(user => user.UserName == userName);
        }

        public void LogIn(string userName, string password)
        {
            var foundUser = FindUser(userName);
            if (foundUser is null)
            {
                throw new BadCredentialsException();
            }
            foundUser.TryToLogIn(password);
        }
    }
}