using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Interfaces
{
    /// <summary>
    /// Contrat applicatif des coupons : expose les cas d'usage CRUD et la recherche d'un coupon par code.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les contrôleurs MVC consomment ce contrat pour déléguer la logique de remise à la couche Application.
    /// - La recherche par code (<see cref="GetCouponByCodeAsync"/>) correspond au scénario d'application d'un coupon au panier.
    /// - La persistance et l'accès DB sont gérés en Infrastructure via des repositories.
    /// </remarks>
    public interface ICouponService
    {
        Task<CouponDto> AddAsync(CouponDto coupon);
        Task<CouponDto?> ReadByIdAsync(Guid couponId);
        Task<CouponDto?> GetCouponByCodeAsync(string couponCode);
        Task<IEnumerable<CouponDto>> ReadAllAsync();
        Task UpdateAsync(UpdateCouponDto updateCouponDto);
        Task DeleteAsync(Guid id);
    }
}
