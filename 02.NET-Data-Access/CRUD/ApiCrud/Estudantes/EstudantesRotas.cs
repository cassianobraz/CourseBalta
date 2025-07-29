using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes;

public static class EstudantesRotas
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        var rotasEstudantes = app.MapGroup("estudantes");

        rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDbContext context) =>
        {
            var isExist = await context.Estudantes.AnyAsync(x => x.Nome == request.Nome);

            if (isExist)
                return Results.Conflict("Estudante já cadastrado.");

            var novoEstudante = new Estudante(request.Nome);

            await context.Estudantes.AddAsync(novoEstudante);
            await context.SaveChangesAsync();

            var estudanteRetorno = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);

            return Results.Ok(estudanteRetorno);
        });

        rotasEstudantes.MapGet("", async (AppDbContext context) =>
        {
            var estudantes = await context.Estudantes
            .Where(e => e.Ativo)
            .Select(e => new EstudanteDto(e.Id, e.Nome))
            .ToListAsync();

            return estudantes;
        });

        rotasEstudantes.MapPut("{id:guid}", async (Guid id, UpdateEstudanteRequest request, AppDbContext context) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(e => e.Id == id);

            if (estudante is null)
                return Results.NotFound("Estudante não encontrado.");

            estudante.update(request.Nome);

            await context.SaveChangesAsync();
            return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
        });

        rotasEstudantes.MapDelete("{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(e => e.Id == id);

            if (estudante is null)
                return Results.NotFound("Estudante não encontrado.");

            estudante.Desativar();

            await context.SaveChangesAsync();
            return Results.Ok(new EstudanteDto(estudante.Id, estudante.Nome));
        });
    }
}
