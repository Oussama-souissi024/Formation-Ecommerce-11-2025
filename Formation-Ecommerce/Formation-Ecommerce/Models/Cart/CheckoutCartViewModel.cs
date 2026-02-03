using System.Collections.Generic;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;

namespace Formation_Ecommerce_11_2025.Models.Cart
{
    /// <summary>
    /// ViewModel de la page Checkout : données du panier nécessaires pour finaliser la commande.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La vue Checkout affiche le récapitulatif (header + lignes) avant la création de la commande.
    /// - Les informations proviennent de la couche Application (DTOs) et non d'entités EF Core.
    /// - Ce ViewModel simplifie le binding côté Razor View (une seule variable modèle).
    /// </remarks>
    public class CheckoutCartViewModel
    {
        public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
        public List<CartDetailsDto> CartDetails { get; set; } = new List<CartDetailsDto>();
    }
}
