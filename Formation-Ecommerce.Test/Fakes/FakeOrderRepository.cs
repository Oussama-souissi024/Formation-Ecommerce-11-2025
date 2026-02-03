using Formation_Ecommerce_11_2025.Core.Entities.Orders;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake (en mémoire) de <see cref="IOrderRepository" />.
/// </summary>
/// <remarks>
/// Objectif pédagogique :
/// - Simuler la création et la lecture de commandes sans dépendre d'EF Core.
/// - Avoir un stockage simple pour les OrderHeader et OrderDetails.
/// </remarks>
public class FakeOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, OrderHeader> _headers = new();
    private readonly List<OrderDetails> _details = new();

    public Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader)
    {
        if (orderHeader.Id == Guid.Empty)
        {
            orderHeader.Id = Guid.NewGuid();
        }

        _headers[orderHeader.Id] = orderHeader;
        return Task.FromResult(orderHeader);
    }

    public Task<OrderHeader?> GetByIdAsync(Guid orderHeaderId)
    {
        _headers.TryGetValue(orderHeaderId, out var header);
        return Task.FromResult<OrderHeader?>(header);
    }

    public Task<OrderHeader?> GetByIdWithDetailsAsync(Guid orderHeaderId)
    {
        _headers.TryGetValue(orderHeaderId, out var header);
        if (header != null)
        {
            header.OrderDetails = _details.Where(d => d.OrderHeaderId == header.Id).ToList();
        }
        return Task.FromResult<OrderHeader?>(header);
    }

    public IEnumerable<OrderHeader> GetAllAsync() => _headers.Values.ToList();

    public IEnumerable<OrderHeader> GetAllByUserIdAsync(string userId) => _headers.Values.Where(h => h.UserId == userId).ToList();

    public Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader)
    {
        _headers[orderHeader.Id] = orderHeader;
        return Task.FromResult<OrderHeader?>(orderHeader);
    }

    public Task<bool> UpdateStatusAsync(Guid orderHeaderId, string newStatus)
    {
        if (!_headers.TryGetValue(orderHeaderId, out var header))
        {
            return Task.FromResult(false);
        }

        header.Status = newStatus;
        return Task.FromResult(true);
    }

    public Task<IEnumerable<OrderDetails>> AddOrderDetailsAsync(IEnumerable<OrderDetails> orderDetailsList)
    {
        var list = orderDetailsList.ToList();
        foreach (var d in list)
        {
            if (d.Id == Guid.Empty)
            {
                d.Id = Guid.NewGuid();
            }
            _details.Add(d);
        }
        return Task.FromResult<IEnumerable<OrderDetails>>(list);
    }
}
