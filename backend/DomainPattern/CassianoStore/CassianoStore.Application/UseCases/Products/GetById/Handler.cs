using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Repositories;
using CassianoStore.Domain.Specifications.Products;
using MediatR;

namespace CassianoStore.Application.UseCases.Products.GetById;

public sealed class Handler(IProductRepository repository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken ct)
    {
        var spec = new GetProductByIdSpecification(request.id);
        var product = await repository.GetByIdAsync(spec, ct);

        return product is null
            ? Result.Failure<Response>(new Error("404", "Product Not Found"))
            : Result.Success(new Response(product.Id, product.Title));
    }
}
