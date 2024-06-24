namespace Catalog.API.Products.DeleteProductById;

public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;

public record DeleteProductByIdResult(bool Success);

public class DeleteProductByIdCommandValidator : AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
    }
}

internal class DeleteProductByIdCommandHandler : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
{
    private readonly IDocumentSession _session;

    public DeleteProductByIdCommandHandler(
        IDocumentSession session)
    {
        _session = session;
    }

    public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        _session.Delete<Product>(command.Id);

        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdResult(true);
    }
}