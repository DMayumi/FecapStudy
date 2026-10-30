namespace FecapStudy;

public class Discente : Usuario
{
    private string ra;
    private int xpTotal;
    private List<SessaoEstudo> historico = new List<SessaoEstudo>();

    public Discente(int id, string nome, string email, string senha, string ra)
        : base(id, nome, email, senha)
    {
        this.ra = ra;
        xpTotal = 0;
    }

    public string GetRa() { return ra; }
    public int GetXpTotal() { return xpTotal; }
    public int GetQuantidadeSessoes() { return historico.Count; }
    public void AdicionarXp(int pontos) { if (pontos > 0) xpTotal += pontos; }

    // Missão 5: as duas formas de atalho reutilizam a primeira sobrecarga.
    public void RegistrarEstudo(SessaoEstudo sessao)
    {
        historico.Add(sessao);
        AdicionarXp(sessao.CalcularXp());
    }

    public void RegistrarEstudo(Disciplina d, int minutos)
    {
        int ciclos = minutos / 25;
        if (ciclos < 1) ciclos = 1;
        RegistrarEstudo(new SessaoPomodoro(d, minutos, ciclos));
    }

    public void RegistrarEstudo(Disciplina d, int horaInicio, int horaFim, int paginasLidas)
    {
        if (horaInicio < 0 || horaInicio > 23 || horaFim <= horaInicio || horaFim > 24)
            throw new ArgumentException("Informe um intervalo de horas válido no mesmo dia.");

        int minutos = (horaFim - horaInicio) * 60;
        RegistrarEstudo(new SessaoLeitura(d, minutos, paginasLidas, true));
    }

    public string ExibirHistorico()
    {
        string texto = "";
        foreach (SessaoEstudo sessao in historico)
            texto += sessao.GerarFeedback() + Environment.NewLine;
        return texto;
    }

    public override string ObterTipoPerfil() { return "Discente"; }
    public override string GerarPainelResumo()
    {
        return $"Discente: {GetNome()} | RA: {ra} | XP: {xpTotal}";
    }
}
