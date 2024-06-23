namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool Success);

internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(
        IDocumentSession session, 
        ILogger<UpdateProductHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var product = await _session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        _session.Update(product);
        await _session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}