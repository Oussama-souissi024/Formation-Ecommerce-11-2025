using System.Linq;
using Formation_Ecommerce_11_2025.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Formation_Ecommerce_11_2025.Test.Integration;

/// <summary>
/// Factory de tests d'intégration basée sur <see cref="WebApplicationFactory{TEntryPoint}" />.
/// </summary>
/// <remarks>
/// Ce factory permet de démarrer l'application ASP.NET Core "en mémoire" pour des tests HTTP.
/// 
/// Points clés :
/// - On remplace la configuration de la base de données par EF Core InMemory.
/// - On donne un nom de base unique pour éviter les interférences entre tests.
/// </remarks>
public class CustomWebApplicationFactory : WebApplicationFactory<global::Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase($"FormationEcommerceTests_{Guid.NewGuid()}");
            });

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
