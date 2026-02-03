using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;
using Formation_Ecommerce_11_2025.Application.Cart.Mapping;
using Formation_Ecommerce_11_2025.Application.Cart.Services;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Core.Entities.Cart;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Entities.Product;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <c>CartService</c> (récupération panier, ajout/upsert, coupon, suppression).
/// </summary>
public class CartServiceTests
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
    public async Task GetCartByUserIdAsync_when_no_cart_returns_null()
    {
        var service = new CartService(new FakeCartRepository(), CreateMapper(), new FakeProductRepository(), new FakeCouponRepository());

        var cart = await service.GetCartByUserIdAsync("u1");

        Assert.Null(cart);
    }

    [Fact]
    public async Task UpsertCartAsync_creates_cart_and_computes_total()
    {
        var cartRepo = new FakeCartRepository();
        var productRepo = new FakeProductRepository();
        var couponRepo = new FakeCouponRepository();
        var service = new CartService(cartRepo, CreateMapper(), productRepo, couponRepo);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cat",
            Description = "Desc",
            Products = new List<Product>()
        };

        var product = await productRepo.AddAsync(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = "Desc",
            Price = 5m,
            ImageUrl = "",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        });

        var cart = await service.UpsertCartAsync(new CartDto
        {
            CartHeader = new CartHeaderDto { UserID = "u1" },
            CartDetails = new List<CartDetailsDto>
            {
                new CartDetailsDto { ProductId = product.Id, Count = 2 }
            }
        });

        Assert.NotNull(cart);
        Assert.Equal(10m, cart.CartHeader.CartTotal);
        Assert.Equal(10m, cart.CartDetails.First().Price);
    }

    [Fact]
    public async Task GetCartByUserIdAsync_applies_coupon_when_minimum_reached()
    {
        var cartRepo = new FakeCartRepository();
        var productRepo = new FakeProductRepository();
        var couponRepo = new FakeCouponRepository();
        var service = new CartService(cartRepo, CreateMapper(), productRepo, couponRepo);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cat",
            Description = "Desc",
            Products = new List<Product>()
        };

        var product = await productRepo.AddAsync(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = "Desc",
            Price = 60m,
            ImageUrl = "",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        });

        var coupon = await couponRepo.AddAsync(new Coupon
        {
            Id = Guid.NewGuid(),
            CouponCode = "SAVE10",
            DiscountAmount = 10m,
            MinimumAmount = 50m
        });

        var header = await cartRepo.AddCartHeaderAsync(new CartHeader
        {
            Id = Guid.NewGuid(),
            UserID = "u1",
            CouponCode = "SAVE10",
            CouponId = coupon.Id
        });

        await cartRepo.AddCartDetailsAsync(new CartDetails
        {
            Id = Guid.NewGuid(),
            CartHeaderId = header.Id,
            ProductId = product.Id,
            Count = 1
        });

        var cart = await service.GetCartByUserIdAsync("u1");

        Assert.NotNull(cart);
        Assert.Equal(10m, cart!.CartHeader.Discount);
        Assert.Equal(50m, cart.CartHeader.CartTotal);
    }

    [Fact]
    public async Task ApplyCouponAsync_when_coupon_not_found_throws_invalid_operation()
    {
        var cartRepo = new FakeCartRepository();
        var productRepo = new FakeProductRepository();
        var couponRepo = new FakeCouponRepository();
        var service = new CartService(cartRepo, CreateMapper(), productRepo, couponRepo);

        await cartRepo.AddCartHeaderAsync(new CartHeader
        {
            Id = Guid.NewGuid(),
            UserID = "u1"
        });

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ApplyCouponAsync("u1", "BAD"));
    }

    [Fact]
    public async Task ApplyCouponAsync_with_empty_string_removes_coupon()
    {
        var cartRepo = new FakeCartRepository();
        var productRepo = new FakeProductRepository();
        var couponRepo = new FakeCouponRepository();
        var service = new CartService(cartRepo, CreateMapper(), productRepo, couponRepo);

        await cartRepo.AddCartHeaderAsync(new CartHeader
        {
            Id = Guid.NewGuid(),
            UserID = "u1",
            CouponCode = "SAVE10",
            CouponId = Guid.NewGuid()
        });

        var cart = await service.ApplyCouponAsync("u1", "");

        var header = await cartRepo.GetCartHeaderByUserIdAsync("u1");
        Assert.NotNull(header);
        Assert.Null(header!.CouponCode);
        Assert.Null(header.CouponId);
        Assert.NotNull(cart);
    }
}
