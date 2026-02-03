using Formation_Ecommerce_11_2025.Core.Entities.Cart;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrat de persistance du panier : définit les opérations spécifiques au panier (header/détails, clear, total count).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Le panier est modélisé avec un <see cref="CartHeader"/> (infos globales) et des <see cref="CartDetails"/> (lignes).
    /// - Les méthodes sont orientées cas d'usage (ex: récupérer le panier d'un utilisateur, vider le panier).
    /// - L'implémentation EF Core vit en Infrastructure ; la couche Application consomme ce contrat.
    /// </remarks>
    public interface ICartRepository
    {
        Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId);
        Task<CartHeader?> GetCartHeaderByCartHeaderIdAsync(Guid cartHeaderId);

        Task<CartHeader> AddCartHeaderAsync(CartHeader cartHeader);
        Task<CartHeader?> UpdateCartHeaderAsync(CartHeader cartHeader);
        Task<CartHeader?> RemoveCartHeaderAsync(CartHeader cartHeader);

        Task<IEnumerable<CartDetails>> GetListCartDetailsByCartHeaderIdAsync(Guid cartHeaderId);

        Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails);
        Task<CartDetails?> UpdateCartDetailsAsync(CartDetails cartDetails);
        Task<CartDetails?> RemoveCartDetailsAsync(CartDetails cartDetails);

        Task<CartDetails?> GetCartDetailsByCartHeaderIdAndProductId(Guid cartHeaderId, Guid productId);
        Task<CartDetails?> GetCartDetailsByCartDetailsId(Guid cartDetailsId);

        Task<bool> ClearCartAsync(string userId);
        Task<int> TotalCountofCartItemAsync(Guid cartHeaderId);
    }
}
