using Pessoa.Models;
using Pessoa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; // Necessário para [FromServices] e [FromBody]

namespace Pessoa.Routes;

public static class PessoaRoute
{
    public static void PessoaRoutes(this WebApplication app)
    {
        var route = app.MapGroup("pessoa");

        // POST - Contexto vem dos services, body vem do body
        route.MapPost("",
            async ([FromBody] PessoaRequest req,
                   [FromServices] PessoaContext context) =>
            {
                var pessoa = new PessoaModel(req.nome);
                await context.AddAsync(pessoa);
                await context.SaveChangesAsync();
                return Results.Ok(pessoa);
            });

        // GET - Apenas o contexto
        route.MapGet("",
            async ([FromServices] PessoaContext context) =>
            {
                var pessoas = await context.Pessoas.ToListAsync();
                return Results.Ok(pessoas);
            });

        // PUT - Guid + Body + Context
        route.MapPut("{id:guid}",
            async (Guid id,
                   [FromBody] PessoaRequest req,
                   [FromServices] PessoaContext context) =>
            {
                var pessoa = await context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);
                if (pessoa == null)
                    return Results.NotFound();

                pessoa.MudarNome(req.nome);
                await context.SaveChangesAsync();
                return Results.Ok(pessoa);
            });

        // DELETE - Guid + Context
        route.MapDelete("{id:guid}",
            async (Guid id,
                   [FromServices] PessoaContext context) =>
            {
                var pessoa = await context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);
                if (pessoa == null)
                    return Results.NotFound();

                pessoa.SetInativoNome();
                await context.SaveChangesAsync();
                return Results.Ok(pessoa);
            });
    }
}
