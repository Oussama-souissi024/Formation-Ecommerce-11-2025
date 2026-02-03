using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Coupon
{
    /// <summary>
    /// ViewModel du formulaire de création de coupon : code + paramètres de remise.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les DataAnnotations imposent des bornes (Range) et des contraintes de longueur pour sécuriser les entrées.
    /// - La création réelle (unicité du code, règles métier) est gérée dans la couche Application.
    /// - Le contrôleur MVC se limite à valider puis déléguer.
    /// </remarks>
    public class CreateCouponViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CouponCode { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal MinimumAmount { get; set; }
    }
}
