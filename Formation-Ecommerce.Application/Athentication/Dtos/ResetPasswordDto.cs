using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO de réinitialisation de mot de passe : porte l'identifiant utilisateur, le token et le nouveau mot de passe.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le token est généré par Identity (Infrastructure) et valide l'autorisation de changer le mot de passe.
    /// - La validation de base (Required, Compare, etc.) est faite via DataAnnotations.
    /// - Ce DTO circule entre Web et Application ; il ne doit pas être persisté en base.
    /// </remarks>
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit contenir au moins {2} caractères et au maximum {1} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et la confirmation du mot de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }
}
