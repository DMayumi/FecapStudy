# FecapStudy — Aula 9

Somente recursos nativos do .NET:
classes, campos privados, construtores, métodos, listas, dicionário, herança,
classes abstratas, sobrescrita, sobrecarga e interfaces.

## Preparação anterior às missões

1. Confira o SDK .NET 10 com `dotnet --version`.
2. Na raiz do repositório, execute `dotnet run --project FecapStudy/FecapStudy`.
3. Inicie `FecapStudy/Backend/iniciar_backend.bat` e abra `http://localhost:5080`.
   O executável precisa do runtime ASP.NET Core 10, também incluído no SDK.
4. Para MySQL, tenha o servidor local ativo e ajuste usuário, porta e senha em
   `FecapStudy/Backend/mysql_config.txt`, ao lado do executável. Não publique
   senhas reais no Git. A configuração existente foi preservada.
5. O backend cria automaticamente o banco `fecap_estudo` e suas quatro tabelas.
   `banco_estudo.sql` é uma alternativa/referência. Não é necessário executar
   também o script; suas inserções podem duplicar dados se ele for repetido.
6. Confira `http://localhost:5080/api/status`: `usandoMySql: true` confirma a
   conexão. Se for `false`, a API está usando `dados_estudo_local.json`

### Execução

Com o SDK .NET 10 instalado e disponível no terminal, execute na raiz do repositório:

```powershell
dotnet run --project FecapStudy/FecapStudy
dotnet run --project FecapStudy/FecapStudy -- --verificar
```

Para iniciar o backend, em outro terminal:

```powershell
.\FecapStudy\Backend\iniciar_backend.bat
```

Se o comando `dotnet` não for reconhecido, instale o SDK .NET 10 ou adicione
sua pasta de instalação ao PATH e reabra o terminal.

O backend pode funcionar com MySQL ou JSON local. Confira o modo ativo em
`http://localhost:5080/api/status`.

### Exemplo de conexão da preparação

`ConectorBackend.cs` segue o exemplo anterior às missões: `HttpClient`, GET e
POST com JSON. Para consultar status e sessões, use:

```powershell
dotnet run --project FecapStudy/FecapStudy -- --backend
```

Essa demonstração apenas consulta a API, sem cadastrar dados a cada execução.
O método de POST está implementado para uso posterior. Nas rotas fornecidas que
exigem filtros, use `?discente=` para listar todos ou
`?discente=Lucas%20Silva` para filtrar; para usuários, use `?perfil=`.

Para a futura tela C#, a aula recomenda Windows Forms. O comando apresentado
é `dotnet new winforms -n FecapStudyUI`, com o SDK Desktop disponível.
A criação da tela pertence à missão 9 e fica para a próxima etapa.

## Missões implementadas

Todos os arquivos estão em `FecapStudy/FecapStudy`.

| Missão | Arquivos principais |
| --- | --- |
| 1 | `Disciplina.cs` |
| 2 | `MetaEstudo.cs` |
| 3 | `Usuario.cs`, `Administrador.cs`, `Responsavel.cs`, `Discente.cs` |
| 4 | `SessaoEstudo.cs`, `SessaoPomodoro.cs`, `SessaoExercicios.cs`, `SessaoLeitura.cs` |
| 5 | Três sobrecargas de `RegistrarEstudo` em `Discente.cs` |
| 6 | `INotificador.cs`, `NotificadorEmail.cs`, `NotificadorPainel.cs`, `IExportavelJson.cs` |
| 7 | `Responsavel.cs`, `TrilhaEstudo.cs` |
| 8 | `PlanoEstudo.cs`, `ItemCronograma.cs`, `ServicoAlertaResponsavel.cs` |

`Program.cs` demonstra as oito etapas em ordem. As disciplinas são exemplos do
material: confirme os nomes e códigos reais do seu semestre antes de entregar.
Usuários e senhas são fictícios, apenas para demonstrar `Autenticar`.

As fórmulas de XP ficam nas subclasses de sessão. Os notificadores de E-mail e
Painel são simulados no console, sem envio externo. O responsável guarda metas
em um dicionário e recebe discentes criados fora dele. A trilha recebe disciplinas
prontas que podem ser compartilhadas com outra trilha. O plano cria os itens
internamente e devolve somente texto e totais. O serviço de alertas não guarda
campos e recebe suas dependências por parâmetro.

Os dados das demonstrações são separados por missão. A meta da missão 2 passa
de 60% para 100% na missão 8; ela não representa a soma do histórico de exemplo
da missão 5. Sincronizar meta e histórico no registro pela tela é parte da missão 9.

## Testes de mesa

```powershell
dotnet run --project FecapStudy/FecapStudy -- --verificar
```

37 verificações passaram: limites da meta, dados inválidos, autenticação,
fórmulas de XP, sobrecargas, JSON e relacionamentos. `Verificacoes.cs` encerra com
erro se alguma verificação falhar, sem depender de um framework de testes.

## Próximas etapas

Missão 9: tela gráfica usando as classes prontas.
Missão 10: registrar e carregar dados pela tela, confirmando persistência no MySQL.
Os DTOs da API fornecida continuam separados das classes de domínio.

