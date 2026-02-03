using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Application.Products.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires complémentaires de <c>ProductServices</c> (read all).
/// </summary>
public class ProductServicesMoreTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_products()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        await service.AddAsync(new CreateProductDto
        {
            Name = "P1",
            Price = 1m,
            Description = "D",
            CategoryID = Guid.NewGuid()
        });

        await service.AddAsync(new CreateProductDto
        {
            Name = "P2",
            Price = 2m,
            Description = "D",
            CategoryID = Guid.NewGuid()
        });

        var list = await service.ReadAllAsync();

        Assert.Equal(2, list.Count());
    }
}
