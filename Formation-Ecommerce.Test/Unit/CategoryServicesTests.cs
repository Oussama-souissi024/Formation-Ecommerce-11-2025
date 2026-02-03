using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Application.Categories.Mapping;
using Formation_Ecommerce_11_2025.Application.Categories.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <c>CategoryServices</c> (CRUD + gestion d'erreurs).
/// </summary>
public class CategoryServicesTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CategoryProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task AddAsync_returns_created_category_dto()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        var dto = await service.AddAsync(new CreateCategoryDto
        {
            Name = "Cat",
            Description = "Desc"
        });

        Assert.NotEqual(Guid.Empty, dto.Id);
        Assert.Equal("Cat", dto.Name);
    }

    [Fact]
    public async Task ReadByIdAsync_when_not_found_throws_key_not_found()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.ReadByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task GetCategoryIdByNameAsync_when_not_found_throws_key_not_found()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetCategoryIdByNameAsync("missing"));
    }

    [Fact]
    public async Task DeleteAsync_when_repository_fails_wraps_exception()
    {
        var repo = new FakeCategoryRepository { ThrowOnDelete = true };
        var service = new CategoryServices(repo, CreateMapper());

        var ex = await Assert.ThrowsAsync<Exception>(() => service.DeleteAsync(Guid.NewGuid()));
        Assert.Contains("Failed to delete category", ex.Message);
    }
}
