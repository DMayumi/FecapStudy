namespace FecapStudy;

// Missão 8: dependência por parâmetro, sem guardar objetos em campos.
public class ServicoAlertaResponsavel
{
    public bool AvaliarDiscente(Discente d, MetaEstudo meta, Responsavel resp, INotificador notificador)
    {
        double percentual = meta.CalcularPercentual();
        string mensagem;
        if (meta.EstaCumprida())
            mensagem = $"Parabéns! {d.GetNome()} atingiu 100% da meta semanal.";
        else
            mensagem = $"{d.GetNome()} cumpriu {percentual:F1}% da meta semanal.";

        return notificador.EnviarAlerta(resp.GetEmail(), mensagem);
    }
}
