# FecapStudy — Aula 9

Plataforma de apoio ao estudo desenvolvida em C#, com Windows Forms e uma API
C# conectada ao MySQL. O projeto reúne as dez missões da aula.

## Como executar

No Windows, abra os arquivos da raiz nesta ordem:

1. `iniciar_mysql.bat`: inicia o MySQL local preparado neste computador.
   Se o seu MySQL já estiver ativo na porta 3306, não execute outra instância.
2. `iniciar_backend.bat`: inicia a API em `http://localhost:5080`.
3. `iniciar_tela.bat`: compila e abre a interface Windows Forms.

Mantenha o MySQL e o backend ativos enquanto usa a tela. Para encerrar cada
servidor iniciado pelo terminal, pressione `Ctrl+C` na janela correspondente.

Os scripts procuram o .NET local em `%LOCALAPPDATA%\FecapStudy\dotnet`.
Em outro computador, também funcionam com o SDK .NET 10 instalado e o comando
`dotnet` disponível no terminal. A interface usa Windows Forms e requer Windows.
As ferramentas locais não são incluídas no Git.

Também é possível executar os projetos pelo terminal:

```powershell
# Compilar os três projetos
dotnet build FecapStudy/FecapStudy.slnx

# Iniciar o backend pelo código-fonte
dotnet run --project FecapStudy/Backend/Source

# Abrir a tela, em outro terminal
dotnet run --project FecapStudy/FecapStudyUI

# Executar a demonstração das missões 1 a 8
dotnet run --project FecapStudy/FecapStudy

# Executar os testes de mesa
dotnet run --project FecapStudy/FecapStudy -- --verificar
```

Ao executar o backend pelo código-fonte, a configuração e o JSON ficam ao lado
da DLL compilada, na pasta de saída. Para usar a configuração distribuída em
`FecapStudy/Backend/mysql_config.txt`, prefira o inicializador `.bat`.

## Banco de dados

A conexão está em `FecapStudy/Backend/mysql_config.txt`. A configuração local da aula é:

```text
Server=127.0.0.1;Port=3306;Database=fecap_estudo;Uid=root;Pwd=;
```

Ajuste usuário, porta e senha se sua instalação usar outros valores. O backend
cria o banco `fecap_estudo` e as tabelas automaticamente. O script
`banco_estudo.sql` serve como referência; não é necessário executá-lo novamente.
Repetir suas inserções pode duplicar os dados de demonstração.

Confira o painel em `http://localhost:5080` ou o JSON em
`http://localhost:5080/api/status`. O campo `usandoMySql` deve estar `true` para
confirmar a conexão com o MySQL. Se o servidor de banco estiver indisponível,
a API usa `dados_estudo_local.json`, alternativa prevista no material.
A tela informa qual modo está ativo quando carrega o histórico.

O MySQL local deste computador fica em `%LOCALAPPDATA%\FecapStudy\mysql`, e os
dados em `%LOCALAPPDATA%\FecapStudy\mysql-dados`. Ele roda em `127.0.0.1:3306`,
sem instalar um serviço do Windows. Em outro computador, utilize sua instalação
local do MySQL e configure a mesma conexão.

## Como usar a tela

A tela usa os perfis de exemplo Lucas Silva e Carlos Silva. Os nomes, RA,
disciplinas e códigos são demonstrativos e podem ser ajustados ao semestre.
Não há cadastro ou login visual: a aula pede os resumos dos perfis e o formulário
de estudo. A autenticação da missão 3 continua demonstrada no console.

1. Ao abrir, a tela busca o histórico e a meta do discente no servidor.
2. Escolha uma das três disciplinas e a modalidade de estudo.
3. Informe a duração, entre 1 e 480 minutos, e os dados da modalidade:
   - Pomodoro: ciclos completos.
   - Exercícios: total de questões e acertos. Acertos não podem superar o total.
   - Leitura: páginas lidas e se foi feito um resumo.
4. Clique em **Registrar Estudo**. A tela cria a subclasse correspondente e
   envia uma sessão para `POST /api/sessoes`.
5. Com o envio confirmado, o discente registra a sessão, a meta recebe os minutos
   e a tela atualiza XP, progresso e histórico. As fórmulas ficam nas classes de
   domínio, não no evento do botão.
6. Use **Salvar meta (min)** para ajustar a meta entre 30 e 3000 minutos.
   A alteração não apaga os minutos já estudados nem cria outra meta do discente.
7. **Carregar Histórico do Servidor** busca os registros novamente. Os totais
   são reconstruídos sem duplicar XP, minutos ou linhas. Assim, fechar e reabrir
   a tela recupera os registros persistidos.
