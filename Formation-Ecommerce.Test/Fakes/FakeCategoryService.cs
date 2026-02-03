using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Application.Categories.Interfaces;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake minimal de <see cref="ICategoryService" />.
/// </summary>
/// <remarks>
/// Il est utilisé pour tester <c>ProductServices</c> sans dépendre du vrai service (et donc sans dépendre du repository des catégories).
/// Seule la méthode <see cref="ICategoryService.GetCategoryIdByNameAsync" /> est implémentée.
/// </remarks>
public class FakeCategoryService : ICategoryService
{
    public Guid? CategoryIdByName { get; set; }

    public Task<CategoryDto> AddAsync(CreateCategoryDto categoryDto) => throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<Guid?> GetCategoryIdByNameAsync(string categoryName) => Task.FromResult(CategoryIdByName);

    public Task<IEnumerable<CategoryDto>> ReadAllAsync() => throw new NotImplementedException();

    public Task<CategoryDto> ReadByIdAsync(Guid categoryId) => throw new NotImplementedException();

    public Task UpdateAsync(UpdateCategoryDto updateCategoryDto) => throw new NotImplementedException();
}
