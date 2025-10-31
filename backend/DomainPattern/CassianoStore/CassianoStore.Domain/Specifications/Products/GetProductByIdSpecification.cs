using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Entities;
using System.Linq.Expressions;

namespace CassianoStore.Domain.Specifications.Products;

public class GetProductByIdSpecification(Guid id) : Specification<Product>
{
    public override Expression<Func<Product, bool>> ToExpresstion()
        => product => product.Id == id;
}
