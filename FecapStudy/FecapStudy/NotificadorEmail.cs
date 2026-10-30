namespace FecapStudy;

// Simulação didática: não envia mensagens para serviços externos.
public class NotificadorEmail : INotificador
{
    public string ObterCanal() { return "E-mail"; }
    public bool EnviarAlerta(string destinatario, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(destinatario) || string.IsNullOrWhiteSpace(mensagem)) return false;
        Console.WriteLine($"[E-mail simulado] Para: {destinatario} | {mensagem}");
        return true;
    }
}
