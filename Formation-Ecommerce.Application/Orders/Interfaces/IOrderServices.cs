using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Formation_Ecommerce_11_2025.Application.Orders.Dtos;

namespace Formation_Ecommerce_11_2025.Application.Orders.Interfaces
{
    /// <summary>
    /// Contrat applicatif des commandes : expose les cas d'usage de création, lecture et mise à jour de statut d'une commande.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - La commande est modélisée en header/détails : création de l'en-tête puis des lignes associées.
    /// - Les lectures peuvent être "simples" (header) ou inclure les détails (<see cref="GetOrderWithDetailsByIdAsync"/>).
    /// - La mise à jour de statut illustre un workflow (Pending → Approved → ReadyForPickup → Completed, etc.).
    /// </remarks>
    public interface IOrderServices
    {
        Task<OrderHeaderDto?> AddOrderHeaderAsync(OrderHeaderDto orderHeaderDto);
        Task<IEnumerable<OrderDetailsDto>?> AddOrderDetailsAsync(IEnumerable<OrderDetailsDto> orderDetailsDtoList);

        IEnumerable<OrderHeaderDto> GetAllOrdersAsync();
        IEnumerable<OrderHeaderDto> GetOrdersByUserIdAsync(string userId);
        Task<OrderHeaderDto?> GetOrderByIdAsync(Guid orderHeaderId);
        Task<OrderHeaderDto?> GetOrderWithDetailsByIdAsync(Guid orderHeaderId);

        Task<bool?> UpdateOrderStatusAsync(Guid orderHeaderId, string newStatus);
        Task<OrderHeaderDto?> UpdateOrderHeaderAsync(OrderHeaderDto orderHeaderDto);
    }
}
