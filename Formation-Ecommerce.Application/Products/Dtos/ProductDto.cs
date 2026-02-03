using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Application.Products.Dtos
{
    /// <summary>
    /// DTO produit (lecture) : projection d'un produit du catalogue pour transport et affichage côté UI.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce DTO est souvent utilisé pour les listes/détails : il expose les champs nécessaires à l'UI.
    /// - <see cref="CategoryName"/> est un champ utile à l'affichage (dénormalisé) sans nécessiter un graphe EF.
    /// - <see cref="ImageFile"/> peut être utilisé côté UI lors des formulaires ; la persistance du fichier est externalisée.
    /// </remarks>
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Count { get; set; } = 1;
        [Display(Name = "Catégorie")]
        public string CategoryName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
