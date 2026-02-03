using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Models.Category;

namespace Formation_Ecommerce_11_2025.Mapping.Category
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : mapping entre les ViewModels MVC de catégorie et les DTOs de la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les vues MVC consomment des ViewModels (UI), tandis que la couche Application expose des DTOs (contrats applicatifs).
    /// - Ce profil centralise les conversions (liste, création, édition, suppression) pour garder le contrôleur "mince".
    /// - Le mapping est enregistré au démarrage via <c>AddAutoMapper</c>.
    /// </remarks>
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Mapping pour CategoryViewModel <-> CategoryDto
            CreateMap<CategoryViewModel, CategoryDto>().ReverseMap();

            CreateMap<DeleteCategoryViewModel, CategoryDto>().ReverseMap();

            CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
            CreateMap<EditCatgoryViewModel, UpdateCategoryDto>().ReverseMap();
        }
    }
}
