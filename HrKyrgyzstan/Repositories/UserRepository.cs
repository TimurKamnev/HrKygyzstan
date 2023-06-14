using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Interfaces;
using HrKyrgyzstan.Repositories;

namespace HrKyrgyzstan.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<CreatedUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public CreatedUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public CreatedUser UpdateUser(CreatedUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }

        public CreatedUser DeleteUser(CreatedUser user)
        {
            _context.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}
