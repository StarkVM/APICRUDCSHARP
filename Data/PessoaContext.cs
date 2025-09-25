using Microsoft.EntityFrameworkCore;
using Pessoa.Models;

namespace Pessoa.Data;
//dotnet tool install --global dotnet-ef --version 8.*
//dotnet ef database update
public class PessoaContext : DbContext
{
    public DbSet<PessoaModel> Pessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        var path = Path.Combine(AppContext.BaseDirectory, "Pessoa.sqLite");
        optionsBuilder.UseSqlite($"Data Source={path}");
        base.OnConfiguring(optionsBuilder);
    }

}
