using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
using EShop.Dtos.Order.Models;
using EShop.Dtos.User.Dtos;
using EShop.Implementations.Core.Utils;

namespace EShop.Implementations.Core.Domain;

public class OrderService : IOrderService
{
    private readonly IBasketProductRepository _basketProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMailService _mailService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBasketRepository _basketRepository;

    public OrderService(IBasketProductRepository basketProductRepository, IOrderRepository orderRepository, IMailService mailService,
        ICategoryRepository categoryRepository, IBasketRepository basketRepository)
    {
        _basketProductRepository = basketProductRepository;
        _orderRepository = orderRepository;
        _mailService = mailService;
        _categoryRepository = categoryRepository;
        _basketRepository = basketRepository;
    }

    public async Task<long?> CreateOrderAsync(CreateOrderModel model)
    {
        try {
            var basket = await _basketRepository.GetOneAsync(x => x.UserId == model.UserId && x.IsDeleted == false);
            
            var products =
                await _basketProductRepository.GetAllAsync(x => x.BasketId == basket.Id && x.IsDeleted == false,
                    x => x.Basket);

            var order = new Order() {
                PaymentType = model.PaymentType,
                ShippingMethodId = model.ShippingMethodId,
                AddressId = model.AddressId,
                Status = OrderStatus.New,
                UserId = products.First().Basket.UserId,
                OrderNumber = await GenerateOrderNumber(),
                OrderProduct = products.Select(x => new OrderProduct() {
                    Count = x.Count,
                    ProductId = x.ProductId,
                }).ToHashSet()
            };

            await _orderRepository.AddAsync(order);

            await _orderRepository.SaveChangesAsync();

            try {
                await _mailService.SendOrderCreatedMailAsync(order);
            } catch {
            }

            return order.Id;
        } catch (Exception) {
            return null;
        }
    }

    public async Task ChangeStatusOfOrderAsync(ChangeOrderStatusModel model)
    {
        var order = await _orderRepository.GetOneAsync(model.OrderId, x => x.User, x => x.OrderProduct);

        order.Status = model.Status;

        await _orderRepository.SaveChangesAsync();

        await _mailService.SendOrderStatusChangedMailAsync(order);
    }

    private async Task<string> GenerateOrderNumber()
    {
        var lastOrderNumber = await _orderRepository.GetLastOrderNumberAsync();

        if (string.IsNullOrWhiteSpace(lastOrderNumber))
            return $"OR{new string('0', 5)}1";

        int number = int.Parse(lastOrderNumber.Substring(2));

        return $"OR{new string('0', 5 - (int)Math.Log10(number))}{number + 1}";
    }

    public async Task<OrderDto> GetOrderDetails(long orderId)
    {
        var order = await _orderRepository.GetOneAsync(orderId);

        return new OrderDto() {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            PaymentType = order.PaymentType,
            Address = MappingHelper.Mapper.Map<AddressDto>(order.Address),
            Items = MappingHelper.Mapper.Map<OrderItemDto[]>(order.OrderProduct),
            OrderStatus = order.Status,
            ShippingMethod = MappingHelper.Mapper.Map<ShippingMethodDto>(order.ShippingMethod)
        };
    }

    public async Task<IReadOnlyCollection<OrderDto>> GetUsersOrdersAsync(long userId)
    {
        var orders = await _orderRepository.GetAllAsync(x => x.IsDeleted == false && x.UserId == userId);

        if (orders.Count == 0) return Array.Empty<OrderDto>();
        
        return orders.OrderByDescending(x => x.InsertDateUtc).Select(order => new OrderDto() {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            PaymentType = order.PaymentType,
            Address = MappingHelper.Mapper.Map<AddressDto>(order.Address),
            Items = MappingHelper.Mapper.Map<OrderItemDto[]>(order.OrderProduct),
            OrderStatus = order.Status,
            ShippingMethod = MappingHelper.Mapper.Map<ShippingMethodDto>(order.ShippingMethod)
        }).ToList();
    }

    public async Task<IReadOnlyCollection<OrderDto>> GetUncompletedOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync(x => x.IsDeleted == false && x.Status != OrderStatus.Completed);

        if (orders.Count == 0) return Array.Empty<OrderDto>();
        
        return orders.OrderByDescending(x => x.InsertDateUtc).Select(order => new OrderDto() {
            OrderId = order.Id,
            OrderNumber = order.OrderNumber,
            PaymentType = order.PaymentType,
            Address = MappingHelper.Mapper.Map<AddressDto>(order.Address),
            Items = MappingHelper.Mapper.Map<OrderItemDto[]>(order.OrderProduct),
            OrderStatus = order.Status,
            ShippingMethod = MappingHelper.Mapper.Map<ShippingMethodDto>(order.ShippingMethod)
        }).ToList();
    }

    public async Task SetNextStatusAsync(long orderId)
    {
        var order = await _orderRepository.GetOneAsync(orderId);

        if (order.Status != OrderStatus.Cancelled)
            order.Status += 1;
        else order.Status = OrderStatus.New;
        await _orderRepository.SaveChangesAsync();
        
        try {
            await _mailService.SendOrderStatusChangedMailAsync(order);
        } catch {
        }
    }

    public async Task CancelOrderAsync(long orderId)
    {
        var order = await _orderRepository.GetOneAsync(orderId);

        order.Status = OrderStatus.Cancelled;
        await _orderRepository.SaveChangesAsync();
        
        try {
            await _mailService.SendOrderStatusChangedMailAsync(order);
        } catch {
        }
    }
}