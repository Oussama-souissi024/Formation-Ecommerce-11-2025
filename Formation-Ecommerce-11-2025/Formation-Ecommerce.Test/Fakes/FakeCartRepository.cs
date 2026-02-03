using Formation_Ecommerce_11_2025.Core.Entities.Cart;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake (en mémoire) de <see cref="ICartRepository" />.
/// </summary>
/// <remarks>
/// Le panier est composé de :
/// - CartHeader (1) : l'en-tête du panier (user, coupon, total...)
/// - CartDetails (n) : les lignes du panier (produit, quantité)
/// 
/// Ici on simule ces tables avec deux dictionnaires.
/// </remarks>
public class FakeCartRepository : ICartRepository
{
    private readonly Dictionary<Guid, CartHeader> _headers = new();
    private readonly Dictionary<Guid, CartDetails> _details = new();

    public Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId)
    {
        var header = _headers.Values.FirstOrDefault(h => h.UserID == userId);
        if (header != null)
        {
            header.CartDetails = _details.Values.Where(d => d.CartHeaderId == header.Id).ToList();
        }
        return Task.FromResult<CartHeader?>(header);
    }

    public Task<CartHeader?> GetCartHeaderByCartHeaderIdAsync(Guid cartHeaderId)
    {
        _headers.TryGetValue(cartHeaderId, out var header);
        if (header != null)
        {
            header.CartDetails = _details.Values.Where(d => d.CartHeaderId == header.Id).ToList();
        }
        return Task.FromResult<CartHeader?>(header);
    }

    public Task<CartHeader> AddCartHeaderAsync(CartHeader cartHeader)
    {
        if (cartHeader.Id == Guid.Empty)
        {
            cartHeader.Id = Guid.NewGuid();
        }
        _headers[cartHeader.Id] = cartHeader;
        return Task.FromResult(cartHeader);
    }

    public Task<CartHeader?> UpdateCartHeaderAsync(CartHeader cartHeader)
    {
        _headers[cartHeader.Id] = cartHeader;
        return Task.FromResult<CartHeader?>(cartHeader);
    }

    public Task<CartHeader?> RemoveCartHeaderAsync(CartHeader cartHeader)
    {
        _headers.Remove(cartHeader.Id);
        return Task.FromResult<CartHeader?>(cartHeader);
    }

    public Task<IEnumerable<CartDetails>> GetListCartDetailsByCartHeaderIdAsync(Guid cartHeaderId)
    {
        var list = _details.Values.Where(d => d.CartHeaderId == cartHeaderId).ToList();
        return Task.FromResult<IEnumerable<CartDetails>>(list);
    }

    public Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails)
    {
        if (cartDetails.Id == Guid.Empty)
        {
            cartDetails.Id = Guid.NewGuid();
        }
        _details[cartDetails.Id] = cartDetails;
        return Task.FromResult(cartDetails);
    }

    public Task<CartDetails?> UpdateCartDetailsAsync(CartDetails cartDetails)
    {
        _details[cartDetails.Id] = cartDetails;
        return Task.FromResult<CartDetails?>(cartDetails);
    }

    public Task<CartDetails?> RemoveCartDetailsAsync(CartDetails cartDetails)
    {
        _details.Remove(cartDetails.Id);
        return Task.FromResult<CartDetails?>(cartDetails);
    }

    public Task<CartDetails?> GetCartDetailsByCartHeaderIdAndProductId(Guid cartHeaderId, Guid productId)
    {
        var detail = _details.Values.FirstOrDefault(d => d.CartHeaderId == cartHeaderId && d.ProductId == productId);
        return Task.FromResult<CartDetails?>(detail);
    }

    public Task<CartDetails?> GetCartDetailsByCartDetailsId(Guid cartDetailsId)
    {
        _details.TryGetValue(cartDetailsId, out var detail);
        return Task.FromResult<CartDetails?>(detail);
    }

    public Task<bool> ClearCartAsync(string userId)
    {
        var header = _headers.Values.FirstOrDefault(h => h.UserID == userId);
        if (header == null) return Task.FromResult(false);

        var toRemove = _details.Values.Where(d => d.CartHeaderId == header.Id).Select(d => d.Id).ToList();
        foreach (var id in toRemove)
        {
            _details.Remove(id);
        }

        return Task.FromResult(true);
    }

    public Task<int> TotalCountofCartItemAsync(Guid cartHeaderId)
    {
        var count = _details.Values.Count(d => d.CartHeaderId == cartHeaderId);
        return Task.FromResult(count);
    }
}
