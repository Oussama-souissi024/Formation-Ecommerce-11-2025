using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.Repositories
{
    // Interface qui définit les opérations spécifiques pour les catégories, hérite de l'interface générique IRepository
    /// <summary>
    /// Contrat de repository pour les catégories : définit les opérations spécifiques au référentiel des catégories.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Hérite de <see cref="IRepository{TEntity}"/> pour réutiliser un socle CRUD générique.
    /// - Expose des méthodes orientées cas d'usage (ex: retrouver l'Id d'une catégorie via son nom).
    /// - L'implémentation concrète (EF Core) vit en Infrastructure ; Application consomme ce contrat.
    /// </remarks>
    public interface ICategoryRepository : IRepository<Category>
    {
        // Ajoute une nouvelle catégorie à la base de données
        Task<Category> AddAsync(Category category);
        
        // Récupère une catégorie par son identifiant
        Task<Category> ReadByIdAsync(Guid categoryId);
        
        // Récupère toutes les catégories existantes
        Task<IEnumerable<Category>> ReadAllAsync();
        
        // Trouve l'identifiant d'une catégorie à partir de son nom
        Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName);
        
        // Met à jour une catégorie existante
        Task Update(Category category);
        
        // Supprime une catégorie par son identifiant
        Task DeleteAsync(Guid categoryId);
    }
}
