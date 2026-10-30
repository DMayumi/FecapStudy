namespace FecapStudy;

// Dados recebidos da API; as regras de negócio continuam nas classes de domínio.
public class StatusServidor
{
    public bool Online { get; set; }
    public bool UsandoMySql { get; set; }
    public string ModoBanco { get; set; } = "";
}

public class SessaoServidor
{
    public int Id { get; set; }
    public string DiscenteNome { get; set; } = "";
    public string Disciplina { get; set; } = "";
    public string TipoModalidade { get; set; } = "";
    public int DuracaoMinutos { get; set; }
    public int XpGanho { get; set; }
    public string ResumoFeedback { get; set; } = "";
    public string DataRegistro { get; set; } = "";
}

public class MetaServidor
{
    public string DiscenteNome { get; set; } = "";
    public string ResponsavelNome { get; set; } = "";
    public int MetaMinutosSemana { get; set; }
    public int MinutosCumpridos { get; set; }
}

public class RelatorioServidor
{
    public string Discente { get; set; } = "";
    public int TotalSessoes { get; set; }
    public int TotalMinutosEstudados { get; set; }
    public int TotalXpAcumulado { get; set; }
    public int MetaMinutosSemana { get; set; }
    public double PercentualMetaConcluida { get; set; }
    public string StatusAcompanhamento { get; set; } = "";
}
