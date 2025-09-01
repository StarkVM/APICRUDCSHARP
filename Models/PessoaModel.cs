namespace Pessoa.Models;

public class PessoaModel
{
    public Guid Id { get; init; }
    public string Nome { get; private set; }

    public PessoaModel(string nome)
    {
        Nome = nome;
        Id = Guid.NewGuid();
    }

    public void MudarNome(string nome)
    {
        Nome = nome;
    }
    
    public void SetInativoNome()
    {
        Nome = "Inativo";
    }
}