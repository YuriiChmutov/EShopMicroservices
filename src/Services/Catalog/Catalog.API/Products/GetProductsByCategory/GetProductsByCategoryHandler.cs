using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductsByCategoryHandler> _logger;
    public GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsByCategoryHandler.Handle called with {@Query}", query);
        
        var products = await _session
            .Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}