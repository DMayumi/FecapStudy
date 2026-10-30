using System.Text;

namespace FecapStudy;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        if (args.Length > 0 && args[0] == "--verificar")
        {
            Verificacoes.Executar();
            return;
        }

        Console.WriteLine("FECAP STUDY — MISSÕES 1 A 8");
        Console.WriteLine("\nMISSÃO 1 — Disciplinas");
        Disciplina poo = new Disciplina("ADS201", "Programação Orientada a Objetos", "Computação", 4);
        Disciplina banco = new Disciplina("ADS202", "Banco de Dados", "Dados", 4);
        Disciplina pi = new Disciplina("ADS203", "Projeto Interdisciplinar", "Prática", 6);
        Console.WriteLine(poo.ExibirResumo());
        Console.WriteLine(banco.ExibirResumo());
        Console.WriteLine(pi.ExibirResumo());

        Console.WriteLine("\nMISSÃO 2 — Encapsulamento");
        MetaEstudo meta = new MetaEstudo(100);
        Console.WriteLine($"Avanço -20 aceito? {meta.RegistrarAvanco(-20)}");
        Console.WriteLine($"Avanço 900 aceito? {meta.RegistrarAvanco(900)}");
        Console.WriteLine($"Minutos após tentativas inválidas: {meta.GetMinutosCumpridos()}");
        meta.RegistrarAvanco(60);
        Console.WriteLine($"Após 60 minutos: {meta.CalcularPercentual()}%");

        Console.WriteLine("\nMISSÃO 3 — Classe abstrata e herança");
        Administrador admin = new Administrador(1, "Helena", "helena@fecap.br", "admin123", "Coordenação PI");
        Responsavel responsavel = new Responsavel(2, "Carlos", "carlos@exemplo.com", "resp123", "Pai");
        Discente discente = new Discente(3, "Lucas Silva", "lucas@exemplo.com", "aluno123", "RA-2026101");
        List<Usuario> usuarios = new List<Usuario> { admin, responsavel, discente };
        foreach (Usuario usuario in usuarios) Console.WriteLine(usuario.GerarPainelResumo());
        Console.WriteLine($"Autenticação correta: {discente.Autenticar("aluno123")}");
        Console.WriteLine($"Autenticação incorreta: {discente.Autenticar("errada")}");

        Console.WriteLine("\nMISSÃO 4 — Polimorfismo dinâmico");
        List<SessaoEstudo> sessoes = new List<SessaoEstudo>
        {
            new SessaoPomodoro(poo, 25, 1),
            new SessaoExercicios(banco, 30, 10, 8),
            new SessaoLeitura(pi, 20, 5, true)
        };
        int totalXp = 0;
        foreach (SessaoEstudo sessao in sessoes)
        {
            Console.WriteLine(sessao.GerarFeedback());
            totalXp += sessao.CalcularXp();
        }
        Console.WriteLine($"XP das três modalidades: {totalXp}");

        Console.WriteLine("\nMISSÃO 5 — As três sobrecargas");
        discente.RegistrarEstudo(sessoes[0]);
        discente.RegistrarEstudo(banco, 50);
        discente.RegistrarEstudo(pi, 14, 15, 10);
        Console.Write(discente.ExibirHistorico());
        Console.WriteLine(discente.GerarPainelResumo());

        Console.WriteLine("\nMISSÃO 6 — Interfaces");
        INotificador email = new NotificadorEmail();
        INotificador painel = new NotificadorPainel();
        Console.WriteLine($"Canais: {email.ObterCanal()} e {painel.ObterCanal()}");
        IExportavelJson sessaoExportavel = sessoes[0];
        IExportavelJson metaExportavel = meta;
        Console.WriteLine(sessaoExportavel.ParaJson());
        Console.WriteLine(metaExportavel.ParaJson());

        Console.WriteLine("\nMISSÃO 7 — Associação e agregação");
        responsavel.VincularDiscente(discente);
        Console.WriteLine($"Vínculo duplicado aceito? {responsavel.VincularDiscente(discente)}");
        responsavel.DefinirMeta(discente, meta);
        Console.WriteLine(responsavel.GerarPainelResumo());
        TrilhaEstudo trilha = new TrilhaEstudo("Trilha ADS", admin);
        trilha.AdicionarDisciplina(poo);
        trilha.AdicionarDisciplina(banco);
        trilha.AdicionarDisciplina(pi);
        TrilhaEstudo revisao = new TrilhaEstudo("Revisão POO", admin);
        revisao.AdicionarDisciplina(poo);
        Console.WriteLine($"{trilha.GetTitulo()}: {trilha.CalcularCargaHorariaTotal()}h/semana");
        Console.WriteLine($"{revisao.GetTitulo()}: {revisao.CalcularCargaHorariaTotal()}h/semana");

        Console.WriteLine("\nMISSÃO 8 — Composição e dependência");
        PlanoEstudo plano = new PlanoEstudo();
        plano.AdicionarItem("Segunda-feira", 60);
        plano.AdicionarItem("Quarta-feira", 40);
        Console.Write(plano.ExibirCronograma());
        Console.WriteLine($"Planejado: {plano.CalcularTotalMinutos()} min");
        ServicoAlertaResponsavel servico = new ServicoAlertaResponsavel();
        servico.AvaliarDiscente(discente, meta, responsavel, email);
        meta.RegistrarAvanco(40);
        servico.AvaliarDiscente(discente, meta, responsavel, painel);

        // Exemplo de conexão anterior às missões: consulta sem alterar o servidor.
        if (args.Length > 0 && args[0] == "--backend")
        {
            try
            {
                ConectorBackend conector = new ConectorBackend();
                Console.WriteLine(await conector.ObterStatusJsonAsync());
                Console.WriteLine(await conector.ListarSessoesJsonAsync());
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Inicie Backend/iniciar_backend.bat para consultar a API.");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("O backend não respondeu dentro de 10 segundos.");
            }
        }
    }
}
