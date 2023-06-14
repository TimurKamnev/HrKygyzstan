using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Entities.Base;

namespace HrKyrgyzstan.Entities
{
    public class Company : BaseEntity
    {
        public string? Name { get; set; }
        public string? AboutUs { get; set; }
        public string? FeedBack { get; set; }
        public List<Vacancy> Vacancies { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}
