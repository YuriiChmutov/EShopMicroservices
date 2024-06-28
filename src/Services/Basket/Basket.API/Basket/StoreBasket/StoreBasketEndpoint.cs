namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

// public record StoreBasketResponse(bool Success);
public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/basket",
            async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();
                
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
    }
}