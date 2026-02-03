using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Application.Coupons.Mapping;
using Formation_Ecommerce_11_2025.Application.Coupons.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <c>CouponServices</c> (CRUD + cas d'erreur).
/// </summary>
public class CouponServicesTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CouponProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task AddAsync_returns_created_coupon_dto()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        var dto = await service.AddAsync(new CouponDto
        {
            CouponCode = "SAVE10",
            DiscountAmount = 10,
            MinimumAmount = 50
        });

        Assert.NotEqual(Guid.Empty, dto.Id);
        Assert.Equal("SAVE10", dto.CouponCode);
    }

    [Fact]
    public async Task ReadByIdAsync_when_not_found_returns_null()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        var dto = await service.ReadByIdAsync(Guid.NewGuid());

        Assert.Null(dto);
    }

    [Fact]
    public async Task UpdateAsync_when_not_found_throws_key_not_found()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateAsync(new UpdateCouponDto
        {
            Id = Guid.NewGuid(),
            CouponCode = "X",
            DiscountAmount = 1,
            MinimumAmount = 1
        }));
    }

    [Fact]
    public async Task DeleteAsync_when_not_found_throws_key_not_found()
    {
        var repo = new FakeCouponRepository();
        var service = new CouponServices(repo, CreateMapper());

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteAsync(Guid.NewGuid()));
    }
}
