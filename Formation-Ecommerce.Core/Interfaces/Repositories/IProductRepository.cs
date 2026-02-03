using Formation_Ecommerce_11_2025.Core.Entities.Product;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrat de repository des produits : définit les opérations d'accès aux données pour le catalogue produit.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La couche Application orchestre les cas d'usage (CRUD, upload d'images, etc.) en s'appuyant sur ce contrat.
    /// - La méthode <see cref="GetProductsByCategoryId"/> correspond à une requête de filtrage fréquente (catalogue par catégorie).
    /// - L'implémentation concrète est en Infrastructure (EF Core) ; l'interface reste en Core pour préserver la séparation des couches.
    /// </remarks>
    public interface IProductRepository : IRepository<Formation_Ecommerce_11_2025.Core.Entities.Product.Product>
    {
        // Ajoute un nouveau produit à la base de données
        Task<Product> AddAsync(Product product);

        // Récupère un produit par son identifiant
        Task<Product> ReadByIdAsync(Guid productId);

        // Récupère tous les produits existants
        Task<IEnumerable<Product>> ReadAllAsync();

        // Met à jour un produit existant
        Task UpdateAsync(Product product);

        // Supprime un produit par son identifiant et retourne true si l'opération a réussi
        Task DeleteAsync(Guid productId);

        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId);
    }
}
