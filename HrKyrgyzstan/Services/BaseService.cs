using HrKyrgyzstan.Context;

namespace HrKyrgyzstan.Entities
{
    public abstract class BaseService
    {
        protected AppDbContext _context { get; }
        //This class is for successfully connection to SQL, on for rule following DRY
        protected BaseService(AppDbContext context)
        {
            _context = context;
        }
    }
}
