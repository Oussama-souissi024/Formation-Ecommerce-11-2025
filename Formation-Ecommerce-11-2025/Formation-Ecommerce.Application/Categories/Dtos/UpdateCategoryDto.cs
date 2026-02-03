using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Categories.Dtos
{
    /// <summary>
    /// DTO de mise à jour de catégorie : porte l'identifiant et les champs modifiables.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Inclure <see cref="Id"/> permet d'identifier la ressource à modifier (pattern CRUD).
    /// - Cette séparation évite de réutiliser un DTO de création pour l'édition.
    /// - La couche Application décide des règles métier avant d'appeler le repository.
    /// </remarks>
    public class UpdateCategoryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
