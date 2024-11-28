namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    {
        _session = session;
        _logger = logger;
    }
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}", query);
        
        var products = await _session.Query<Product>().ToListAsync(cancellationToken);
        
        return new GetProductResult(products);
    }
}