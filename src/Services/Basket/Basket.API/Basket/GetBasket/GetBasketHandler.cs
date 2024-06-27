namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string userName) : IQuery<GetBasketResult>, ICommand<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketHandler : ICommandHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // 1. get basket from db
        //var basket = _repository.GetBasket(request.UserName);

        return new GetBasketResult(new ShoppingCart("swn"));
    }
}