using Formation_Ecommerce_11_2025.Core.Not_Mapped_Entities;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.External.Mailing
{
    /// <summary>
    /// Contrat d'envoi d'emails : abstraction Core permettant à la couche Application/Web de déclencher des emails sans dépendre d'un fournisseur.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le Core expose un contrat ; l'implémentation concrète (SMTP/MailKit, etc.) vit dans Infrastructure.
    /// - Les méthodes "métier" (welcome, confirmation, reset) représentent des cas d'usage fréquents en e-commerce.
    /// - Le paramètre <see cref="EmailMessage"/> évite de coupler l'API d'envoi à des ViewModels ou à des entités EF.
    /// </remarks>
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage emailMessage);
        Task SendWelcomeEmailAsync(string email, string username);
        Task SendPasswordResetEmailAsync(string email, string resetToken, string userId);
        Task SendEmailConfirmationAsync(string email, string confirmationToken, string userId);
    }
}
