using System.ComponentModel.DataAnnotations;

namespace Formation_Ecommerce_11_2025.Models.Auth
{
    /// <summary>
    /// ViewModel du formulaire de réinitialisation de mot de passe : porte l'identité utilisateur, le token et le nouveau mot de passe.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le token est généré côté serveur (Identity) et sert à prouver que l'utilisateur est autorisé à changer le mot de passe.
    /// - Les DataAnnotations (Required, StringLength, Compare) permettent de valider le formulaire côté serveur.
    /// - Ce ViewModel est spécifique à l'UI (vue MVC) et ne doit pas être réutilisé comme modèle de persistance.
    /// </remarks>
    public class ResetPasswordViewModel
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
