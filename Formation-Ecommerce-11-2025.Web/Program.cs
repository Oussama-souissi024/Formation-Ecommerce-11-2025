using Formation_Ecommerce_11_2025.Core.Entities.Identity;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base;
using Formation_Ecommerce_11_2025.Infrastructure;
using Formation_Ecommerce_11_2025.Infrastructure.Persistence;
using Formation_Ecommerce_11_2025.Infrastructure.Persistence.Repositories;
using Formation_Ecommerce_11_2025.Infrastructure.Persistence.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();



// Add services to the container.
builder.Services.AddRazorPages();

//Dependancy Injection 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
