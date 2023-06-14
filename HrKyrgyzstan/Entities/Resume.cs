using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Entities.Base;

namespace HrKyrgyzstan.Entities
{
    public class Resume : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WorkExperience { get; set; }
        public string? Skills { get; set; }
        public string? Certifications { get; set; }
        public string? References { get; set; }
        public List<CreatedUser> Users { get; set; } = new List<CreatedUser>();
    }
}
