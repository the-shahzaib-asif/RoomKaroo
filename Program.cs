using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomIQ.Data;
using RoomIQ.Models;
using RoomIQ.Repositories;
using RoomIQ.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 1. Database Context for Customized Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Customized ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// 3. Dependency Injection (D.I) for 4 Dapper Repositories
builder.Services.AddScoped<IHostelRepository, HostelRepository>();
builder.Services.AddScoped<IRoommateRepository, RoommateRepository>();
builder.Services.AddScoped<ISavedHostelRepository, SavedHostelRepository>();
builder.Services.AddScoped<IInquiryRepository, InquiryRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Identity Authentication
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();