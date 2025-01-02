using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order)
    : ICommand<UpdateOrderResult>;
 
public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(p => p.Order.Id).NotNull().WithMessage("Id is required");
        RuleFor(p => p.Order.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(p => p.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
    }
}