var builder = WebApplication.CreateBuilder(args);

// services to container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// pipeline

app.MapCarter();

app.Run();
