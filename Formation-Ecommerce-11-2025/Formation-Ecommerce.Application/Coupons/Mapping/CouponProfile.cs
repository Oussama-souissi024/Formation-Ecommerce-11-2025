using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Core.Entities.Coupon;

namespace Formation_Ecommerce_11_2025.Application.Coupons.Mapping
{
    /// <summary>
    /// Profil AutoMapper pour les coupons : définit les règles de conversion entre entités Core et DTOs Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le mapping Entity ↔ DTO permet aux contrôleurs de manipuler des modèles simples (DTO/ViewModel) plutôt que des entités EF.
    /// - Les DTOs de création/mise à jour portent uniquement les champs nécessaires aux commandes.
    /// </remarks>
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            // Entity <-> DTO
            CreateMap<Coupon, CouponDto>();
            CreateMap<CouponDto, Coupon>();

            // Create/Update
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>();
        }
    }
}
