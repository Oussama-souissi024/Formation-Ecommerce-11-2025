using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Orders.Dtos;
using Formation_Ecommerce_11_2025.Application.Orders.Mapping;
using Formation_Ecommerce_11_2025.Application.Orders.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires complémentaires de <c>OrderServices</c> (détails, lecture avec détails, filtre par utilisateur).
/// </summary>
public class OrderServicesMoreTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderMappingProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task AddOrderDetailsAsync_creates_details_and_returns_list()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderServices(repo, CreateMapper());

        var header = await repo.AddOrderHeaderAsync(new Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderHeader
        {
            UserId = "u1",
            OrderTime = DateTime.UtcNow,
            Status = "Pending",
            OrderDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderDetails>()
        });

        var details = new List<OrderDetailsDto>
        {
            new OrderDetailsDto { OrderHeaderId = header.Id, ProductId = Guid.NewGuid(), Price = 5m, Count = 1, ProductName = "P", ProductUrl = "" }
        };

        var result = await service.AddOrderDetailsAsync(details);

        Assert.NotNull(result);
        Assert.Single(result!);
        Assert.NotEqual(Guid.Empty, result!.First().Id);
    }

    [Fact]
    public async Task GetOrderWithDetailsByIdAsync_returns_order_with_details()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderServices(repo, CreateMapper());

        var header = await repo.AddOrderHeaderAsync(new Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderHeader
        {
            UserId = "u1",
            OrderTime = DateTime.UtcNow,
            Status = "Pending",
            OrderDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderDetails>()
        });

        await repo.AddOrderDetailsAsync(new[]
        {
            new Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderDetails
            {
                OrderHeaderId = header.Id,
                ProductId = Guid.NewGuid(),
                Price = 5m,
                Count = 2
            }
        });

        var dto = await service.GetOrderWithDetailsByIdAsync(header.Id);

        Assert.NotNull(dto);
        Assert.NotNull(dto!.OrderDetails);
        Assert.Single(dto.OrderDetails);
    }

    [Fact]
    public void GetOrdersByUserIdAsync_returns_only_user_orders()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderServices(repo, CreateMapper());

        repo.AddOrderHeaderAsync(new Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderHeader { UserId = "u1", OrderTime = DateTime.UtcNow, Status = "Pending", OrderDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderDetails>() }).GetAwaiter().GetResult();
        repo.AddOrderHeaderAsync(new Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderHeader { UserId = "u2", OrderTime = DateTime.UtcNow, Status = "Pending", OrderDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Orders.OrderDetails>() }).GetAwaiter().GetResult();

        var orders = service.GetOrdersByUserIdAsync("u1");

        Assert.Single(orders);
        Assert.Equal("u1", orders.First().UserId);
    }
}
