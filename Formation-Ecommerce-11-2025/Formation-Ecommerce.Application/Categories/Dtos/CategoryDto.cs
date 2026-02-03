using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Categories.Dtos
{
    /// <summary>
    /// DTO de catégorie (lecture) : projection des champs d'une catégorie pour transport entre couches.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les DTOs évitent d'exposer directement les entités Core à la couche Web.
    /// - Les DataAnnotations peuvent servir à valider des entrées côté serveur (ModelState) même sur des DTOs.
    /// - Ce DTO est utilisé pour les listes/détails et peut être mappé vers des ViewModels MVC.
    /// </remarks>
    public class CategoryDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
