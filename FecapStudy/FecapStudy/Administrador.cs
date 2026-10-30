namespace FecapStudy;

public class Administrador : Usuario
{
    private string cargo;

    public Administrador(int id, string nome, string email, string senha, string cargo)
        : base(id, nome, email, senha)
    {
        this.cargo = cargo;
    }

    public string GetCargo() { return cargo; }
    public override string ObterTipoPerfil() { return "Administrador"; }
    public override string GerarPainelResumo()
    {
        return $"{ObterTipoPerfil()}: {GetNome()} | Cargo: {cargo}";
    }
}
