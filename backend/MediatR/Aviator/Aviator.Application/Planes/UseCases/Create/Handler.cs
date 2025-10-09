using MediatR;

namespace Aviator.Application.Planes.UseCases.Create;

public class Handler : IRequestHandler<Command, Response>
{
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken = default)
    {
        await Task.Delay(10);
        return new Response("Plane created");
    }
}
