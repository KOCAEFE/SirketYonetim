using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SirketYonetim.Data;
using SirketYonetim.Entities;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SirketYonetimContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SirketYonetimDb") + ";TrustServerCertificate=True"));

// Identity yapýlandýrmasý
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<SirketYonetimContext>()
.AddDefaultTokenProviders();

// Dependency Injection
//builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
//builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

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

app.Run();
