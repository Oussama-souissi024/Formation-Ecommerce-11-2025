using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Product;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires du mapping AutoMapper <c>ProductProfile</c> (ex: calcul de CategoryName).
/// </summary>
public class ProductProfileTests
{
    [Fact]
    public void Map_Product_to_ProductDto_sets_CategoryName_from_Category()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());

        var mapper = config.CreateMapper();

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cat",
            Description = string.Empty,
            Products = new List<Product>()
        };

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = string.Empty,
            Price = 10m,
            ImageUrl = string.Empty,
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        };

        var dto = mapper.Map<ProductDto>(product);

        Assert.Equal("Prod", dto.Name);
        Assert.Equal("Cat", dto.CategoryName);
    }
}
