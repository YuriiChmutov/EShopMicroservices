using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddBehavior(typeof(ValidationBehavior<,>));
});

var app = builder.Build();

app.MapCarter();


app.Run();
