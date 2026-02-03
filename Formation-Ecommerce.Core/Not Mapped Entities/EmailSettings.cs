using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Core.Not_Mapped_Entities
{
    /// <summary>
    /// Modèle de configuration SMTP (non mappé EF) utilisé pour paramétrer l'envoi d'emails.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce type est typiquement lié à une section de configuration (appsettings) et injecté via IOptions côté Infrastructure.
    /// - Centraliser ces paramètres évite de disperser les valeurs SMTP dans le code.
    /// </remarks>
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public bool EnableSsl { get; set; }
    }
}
