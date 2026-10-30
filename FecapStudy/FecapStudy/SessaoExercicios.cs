namespace FecapStudy;

public class SessaoExercicios : SessaoEstudo
{
    private int totalQuestoes;
    private int acertos;

    public SessaoExercicios(Disciplina disciplina, int duracaoMinutos, int totalQuestoes, int acertos)
        : base(disciplina, duracaoMinutos)
    {
        if (totalQuestoes <= 0 || acertos < 0 || acertos > totalQuestoes)
            throw new ArgumentException("Informe questões e acertos válidos.");
        this.totalQuestoes = totalQuestoes;
        this.acertos = acertos;
    }

    public override string ObterModalidade() { return "Exercicios"; }
    public override int CalcularXp() { return GetDuracaoMinutos() + acertos * 10; }
    public override string GerarFeedback()
    {
        double percentual = acertos * 100.0 / totalQuestoes;
        return base.GerarFeedback() + $" | Acertos: {percentual:F1}%";
    }
}
