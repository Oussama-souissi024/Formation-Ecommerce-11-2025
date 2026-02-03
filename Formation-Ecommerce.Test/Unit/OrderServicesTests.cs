using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Orders.Dtos;
using Formation_Ecommerce_11_2025.Application.Orders.Mapping;
using Formation_Ecommerce_11_2025.Application.Orders.Services;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <c>OrderServices</c> (création de commande, mise à jour de statut).
/// </summary>
public class OrderServicesTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderMappingProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task AddOrderHeaderAsync_creates_order_header()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderServices(repo, CreateMapper());

        var result = await service.AddOrderHeaderAsync(new OrderHeaderDto
        {
            UserId = "u1",
            Status = "Pending",
            OrderTime = DateTime.UtcNow,
            OrderTotal = 10m,
            OrderDetails = new List<OrderDetailsDto>()
        });

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result!.Id);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_returns_false_when_order_missing()
    {
        var repo = new FakeOrderRepository();
        var service = new OrderServices(repo, CreateMapper());

        var ok = await service.UpdateOrderStatusAsync(Guid.NewGuid(), "Approved");

        Assert.False(ok);
    }

    [Fact]
    public async Task UpdateOrderStatusAsync_returns_true_when_order_exists()
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

        var ok = await service.UpdateOrderStatusAsync(header.Id, "Approved");

        Assert.True(ok);
    }
}
