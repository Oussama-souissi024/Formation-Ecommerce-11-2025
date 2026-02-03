using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake (en mémoire) de <see cref="ICategoryRepository" />.
/// </summary>
/// <remarks>
/// Objectif pédagogique :
/// - Simuler une base de données avec un dictionnaire (clé = Id).
/// - Rendre les tests unitaires rapides et déterministes.
/// - Permettre de simuler des erreurs (ex: ThrowOnDelete) pour tester la gestion d'exception.
/// </remarks>
public class FakeCategoryRepository : ICategoryRepository
{
    private readonly Dictionary<Guid, Category> _store = new();

    public bool ThrowOnDelete { get; set; }

    public Task<Category> AddAsync(Category category)
    {
        if (category.Id == Guid.Empty)
        {
            category.Id = Guid.NewGuid();
        }

        _store[category.Id] = category;
        return Task.FromResult(category);
    }

    public Task<Category> ReadByIdAsync(Guid categoryId)
    {
        _store.TryGetValue(categoryId, out var category);
        return Task.FromResult(category!);
    }

    public Task<IEnumerable<Category>> ReadAllAsync() => Task.FromResult<IEnumerable<Category>>(_store.Values.ToList());

    public Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName)
    {
        var found = _store.Values.FirstOrDefault(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult<Guid?>(found?.Id);
    }

    public Task Update(Category category)
    {
        _store[category.Id] = category;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid categoryId)
    {
        if (ThrowOnDelete)
        {
            throw new InvalidOperationException("Delete failed");
        }

        _store.Remove(categoryId);
        return Task.CompletedTask;
    }

    Task<Category> Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base.IRepository<Category>.AddAsync(Category entity) => AddAsync(entity);

    public Task<Category> GetByIdAsync(Guid id) => ReadByIdAsync(id);

    public Task<IEnumerable<Category>> GetAllAsync() => ReadAllAsync();

    Task Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base.IRepository<Category>.Update(Category entity) => Update(entity);

    public Task Remove(Category entity)
    {
        _store.Remove(entity.Id);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync() => Task.FromResult(1);
}
