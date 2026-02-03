using System;
using System.Collections.Generic;

namespace Formation_Ecommerce_11_2025.Application.Orders.Dtos
{
    /// <summary>
    /// DTO d'en-tête de commande : informations globales d'une commande (client, montants, statut, date).
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - Modélisation header/détails : le header regroupe les données communes et les détails portent les lignes.
    /// - Ce DTO circule entre Application et Web pour afficher des listes/détails de commande.
    /// - Le statut est généralement une chaîne pilotée par des constantes (ex: StaticDetails) afin d'éviter les valeurs magiques.
    /// </remarks>
    public class OrderHeaderDto
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }
        public decimal? OrderTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public IEnumerable<OrderDetailsDto> OrderDetails { get; set; }
    }
}
