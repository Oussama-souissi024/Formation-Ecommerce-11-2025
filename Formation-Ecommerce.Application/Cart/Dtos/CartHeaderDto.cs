using System;

namespace Formation_Ecommerce_11_2025.Application.Cart.Dtos
{
    /// <summary>
    /// DTO d'en-tête de panier : informations globales du panier (utilisateur, coupon, totaux, coordonnées).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le panier est modélisé en header/détails : le header porte les infos communes à toutes les lignes.
    /// - Certaines propriétés (total, remise, coordonnées) servent surtout à l'UI et au checkout.
    /// - Le calcul des totaux peut être réalisé côté Application afin de garder les contrôleurs MVC simples.
    /// </remarks>
    public class CartHeaderDto
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string? CouponCode { get; set; }

        // For UI/calculation
        public decimal? CartTotal { get; set; }
        public decimal? Discount { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
