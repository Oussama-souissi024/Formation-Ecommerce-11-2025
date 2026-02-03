using Formation_Ecommerce_11_2025.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formation_Ecommerce_11_2025.Core.Interfaces.Repositories
{
    /// <summary>
    /// Contrat de repository des commandes : définit les opérations de persistance/lecture pour les commandes (header + détails).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Modélisation header/détails : <see cref="OrderHeader"/> contient les infos globales, <see cref="OrderDetails"/> les lignes.
    /// - Certaines lectures demandent le graphe complet (header + détails + produit), d'où <see cref="GetByIdWithDetailsAsync"/>.
    /// - La mise à jour de statut (<see cref="UpdateStatusAsync"/>) illustre le workflow d'une commande (Pending → Approved → ...).
    /// </remarks>
    public interface IOrderRepository
    {
        // OrderHeader
        Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader);
        Task<OrderHeader?> GetByIdAsync(Guid orderHeaderId);
        Task<OrderHeader?> GetByIdWithDetailsAsync(Guid orderHeaderId);
        IEnumerable<OrderHeader> GetAllAsync();
        IEnumerable<OrderHeader> GetAllByUserIdAsync(string userId);
        Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader);
        Task<bool> UpdateStatusAsync(Guid orderHeaderId, string newStatus);

        // OrderDetails
        Task<IEnumerable<OrderDetails>> AddOrderDetailsAsync(IEnumerable<OrderDetails> orderDetailsList);
    }
}
