using System;
using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Models.Product;

namespace Formation_Ecommerce_11_2025.Mapping.Product
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : mapping entre les ViewModels MVC de produits et les DTOs produits de la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le mapping gère plusieurs scénarios UI : liste, confirmation de suppression, création et mise à jour.
    /// - Les <c>ForMember</c> illustrent la transformation de champs (ex: convertir <c>Guid?</c> en <c>Guid</c> avec valeur par défaut).
    /// - <c>Ignore()</c> sur certains champs empêche d'écraser des valeurs qui doivent être fournies par l'UI (ex: CategoryId lors de l'édition).
    /// </remarks>
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // DTO <-> ViewModel for listing
            CreateMap<ProductDto, ProductViewModel>().ReverseMap();

            // DTO -> ViewModel for delete confirmation
            CreateMap<ProductDto, DeleteProductViewModel>();

            // Create ViewModel -> Create DTO
            CreateMap<CreateProductViewModel, CreateProductDto>()
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryId.HasValue ? src.CategoryId.Value : Guid.Empty));

            // DTO -> Update ViewModel for edit form (CategoryId set from UI)
            CreateMap<ProductDto, UpdateProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? string.Empty))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName ?? string.Empty))
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());

            // Update ViewModel -> Update DTO for update action
            CreateMap<UpdateProductViewModel, UpdateProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? string.Empty))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.HasValue ? src.Price.Value : 0))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count.HasValue ? src.Count.Value : 1))
                .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.ImageFile))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId.HasValue ? src.CategoryId.Value : Guid.Empty));
        }
    }
}
