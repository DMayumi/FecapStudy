using System.Text;
using System.Text.Json;

namespace FecapStudy;

// Preparação anterior às missões: exemplo de HttpClient do material.
public class ConectorBackend
{
    private static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5080"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    public async Task<string> ObterStatusJsonAsync()
    {
        return await http.GetStringAsync("/api/status");
    }

    public async Task<string> ListarSessoesJsonAsync()
    {
        // O backend fornecido exige o filtro. Vazio lista todas as sessões.
        return await http.GetStringAsync("/api/sessoes?discente=");
    }

    public async Task<bool> SalvarSessaoNoBancoAsync(string nomeAluno, SessaoEstudo sessao)
    {
        var dados = new
        {
            discenteNome = nomeAluno,
            disciplina = sessao.GetDisciplina().GetNome(),
            tipoModalidade = sessao.ObterModalidade(),
            duracaoMinutos = sessao.GetDuracaoMinutos(),
            xpGanho = sessao.CalcularXp(),
            resumoFeedback = sessao.GerarFeedback()
        };
        string json = JsonSerializer.Serialize(dados);
        using StringContent conteudo = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage resposta = await http.PostAsync("/api/sessoes", conteudo);
        return resposta.IsSuccessStatusCode;
    }
}
