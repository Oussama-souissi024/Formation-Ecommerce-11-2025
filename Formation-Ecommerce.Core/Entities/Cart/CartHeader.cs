using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Formation_Ecommerce_11_2025.Core.Common;
using Formation_Ecommerce_11_2025.Core.Entities.Identity;

namespace Formation_Ecommerce_11_2025.Core.Entities.Cart
{
    // Représente l'en-tête d'un panier d'achat (un utilisateur = un panier actif)
    /// <summary>
    /// Entité "en-tête" du panier : porte l'identité du propriétaire et les informations globales (coupon).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Modélisation classique d'un panier : un header (1) + des lignes <see cref="CartDetails"/> (n).
    /// - La clé étrangère <see cref="UserID"/> relie le panier à <see cref="ApplicationUser"/> (ASP.NET Identity).
    /// - Un coupon est optionnel : <see cref="CouponId"/> peut être null, ce qui évite de créer des enregistrements inutiles.
    /// </remarks>
    public class CartHeader : BaseEntity
    {
        // ID de l'utilisateur propriétaire du panier (clé étrangère)
        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }

        // ID du coupon appliqué (optionnel)
        [ForeignKey("Coupon")]
        public Guid? CouponId { get; set; }

        // Code du coupon de réduction appliqué
        public string? CouponCode { get; set; }

        // Navigation vers l'utilisateur du panier
        public ApplicationUser User { get; set; }

        // Navigation vers le coupon appliqué
        public Formation_Ecommerce_11_2025.Core.Entities.Coupon.Coupon Coupon { get; set; }

        // Collection des lignes du panier (produits + quantités)
        public ICollection<CartDetails>? CartDetails { get; set; }
    }
}
