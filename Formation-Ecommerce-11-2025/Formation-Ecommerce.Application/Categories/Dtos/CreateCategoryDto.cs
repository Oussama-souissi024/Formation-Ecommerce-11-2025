using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Categories.Dtos
{
    /// <summary>
    /// DTO de création de catégorie : données nécessaires pour créer une nouvelle catégorie.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Séparer Create/Update DTO permet de contrôler les champs acceptés selon le cas d'usage.
    /// - Les DataAnnotations expriment des contraintes basiques (Required, StringLength) et alimentent le ModelState.
    /// - La couche Application orchestre la création et délègue la persistance au repository (Infrastructure).
    /// </remarks>
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
