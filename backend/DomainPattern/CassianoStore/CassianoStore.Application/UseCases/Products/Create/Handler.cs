using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Entities;
using CassianoStore.Domain.Repositories;
using MediatR;

namespace CassianoStore.Application.UseCases.Products.Create;

public class Handler(IProductRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken ct)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
        };

        await repository.CreateAsync(product, ct);
        await unitOfWork.CommitAsync();

        return Result.Success(new Response("Producto criado com sucesso"));
    }
}
