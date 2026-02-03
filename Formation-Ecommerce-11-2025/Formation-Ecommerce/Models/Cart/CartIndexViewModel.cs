using System.Collections.Generic;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;

namespace Formation_Ecommerce_11_2025.Models.Cart
{
    /// <summary>
    /// ViewModel de la page Panier : agrège le header et les lignes du panier pour l'affichage.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ici, la vue consomme des DTOs issus de la couche Application (CartHeaderDto/CartDetailsDto).
    /// - Le ViewModel sert de "conteneur" pour transmettre plusieurs objets à une même vue.
    /// - Cela évite de passer directement des entités EF Core à la vue (séparation des responsabilités).
    /// </remarks>
    public class CartIndexViewModel
    {
        public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
        public List<CartDetailsDto> CartDetails { get; set; } = new List<CartDetailsDto>();
    }
}
