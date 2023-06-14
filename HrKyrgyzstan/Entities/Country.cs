using HrKyrgyzstan.Entities.Base;
using System.Drawing;

namespace HrKyrgyzstan.Entities
{
    public class Country : BaseEntity
    {
        public string? Name { get; set; }
        public List<Company> Companies { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
