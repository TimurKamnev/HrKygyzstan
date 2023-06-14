using Microsoft.AspNetCore.Identity;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Interfaces;

namespace HrKyrgyzstan.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
