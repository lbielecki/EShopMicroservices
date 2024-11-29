namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>
{
    private readonly IDocumentSession _session;

    public GetProductsQueryHandler(IDocumentSession session)
    {
        _session = session;
    }
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var products = await _session.Query<Product>().ToListAsync(cancellationToken);
        
        return new GetProductResult(products);
    }
}