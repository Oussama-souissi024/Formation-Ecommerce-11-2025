using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Category
{
    /// <summary>
    /// ViewModel de lecture d'une catégorie : données affichées dans les listes et les écrans de détail.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce ViewModel est destiné à l'UI (MVC) : il contient uniquement les champs utiles aux vues.
    /// - Il peut être issu d'un DTO (couche Application) puis mappé via AutoMapper.
    /// - Le fait de séparer ViewModels et entités du Core évite d'exposer le domaine directement à la vue.
    /// </remarks>
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
