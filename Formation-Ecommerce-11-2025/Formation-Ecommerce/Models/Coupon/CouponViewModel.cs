using System;

namespace Formation_Ecommerce_11_2025.Models.Coupon
{
    /// <summary>
    /// ViewModel de lecture d'un coupon : informations affichées dans la liste des codes promo.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Un coupon se manipule souvent côté UI via son code (<see cref="CouponCode"/>) et ses montants.
    /// - Les montants utilisent <see cref="decimal"/> car on manipule des valeurs financières.
    /// - Ce modèle est destiné à l'affichage ; la validation métier (ex: minimum, règle de remise) reste côté Application.
    /// </remarks>
    public class CouponViewModel
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}
