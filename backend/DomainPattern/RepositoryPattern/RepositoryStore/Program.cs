using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repository;
using RepositoryStore.Repository.Abstractions;

var builder = WebApplication.CreateBuilder(args);

var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(ConnectionStrings);
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

app.MapGet("/v1/products", async (CancellationToken token, IProductRepository repository, int skip = 0, int take = 25)
    => Results.Ok(await repository.GetAllAsync(skip, take, token)));

app.MapGet("/v1/products/{id}", async (CancellationToken token, IProductRepository repository, int id)
    => Results.Ok(await repository.GetByIdAsync(id)));

app.MapPost("/v1/products", async (CancellationToken token, IProductRepository repository, Product product)
    => Results.Ok(await repository.CreateAsynt(product, token)));

app.MapPut("/v1/products", async (CancellationToken token, IProductRepository repository, Product product)
    => Results.Ok(await repository.UpdateAsync(product, token)));

app.MapDelete("/v1/products/{id}", async (CancellationToken token, IProductRepository repository, int id) =>
{
    var product = await repository.GetByIdAsync(id, token);
    return product is null ? Results.NotFound() : Results.Ok(await repository.DeleteAsync(product, token));
});

app.Run();
