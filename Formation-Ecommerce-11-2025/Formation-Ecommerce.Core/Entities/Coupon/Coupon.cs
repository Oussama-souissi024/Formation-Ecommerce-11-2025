
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Formation_Ecommerce_11_2025.Core.Common;

namespace Formation_Ecommerce_11_2025.Core.Entities.Coupon
{
    /// <summary>
    /// Entité représentant un coupon de réduction (code promo) utilisable lors du checkout.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le coupon est identifié par un code texte (<see cref="CouponCode"/>) saisi par l'utilisateur.
    /// - <see cref="DiscountAmount"/> et <see cref="MinimumAmount"/> sont typés en <see cref="decimal"/> pour des montants financiers.
    /// - L'attribut <see cref="ColumnAttribute"/> force une précision SQL afin d'éviter des arrondis inattendus.
    /// </remarks>
    public class Coupon : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string CouponCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumAmount { get; set; }

    }
}