8. **Ver Relatório** exibe o relatório retornado pela API.
9. Escolha Painel ou E-mail simulado e clique em **Notificar Responsável**.
   O serviço informa o percentual atual ou parabeniza ao atingir a meta.
   O canal Painel abre uma mensagem; E-mail é uma simulação, sem envio externo.

O histórico tem rolagem horizontal. Dê dois cliques em uma sessão para ler o
feedback completo. Os campos que não pertencem à modalidade ficam desabilitados.

Sem o backend, a tela mantém o botão de carregar disponível e bloqueia novos
registros. Se um envio não for confirmado, consulte o histórico antes de tentar
novamente: em um timeout, o servidor pode ter recebido a requisição.

O relatório segue o modelo simplificado da aula: soma as sessões registradas
para o discente. Não há virada automática da semana nem filtro por período.
A meta pode ser redefinida pelo responsável.

## Organização e conceitos

| Missão | Implementação |
| --- | --- |
| 1 — Classes e objetos | `Disciplina`: código, nome, categoria, carga semanal, resumo e conversão para minutos. Há três disciplinas no console e na tela. |
| 2 — Encapsulamento | `MetaEstudo`: campos privados, avanço validado, meta entre 30 e 3000 e percentual limitado a 100%. |
| 3 — Herança e abstração | `Usuario` abstrata; `Administrador`, `Responsavel` e `Discente`; senha privada, autenticação e resumos sobrescritos. |
| 4 — Polimorfismo dinâmico | `SessaoEstudo` abstrata com `SessaoPomodoro`, `SessaoExercicios` e `SessaoLeitura`, cada uma com sua fórmula. |
| 5 — Sobrecarga | Três formas de `Discente.RegistrarEstudo`: por sessão, por minutos e por horário. |
| 6 — Interfaces | `INotificador`, notificadores de E-mail/Painel e `IExportavelJson` em metas e sessões. |
| 7 — Associação e agregação | Responsável recebe discentes existentes e suas metas; `TrilhaEstudo` recebe disciplinas prontas e soma as cargas. |
| 8 — Composição e dependência | `PlanoEstudo` cria os próprios itens; `ServicoAlertaResponsavel` recebe os objetos por parâmetro, sem guardá-los em campos. |
| 9 — Interface gráfica | `TelaPrincipal`, com controles nativos de Windows Forms, resumos, formulário, progresso e histórico. |
| 10 — Integração | `ClienteBackendEstudo` usa `HttpClient`, JSON, POST de sessões/metas e GET de histórico/relatório. |

As classes de domínio e os exemplos das missões 1 a 8 ficam em
`FecapStudy/FecapStudy`. A tela fica em `FecapStudy/FecapStudyUI` e referencia o
projeto de domínio. `DadosServidor.cs` contém somente os dados recebidos da API.
O histórico antigo é exibido a partir desses dados; os totais são recuperados
sem tentar adivinhar ciclos, questões ou páginas que a API não armazena.

O exemplo anterior às missões permanece em `ConectorBackend.cs`. O console pode
consultar a API com `dotnet run --project FecapStudy/FecapStudy -- --backend`.

A solução usa `List`, `Dictionary`, `if`, `foreach`, construtores, getters,
herança, sobrescrita, sobrecarga e interfaces. A comunicação segue o exemplo de
`HttpClient`, `async` e `await` do material. Na tela, `try/catch` trata falhas e
`finally` libera os botões após a operação. O layout usa `TableLayoutPanel`,
`FlowLayoutPanel` e controles nativos, sem frameworks visuais adicionais.

No backend, uma transação agrupa a inserção da sessão e o avanço da meta: ou os
dois são gravados, ou a operação é desfeita. O conector MySQL foi atualizado
mantendo a mesma biblioteca e a mesma forma de usar SQL com parâmetros.

## Conferência da entrega

- [x] Três disciplinas e seus resumos.
- [x] Meta protegida e validações de valores.
- [x] Três perfis herdando de usuário abstrato.
- [x] Três modalidades, fórmulas de XP e feedbacks polimórficos.
- [x] Três sobrecargas de registro.
- [x] Interfaces de notificação e exportação JSON.
- [x] Associação, agregação, composição e dependência.
- [x] Tela gráfica com formulário, XP, progresso e histórico.
- [x] Notificação do responsável e edição de meta.
- [x] POST de sessões e GET de histórico/relatório.
- [x] Persistência real no MySQL e modo alternativo em JSON.

Os 37 testes de mesa das missões 1 a 8 passaram. A tela também foi conferida
com as três modalidades, total de 235 XP e 75 minutos, recarga sem duplicação,
alteração de meta, relatório e indisponibilidade da API. A gravação foi consultada
diretamente no MySQL e permaneceu após reiniciar o backend. Os testes usaram um
banco separado, `fecap_estudo_teste`, sem incluir seus registros na base principal.

