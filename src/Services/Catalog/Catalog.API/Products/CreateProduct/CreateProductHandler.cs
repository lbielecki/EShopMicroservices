namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
     public CreateProductCommandValidator()
     {
         RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
         RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
         RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
         RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater 0");
     }
}

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IDocumentSession _session;

    public CreateProductCommandHandler(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Category = command.Category
        };
        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }
}   