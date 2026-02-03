using System;
using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Dtos
{
    /// <summary>
    /// DTO de création de coupon : données nécessaires pour créer un nouveau code promo.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Séparer Create DTO du DTO de lecture permet de contrôler les champs acceptés lors de la création.
    /// - Les attributs <see cref="RequiredAttribute"/> expriment des contraintes minimales ; les règles métier restent côté service.
    /// - Ce DTO est généralement mappé depuis un ViewModel MVC (CreateCouponViewModel).
    /// </remarks>
    public class CreateCouponDto
    {
        [Required]
        [MaxLength(50)]
        public string CouponCode { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

        [Required]
        public decimal MinimumAmount { get; set; }
    }
}
