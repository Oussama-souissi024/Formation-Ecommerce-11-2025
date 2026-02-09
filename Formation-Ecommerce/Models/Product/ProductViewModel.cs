using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Product
{
    /// <summary>
    /// ViewModel de lecture d'un produit : informations principales affichées en liste (nom, prix, catégorie).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce modèle est pensé pour l'affichage : il ne contient pas nécessairement tous les champs de l'entité Produit.
    /// - <see cref="CategoryName"/> illustre un champ "dénormalisé" utile à la vue.
    /// - Le mapping depuis des DTOs/entités est généralement fait via AutoMapper.
    /// </remarks>
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nom du produit")]
        public string Name { get; set; }

        [Display(Name = "Prix")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Catégorie")]
        public string CategoryName { get; set; }

    }
}
