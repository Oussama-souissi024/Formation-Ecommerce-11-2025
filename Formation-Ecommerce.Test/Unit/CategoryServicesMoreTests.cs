using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Application.Categories.Mapping;
using Formation_Ecommerce_11_2025.Application.Categories.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires complémentaires de <c>CategoryServices</c> (read all, update, recherche par nom).
/// </summary>
public class CategoryServicesMoreTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CategoryProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task ReadAllAsync_returns_added_categories()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        await service.AddAsync(new CreateCategoryDto { Name = "A", Description = "D" });
        await service.AddAsync(new CreateCategoryDto { Name = "B", Description = "D" });

        var list = await service.ReadAllAsync();

        Assert.Equal(2, list.Count());
    }

    [Fact]
    public async Task GetCategoryIdByNameAsync_when_exists_returns_id()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        var created = await service.AddAsync(new CreateCategoryDto { Name = "Electro", Description = "D" });

        var id = await service.GetCategoryIdByNameAsync("Electro");

        Assert.Equal(created.Id, id);
    }

    [Fact]
    public async Task UpdateAsync_updates_category_in_repository()
    {
        var repo = new FakeCategoryRepository();
        var service = new CategoryServices(repo, CreateMapper());

        var created = await service.AddAsync(new CreateCategoryDto { Name = "A", Description = "D" });

        await service.UpdateAsync(new UpdateCategoryDto
        {
            Id = created.Id,
            Name = "A2",
            Description = "D2"
        });

        var updated = await repo.ReadByIdAsync(created.Id);
        Assert.Equal("A2", updated.Name);
        Assert.Equal("D2", updated.Description);
    }
}
