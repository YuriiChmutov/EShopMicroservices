using MediatR;

namespace BuildingBlocks.SQRS;

public interface ICommand : ICommand<Unit>
{

}
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}