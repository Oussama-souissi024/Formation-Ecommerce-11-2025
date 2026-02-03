namespace Formation_Ecommerce_11_2025.Models.Auth
{
    /// <summary>
    /// ViewModel du formulaire de connexion : transporte les identifiants saisis par l'utilisateur.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - En MVC, un ViewModel représente les données nécessaires à une vue (ici la page Login).
    /// - Ce modèle est volontairement minimal : l'authentification réelle est déléguée au service applicatif.
    /// - Il découple l'UI des DTOs Application afin d'éviter un couplage direct entre vues et logique métier.
    /// </remarks>
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
