namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>, ICommand<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketHandler(IBasketRepository _repository) 
    : ICommandHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await _repository.GetBasket(query.UserName, cancellationToken);

        return new GetBasketResult(basket);
    }
}