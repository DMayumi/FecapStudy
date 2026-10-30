using FecapStudy;

namespace FecapStudyUI;

public class NotificadorTela : INotificador
{
    public string ObterCanal() { return "Painel"; }

    public bool EnviarAlerta(string destinatario, string mensagem)
    {
        if (string.IsNullOrWhiteSpace(destinatario) || string.IsNullOrWhiteSpace(mensagem)) return false;
        MessageBox.Show($"Para: {destinatario}\n\n{mensagem}", "Notificação no painel",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return true;
    }
}
