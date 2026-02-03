using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Product
{
    /// <summary>
    /// ViewModel de confirmation de suppression d'un produit : affiche un récapitulatif avant suppression.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Utilisé sur une vue "Delete" afin d'éviter une suppression accidentelle.
    /// - Contient les informations nécessaires à l'utilisateur (nom, prix, image, catégorie).
    /// - L'opération de suppression (base + éventuelle image) est orchestrée dans la couche Application/Infrastructure.
    /// </remarks>
    public class DeleteProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nom du produit")]
        public string Name { get; set; }

        [Display(Name = "Prix")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal? Price { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Catégorie")]
        public string? CategoryName { get; set; }
    }
}
