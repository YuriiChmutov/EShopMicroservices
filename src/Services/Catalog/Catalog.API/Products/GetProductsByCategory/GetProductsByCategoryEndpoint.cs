namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductsByCategoryRequest(string Category);

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products by Category")
            .WithDescription("Get products by category");

        //app.MapGet("/products",
        //    async (HttpContext context, ISender sender) =>
        //    {
        //        var category = context.Request.Query["category"].ToString();
        //        if (string.IsNullOrEmpty(category))
        //        {
        //            return Results.BadRequest("Category parameter is required.");
        //        }

        //        var result = await sender.Send(new GetProductsByCategoryQuery(category));
        //        var response = result.Adapt<GetProductsByCategoryResponse>();

        //        return Results.Ok(response);
        //    })
        //    .WithName("GetProductsByCategory")
        //    .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        //    .ProducesProblem(StatusCodes.Status400BadRequest)
        //    .WithSummary("Get Products by Category")
        //    .WithDescription("Get products by category");
    }
}