using System.Collections.Generic;

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

        public User FindUser(string userUserName)
        {
            return _users[0];
        }
    }
}