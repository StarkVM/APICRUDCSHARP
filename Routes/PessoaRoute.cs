using Pessoa.Models;
using Pessoa.Data;
using Microsoft.EntityFrameworkCore;

namespace Pessoa.Routes;

public static class PessoaRoute
{
    public static void PessoaRoutes(this WebApplication app)
    {
        var route = app.MapGroup("pessoa");

        route.MapPost("", 
            async (PessoaRequest req, PessoaContext context) =>
            {
                var pessoa = new PessoaModel(req.nome);
                await context.AddAsync(pessoa);
                await context.SaveChangesAsync();
            });

        route.MapGet("", 
            async (PessoaContext contex) =>
            {
                var pessoas = await contex.Pessoas.ToListAsync();
                return Results.Ok(pessoas);

            });

        route.MapPut("{id:guid}", 
            async (Guid id, PessoaRequest req, PessoaContext context) =>
            {
                var pessoa = await context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);

                if (pessoa == null)
                {
                    return Results.NotFound();
                }
                
                pessoa.MudarNome(req.nome);
                
                await context.SaveChangesAsync();

                return Results.Ok(pessoa);
            }
        );

        route.MapDelete("{id:guid}", 
            async(Guid id, PessoaContext context) =>
        {
            var pessoa = await context.Pessoas.FirstOrDefaultAsync(x => x.Id == id);
            
            if (pessoa == null)
            {
                return Results.NotFound();
            }
            
            pessoa.SetInativoNome();
                
            await context.SaveChangesAsync();
            
            return Results.Ok(pessoa);   
        });

    }


}