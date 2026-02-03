using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Models.Home;

namespace Formation_Ecommerce_11_2025.Mapping.Home
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : mapping entre les produits affichés sur la page d'accueil et les DTOs produits de la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La page Home (catalogue) utilise un ViewModel optimisé pour l'affichage (<see cref="HomeProductViewModel"/>).
    /// - La couche Application expose des DTOs (ex: <see cref="ProductDto"/>) ; ce profil assure la conversion bidirectionnelle.
    /// - Le mapping réduit le couplage entre UI et couche applicative et garde les contrôleurs plus "minces".
    /// </remarks>
    public class HomeMappingProfile :Profile 
    {
        public HomeMappingProfile()
        {
            CreateMap<HomeProductViewModel, ProductDto>().ReverseMap();
        }
    }
}
