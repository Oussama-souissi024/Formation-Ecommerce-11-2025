using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Test.Common;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires sur les DataAnnotations des DTOs (validation côté serveur).
/// </summary>
public class DtoValidationTests
{
    [Fact]
    public void CreateCategoryDto_without_name_is_invalid()
    {
        var dto = new CreateCategoryDto
        {
            Name = string.Empty,
            Description = "Desc"
        };

        var results = ValidationHelper.Validate(dto);

        Assert.NotEmpty(results);
    }

    [Fact]
    public void CreateCouponDto_without_code_is_invalid()
    {
        var dto = new CreateCouponDto
        {
            CouponCode = string.Empty,
            DiscountAmount = 10,
            MinimumAmount = 50
        };

        var results = ValidationHelper.Validate(dto);

        Assert.NotEmpty(results);
    }

    [Fact]
    public void UpdateProductDto_without_name_is_invalid()
    {
        var dto = new UpdateProductDto
        {
            Id = Guid.NewGuid(),
            Name = null!,
            Price = 10,
            CategoryId = Guid.NewGuid()
        };

        var results = ValidationHelper.Validate(dto);

        Assert.NotEmpty(results);
    }
}
