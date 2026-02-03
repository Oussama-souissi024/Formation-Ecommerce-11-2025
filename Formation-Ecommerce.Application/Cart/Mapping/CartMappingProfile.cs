using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;
using Formation_Ecommerce_11_2025.Core.Entities.Cart;

namespace Formation_Ecommerce_11_2025.Application.Cart.Mapping
{
    /// <summary>
    /// Profil AutoMapper pour le panier : définit les conversions entre entités Core (CartHeader/CartDetails) et DTOs.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le panier est composé de deux agrégats : header (infos générales) + details (lignes).
    /// - Le mapping bidirectionnel (ReverseMap) simplifie les scénarios de lecture et d'écriture (upsert).
    /// </remarks>
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            // Map CartHeader <-> CartHeaderDto
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();

            // Map CartDetails <-> CartDetailsDto
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
