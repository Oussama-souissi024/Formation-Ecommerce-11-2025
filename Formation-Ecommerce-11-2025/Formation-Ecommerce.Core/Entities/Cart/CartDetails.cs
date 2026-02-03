using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Formation_Ecommerce_11_2025.Core.Common;

namespace Formation_Ecommerce_11_2025.Core.Entities.Cart

{
    // Représente une ligne du panier (un produit avec sa quantité)
    /// <summary>
    /// Entité représentant une ligne de panier : un produit et une quantité associée.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Une ligne référence l'en-tête via <see cref="CartHeaderId"/> (relation n-1).
    /// - <see cref="ProductId"/> identifie le produit commandé ; la navigation <see cref="Product"/> facilite les Includes EF Core.
    /// - La validation de <see cref="Count"/> (Range) sécurise les valeurs avant persistance.
    /// </remarks>
    public class CartDetails : BaseEntity
    {
        // ID de l'en-tête du panier (clé étrangère)
        [Required]
        [ForeignKey("CartHeader")]
        public Guid CartHeaderId { get; set; }

        // ID du produit ajouté au panier
        public Guid ProductId { get; set; }

        // Quantité du produit (entre 1 et 100)
        [Required]
        [Range(1, 100, ErrorMessage = "La quantité doit être entre 1 et 100.")]
        public int Count { get; set; }

        // Navigation vers le panier parent
        public CartHeader CartHeader { get; set; }

        // Navigation vers le produit
        public Formation_Ecommerce_11_2025.Core.Entities.Product.Product Product { get; set; }
    }
}
