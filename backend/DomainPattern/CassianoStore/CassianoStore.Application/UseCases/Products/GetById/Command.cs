using CassianoStore.Domain.Abstractions;
using MediatR;

namespace CassianoStore.Application.UseCases.Products.GetById;

public sealed record Command(Guid id) : IRequest<Result<Response>>;
