using System.ComponentModel.DataAnnotations;
using Formation_Ecommerce_11_2025.Core.Common;

namespace Formation_Ecommerce_11_2025.Core.Entities.Category
{
    // Représente une catégorie de produits (ex: Électronique, Vêtements)
    /// <summary>
    /// Entité métier représentant une catégorie du catalogue (ex: "Électronique", "Vêtements").
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Couche Core : l'entité porte la structure des données et les contraintes de base (DataAnnotations).
    /// - L'héritage de <see cref="BaseEntity"/> apporte l'identifiant et les champs d'audit communs.
    /// - La navigation <see cref="Products"/> matérialise la relation 1-n (une catégorie contient plusieurs produits).
    /// </remarks>
    public class Category : BaseEntity
    {
        // Nom de la catégorie affiché dans le menu
        [Required(ErrorMessage = "Le nom de la catégorie est requis.")]
        [MaxLength(100, ErrorMessage = "Le nom de la catégorie ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; }

        // Description optionnelle de la catégorie
        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string Description { get; set; }

        // Collection des produits appartenant à cette catégorie
        public ICollection<Formation_Ecommerce_11_2025.Core.Entities.Product.Product> Products { get; set; }
    }
}
