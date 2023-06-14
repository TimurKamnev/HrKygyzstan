using HrKyrgyzstan.Interfaces;
using HrKyrgyzstan.Repositories;

namespace HrKyrgyzstan.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }
    }
}
