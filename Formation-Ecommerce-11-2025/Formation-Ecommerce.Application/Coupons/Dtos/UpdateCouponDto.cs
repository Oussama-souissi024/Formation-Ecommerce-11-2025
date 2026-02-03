using System;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Dtos
{
    /// <summary>
    /// DTO de mise à jour de coupon : porte l'identifiant et les champs modifiables d'un code promo.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Inclure <see cref="Id"/> permet d'identifier le coupon à modifier.
    /// - La validation des règles (unicité, bornes, etc.) est gérée dans la couche Application.
    /// - Ce DTO circule entre Web et Application ; l'implémentation EF/SQL reste en Infrastructure.
    /// </remarks>
    public class UpdateCouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}
