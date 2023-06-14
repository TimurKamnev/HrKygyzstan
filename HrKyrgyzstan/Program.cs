using HrKyrgyzstan.Areas.Identity.Data;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Interfaces;
using HrKyrgyzstan.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HrKyrgyzstan.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CreatedUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies();

#endregion

AddScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireCompanion, policy => policy.RequireRole(Constants.Roles.Companion));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}