namespace FecapStudy;

public class ItemCronograma
{
    private string diaSemana;
    private int minutosPlanejados;

    // A aplicação cria os itens somente dentro de PlanoEstudo.
    internal ItemCronograma(string diaSemana, int minutosPlanejados)
    {
        this.diaSemana = diaSemana;
        this.minutosPlanejados = minutosPlanejados;
    }

    public string GetDiaSemana() { return diaSemana; }
    public int GetMinutosPlanejados() { return minutosPlanejados; }
}
