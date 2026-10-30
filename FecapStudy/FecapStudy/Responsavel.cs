namespace FecapStudy;

public class Responsavel : Usuario
{
    private string parentesco;
    private List<Discente> discentes = new List<Discente>();
    private Dictionary<Discente, MetaEstudo> metas = new Dictionary<Discente, MetaEstudo>();

    public Responsavel(int id, string nome, string email, string senha, string parentesco)
        : base(id, nome, email, senha)
    {
        this.parentesco = parentesco;
    }

    public string GetParentesco() { return parentesco; }
    public int GetQuantidadeDiscentes() { return discentes.Count; }

    // Missão 7: recebe um discente que já existe fora do responsável.
    public bool VincularDiscente(Discente d)
    {
        foreach (Discente existente in discentes)
        {
            if (existente.GetId() == d.GetId()) return false;
        }
        discentes.Add(d);
        return true;
    }

    public bool DefinirMeta(Discente d, MetaEstudo meta)
    {
        if (!discentes.Contains(d)) return false;
        metas[d] = meta;
        return true;
    }

    public MetaEstudo? GetMeta(Discente d)
    {
        if (metas.ContainsKey(d)) return metas[d];
        return null;
    }

    public override string ObterTipoPerfil() { return "Responsavel"; }
    public override string GerarPainelResumo()
    {
        return $"Responsável: {GetNome()} | Parentesco: {parentesco} | Discentes: {discentes.Count}";
    }
}
