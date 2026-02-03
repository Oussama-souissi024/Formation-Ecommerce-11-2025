namespace Formation_Ecommerce_11_2025.Models
{
    /// <summary>
    /// ViewModel de la page d'erreur : expose un identifiant de requête pour diagnostiquer un incident.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Utilisé par la vue Error.cshtml pour afficher un message et un RequestId.
    /// - La propriété calculée <see cref="ShowRequestId"/> permet de n'afficher l'Id que s'il est disponible.
    /// - Dans un environnement de production, le RequestId facilite le rapprochement avec les logs.
    /// </remarks>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
