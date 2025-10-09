using Aviator.Application;
using MediatR;
using CreatePlaneCommand = Aviator.Application.Planes.UseCases.Create.Command;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();

var app = builder.Build();

app.MapPost("/v1/planes", async (ISender handler, CreatePlaneCommand command) =>
{
    var result = await handler.Send(command);
    return Results.Created("/v1/planes", result);
});

app.Run();
