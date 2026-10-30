namespace FecapStudy;

public class SessaoLeitura : SessaoEstudo
{
    private int paginasLidas;
    private bool fezResumo;

    public SessaoLeitura(Disciplina disciplina, int duracaoMinutos, int paginasLidas, bool fezResumo)
        : base(disciplina, duracaoMinutos)
    {
        if (paginasLidas < 0) throw new ArgumentException("As páginas não podem ser negativas.");
        this.paginasLidas = paginasLidas;
        this.fezResumo = fezResumo;
    }

    public override string ObterModalidade() { return "Leitura"; }
    public override int CalcularXp()
    {
        return GetDuracaoMinutos() + paginasLidas * 3 + (fezResumo ? 25 : 0);
    }
}
