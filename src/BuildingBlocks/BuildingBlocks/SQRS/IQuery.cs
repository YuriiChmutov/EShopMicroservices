using MediatR;

namespace BuildingBlocks.SQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}