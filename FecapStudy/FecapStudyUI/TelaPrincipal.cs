using FecapStudy;
using System.Text.Json;

namespace FecapStudyUI;

public class TelaPrincipal : Form
{
    private Discente discente = CriarDiscente();
    private Responsavel responsavel = new Responsavel(2, "Carlos Silva", "carlos@exemplo.com", "resp123", "Pai");
    private MetaEstudo meta = new MetaEstudo(180);
    private ClienteBackendEstudo cliente = new ClienteBackendEstudo();
    private ServicoAlertaResponsavel servicoAlerta = new ServicoAlertaResponsavel();
    private List<Disciplina> disciplinas = new List<Disciplina>
    {
        new Disciplina("ADS201", "Programação Orientada a Objetos", "Computação", 4),
        new Disciplina("ADS202", "Banco de Dados", "Dados", 4),
        new Disciplina("ADS203", "Projeto Interdisciplinar", "Prática", 6)
    };

    private Label perfilDiscente = new Label();
    private Label perfilResponsavel = new Label();
    private Label resumoMeta = new Label();
    private Label status = new Label();
    private Label resumoHistorico = new Label();
    private ComboBox disciplina = new ComboBox();
    private ComboBox modalidade = new ComboBox();
    private NumericUpDown minutos = new NumericUpDown();
    private NumericUpDown ciclos = new NumericUpDown();
    private NumericUpDown questoes = new NumericUpDown();
    private NumericUpDown acertos = new NumericUpDown();
    private NumericUpDown paginas = new NumericUpDown();
    private CheckBox fezResumo = new CheckBox();
    private NumericUpDown metaSemanal = new NumericUpDown();
    private ComboBox canal = new ComboBox();
    private ProgressBar progresso = new ProgressBar();
    private ListBox historico = new ListBox();
    private Button registrar = new Button();
    private Button carregar = new Button();
    private Button relatorio = new Button();
    private Button salvarMeta = new Button();
    private Button notificar = new Button();
    private bool carregado;
    private bool ocupado;
    private int quantidadeSessoes;

    private static Discente CriarDiscente()
    {
        return new Discente(3, "Lucas Silva", "lucas@exemplo.com", "aluno123", "RA-2026101");
    }

    public TelaPrincipal()
    {
        Text = "FecapStudy — Plataforma de Apoio ao Estudo";
        ClientSize = new Size(1120, 840);
        MinimumSize = new Size(1040, 820);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10);
        BackColor = Color.FromArgb(242, 247, 245);
        AutoScaleMode = AutoScaleMode.Dpi;

