using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Category
{
    /// <summary>
    /// ViewModel du formulaire de création de catégorie : données saisies par l'utilisateur (nom + description).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les DataAnnotations (Required, MaxLength, etc.) valident le formulaire côté serveur (ModelState).
    /// - Ce ViewModel est propre à l'écran de création : on évite de réutiliser directement l'entité Core.
    /// - Après validation, le contrôleur délègue la création au service applicatif (Application layer).
    /// </remarks>
    public class CreateCategoryViewModel
    {
        // Nom de la catégorie affiché dans le menu
        [Required(ErrorMessage = "Le nom de la catégorie est requis.")]
        [MaxLength(100, ErrorMessage = "Le nom de la catégorie ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; }

        // Description optionnelle de la catégorie
        [Required(ErrorMessage = "La description de la catégorie est requis.")]
        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string Description { get; set; }

    }
}
