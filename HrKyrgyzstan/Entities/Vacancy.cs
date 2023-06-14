using HrKyrgyzstan.Entities.Base;

namespace HrKyrgyzstan.Entities
{
    public class Vacancy : BaseEntity
    {
        public string? Name { get; set; }
        public string? About { get; set; }
        public decimal MinSalary { get; set; }
        public string? Responsibilities { get; set; }
        public string? YourHope { get; set; }
        public string? Skills { get; set; }
        public int VacancyId { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}