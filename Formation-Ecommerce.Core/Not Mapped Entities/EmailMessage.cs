using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Core.Not_Mapped_Entities
{
    /// <summary>
    /// Modèle technique (non mappé EF) représentant un email à envoyer (destinataire, sujet, corps, pièces jointes).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - "Not Mapped" : cette classe n'est pas une entité persistée, c'est un objet de transport pour l'envoi d'email.
    /// - Elle est consommée par <see cref="Formation_Ecommerce_11_2025.Core.Interfaces.External.Mailing.IEmailSender"/>.
    /// - Isoler ce modèle simplifie les tests et évite de coupler l'email à des ViewModels ou entités du domaine.
    /// </remarks>
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
        public List<string> Attachments { get; set; } = new List<string>();
    }
}
