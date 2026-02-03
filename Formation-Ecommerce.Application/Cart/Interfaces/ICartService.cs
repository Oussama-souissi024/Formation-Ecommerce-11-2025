using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Cart.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Cart.Interfaces
{
    /// <summary>
    /// Contrat applicatif du panier : expose les cas d'usage (lecture, upsert, appliquer coupon, supprimer ligne, vider panier).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Couche Application : orchestre les opérations sur le panier et délègue la persistance aux repositories (Infrastructure).
    /// - Le contrôleur MVC consomme ce contrat afin d'éviter toute logique d'accès données dans la couche Web.
    /// - Les méthodes utilisent des DTOs (<see cref="CartDto"/>) pour ne pas exposer le modèle Core/EF à l'UI.
    /// </remarks>
    public interface ICartService
    {
        Task<CartDto?> GetCartByUserIdAsync(string userId);
        Task<CartDto> UpsertCartAsync(CartDto cartDto);
        Task<CartDto?> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> RemoveCartItemAsync(Guid cartDetailsId);
        Task<bool> ClearCartAsync(string userId);
    }
}
