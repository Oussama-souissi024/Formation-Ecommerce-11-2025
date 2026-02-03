using Formation_Ecommerce_11_2025.Core.Entities.Coupon;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories.Base;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.Repositories
{
    // Interface qui définit les opérations spécifiques pour les coupons de réduction, hérite de l'interface générique IRepository
    /// <summary>
    /// Contrat de repository pour les coupons : définit les opérations de persistance et de recherche des codes promo.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le coupon est souvent recherché par son code (<see cref="ReadByCouponCodeAsync"/>), pas uniquement par son Id.
    /// - Hériter du repository générique permet de factoriser le CRUD, tout en ajoutant des besoins spécifiques.
    /// - Les règles métier (validité, expiration, unicité, etc.) restent côté Application ; ici on gère l'accès aux données.
    /// </remarks>
    public interface ICouponRepository : IRepository<Coupon>
    {
        // Ajoute un nouveau coupon à la base de données
        Task<Coupon> AddAsync(Coupon coupon);
        
        // Récupère un coupon par son identifiant
        Task<Coupon> ReadByIdAsync(Guid couponId);
        
        // Récupère un coupon par son code de réduction
        Task<Coupon> ReadByCouponCodeAsync(string couponCode);
        
        // Récupère tous les coupons existants
        Task<IEnumerable<Coupon>> ReadAllAsync();
        
        // Met à jour un coupon existant
        Task UpdateAsync(Coupon coupon);
        
        // Supprime un coupon par son identifiant
        Task DeleteAsync(Guid id);
    }
}
