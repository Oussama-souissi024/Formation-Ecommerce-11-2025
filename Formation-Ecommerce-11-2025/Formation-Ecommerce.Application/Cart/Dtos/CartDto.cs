using System.Collections.Generic;

namespace Formation_Ecommerce_11_2025.Application.Cart.Dtos
{
    /// <summary>
    /// DTO panier : agrège l'en-tête du panier et ses lignes (détails).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce DTO représente un "graphe" de données (header + lignes) souvent nécessaire à l'affichage du panier.
    /// - Il évite d'exposer les entités Core (CartHeader/CartDetails) à la couche Web.
    /// - La couche Application peut enrichir ce DTO (totaux, remise) sans impacter le modèle de persistance.
    /// </remarks>
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
