using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SirketYonetim.Data;
using SirketYonetim.Entities;
using Microsoft.Extensions.DependencyInjection;
using SirketYonetim.Repositories.Abstract;
using SirketYonetim.Repositories.Concrete;
using SirketYonetim.Repositories.Abstract.Customer;
using SirketYonetim.Repositories.Abstract.Order;
using SirketYonetim.Repositories.Abstract.Product;
using SirketYonetim.Repositories.Concrete.Customer;
using SirketYonetim.Repositories.Concrete.Order;
using SirketYonetim.Repositories.Concrete.Product;
using SirketYonetim.Repositories.Abstract.AppUser;
using SirketYonetim.Repositories.Concrete.AppUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SirketYonetimContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SirketYonetimDb") + ";TrustServerCertificate=True"));

// Identity yap�land�rmas�
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<SirketYonetimContext>()
.AddDefaultTokenProviders();

// Dependency Injection
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

builder.Services.AddScoped<IAppUserReadRepository, AppUserReadRepository>();
builder.Services.AddScoped<IAppUserWriteRepository, AppUserWriteRepository>();

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
