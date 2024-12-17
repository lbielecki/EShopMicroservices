using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.Order.OrderName).NotNull().WithMessage("Name is required");
        RuleFor(p => p.Order.CustomerId).NotNull().WithMessage("Customer Id is required");
        RuleFor(p => p.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}