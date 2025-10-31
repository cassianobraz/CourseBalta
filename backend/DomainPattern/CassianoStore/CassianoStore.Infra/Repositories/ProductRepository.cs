using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Entities;
using CassianoStore.Domain.Repositories;
using CassianoStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CassianoStore.Infra.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Specification<Product> specification, CancellationToken ct = default)
        => await context
            .Products
            .Where(specification.ToExpresstion())
            .FirstOrDefaultAsync(ct);

    public async Task CreateAsync(Product product, CancellationToken ct)
        => await context.Products.AddAsync(product, ct);
}
