using HrKyrgyzstan.Areas.Identity.Data;

namespace HrKyrgyzstan.Interfaces
{
    public interface IUserRepository
    {
        ICollection<CreatedUser> GetUsers();
        CreatedUser GetUser(string id);
        CreatedUser UpdateUser(CreatedUser user);
        CreatedUser DeleteUser(CreatedUser? user);
    }
}
