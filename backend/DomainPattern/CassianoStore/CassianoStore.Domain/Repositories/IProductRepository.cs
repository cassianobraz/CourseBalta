using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Entities;

namespace CassianoStore.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByIdAsync(Specification<Product> specification, CancellationToken ct = default);
    Task CreateAsync(Product product, CancellationToken ct);
}
