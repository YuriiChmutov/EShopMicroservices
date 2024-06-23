namespace Catalog.API.Products.DeleteProductById;

public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;

public record DeleteProductByIdResult(bool Success);


internal class DeleteProductByIdCommandHandler : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<DeleteProductByIdCommandHandler> _logger;

    public DeleteProductByIdCommandHandler(
        IDocumentSession session, 
        ILogger<DeleteProductByIdCommandHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteProductByIdHandler.Handle called with {@Command}", command);

        _session.Delete<Product>(command.Id);

        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdResult(true);
    }
}