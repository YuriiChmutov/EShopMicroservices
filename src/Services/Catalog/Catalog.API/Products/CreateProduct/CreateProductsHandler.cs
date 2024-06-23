namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name, List<string> Category, string Description, string ImageFile, decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(
        IDocumentSession session, 
        ILogger<CreateProductCommandHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateProductCommandHandler.Handle called with {@Query}", command);
        
        // 1. create Product entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO
        // 2. save to db

        _session.Store(product);
        await _session.SaveChangesAsync(cancellationToken);


        // 3. return CreateProductResult result

        return new CreateProductResult(product.Id);
    }
}