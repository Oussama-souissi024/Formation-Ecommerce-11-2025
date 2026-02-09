namespace Formation_Ecommerce_11_2025.Models.Category
{
    /// <summary>
    /// ViewModel de confirmation de suppression d'une catégorie : affiche les informations avant suppression.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Souvent utilisé sur une vue "Delete" pour afficher un récapitulatif avant un POST de confirmation.
    /// - Le ViewModel évite d'exposer l'entité Core à la vue tout en affichant les champs nécessaires.
    /// - En pratique, la suppression réelle est effectuée via le service applicatif / repository.
    /// </remarks>
    public class DeleteCategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
