namespace FecapStudy;

public class NotificadorPainel : INotificador
{
    public string ObterCanal() { return "Painel"; }
    public bool EnviarAlerta(string destinatario, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(destinatario) || string.IsNullOrWhiteSpace(mensagem)) return false;
        Console.WriteLine($"[Painel] Para: {destinatario} | {mensagem}");
        return true;
    }
}
