using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Coupons.Dtos;
using Formation_Ecommerce_11_2025.Models.Coupon;

namespace Formation_Ecommerce_11_2025.Mapping.Coupon
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : définit les conversions entre ViewModels MVC de coupons et DTOs de la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le contrôleur MVC manipule des ViewModels (UI), alors que les services applicatifs manipulent des DTOs.
    /// - Centraliser le mapping évite du code répétitif dans les actions MVC (Create/Edit/Delete).
    /// - <c>ReverseMap()</c> permet la conversion dans les deux sens lorsque nécessaire.
    /// </remarks>
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<CouponDto, CouponViewModel>().ReverseMap();
            CreateMap<CreateCouponViewModel, CouponDto>().ReverseMap();
            CreateMap<DeleteCouponViewModel, CouponDto>().ReverseMap();
        }
    }
}
