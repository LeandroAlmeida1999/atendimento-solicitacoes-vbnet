Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports SolicitacoesAtendimento.Domain.Entities
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.Domain.Interfaces
Imports SolicitacoesAtendimento.Infrastructure.Data
Imports SolicitacoesAtendimento.Infrastructure.Queries

Namespace SolicitacoesAtendimento.Infrastructure.Repositories
    Public Class SolicitacaoRepository
        Implements ISolicitacaoRepository

        Private ReadOnly _connectionFactory As SqlConnectionFactory
        Private ReadOnly _executionContext As SqlExecutionContext

        Public Sub New(connectionFactory As SqlConnectionFactory, executionContext As SqlExecutionContext)
            If connectionFactory Is Nothing Then
                Throw New ArgumentNullException(NameOf(connectionFactory))
            End If

            If executionContext Is Nothing Then
                Throw New ArgumentNullException(NameOf(executionContext))
            End If

            _connectionFactory = connectionFactory
            _executionContext = executionContext
        End Sub

        Public Function Inserir(solicitacao As Solicitacao) As Integer Implements ISolicitacaoRepository.Inserir
            If solicitacao Is Nothing Then
                Throw New ArgumentNullException(NameOf(solicitacao))
            End If

            Return Execute(
                Function(command)
                    command.CommandText = SolicitacaoSqlQueries.Inserir
                    AdicionarParametrosDeSolicitacao(command, solicitacao)
                    Return Convert.ToInt32(command.ExecuteScalar())
                End Function)
        End Function

        Public Function ObterPorId(solicitacaoId As Integer) As Solicitacao Implements ISolicitacaoRepository.ObterPorId
            If solicitacaoId <= 0 Then
                Throw New ArgumentException("O identificador da solicitacao deve ser valido.", NameOf(solicitacaoId))
            End If

            Return Execute(
                Function(command)
                    command.CommandText = SolicitacaoSqlQueries.ObterPorId
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = solicitacaoId

                    Using reader = command.ExecuteReader()
                        If Not reader.Read() Then
                            Return Nothing
                        End If

                        Return MapearSolicitacao(reader)
                    End Using
                End Function)
        End Function

        Public Function Listar(
            status As StatusSolicitacao?,
            prioridade As PrioridadeSolicitacao?,
            dataCriacaoInicial As DateTime?,
            dataCriacaoFinal As DateTime?,
            nomeSolicitante As String) As IList(Of Solicitacao) Implements ISolicitacaoRepository.Listar

            Return Execute(
                Function(command)
                    Dim sql = New StringBuilder(SolicitacaoSqlQueries.ListarBase)

                    If status.HasValue Then
                        sql.Append(" AND Status = @Status")
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = CInt(status.Value)
                    End If

                    If prioridade.HasValue Then
                        sql.Append(" AND Prioridade = @Prioridade")
                        command.Parameters.Add("@Prioridade", SqlDbType.Int).Value = CInt(prioridade.Value)
                    End If

                    If dataCriacaoInicial.HasValue Then
                        sql.Append(" AND DataCriacao >= @DataCriacaoInicial")
                        command.Parameters.Add("@DataCriacaoInicial", SqlDbType.DateTime).Value = dataCriacaoInicial.Value
                    End If

                    If dataCriacaoFinal.HasValue Then
                        sql.Append(" AND DataCriacao <= @DataCriacaoFinal")
                        command.Parameters.Add("@DataCriacaoFinal", SqlDbType.DateTime).Value = dataCriacaoFinal.Value
                    End If

                    If Not String.IsNullOrWhiteSpace(nomeSolicitante) Then
                        sql.Append(" AND NomeSolicitante LIKE @NomeSolicitante")
                        command.Parameters.Add("@NomeSolicitante", SqlDbType.NVarChar, 150).Value = $"%{nomeSolicitante.Trim()}%"
                    End If

                    sql.Append(" ORDER BY DataCriacao DESC, Id DESC")
                    command.CommandText = sql.ToString()

                    Dim itens = New List(Of Solicitacao)()

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            itens.Add(MapearSolicitacao(reader))
                        End While
                    End Using

                    Return CType(itens, IList(Of Solicitacao))
                End Function)
        End Function

        Public Sub AtualizarStatus(solicitacao As Solicitacao) Implements ISolicitacaoRepository.AtualizarStatus
            If solicitacao Is Nothing Then
                Throw New ArgumentNullException(NameOf(solicitacao))
            End If

            Execute(
                Function(command)
                    command.CommandText = SolicitacaoSqlQueries.AtualizarStatus
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = solicitacao.Id
                    command.Parameters.Add("@Status", SqlDbType.Int).Value = CInt(solicitacao.Status)

                    Dim rowsAffected = command.ExecuteNonQuery()

                    If rowsAffected = 0 Then
                        Throw New InvalidOperationException("Nenhuma solicitacao foi atualizada.")
                    End If

                    Return True
                End Function)
        End Sub

        Private Function Execute(Of T)(executor As Func(Of SqlCommand, T)) As T
            If _executionContext.HasActiveTransaction Then
                Using command = CriarComando(_executionContext.CurrentConnection, _executionContext.CurrentTransaction)
                    Return executor(command)
                End Using
            End If

            Using connection = _connectionFactory.CreateOpenConnection()
                Using command = CriarComando(connection, Nothing)
                    Return executor(command)
                End Using
            End Using
        End Function

        Private Shared Function CriarComando(connection As SqlConnection, transaction As SqlTransaction) As SqlCommand
            Dim command = connection.CreateCommand()
            command.Transaction = transaction
            command.CommandType = CommandType.Text
            Return command
        End Function

        Private Shared Sub AdicionarParametrosDeSolicitacao(command As SqlCommand, solicitacao As Solicitacao)
            command.Parameters.Add("@Titulo", SqlDbType.NVarChar, 150).Value = solicitacao.Titulo
            command.Parameters.Add("@Descricao", SqlDbType.NVarChar, -1).Value = solicitacao.Descricao
            command.Parameters.Add("@NomeSolicitante", SqlDbType.NVarChar, 150).Value = solicitacao.NomeSolicitante
            command.Parameters.Add("@DataCriacao", SqlDbType.DateTime).Value = solicitacao.DataCriacao
            command.Parameters.Add("@Status", SqlDbType.Int).Value = CInt(solicitacao.Status)
            command.Parameters.Add("@Prioridade", SqlDbType.Int).Value = CInt(solicitacao.Prioridade)
        End Sub

        Private Shared Function MapearSolicitacao(reader As SqlDataReader) As Solicitacao
            Return Solicitacao.Restaurar(
                CInt(reader("Id")),
                CStr(reader("Titulo")),
                CStr(reader("Descricao")),
                CStr(reader("NomeSolicitante")),
                CDate(reader("DataCriacao")),
                CType(CInt(reader("Status")), StatusSolicitacao),
                CType(CInt(reader("Prioridade")), PrioridadeSolicitacao))
        End Function
    End Class
End Namespace
