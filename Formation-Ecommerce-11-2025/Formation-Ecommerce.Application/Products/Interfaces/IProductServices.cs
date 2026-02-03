using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Products.Interfaces
{
    /// <summary>
    /// Contrat applicatif des produits : expose les cas d'usage CRUD et des lectures spécialisées (filtrage par catégorie).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La couche Application orchestre la logique (ex: upload d'image, validation) et délègue la persistance aux repositories.
    /// - Les contrôleurs MVC consomment ce contrat pour rester "minces" (pas d'accès DB ni de logique technique).
    /// - Les méthodes retournent des DTOs (<see cref="ProductDto"/>) afin de découpler l'UI du domaine.
    /// </remarks>
    public interface IProductServices
    {
        Task<ProductDto?> AddAsync(CreateProductDto createProductDto);
        Task<ProductDto> ReadByIdAsync(Guid productId);
        Task<IEnumerable<ProductDto>> ReadAllAsync();
        Task<IEnumerable<ProductDto>>? ReadProductsByCategoryName(string categoryName);
        Task UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(Guid id);
    }
}
