using Microsoft.AspNetCore.Identity;

namespace HrKyrgyzstan.Areas.Identity.Data
{
    public class CreatedUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }
}
