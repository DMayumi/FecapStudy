# FecapStudy

Plataforma de apoio ao estudo desenvolvida para a Aula 9 de Programação Orientada
a Objetos (POO), com C#, Windows Forms, API ASP.NET Core e MySQL.

## Funcionalidades

- Registro de sessões Pomodoro, Exercícios e Leitura.
- Cálculo de XP e acompanhamento da meta de estudo.
- Histórico e relatório dos registros salvos no servidor.
- Alteração da meta e notificação simulada do responsável.

O projeto aplica classes, encapsulamento, herança, polimorfismo, sobrecarga,
interfaces, associação, agregação, composição e dependência.

## Como executar

Requisitos: Windows, SDK .NET 10 e MySQL local para persistência no banco.

Na raiz do projeto, execute nesta ordem:

1. `iniciar_mysql.bat` — inicia o MySQL preparado neste computador. Se já houver
   um MySQL ativo na porta 3306, use essa instalação e pule esta etapa.
2. `iniciar_backend.bat` — inicia a API em `http://localhost:5080`.
3. `iniciar_tela.bat` — abre a interface gráfica.

Em outro computador, instale o .NET 10 e o MySQL. Configure a conexão em
`FecapStudy/Backend/mysql_config.txt`. O banco e as tabelas são criados
automaticamente pelo backend.

Confira `http://localhost:5080/api/status`: `usandoMySql: true` indica conexão
com o banco. Sem MySQL, o backend utiliza um arquivo JSON local.

## Estrutura

- `FecapStudy/FecapStudy`: classes e exemplos das missões 1 a 8.
- `FecapStudy/FecapStudyUI`: tela e integração das missões 9 e 10.
- `FecapStudy/Backend`: API, configuração e script SQL.

Para executar os testes de mesa:

```powershell
dotnet run --project FecapStudy/FecapStudy -- --verificar
```

Os perfis e disciplinas são exemplos. As notificações são simuladas, sem envio
externo. O relatório soma as sessões do discente, sem reinício automático semanal.
