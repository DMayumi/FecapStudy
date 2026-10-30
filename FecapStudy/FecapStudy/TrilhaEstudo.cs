namespace FecapStudy;

// Missão 7: as disciplinas são recebidas prontas (agregação).
public class TrilhaEstudo
{
    private string titulo;
    private Administrador administrador;
    private List<Disciplina> disciplinas = new List<Disciplina>();

    public TrilhaEstudo(string titulo, Administrador administrador)
    {
        this.titulo = titulo;
        this.administrador = administrador;
    }

    public string GetTitulo() { return titulo; }
    public Administrador GetAdministrador() { return administrador; }
    public int GetQuantidadeDisciplinas() { return disciplinas.Count; }

    public void AdicionarDisciplina(Disciplina d)
    {
        foreach (Disciplina existente in disciplinas)
            if (existente.GetCodigo() == d.GetCodigo()) return;
        disciplinas.Add(d);
    }

    public int CalcularCargaHorariaTotal()
    {
        int total = 0;
        foreach (Disciplina d in disciplinas) total += d.GetCargaHorariaSemanalHoras();
        return total;
    }
}
