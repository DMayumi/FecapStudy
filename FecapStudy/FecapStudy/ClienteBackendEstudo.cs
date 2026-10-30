using System.Text;
using System.Text.Json;

namespace FecapStudy;

// Missão 10: a tela conversa com a API por esta classe.
public class ClienteBackendEstudo
{
    private static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5080"),
        Timeout = TimeSpan.FromSeconds(10)
    };
    private static readonly JsonSerializerOptions opcoes = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<StatusServidor> ObterStatusAsync()
    {
        string json = await http.GetStringAsync("/api/status");
        return JsonSerializer.Deserialize<StatusServidor>(json, opcoes)
            ?? throw new JsonException("Status vazio.");
    }

    public async Task<List<SessaoServidor>> CarregarHistoricoAsync(string nomeDiscente)
    {
        string nome = Uri.EscapeDataString(nomeDiscente);
        string json = await http.GetStringAsync("/api/sessoes?discente=" + nome);
        List<SessaoServidor> recebidas = JsonSerializer.Deserialize<List<SessaoServidor>>(json, opcoes)
            ?? throw new JsonException("Histórico vazio.");
        List<SessaoServidor> resultado = new List<SessaoServidor>();
        foreach (SessaoServidor sessao in recebidas)
            if (string.Equals(sessao.DiscenteNome, nomeDiscente, StringComparison.OrdinalIgnoreCase))
                resultado.Add(sessao);
        return resultado;
    }

    public async Task<MetaServidor?> CarregarMetaAsync(string nomeDiscente)
    {
        string json = await http.GetStringAsync("/api/metas?discente=" + Uri.EscapeDataString(nomeDiscente));
        List<MetaServidor> metas = JsonSerializer.Deserialize<List<MetaServidor>>(json, opcoes)
            ?? throw new JsonException("Resposta de metas vazia.");
        foreach (MetaServidor meta in metas)
            if (string.Equals(meta.DiscenteNome, nomeDiscente, StringComparison.OrdinalIgnoreCase)) return meta;
        return null;
    }

    public async Task<RelatorioServidor> CarregarRelatorioAsync(string nomeDiscente)
    {
        string json = await http.GetStringAsync("/api/relatorio/" + Uri.EscapeDataString(nomeDiscente));
        return JsonSerializer.Deserialize<RelatorioServidor>(json, opcoes)
            ?? throw new JsonException("Relatório vazio.");
    }

    public async Task SalvarSessaoAsync(string nomeDiscente, SessaoEstudo sessao)
    {
        var dados = new
        {
            discenteNome = nomeDiscente,
            disciplina = sessao.GetDisciplina().GetNome(),
            tipoModalidade = sessao.ObterModalidade(),
            duracaoMinutos = sessao.GetDuracaoMinutos(),
            xpGanho = sessao.CalcularXp(),
            resumoFeedback = sessao.GerarFeedback()
        };
        using StringContent conteudo = new StringContent(JsonSerializer.Serialize(dados, opcoes), Encoding.UTF8, "application/json");
        using HttpResponseMessage resposta = await http.PostAsync("/api/sessoes", conteudo);
        resposta.EnsureSuccessStatusCode();
    }

    public async Task SalvarMetaAsync(string discenteNome, string responsavelNome, MetaEstudo meta)
    {
        var dados = new
        {
            discenteNome,
            responsavelNome,
            metaMinutosSemana = meta.GetMetaMinutosSemana(),
            minutosCumpridos = meta.GetMinutosCumpridos()
        };
        using StringContent conteudo = new StringContent(JsonSerializer.Serialize(dados, opcoes), Encoding.UTF8, "application/json");
        using HttpResponseMessage resposta = await http.PostAsync("/api/metas", conteudo);
        resposta.EnsureSuccessStatusCode();
    }
}
