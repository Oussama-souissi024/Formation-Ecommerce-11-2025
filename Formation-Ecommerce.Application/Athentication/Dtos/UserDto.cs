using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Application.Athentication.Dtos
{
    /// <summary>
    /// DTO utilisateur : projection des informations d'un compte (Identity) pour l'affichage ou des écrans d'administration.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce DTO évite d'exposer directement <c>ApplicationUser</c> (Identity) à l'UI.
    /// - Il peut agréger des informations supplémentaires (ex: liste de rôles) utiles à l'affichage.
    /// - La source de vérité reste Identity ; ce modèle sert uniquement au transport/présentation.
    /// </remarks>
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
    }
}
