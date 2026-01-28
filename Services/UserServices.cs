using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        private bool EmailExists(string email, int? ignoreUserId = null)
        {
            return _users.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                (!ignoreUserId.HasValue || u.Id != ignoreUserId.Value));
        }

        public User? Add(User user)
        {
            if (EmailExists(user.Email))
                return null; // signal duplicate

            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User updatedUser)
        {
            var existing = GetById(id);
            if (existing == null)
                return false;

            // Prevent duplicate email on update
            if (!string.IsNullOrWhiteSpace(updatedUser.Email) &&
                EmailExists(updatedUser.Email, ignoreUserId: id))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(updatedUser.Name))
                existing.Name = updatedUser.Name;

            if (!string.IsNullOrWhiteSpace(updatedUser.Email))
                existing.Email = updatedUser.Email;

            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null)
                return false;

            _users.Remove(user);
            return true;
        }
    }
}