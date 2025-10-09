using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes;

public static class EstudantesRotas
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        var rotasEstudantes = app.MapGroup("estudantes");

        rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
        {
            var isExist = await context.Estudantes.AnyAsync(x => x.Nome == request.Nome, ct);

            if (isExist)
                return Results.Conflict("Estudante já cadastrado.");

            var novoEstudante = new Estudante(request.Nome);

            await context.Estudantes.AddAsync(novoEstudante, ct);
            await context.SaveChangesAsync(ct);

            var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);

            return Results.Ok(estudanteRetorno);
        });

        rotasEstudantes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
        {
            var estudantes = await context.Estudantes
            .Where(e => e.Ativo)
            .Select(e => new EstudanteDto(e.Id, e.Nome))
            .ToListAsync(ct);

            return estudantes;
        });

        rotasEstudantes.MapPut("{id:guid}", async (Guid id, UpdateEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(e => e.Id == id, ct);

            if (estudante is null)
                return Results.NotFound("Estudante não encontrado.");

            estudante.update(request.Nome);

            await context.SaveChangesAsync(ct);
            return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
        });

        rotasEstudantes.MapDelete("{id:guid}", async (Guid id, AppDbContext context, CancellationToken ct) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(e => e.Id == id, ct);

            if (estudante is null)
                return Results.NotFound("Estudante não encontrado.");

            estudante.Desativar();

            await context.SaveChangesAsync(ct);
            return Results.Ok();
        });
    }
}
