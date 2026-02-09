namespace Formation_Ecommerce_11_2025.Models.Auth
{
    /// <summary>
    /// ViewModel du formulaire d'inscription : données saisies pour créer un nouveau compte.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le contrôleur MVC reçoit ce ViewModel, valide le ModelState, puis délègue l'inscription au service Application.
    /// - Le champ <see cref="Role"/> permet de sélectionner un rôle lors de l'inscription (scénario pédagogique / admin).
    /// - Dans un projet réel, on ajoute souvent des DataAnnotations (Required, EmailAddress, etc.) pour la validation UI.
    /// </remarks>
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
