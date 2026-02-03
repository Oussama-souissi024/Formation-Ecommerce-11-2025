using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Categories.Interfaces
{
    /// <summary>
    /// Contrat applicatif des catégories : expose les cas d'usage CRUD et des requêtes spécifiques (Id par nom).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Couche Application : orchestre la logique autour des catégories et délègue la persistance aux repositories.
    /// - Les contrôleurs MVC consomment ce contrat pour éviter l'accès direct au DbContext.
    /// - Les méthodes retournent des DTOs (<see cref="CategoryDto"/>) afin de préserver la séparation des couches.
    /// </remarks>
    public interface ICategoryService
    {
        Task<CategoryDto> AddAsync(CreateCategoryDto categoryDto);
        Task<CategoryDto> ReadByIdAsync(Guid categoryId);
        Task<Guid?> GetCategoryIdByNameAsync(string categoryName);
        Task<IEnumerable<CategoryDto>> ReadAllAsync();
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(Guid id);
    }
}