        MontarTela();
        responsavel.VincularDiscente(discente);
        responsavel.DefinirMeta(discente, meta);
        AtualizarPainel();
        AtualizarModalidade();
        DefinirOcupado(false);
        Shown += async (sender, e) => await CarregarDadosAsync();
    }

    private void MontarTela()
    {
        TableLayoutPanel pagina = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(20)
        };
        pagina.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
        pagina.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        pagina.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        pagina.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        Controls.Add(pagina);

        Panel cabecalho = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(2, 51, 39) };
        Label titulo = new Label
        {
            Text = "FecapStudy", Location = new Point(18, 10), AutoSize = true,
            ForeColor = Color.FromArgb(0, 227, 135), Font = new Font("Segoe UI", 23, FontStyle.Bold)
        };
        Label subtitulo = new Label
        {
            Text = "PLATAFORMA DE APOIO AO ESTUDO  •  AULA 9", AutoSize = true,
            Location = new Point(21, 55), ForeColor = Color.White, Font = new Font("Segoe UI", 9)
        };
        cabecalho.Controls.Add(titulo);
        cabecalho.Controls.Add(subtitulo);
        pagina.Controls.Add(cabecalho, 0, 0);

        FlowLayoutPanel perfis = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false,
            Padding = new Padding(0, 10, 0, 0)
        };
        perfilDiscente.AutoSize = true;
        perfilDiscente.Font = new Font(Font, FontStyle.Bold);
        perfilResponsavel.AutoSize = true;
        perfis.Controls.Add(perfilDiscente);
        perfis.Controls.Add(perfilResponsavel);
        pagina.Controls.Add(perfis, 0, 1);

        TableLayoutPanel colunas = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
        colunas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41));
        colunas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 59));
        pagina.Controls.Add(colunas, 0, 2);

        GroupBox formulario = new GroupBox { Text = "Nova sessão de estudo", Dock = DockStyle.Fill, Padding = new Padding(14) };
        TableLayoutPanel campos = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 11 };
        campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43));
        campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57));
        for (int i = 0; i < 10; i++) campos.RowStyles.Add(new RowStyle(SizeType.Absolute, 43));
        campos.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        formulario.Controls.Add(campos);
        colunas.Controls.Add(formulario, 0, 0);

        disciplina.DropDownStyle = ComboBoxStyle.DropDownList;
        disciplina.DropDownWidth = 360;
        foreach (Disciplina d in disciplinas) disciplina.Items.Add(d.GetNome());
        disciplina.SelectedIndex = 0;
        modalidade.DropDownStyle = ComboBoxStyle.DropDownList;
        modalidade.Items.AddRange(new string[] { "Pomodoro", "Exercícios", "Leitura" });
        modalidade.SelectedIndex = 0;
        modalidade.SelectedIndexChanged += (sender, e) => AtualizarModalidade();
        ConfigurarNumero(minutos, 1, 480, 25);
        ConfigurarNumero(ciclos, 0, 100, 1);
        ConfigurarNumero(questoes, 1, 1000, 10);
        ConfigurarNumero(acertos, 0, 1000, 8);
        ConfigurarNumero(paginas, 0, 1000, 5);
        fezResumo.Text = "Fiz um resumo";
        fezResumo.Checked = true;
        fezResumo.AutoSize = true;
        AdicionarCampo(campos, "Disciplina", disciplina, 0);
        AdicionarCampo(campos, "Modalidade", modalidade, 1);
        AdicionarCampo(campos, "Duração (min)", minutos, 2);
        AdicionarCampo(campos, "Ciclos completos", ciclos, 3);
        AdicionarCampo(campos, "Total de questões", questoes, 4);
        AdicionarCampo(campos, "Acertos", acertos, 5);
        AdicionarCampo(campos, "Páginas lidas", paginas, 6);
        campos.Controls.Add(fezResumo, 1, 7);
        registrar.Text = "Registrar Estudo";
        EstilizarBotao(registrar, true);
        registrar.Click += async (sender, e) => await RegistrarAsync();
        campos.Controls.Add(registrar, 0, 8);
        campos.SetColumnSpan(registrar, 2);
        Label dica = new Label
        {
            Text = "Preencha somente os campos habilitados para a modalidade escolhida. A sessão será enviada ao servidor.",
            Dock = DockStyle.Fill, ForeColor = Color.DimGray, AutoSize = true, Margin = new Padding(3, 8, 3, 0)
        };
        campos.Controls.Add(dica, 0, 9);
        campos.SetColumnSpan(dica, 2);
        campos.SetRowSpan(dica, 2);

        TableLayoutPanel acompanhamento = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        acompanhamento.RowStyles.Add(new RowStyle(SizeType.Absolute, 153));
        acompanhamento.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        acompanhamento.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
        colunas.Controls.Add(acompanhamento, 1, 0);

        GroupBox grupoMeta = new GroupBox { Text = "Meta semanal", Dock = DockStyle.Fill, Padding = new Padding(12) };
        TableLayoutPanel conteudoMeta = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        conteudoMeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        conteudoMeta.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        conteudoMeta.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        resumoMeta.Dock = DockStyle.Fill;
        progresso.Dock = DockStyle.Fill;
        progresso.Maximum = 100;
        FlowLayoutPanel editarMeta = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 8, 0, 0) };
        ConfigurarNumero(metaSemanal, 30, 3000, 180);
        metaSemanal.Width = 95;
        salvarMeta.Text = "Salvar meta (min)";
        salvarMeta.AutoSize = true;
        salvarMeta.Click += async (sender, e) => await SalvarMetaAsync();
        editarMeta.Controls.Add(metaSemanal);
        editarMeta.Controls.Add(salvarMeta);
        conteudoMeta.Controls.Add(resumoMeta, 0, 0);
        conteudoMeta.Controls.Add(progresso, 0, 1);
        conteudoMeta.Controls.Add(editarMeta, 0, 2);
        grupoMeta.Controls.Add(conteudoMeta);
        acompanhamento.Controls.Add(grupoMeta, 0, 0);

        GroupBox grupoHistorico = new GroupBox { Text = "Histórico de estudos", Dock = DockStyle.Fill, Padding = new Padding(12) };
        TableLayoutPanel conteudoHistorico = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
        conteudoHistorico.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        conteudoHistorico.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        conteudoHistorico.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
        resumoHistorico.Dock = DockStyle.Fill;
        historico.Dock = DockStyle.Fill;
        historico.HorizontalScrollbar = true;
        historico.IntegralHeight = false;
        historico.ItemHeight = 25;
        historico.DoubleClick += (sender, e) =>
        {
            if (historico.SelectedItem != null) MessageBox.Show(historico.SelectedItem.ToString(), "Detalhes da sessão");
        };
        FlowLayoutPanel acoes = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        carregar.Text = "Carregar Histórico do Servidor";
        carregar.AutoSize = true;
        carregar.Height = 34;
        carregar.Click += async (sender, e) => await CarregarDadosAsync();
        relatorio.Text = "Ver Relatório";
        relatorio.AutoSize = true;
        relatorio.Height = 34;
        relatorio.Click += async (sender, e) => await ExibirRelatorioAsync();
        acoes.Controls.Add(carregar);
        acoes.Controls.Add(relatorio);
        conteudoHistorico.Controls.Add(resumoHistorico, 0, 0);
        conteudoHistorico.Controls.Add(historico, 0, 1);
        conteudoHistorico.Controls.Add(acoes, 0, 2);
        grupoHistorico.Controls.Add(conteudoHistorico);
        acompanhamento.Controls.Add(grupoHistorico, 0, 1);

        GroupBox grupoAlerta = new GroupBox { Text = "Acompanhamento do responsável", Dock = DockStyle.Fill };
        FlowLayoutPanel alertas = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(9) };
        canal.DropDownStyle = ComboBoxStyle.DropDownList;
        canal.Items.AddRange(new string[] { "Painel", "E-mail (simulado)" });
        canal.SelectedIndex = 0;
        canal.Width = 165;
        notificar.Text = "Notificar Responsável";
        notificar.AutoSize = true;
        notificar.Height = 34;
        notificar.Click += (sender, e) => NotificarResponsavel();
        alertas.Controls.Add(canal);
        alertas.Controls.Add(notificar);
        grupoAlerta.Controls.Add(alertas);
        acompanhamento.Controls.Add(grupoAlerta, 0, 2);

        status.Dock = DockStyle.Fill;
        status.AutoEllipsis = true;
        status.TextAlign = ContentAlignment.MiddleLeft;
        status.Text = "Conectando ao servidor...";
        status.ForeColor = Color.FromArgb(2, 82, 59);
        pagina.Controls.Add(status, 0, 3);
    }

    private void ConfigurarNumero(NumericUpDown campo, int minimo, int maximo, int valor)
    {
        campo.Minimum = minimo;
        campo.Maximum = maximo;
        campo.Value = valor;
        campo.Dock = DockStyle.Fill;
    }

    private void AdicionarCampo(TableLayoutPanel tabela, string texto, Control campo, int linha)
    {
        Label rotulo = new Label { Text = texto, AutoSize = true, Anchor = AnchorStyles.Left };
        campo.Dock = DockStyle.Fill;
        tabela.Controls.Add(rotulo, 0, linha);
        tabela.Controls.Add(campo, 1, linha);
    }

    private void EstilizarBotao(Button botao, bool destaque)
    {
        botao.Dock = DockStyle.Fill;
        botao.FlatStyle = FlatStyle.Flat;
        botao.BackColor = destaque ? Color.FromArgb(2, 82, 59) : Color.White;
        botao.ForeColor = destaque ? Color.White : Color.Black;
        botao.Font = new Font(Font, FontStyle.Bold);
    }

    private void AtualizarModalidade()
    {
        ciclos.Enabled = modalidade.SelectedIndex == 0;
        questoes.Enabled = modalidade.SelectedIndex == 1;
        acertos.Enabled = modalidade.SelectedIndex == 1;
        paginas.Enabled = modalidade.SelectedIndex == 2;
        fezResumo.Enabled = modalidade.SelectedIndex == 2;
    }

    private void AtualizarPainel()
    {
        perfilDiscente.Text = discente.GerarPainelResumo();
        perfilResponsavel.Text = responsavel.GerarPainelResumo();
        resumoMeta.Text = $"{meta.GetMinutosCumpridos()} / {meta.GetMetaMinutosSemana()} minutos  •  {meta.CalcularPercentual():F1}%";
        progresso.Value = (int)meta.CalcularPercentual();
        metaSemanal.Value = meta.GetMetaMinutosSemana();
        resumoHistorico.Text = $"{quantidadeSessoes} sessões registradas  •  {discente.GetXpTotal()} XP acumulados";
    }

    private void DefinirOcupado(bool valor)
    {
        ocupado = valor;
        carregar.Enabled = !valor;
        registrar.Enabled = !valor && carregado;
        salvarMeta.Enabled = !valor && carregado;
        relatorio.Enabled = !valor && carregado;
        notificar.Enabled = !valor && carregado;
        UseWaitCursor = valor;
    }

    private async Task CarregarDadosAsync()
    {
        if (ocupado) return;
        DefinirOcupado(true);
        status.Text = "Carregando os dados do servidor...";
        try
        {
            StatusServidor situacao = await cliente.ObterStatusAsync();
            List<SessaoServidor> sessoes = await cliente.CarregarHistoricoAsync(discente.GetNome());
            MetaServidor? metaSalva = await cliente.CarregarMetaAsync(discente.GetNome());
            MetaEstudo metaCarregada = new MetaEstudo(metaSalva == null ? 180 : metaSalva.MetaMinutosSemana);
            Discente discenteCarregado = CriarDiscente();
            // Reconstrói os totais em objetos novos: recarregar não duplica XP nem minutos.
            foreach (SessaoServidor sessao in sessoes)
            {
                if (sessao.DuracaoMinutos <= 0 || sessao.DuracaoMinutos > 480 || sessao.XpGanho < 0)
                    throw new JsonException("O servidor retornou uma sessão com valores inválidos.");
                discenteCarregado.AdicionarXp(sessao.XpGanho);
                metaCarregada.RegistrarAvanco(sessao.DuracaoMinutos);
            }
            if (metaSalva == null)
                await cliente.SalvarMetaAsync(discente.GetNome(), responsavel.GetNome(), metaCarregada);

            discente = discenteCarregado;
            meta = metaCarregada;
            responsavel = new Responsavel(2, "Carlos Silva", "carlos@exemplo.com", "resp123", "Pai");
            responsavel.VincularDiscente(discente);
            responsavel.DefinirMeta(discente, meta);
            historico.Items.Clear();
            sessoes.Sort((a, b) => a.Id.CompareTo(b.Id));
            foreach (SessaoServidor sessao in sessoes)
                historico.Items.Add($"{sessao.DataRegistro} | {sessao.ResumoFeedback}");
            quantidadeSessoes = sessoes.Count;
            carregado = true;
            AtualizarPainel();
            status.Text = situacao.UsandoMySql
                ? "Conectado • Dados salvos no MySQL local."
                : "Conectado em modo JSON local • MySQL não conectado.";
        }
        catch (Exception erro) when (erro is HttpRequestException || erro is TaskCanceledException || erro is JsonException)
        {
            carregado = false;
            status.Text = "Servidor indisponível. Inicie o backend e clique em Carregar Histórico do Servidor.";
        }
        finally { DefinirOcupado(false); }
    }

    private SessaoEstudo CriarSessao()
    {
        Disciplina d = disciplinas[disciplina.SelectedIndex];
        int duracao = (int)minutos.Value;
        if (modalidade.SelectedIndex == 0) return new SessaoPomodoro(d, duracao, (int)ciclos.Value);
        if (modalidade.SelectedIndex == 1)
            return new SessaoExercicios(d, duracao, (int)questoes.Value, (int)acertos.Value);
        return new SessaoLeitura(d, duracao, (int)paginas.Value, fezResumo.Checked);
    }

    private async Task RegistrarAsync()
    {
        if (ocupado || !carregado) return;
        SessaoEstudo sessao;
        try { sessao = CriarSessao(); }
        catch (ArgumentException erro)
        {
            MessageBox.Show(erro.Message, "Confira os campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        DefinirOcupado(true);
        status.Text = "Salvando a sessão no servidor...";
        try
        {
            await cliente.SalvarSessaoAsync(discente.GetNome(), sessao);
            discente.RegistrarEstudo(sessao);
            meta.RegistrarAvanco(sessao.GetDuracaoMinutos());
            quantidadeSessoes++;
            historico.Items.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} | {sessao.GerarFeedback()}");
            historico.SelectedIndex = historico.Items.Count - 1;
            AtualizarPainel();
            status.Text = "Sessão salva no servidor. " + sessao.GerarFeedback();
        }
        catch (Exception erro) when (erro is HttpRequestException || erro is TaskCanceledException)
        {
            // Em um timeout o servidor pode ter gravado. Consulte antes de reenviar.
            carregado = false;
            status.Text = "Envio não confirmado. Carregue o histórico antes de tentar registrar novamente.";
            MessageBox.Show("Não foi possível confirmar o envio. Clique em Carregar Histórico do Servidor para conferir se a sessão foi salva antes de repetir.",
                "Confira o servidor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally { DefinirOcupado(false); }
    }

    private async Task SalvarMetaAsync()
    {
        if (ocupado || !carregado) return;
        DefinirOcupado(true);
        try
        {
            MetaEstudo novaMeta = new MetaEstudo((int)metaSemanal.Value);
            await cliente.SalvarMetaAsync(discente.GetNome(), responsavel.GetNome(), novaMeta);
            meta.RedefinirMeta(novaMeta.GetMetaMinutosSemana());
            AtualizarPainel();
            status.Text = "Meta semanal salva. Os minutos já estudados foram mantidos.";
        }
        catch (Exception erro) when (erro is HttpRequestException || erro is TaskCanceledException)
        {
            carregado = false;
            status.Text = "Alteração não confirmada. Recarregue o histórico para conferir a meta.";
        }
        finally { DefinirOcupado(false); }
    }

    private async Task ExibirRelatorioAsync()
    {
        if (ocupado || !carregado) return;
        DefinirOcupado(true);
        try
        {
            RelatorioServidor r = await cliente.CarregarRelatorioAsync(discente.GetNome());
            MessageBox.Show($"Discente: {r.Discente}\nSessões: {r.TotalSessoes}\nMinutos estudados: {r.TotalMinutosEstudados}\nXP acumulado: {r.TotalXpAcumulado}\nMeta semanal: {r.MetaMinutosSemana} min\nProgresso: {r.PercentualMetaConcluida:F1}%\n\n{r.StatusAcompanhamento}",
                "Relatório do servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception erro) when (erro is HttpRequestException || erro is TaskCanceledException || erro is JsonException)
        {
            status.Text = "Não foi possível carregar o relatório. Confira se o backend está ativo.";
        }
        finally { DefinirOcupado(false); }
    }

    private void NotificarResponsavel()
    {
        INotificador notificador;
        if (canal.SelectedIndex == 0) notificador = new NotificadorTela();
        else notificador = new NotificadorEmail();
        bool enviado = servicoAlerta.AvaliarDiscente(discente, meta, responsavel, notificador);
        if (canal.SelectedIndex == 1 && enviado)
            MessageBox.Show($"E-mail simulado para {responsavel.GetEmail()}.\n{discente.GetNome()}: {meta.CalcularPercentual():F1}% da meta.\nNenhum e-mail externo foi enviado.", "E-mail simulado");
    }
}
