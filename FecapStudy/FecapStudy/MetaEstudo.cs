using System.Text.Json;

namespace FecapStudy;

// Missões 2 e 6: o avanço só muda pelo método que faz a validação.
public class MetaEstudo : IExportavelJson
{
    private int metaMinutosSemana;
    private int minutosCumpridos;

    public MetaEstudo(int metaMinutosSemana)
    {
        RedefinirMeta(metaMinutosSemana);
        minutosCumpridos = 0;
    }

    public int GetMetaMinutosSemana() { return metaMinutosSemana; }
    public int GetMinutosCumpridos() { return minutosCumpridos; }

    public bool RegistrarAvanco(int minutos)
    {
        if (minutos <= 0 || minutos > 480) return false;
        minutosCumpridos += minutos;
        return true;
    }

    public void RedefinirMeta(int novaMeta)
    {
        if (novaMeta < 30) novaMeta = 30;
        if (novaMeta > 3000) novaMeta = 3000;
        metaMinutosSemana = novaMeta;
    }

    public double CalcularPercentual()
    {
        double percentual = minutosCumpridos * 100.0 / metaMinutosSemana;
        return percentual > 100 ? 100 : percentual;
    }

    public bool EstaCumprida() { return minutosCumpridos >= metaMinutosSemana; }

    public string ParaJson()
    {
        return JsonSerializer.Serialize(new { metaMinutosSemana, minutosCumpridos });
    }
}
