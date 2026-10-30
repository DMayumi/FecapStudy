namespace FecapStudy;

public class SessaoPomodoro : SessaoEstudo
{
    private int ciclosCompletos;

    public SessaoPomodoro(Disciplina disciplina, int duracaoMinutos, int ciclosCompletos)
        : base(disciplina, duracaoMinutos)
    {
        if (ciclosCompletos < 0) throw new ArgumentException("Os ciclos não podem ser negativos.");
        this.ciclosCompletos = ciclosCompletos;
    }

    public override string ObterModalidade() { return "Pomodoro"; }
    public override int CalcularXp() { return GetDuracaoMinutos() * 2 + ciclosCompletos * 15; }
    public override string GerarFeedback()
    {
        return base.GerarFeedback() + $" | Ciclos concluídos: {ciclosCompletos}";
    }
}
