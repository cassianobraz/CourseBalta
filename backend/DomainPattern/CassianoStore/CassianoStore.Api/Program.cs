using CassianoStore.Application;
using CassianoStore.Infra;
using CassianoStore.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connectionString, b
        => b.MigrationsAssembly("CassianoStore.Api"));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("v1/products/{id}", async (ISender sender, Guid id, CancellationToken ct) =>
{
    var command = new CassianoStore.Application.UseCases.Products.GetById.Command(id);
    var result = await sender.Send(command, ct);

    return result.IsSuccess
        ? Results.Ok(result.Value)
        : Results.BadRequest(result.Error);
});

app.MapPost("v1/products", async (
    ISender sender, 
    CassianoStore.Application.UseCases.Products.Create.Command command, 
    CancellationToken ct) =>
{
    var result = await sender.Send(command, ct);

    return result.IsSuccess
        ? Results.Ok(result.Value)
        : Results.BadRequest(result.Error);
});

app.Run();
