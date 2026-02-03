using Formation_Ecommerce_11_2025.Core.Entities.Cart;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Entities.Identity;
using Formation_Ecommerce_11_2025.Core.Entities.Orders;
using Formation_Ecommerce_11_2025.Core.Entities.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Formation_Ecommerce_11_2025.Infrastructure.Persistence
{
    /// <summary>
    /// DbContext EF Core de l'application : point d'accès aux tables (DbSet) et intégration ASP.NET Identity.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le DbContext représente une "session" de travail avec la base (Unit of Work).
    /// - L'héritage de <see cref="IdentityDbContext{TUser}"/> ajoute automatiquement les tables Identity (users/roles/etc.).
    /// - Les <see cref="DbSet{TEntity}"/> correspondent aux agrégats persistés (Products, Categories, Orders...).
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
