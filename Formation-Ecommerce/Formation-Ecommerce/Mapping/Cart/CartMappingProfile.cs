using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;
using Formation_Ecommerce_11_2025.Application.Orders.Dtos;
using Formation_Ecommerce_11_2025.Models.Cart;

namespace Formation_Ecommerce_11_2025.Mapping.Cart
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : mapping entre DTOs de panier/commande (Application) et ViewModels utilisés par les vues MVC.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Les vues Panier/Checkout consomment des ViewModels, tandis que la couche Application fournit des DTOs (<see cref="CartDto"/>).
    /// - Ce profil mappe aussi panier → commande : le checkout transforme un panier en <see cref="OrderHeaderDto"/> et <see cref="OrderDetailsDto"/>.
    /// - Les <c>ForMember(... Ignore())</c> évitent d'écraser les identifiants lors de la création d'une nouvelle commande.
    /// </remarks>
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartDto, CartIndexViewModel>().ReverseMap();
            CreateMap<CartDto, CheckoutCartViewModel>().ReverseMap();
            CreateMap<CartHeaderDto, OrderHeaderDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : (src.Price ?? 0m)));
        }
    }
}
