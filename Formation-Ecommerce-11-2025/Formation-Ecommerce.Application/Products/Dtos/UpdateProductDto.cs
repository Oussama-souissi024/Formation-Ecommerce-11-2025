using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Application.Products.Dtos
{
    /// <summary>
    /// DTO de mise à jour de produit : porte l'identifiant et les champs éditables (incluant image optionnelle).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Inclure <see cref="Id"/> permet d'identifier le produit à modifier (pattern CRUD).
    /// - <see cref="ImageFile"/> est optionnel : l'UI peut ne pas remplacer l'image existante.
    /// - Les règles métier (ex: gestion d'images, validations) sont orchestrées par le service applicatif.
    /// </remarks>
    public class UpdateProductDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public int? Count { get; set; } = 1;
        public IFormFile? ImageFile { get; set; }
    }
}
