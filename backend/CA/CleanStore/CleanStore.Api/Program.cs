using CleanStore.Application;
using CleanStore.Infrastructure.SharedContext.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "";
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapPost("/v1/accounts", async (
    ISender sender,
    CleanStore.Application.AccountContext.UseCases.Create.Command command) =>
{
    var result = await sender.Send(command);
    return TypedResults.Created($"v1/accounts/{result.Value.Id}", result);
});

app.Run();
