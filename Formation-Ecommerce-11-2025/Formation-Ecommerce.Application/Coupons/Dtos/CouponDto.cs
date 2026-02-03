using System;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Dtos
{
    /// <summary>
    /// DTO coupon (lecture) : projection des informations d'un code promo pour transport entre couches.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le coupon est principalement identifié par son code (<see cref="CouponCode"/>) côté UI.
    /// - Les montants utilisent <see cref="decimal"/> car on manipule des valeurs financières.
    /// - Ce DTO peut être mappé vers un ViewModel MVC pour l'affichage (liste, détail).
    /// </remarks>
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}
