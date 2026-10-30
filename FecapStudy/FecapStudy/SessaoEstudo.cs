using System.Text.Json;

namespace FecapStudy;

// Missões 4 e 6: cada modalidade calcula seu próprio XP.
public abstract class SessaoEstudo : IExportavelJson
{
    private Disciplina disciplina;
    private int duracaoMinutos;

    protected SessaoEstudo(Disciplina disciplina, int duracaoMinutos)
    {
        if (duracaoMinutos <= 0 || duracaoMinutos > 480)
            throw new ArgumentException("A sessão deve durar entre 1 e 480 minutos.");
        this.disciplina = disciplina;
        this.duracaoMinutos = duracaoMinutos;
    }

    public Disciplina GetDisciplina() { return disciplina; }
    public int GetDuracaoMinutos() { return duracaoMinutos; }
    public abstract string ObterModalidade();
    public abstract int CalcularXp();

    public virtual string GerarFeedback()
    {
        return $"{ObterModalidade()} em {disciplina.GetNome()} ({duracaoMinutos} min) -> +{CalcularXp()} XP";
    }

    public string ParaJson()
    {
        return JsonSerializer.Serialize(new
        {
            disciplina = disciplina.GetNome(),
            tipoModalidade = ObterModalidade(),
            duracaoMinutos,
            xpGanho = CalcularXp(),
            resumoFeedback = GerarFeedback()
        });
    }
}
