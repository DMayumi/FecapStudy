namespace FecapStudy;

public interface INotificador
{
    string ObterCanal();
    bool EnviarAlerta(string destinatario, string mensagem);
}
