using System;

namespace Formation_Ecommerce_11_2025.Application.Orders.Dtos
{
    /// <summary>
    /// DTO ligne de commande : représente un article commandé (produit, quantité, prix) pour affichage et transport.
    /// </summary>
    /// <remarks>
    /// Points pédagogiques :
    /// - <see cref="Price"/> est un prix "historisé" : il correspond au prix au moment de l'achat.
    /// - Les champs <see cref="ProductName"/> et <see cref="ProductUrl"/> sont utiles à l'UI sans nécessiter de navigation EF.
    /// - Ce DTO est construit/mappé par la couche Application à partir des entités et repositories.
    /// </remarks>
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
