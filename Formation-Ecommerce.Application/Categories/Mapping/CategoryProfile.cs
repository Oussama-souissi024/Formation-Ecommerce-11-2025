using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Core.Entities.Category;

namespace Formation_Ecommerce_11_2025.Application.Categories.Mapping
{
    /// <summary>
    /// Profil AutoMapper pour les catégories : définit les règles de conversion entre entités Core et DTOs Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - AutoMapper centralise les transformations (mapping) pour éviter des affectations manuelles répétitives.
    /// - On distingue les DTOs de lecture (<see cref="CategoryDto"/>) et ceux de commande (create/update).
    /// </remarks>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Mapping between entity and DTO
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Mapping for creation operation
            CreateMap<CreateCategoryDto, Category>();

            // Mapping for update operation
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
