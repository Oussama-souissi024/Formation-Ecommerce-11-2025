using System;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Cart.Dtos
{
    /// <summary>
    /// DTO ligne de panier : représente un produit ajouté au panier et la quantité associée.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La présence de <see cref="Product"/> permet à l'UI d'afficher les informations du produit sans requête supplémentaire.
    /// - <see cref="Price"/> peut être un champ calculé/figé pour l'affichage (ex: prix du produit au moment de l'ajout).
    /// - Ce DTO est construit par la couche Application à partir des entités et repositories (Infrastructure).
    /// </remarks>
    public class CartDetailsDto
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        // For UI display purposes
        public ProductDto? Product { get; set; }

        // Additional calculated properties for UI
        public decimal? Price { get; set; }
    }
}
