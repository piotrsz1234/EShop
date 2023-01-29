using EShop.Core.Common.Enums;
using EShop.Core.Domain;
using EShop.Core.Entities;
using EShop.Core.Extensions;
using EShop.Core.Infrastructure.Repositories;
using EShop.Dtos.Order.Dtos;
using EShop.Dtos.Order.Models;

namespace EShop.Implementations.Core.Domain;

public class OrderService : IOrderService
{
    private readonly IBasketProductRepository _basketProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMailService _mailService;
    private readonly ICategoryRepository _categoryRepository;

    public OrderService(IBasketProductRepository basketProductRepository, IOrderRepository orderRepository, IMailService mailService,
        ICategoryRepository categoryRepository)
    {
        _basketProductRepository = basketProductRepository;
        _orderRepository = orderRepository;
        _mailService = mailService;
        _categoryRepository = categoryRepository;
    }

    public async Task<long?> CreateOrderAsync(CreateOrderModel model)
    {
        try {
            var products =
                await _basketProductRepository.GetAllAsync(x => x.BasketId == model.BasketId && x.IsDeleted == false,
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
}