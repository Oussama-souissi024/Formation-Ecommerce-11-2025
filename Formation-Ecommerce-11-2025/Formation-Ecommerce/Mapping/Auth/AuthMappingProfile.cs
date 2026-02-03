using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Athentication.Dtos;
using Formation_Ecommerce_11_2025.Models.Auth;

namespace Formation_Ecommerce_11_2025.Mapping.Auth
{
    /// <summary>
    /// Profil AutoMapper (couche Web) : définit les conversions entre ViewModels MVC d'authentification et DTOs de la couche Application.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le contrôleur MVC manipule des ViewModels (UI), tandis que la couche Application manipule des DTOs (contrats applicatifs).
    /// - AutoMapper automatise ces conversions pour éviter du code répétitif (mapping manuel) dans les contrôleurs.
    /// - Ce profil est enregistré au démarrage (Program.cs) via <c>AddAutoMapper</c>.
    /// </remarks>
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterViewModel, RegistrationRequestDto>();
            CreateMap<LoginViewModel, LoginRequestDto>();
            CreateMap<ResetPasswordViewModel, ResetPasswordDto>();
        }
    }
}
