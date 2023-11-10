using AutoMapper;
using Azure.Core;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;
using System.Collections.Generic;
using System.Threading;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderValidator validator = new OrderValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<OrderResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }

        var order = CreateOrder(request.Model);
        await unitOfWork.OrderRepository.InsertAsync(order, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        decimal totalOrderAmount = 0;

        foreach (var orderItemModel in request.Model.OrderItems)
        {
            Product product = (Product)unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == orderItemModel.ProductId);
            totalOrderAmount += (product.Price * orderItemModel.Quantity); 

            if (CheckInventory(product.StockQuantity, orderItemModel.Quantity))
            {
                OrderItem orderitem = AddOrderItem(order, product, orderItemModel.Quantity, cancellationToken);
                order.AllQuantity += orderitem.Quantity;
                order.OrderItems.Add(orderitem);
            }
            else
            {
                throw new InvalidOperationException($"There are not enough {product.ProductName} products in stock");
            }
        }

        order.TotalAmount = totalOrderAmount;

        order = OpenAccountOrder(order);
        order.InsertDate = DateTime.Now;

        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.CompleteAsync(cancellationToken);
        var response = mapper.Map<OrderResponse>(order);
        return new ApiResponse<OrderResponse>(response);
    }

    private Order CreateOrder(OrderRequest model)
    {
        return new Order
        {
            DealerId = sessionService.CheckSession().sessionId,
            TotalAmount = 0,
            OrderItems = new List<OrderItem>(),
            OpenAccountOrder = model.OpenAccountOrder
        };
    }

    private OrderItem AddOrderItem(Order order, Product product, int quantity, CancellationToken cancellationToken)
    {
        var sessionid = sessionService.CheckSession().sessionId;
        decimal divident = unitOfWork.DealerRepository.GetById(sessionid).Dividend;

        var orderitem = new OrderItem
        {
            Quantity = quantity,
            UnitPrice = product.Price + (product.Price * divident / 100),
            TotalPrice = quantity * (product.Price + (product.Price * divident / 100)),
            ProductId = product.Id,
        };

        UpdateInventory(product, quantity, cancellationToken);

        return orderitem;
    }

    private static bool CheckInventory(int stockQuantity, int requiredStock)
    {
        if(stockQuantity >= requiredStock)
        {
            return true;
        }
        return false;
    }

    private void UpdateInventory(Product product, int requiredStock, CancellationToken cancellationToken)
    {
        product.StockQuantity -= requiredStock;
        unitOfWork.ProductRepository.Update(product); ;
    }

    private Order OpenAccountOrder(Order order)
    {
        Dealer dealer = unitOfWork.DealerRepository.GetById(order.DealerId);

        if (order.OpenAccountOrder == true && dealer.OpenAccountLimit >= order.TotalAmount)
        {
            dealer.OpenAccountLimit -= order.TotalAmount;
            unitOfWork.DealerRepository.Update(dealer);
            order.PaymentStatus = true;
            return order;
        }
        else if (order.OpenAccountOrder == true && dealer.OpenAccountLimit <= order.TotalAmount)
        {
            throw new InvalidOperationException("Open Account Limit is not enough!");
        }
        else return order;
    }
}
