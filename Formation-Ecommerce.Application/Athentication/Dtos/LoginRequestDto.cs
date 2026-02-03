using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO de requête de connexion : transporte l'email et le mot de passe saisis côté UI vers la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Un DTO (Data Transfer Object) sert à échanger des données entre couches sans exposer les entités du Core.
    /// - Ce DTO est typiquement mappé depuis un ViewModel MVC (ex: LoginViewModel) via AutoMapper.
    /// - La validation/contrôle d'accès réel est gérée par le service applicatif d'authentification.
    /// </remarks>
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
