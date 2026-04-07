# Solicitacoes de Atendimento - VB.NET WebForms

## Banco local

O projeto Web esta configurado para usar a seguinte `connection string`:

- Servidor: `(localdb)\MSSQLLocalDB`
- Banco: `SolicitacoesAtendimento`
- Autenticacao: `Windows Authentication`

Arquivo de configuracao:

- [Web.config]

## Como criar o banco no LocalDB

Abra um `PowerShell` na raiz do repositorio e execute os comandos abaixo.

### 1. Iniciar a instancia do LocalDB

```powershell
sqllocaldb start MSSQLLocalDB
```

### 2. Criar o banco

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "IF DB_ID('SolicitacoesAtendimento') IS NULL CREATE DATABASE SolicitacoesAtendimento;"
```

### 3. Aplicar o script do projeto

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d "SolicitacoesAtendimento" -i ".\database\script.sql"
```

## Como validar se o banco foi criado

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -d "SolicitacoesAtendimento" -Q "SELECT name FROM sys.tables ORDER BY name;"
```
