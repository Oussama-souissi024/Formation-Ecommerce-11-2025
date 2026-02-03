using System;

namespace Formation_Ecommerce_11_2025.Models.Coupon
{
    /// <summary>
    /// ViewModel de confirmation de suppression d'un coupon : affiche les informations avant suppression.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Utilisé sur une vue de confirmation (GET) avant la suppression (POST).
    /// - Permet d'afficher le récapitulatif sans exposer l'entité du domaine.
    /// - La suppression réelle est effectuée via le service applicatif / repository.
    /// </remarks>
    public class DeleteCouponViewModel
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}
