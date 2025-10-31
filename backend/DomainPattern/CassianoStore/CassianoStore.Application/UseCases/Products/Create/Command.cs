using CassianoStore.Domain.Abstractions;
using MediatR;

namespace CassianoStore.Application.UseCases.Products.Create;

public record Command(string Title) : IRequest<Result<Response>>;