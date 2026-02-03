using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Application.Coupons.Mapping;
using Formation_Ecommerce_11_2025.Application.Coupons.Services;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires complémentaires de <c>CouponServices</c> (recherche par code, read all, update).
/// </summary>
public class CouponServicesMoreTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CouponProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task GetCouponByCodeAsync_when_exists_returns_coupon()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        var entity = await repo.AddAsync(new Coupon
        {
            CouponCode = "SAVE10",
            DiscountAmount = 10,
            MinimumAmount = 50
        });

        var dto = await service.GetCouponByCodeAsync("SAVE10");

        Assert.NotNull(dto);
        Assert.Equal(entity.Id, dto!.Id);
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_coupons()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        await repo.AddAsync(new Coupon { CouponCode = "A", DiscountAmount = 1, MinimumAmount = 1 });
        await repo.AddAsync(new Coupon { CouponCode = "B", DiscountAmount = 2, MinimumAmount = 2 });

        var list = await service.ReadAllAsync();

        Assert.Equal(2, list.Count());
    }

    [Fact]
    public async Task UpdateAsync_when_exists_updates_coupon()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        var existing = await repo.AddAsync(new Coupon { CouponCode = "A", DiscountAmount = 1, MinimumAmount = 1 });

        await service.UpdateAsync(new UpdateCouponDto
        {
            Id = existing.Id,
            CouponCode = "A2",
            DiscountAmount = 5,
            MinimumAmount = 10
        });

        var updated = await repo.ReadByIdAsync(existing.Id);
        Assert.Equal("A2", updated.CouponCode);
        Assert.Equal(5, updated.DiscountAmount);
        Assert.Equal(10, updated.MinimumAmount);
    }
}
