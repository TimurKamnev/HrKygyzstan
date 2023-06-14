using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrKyrgyzstan.Context;
public class AppDbContext : IdentityDbContext<CreatedUser, IdentityRole, string>
{
    public DbSet<City> City { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Resume> Resume { get; set; }
    public DbSet<Vacancy> Vacancy { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());


        builder.Entity<City>()
            .HasKey(x => x.Id);
        builder.Entity<Company>()
            .HasKey(x => x.Id);
        builder.Entity<Country>()
            .HasKey(x => x.Id);
        builder.Entity<Resume>()
            .HasKey(x => x.Id);
        builder.Entity<Vacancy>()
            .HasKey(x => x.Id);


        builder.Entity<Company>()
            .HasMany(x => x.Vacancies)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.VacancyId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Company>()
            .HasOne(x => x.Country)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Company>()
            .HasOne(x => x.City)
            .WithMany(x => x.Companies)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Country>()
            .HasMany(x => x.Cities)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<City>()
            .HasOne(x => x.Country)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Vacancy>()
            .HasOne(x => x.Company)
            .WithMany(x => x.Vacancies)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Vacancy>()
            .HasOne(x => x.Country)
            .WithMany(x => x.Vacancies)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Entity<Vacancy>()
            .HasOne(x => x.City)
            .WithMany(x => x.Vacancies)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.Entity<CreatedUser>().HasData(
        //        new CreatedUser
        //        {
        //            Id = "1b3f06b5-222a-44be-948e-303b5b02e8fa",
        //            FirstName = "Kevin",
        //            LastName = "Levrone",
        //            Email = "admin@mail.com",
        //            PasswordHash = "Password1!"
        //        }
        //    );
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<CreatedUser>
{
    public void Configure(EntityTypeBuilder<CreatedUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
