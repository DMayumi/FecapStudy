using System.Text.Json;

namespace FecapStudy;

// Testes de mesa da aula: execute com --verificar.
internal static class Verificacoes
{
    private static int total;

    private static void Conferir(bool resultado, string descricao)
    {
        if (!resultado) throw new Exception("Falhou: " + descricao);
        total++;
        Console.WriteLine("OK: " + descricao);
    }

    public static void Executar()
    {
        total = 0;
        Disciplina poo = new Disciplina("ADS201", "POO", "Computação", 4);
        Disciplina invalida = new Disciplina("ADS202", "Banco", "Dados", -4);
        Conferir(poo.CalcularCargaEmMinutos() == 240, "4h equivalem a 240 minutos");
        Conferir(invalida.CalcularCargaEmMinutos() == 120, "Carga negativa vira 2h");
        Conferir(new Disciplina("X", "X", "X", 0).CalcularCargaEmMinutos() == 120, "Carga zero vira 2h");
        Conferir(poo.ExibirResumo().Contains("[ADS201]") && poo.GetNome() == "POO", "Resumo e objetos independentes");
        MetaEstudo meta = new MetaEstudo(100);
        Conferir(!meta.RegistrarAvanco(-20) && !meta.RegistrarAvanco(900) && !meta.RegistrarAvanco(0), "Avanços inválidos recusados");
        Conferir(meta.GetMinutosCumpridos() == 0, "Tentativas inválidas não alteram os minutos");
        Conferir(meta.RegistrarAvanco(60) && meta.CalcularPercentual() == 60, "60 de 100 minutos = 60%");
        Conferir(!meta.EstaCumprida(), "Meta parcial não está cumprida");
        meta.RegistrarAvanco(60);
        Conferir(meta.CalcularPercentual() == 100 && meta.EstaCumprida(), "Percentual trava em 100%");
        Conferir(new MetaEstudo(-100).GetMetaMinutosSemana() == 30, "Meta mínima no construtor");
        Conferir(new MetaEstudo(4000).GetMetaMinutosSemana() == 3000, "Meta máxima no construtor");
        meta.RedefinirMeta(10);
        Conferir(meta.GetMetaMinutosSemana() == 30, "Redefinição respeita limite inferior");
        meta.RedefinirMeta(5000);
        Conferir(meta.GetMetaMinutosSemana() == 3000, "Redefinição respeita limite superior");
        Conferir(new MetaEstudo(500).RegistrarAvanco(480), "480 minutos aceitos");
        Conferir(!new MetaEstudo(500).RegistrarAvanco(481), "481 minutos recusados");
        Administrador admin = new Administrador(1, "Helena", "h@exemplo.com", "123", "Coordenação");
        Responsavel resp = new Responsavel(2, "Carlos", "c@exemplo.com", "123", "Pai");
        Discente d = new Discente(3, "Lucas", "l@exemplo.com", "123", "RA1");
        Conferir(d.Autenticar("123") && !d.Autenticar("errada"), "Autenticação correta e incorreta");
        Conferir(d.GetXpTotal() == 0 && d.GerarPainelResumo().Contains("RA1"), "Discente começa com zero XP e mostra RA");
        Conferir(admin.ObterTipoPerfil() == "Administrador" && resp.ObterTipoPerfil() == "Responsavel", "Perfis concretos");
        d.AdicionarXp(-10);
        Conferir(d.GetXpTotal() == 0, "XP negativo não é somado");
        SessaoEstudo pomodoro = new SessaoPomodoro(poo, 25, 1);
        SessaoEstudo exercicios = new SessaoExercicios(poo, 30, 10, 8);
        SessaoEstudo leitura = new SessaoLeitura(poo, 20, 5, true);
        Conferir(pomodoro.CalcularXp() == 65, "XP Pomodoro");
        Conferir(exercicios.CalcularXp() == 110 && exercicios.GerarFeedback().Contains("80"), "XP e percentual de exercícios");
        Conferir(leitura.CalcularXp() == 60, "XP leitura com resumo");
        Conferir(new SessaoLeitura(poo, 20, 5, false).CalcularXp() == 35, "XP leitura sem resumo");
        d.RegistrarEstudo(pomodoro);
        d.RegistrarEstudo(poo, 50);
        d.RegistrarEstudo(poo, 14, 15, 10);
        Conferir(d.GetQuantidadeSessoes() == 3 && d.GetXpTotal() == 310, "Três sobrecargas acumulam histórico e XP");
        Discente rapido = new Discente(4, "Ana", "a@exemplo.com", "123", "RA2");
        rapido.RegistrarEstudo(poo, 10);
        Conferir(rapido.GetXpTotal() == 35, "Atalho usa pelo menos um ciclo");
        IExportavelJson exportavel = pomodoro;
        using JsonDocument jsonSessao = JsonDocument.Parse(exportavel.ParaJson());
        using JsonDocument jsonMeta = JsonDocument.Parse(meta.ParaJson());
        Conferir(jsonSessao.RootElement.GetProperty("xpGanho").GetInt32() == 65, "Contrato JSON da sessão");
        Conferir(jsonMeta.RootElement.GetProperty("minutosCumpridos").GetInt32() == 120, "Contrato JSON da meta");
        Conferir(resp.VincularDiscente(d) && !resp.VincularDiscente(d), "Associação sem duplicação");
        Conferir(!resp.VincularDiscente(new Discente(3, "Mesmo ID", "x@exemplo.com", "123", "RA1")), "ID repetido não duplica vínculo");
        Conferir(resp.DefinirMeta(d, meta) && resp.GetMeta(d) == meta, "Responsável guarda referência à meta");
        Conferir(!resp.DefinirMeta(rapido, meta), "Meta exige vínculo com discente");
        TrilhaEstudo trilha = new TrilhaEstudo("ADS", admin);
        TrilhaEstudo outra = new TrilhaEstudo("Revisão", admin);
        trilha.AdicionarDisciplina(poo);
        trilha.AdicionarDisciplina(invalida);
        outra.AdicionarDisciplina(poo);
        Conferir(trilha.CalcularCargaHorariaTotal() == 6 && outra.CalcularCargaHorariaTotal() == 4, "Agregação compartilha disciplina e soma horas");
        PlanoEstudo plano = new PlanoEstudo();
        Conferir(plano.AdicionarItem("Segunda", 60) && plano.AdicionarItem("Quarta", 40), "Plano cria seus próprios itens");
        Conferir(!plano.AdicionarItem("", 60) && !plano.AdicionarItem("Sexta", -1), "Cronograma rejeita dados inválidos");
        Conferir(plano.CalcularTotalMinutos() == 100 && plano.ExibirCronograma().Contains("Segunda"), "Cronograma soma e exibe itens");
        ServicoAlertaResponsavel servico = new ServicoAlertaResponsavel();
        MetaEstudo alerta = new MetaEstudo(100);
        alerta.RegistrarAvanco(60);
        Conferir(servico.AvaliarDiscente(d, alerta, resp, new NotificadorEmail()), "Alerta parcial por interface E-mail");
        alerta.RegistrarAvanco(40);
        Conferir(servico.AvaliarDiscente(d, alerta, resp, new NotificadorPainel()), "Alerta de conclusão por interface Painel");
        Console.WriteLine($"\n{total} verificações concluídas sem falhas.");
    }
}
