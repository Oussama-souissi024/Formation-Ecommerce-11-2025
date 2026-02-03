using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Athentication.Dtos;
using Formation_Ecommerce_11_2025.Core.Entities.Identity;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Interfaces
{
    /// <summary>
    /// Contrat applicatif d'authentification : expose les cas d'usage liés aux comptes (register/login/logout, rôles, tokens).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Couche Application : orchestre les règles et le workflow, puis délègue la technique à des repositories (Infrastructure).
    /// - Les contrôleurs MVC consomment ce contrat afin de rester "minces" (pas de logique Identity dans la couche Web).
    /// - Les méthodes de tokens (confirmation email, reset password) s'appuient généralement sur ASP.NET Identity.
    /// </remarks>
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> Logout();
        Task<bool> AssingnRole(string email, string roleName);

        // Méthodes pour la gestion des utilisateurs
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(string userId);

        // Méthodes pour la confirmation d'email
        Task<string> GenerateEmailConfirmationToken(string userId);
        Task<bool> ConfirmEmail(string userId, string token);

        // Méthodes pour la réinitialisation de mot de passe
        Task<string> GeneratePasswordResetToken(string userId);
        Task<bool> ResetPassword(string userId, string token, string newPassword);

        Task<bool?> CheckConfirmedEmail(string email);
    }
}
