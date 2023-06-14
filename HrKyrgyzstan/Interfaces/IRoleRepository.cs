using Microsoft.AspNetCore.Identity;

namespace HrKyrgyzstan.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
