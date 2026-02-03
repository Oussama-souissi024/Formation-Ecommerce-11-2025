using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO de réponse de connexion : informations minimales renvoyées après authentification (identité + rôle).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce DTO représente un résultat applicatif, pas une entité persistée.
    /// - Il permet de retourner des informations d'utilisateur sans exposer l'objet Identity complet.
    /// - Le rôle est souvent utilisé côté Web pour adapter l'UI (menus, autorisations) et/ou stocker des claims.
    /// </remarks>
    public class LoginResponseDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Role { get; set; }
    }
}
