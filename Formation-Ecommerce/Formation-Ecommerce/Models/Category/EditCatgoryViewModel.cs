namespace Formation_Ecommerce_11_2025.Models.Category
{
    /// <summary>
    /// ViewModel du formulaire d'édition de catégorie : porte l'identifiant et les champs modifiables.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le champ <see cref="Id"/> identifie la catégorie à modifier.
    /// - Ce ViewModel sert au binding MVC lors du POST (édition) et permet de contrôler les champs éditables.
    /// - La mise à jour est ensuite déléguée au service applicatif, pas traitée directement dans la vue.
    /// </remarks>
    public class EditCatgoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
