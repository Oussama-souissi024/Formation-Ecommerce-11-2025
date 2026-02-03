using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Application.Products.Dtos
{
    /// <summary>
    /// DTO de création de produit : données nécessaires pour ajouter un produit au catalogue (incluant image et catégorie).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Séparer Create DTO du DTO de lecture permet de contrôler les champs acceptés lors de la création.
    /// - <see cref="ImageFile"/> représente un fichier uploadé ; la gestion du stockage est prise en charge par Infrastructure.
    /// - <see cref="CategoryID"/> est la clé de liaison vers la catégorie choisie côté UI.
    /// </remarks>
    public class CreateProductDto
    {
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryID { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
