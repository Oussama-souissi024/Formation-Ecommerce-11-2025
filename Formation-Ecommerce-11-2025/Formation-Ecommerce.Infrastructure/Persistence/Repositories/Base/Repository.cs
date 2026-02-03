using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Formation_Ecommerce_11_2025.Infrastructure.Persistence.Repositories.Base
{
    /// <summary>
    /// Repository générique EF Core : fournit des opérations CRUD de base pour une entité donnée.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le pattern Repository encapsule l'accès aux données et évite de manipuler directement le DbContext dans la couche Application.
    /// - <see cref="DbSet{TEntity}"/> représente la collection EF associée à l'entité.
    /// - Le SaveChanges est exposé pour contrôler le moment de la persistance (transaction logique).
    /// </remarks>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public Task Remove(TEntity entity)
        {
            _entities.Remove(entity);
            return Task.CompletedTask;
        }
        public Task Update(TEntity entity)
        {
            _entities.Update(entity);
            return Task.CompletedTask;
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
