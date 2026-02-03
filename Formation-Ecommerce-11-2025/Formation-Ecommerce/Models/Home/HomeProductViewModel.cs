namespace Formation_Ecommerce_11_2025.Models.Home
{
    /// <summary>
    /// ViewModel produit pour la page d'accueil (catalogue) : informations affichées + quantité pour l'ajout au panier.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Ce ViewModel est adapté à l'UI : il regroupe les champs utiles à l'affichage (nom, prix, image, catégorie).
    /// - <see cref="Count"/> représente la quantité souhaitée lors de l'ajout au panier.
    /// - Le modèle peut contenir des champs spécifiques à la vue (ex: <see cref="Image"/>) sans impacter le domaine.
    /// </remarks>
    public class HomeProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; } = 1;
        public IFormFile? Image { get; set; }
    }
}
