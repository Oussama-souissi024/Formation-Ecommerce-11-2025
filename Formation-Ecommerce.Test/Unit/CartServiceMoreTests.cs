using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;
using Formation_Ecommerce_11_2025.Application.Cart.Mapping;
using Formation_Ecommerce_11_2025.Application.Cart.Services;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Core.Entities.Cart;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Product;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires complémentaires de <c>CartService</c> (scénarios avancés / cas limites).
/// </summary>
public class CartServiceMoreTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CartMappingProfile>();
            cfg.AddProfile<ProductProfile>();
        });
        return config.CreateMapper();
    }

    [Fact]
    public async Task UpsertCartAsync_when_same_product_is_added_twice_increments_count()
    {
        var cartRepo = new FakeCartRepository();
        var productRepo = new FakeProductRepository();
        var couponRepo = new FakeCouponRepository();
        var service = new CartService(cartRepo, CreateMapper(), productRepo, couponRepo);

        var category = new Category { Id = Guid.NewGuid(), Name = "Cat", Description = "", Products = new List<Product>() };
        var product = await productRepo.AddAsync(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = "",
            Price = 2m,
            ImageUrl = "",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        });

        await service.UpsertCartAsync(new CartDto
        {
            CartHeader = new CartHeaderDto { UserID = "u1" },
            CartDetails = new List<CartDetailsDto> { new CartDetailsDto { ProductId = product.Id, Count = 1 } }
        });

        var cart = await service.UpsertCartAsync(new CartDto
        {
            CartHeader = new CartHeaderDto { UserID = "u1" },
            CartDetails = new List<CartDetailsDto> { new CartDetailsDto { ProductId = product.Id, Count = 3 } }
        });

        Assert.NotNull(cart);
        Assert.Equal(8m, cart.CartHeader.CartTotal);
        Assert.Equal(1, cart.CartDetails.Count());
        Assert.Equal(4, cart.CartDetails.First().Count);
    }

    [Fact]
    public async Task ApplyCouponAsync_when_cart_header_missing_returns_empty_cart_dto()
    {
        var service = new CartService(new FakeCartRepository(), CreateMapper(), new FakeProductRepository(), new FakeCouponRepository());

        var cart = await service.ApplyCouponAsync("missing", "SAVE10");

        Assert.NotNull(cart);
        Assert.Null(cart!.CartHeader);
    }

    [Fact]
    public async Task RemoveCartItemAsync_when_item_missing_returns_false()
    {
        var service = new CartService(new FakeCartRepository(), CreateMapper(), new FakeProductRepository(), new FakeCouponRepository());

        var ok = await service.RemoveCartItemAsync(Guid.NewGuid());

        Assert.False(ok);
    }

    [Fact]
    public async Task RemoveCartItemAsync_when_item_exists_returns_true()
    {
        var cartRepo = new FakeCartRepository();
        var service = new CartService(cartRepo, CreateMapper(), new FakeProductRepository(), new FakeCouponRepository());

        var header = await cartRepo.AddCartHeaderAsync(new CartHeader { Id = Guid.NewGuid(), UserID = "u1" });
        var detail = await cartRepo.AddCartDetailsAsync(new CartDetails { Id = Guid.NewGuid(), CartHeaderId = header.Id, ProductId = Guid.NewGuid(), Count = 1 });

        var ok = await service.RemoveCartItemAsync(detail.Id);

        Assert.True(ok);
    }

    [Fact]
    public async Task ClearCartAsync_when_user_has_cart_returns_true()
    {
        var cartRepo = new FakeCartRepository();
        var service = new CartService(cartRepo, CreateMapper(), new FakeProductRepository(), new FakeCouponRepository());

        var header = await cartRepo.AddCartHeaderAsync(new CartHeader { Id = Guid.NewGuid(), UserID = "u1" });
        await cartRepo.AddCartDetailsAsync(new CartDetails { Id = Guid.NewGuid(), CartHeaderId = header.Id, ProductId = Guid.NewGuid(), Count = 1 });

        var ok = await service.ClearCartAsync("u1");

        Assert.True(ok);
        var details = await cartRepo.GetListCartDetailsByCartHeaderIdAsync(header.Id);
        Assert.Empty(details);
    }
}
