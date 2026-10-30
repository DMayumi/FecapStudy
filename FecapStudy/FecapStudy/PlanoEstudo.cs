namespace FecapStudy;

// Missão 8: cria os itens internamente e não devolve suas referências.
public class PlanoEstudo
{
    private List<ItemCronograma> itens = new List<ItemCronograma>();

    public bool AdicionarItem(string diaSemana, int minutos)
    {
        if (string.IsNullOrWhiteSpace(diaSemana) || minutos <= 0 || minutos > 480) return false;
        itens.Add(new ItemCronograma(diaSemana, minutos));
        return true;
    }

    public int CalcularTotalMinutos()
    {
        int total = 0;
        foreach (ItemCronograma item in itens) total += item.GetMinutosPlanejados();
        return total;
    }

    public string ExibirCronograma()
    {
        string texto = "";
        foreach (ItemCronograma item in itens)
            texto += $"{item.GetDiaSemana()}: {item.GetMinutosPlanejados()} min" + Environment.NewLine;
        return texto;
    }
}
