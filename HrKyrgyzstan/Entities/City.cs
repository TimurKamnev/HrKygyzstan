using HrKyrgyzstan.Entities.Base;

namespace HrKyrgyzstan.Entities
{
    public class City : BaseEntity
    {
        public string? Name { get; set; }
        public int CityId { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public List<Company> Companies { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
