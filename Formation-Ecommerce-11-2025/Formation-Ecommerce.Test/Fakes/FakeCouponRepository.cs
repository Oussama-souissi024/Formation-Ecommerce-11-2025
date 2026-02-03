using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake (en mémoire) de <see cref="ICouponRepository" />.
/// </summary>
/// <remarks>
/// Objectif pédagogique :
/// - Tester la logique métier (application de coupons) sans dépendre de la DB.
/// - Pouvoir créer/lire/metttre à jour/supprimer des coupons en mémoire.
/// </remarks>
public class FakeCouponRepository : ICouponRepository
{
    private readonly Dictionary<Guid, Coupon> _store = new();

    public Task<Coupon> AddAsync(Coupon coupon)
    {
        if (coupon.Id == Guid.Empty)
        {
            coupon.Id = Guid.NewGuid();
        }

        _store[coupon.Id] = coupon;
        return Task.FromResult(coupon);
    }

    public Task<Coupon> ReadByIdAsync(Guid couponId)
    {
        _store.TryGetValue(couponId, out var coupon);
        return Task.FromResult(coupon!);
    }

    public Task<Coupon> ReadByCouponCodeAsync(string couponCode)
    {
        var found = _store.Values.FirstOrDefault(c => string.Equals(c.CouponCode, couponCode, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(found!);
    }

    public Task<IEnumerable<Coupon>> ReadAllAsync() => Task.FromResult<IEnumerable<Coupon>>(_store.Values.ToList());

    public Task UpdateAsync(Coupon coupon)
    {
        _store[coupon.Id] = coupon;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _store.Remove(id);
        return Task.CompletedTask;
    }

    Task<Coupon> Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base.IRepository<Coupon>.AddAsync(Coupon entity) => AddAsync(entity);

    public Task<Coupon> GetByIdAsync(Guid id) => ReadByIdAsync(id);

    public Task<IEnumerable<Coupon>> GetAllAsync() => ReadAllAsync();

    Task Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base.IRepository<Coupon>.Update(Coupon entity) => UpdateAsync(entity);

    public Task Remove(Coupon entity)
    {
        _store.Remove(entity.Id);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync() => Task.FromResult(1);
}
