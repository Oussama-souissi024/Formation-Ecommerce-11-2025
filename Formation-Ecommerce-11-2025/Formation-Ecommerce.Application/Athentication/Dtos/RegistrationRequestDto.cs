using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO de requête d'inscription : données nécessaires pour créer un compte utilisateur (email, téléphone, mot de passe, rôle).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La couche Web mappe un ViewModel (RegisterViewModel) vers ce DTO avant d'appeler <see cref="Formation_Ecommerce_11_2025.Application.Athentication.Interfaces.IAuthService"/>.
    /// - Le mot de passe est transmis ici car la création du compte est orchestrée par la couche Application (via Identity en Infrastructure).
    /// - Le champ Role est souvent optionnel et dépend du scénario (formation/admin).
    /// </remarks>
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
