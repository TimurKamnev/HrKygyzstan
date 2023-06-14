using Microsoft.AspNetCore.Mvc.Rendering;
using HrKyrgyzstan.Areas.Identity.Data;

namespace HrKyrgyzstan.Models
{
    public class EditUserViewModel
    {
        public CreatedUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
    }
}
