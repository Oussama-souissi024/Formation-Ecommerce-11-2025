using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Cart.Mapping;
using Formation_Ecommerce_11_2025.Application.Categories.Mapping;
using Formation_Ecommerce_11_2025.Application.Coupons.Mapping;
using Formation_Ecommerce_11_2025.Application.Orders.Mapping;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Core.Entities.Cart;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Entities.Orders;
using Formation_Ecommerce_11_2025.Core.Entities.Product;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Smoke test unitaire pour vérifier que les profils AutoMapper principaux peuvent mapper des objets usuels.
/// </summary>
public class AutoMapperProfileTests
{
    [Fact]
    public void Mapper_can_map_common_objects_without_throwing()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<ProductProfile>();
            cfg.AddProfile<CouponProfile>();
            cfg.AddProfile<CartMappingProfile>();
            cfg.AddProfile<OrderMappingProfile>();
        });

        var mapper = config.CreateMapper();

        var category = new Category { Id = Guid.NewGuid(), Name = "Cat", Description = "Desc", Products = new List<Product>() };
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = "Desc",
            Price = 10m,
            ImageUrl = "img.png",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        };
        var coupon = new Coupon { Id = Guid.NewGuid(), CouponCode = "SAVE10", DiscountAmount = 10m, MinimumAmount = 50m };
        var cartHeader = new CartHeader { Id = Guid.NewGuid(), UserID = "u1" };
        var cartDetails = new Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails
        {
            Id = Guid.NewGuid(),
            CartHeaderId = cartHeader.Id,
            ProductId = product.Id,
            Product = product,
            Count = 1
        };
        var orderHeader = new OrderHeader
        {
            Id = Guid.NewGuid(),
            UserId = "u1",
            OrderTime = DateTime.UtcNow,
            Status = "Pending",
            OrderDetails = new List<OrderDetails>()
        };

        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Categories.Dtos.CategoryDto>(category));
        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Products.Dtos.ProductDto>(product));
        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Coupons.Dtos.CouponDto>(coupon));
        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Cart.Dtos.CartHeaderDto>(cartHeader));
        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Cart.Dtos.CartDetailsDto>(cartDetails));
        Assert.NotNull(mapper.Map<Formation_Ecommerce_11_2025.Application.Orders.Dtos.OrderHeaderDto>(orderHeader));
    }
}
