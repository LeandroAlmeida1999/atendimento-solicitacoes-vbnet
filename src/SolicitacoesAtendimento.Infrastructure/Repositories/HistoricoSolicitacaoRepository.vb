Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports SolicitacoesAtendimento.Domain.Entities
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.Domain.Interfaces
Imports SolicitacoesAtendimento.Infrastructure.Data
Imports SolicitacoesAtendimento.Infrastructure.Queries

Namespace SolicitacoesAtendimento.Infrastructure.Repositories
    Public Class HistoricoSolicitacaoRepository
        Implements IHistoricoSolicitacaoRepository

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

        Public Function Inserir(historico As HistoricoSolicitacao) As Integer Implements IHistoricoSolicitacaoRepository.Inserir
            If historico Is Nothing Then
                Throw New ArgumentNullException(NameOf(historico))
            End If

            Return Execute(
                Function(command)
                    command.CommandText = HistoricoSolicitacaoSqlQueries.Inserir
                    command.Parameters.Add("@SolicitacaoId", SqlDbType.Int).Value = historico.SolicitacaoId
                    command.Parameters.Add("@StatusAnterior", SqlDbType.Int).Value = CInt(historico.StatusAnterior)
                    command.Parameters.Add("@NovoStatus", SqlDbType.Int).Value = CInt(historico.NovoStatus)
                    command.Parameters.Add("@DataAlteracao", SqlDbType.DateTime).Value = historico.DataAlteracao
                    command.Parameters.Add("@Observacao", SqlDbType.NVarChar, 500).Value =
                        If(String.IsNullOrWhiteSpace(historico.Observacao), CType(DBNull.Value, Object), historico.Observacao)

                    Return Convert.ToInt32(command.ExecuteScalar())
                End Function)
        End Function

        Public Function ObterPorSolicitacaoId(solicitacaoId As Integer) As IList(Of HistoricoSolicitacao) Implements IHistoricoSolicitacaoRepository.ObterPorSolicitacaoId
            If solicitacaoId <= 0 Then
                Throw New ArgumentException("O identificador da solicitacao deve ser valido.", NameOf(solicitacaoId))
            End If

            Return Execute(
                Function(command)
                    command.CommandText = HistoricoSolicitacaoSqlQueries.ObterPorSolicitacaoId
                    command.Parameters.Add("@SolicitacaoId", SqlDbType.Int).Value = solicitacaoId

                    Dim itens = New List(Of HistoricoSolicitacao)()

                    Using reader = command.ExecuteReader()
                        While reader.Read()
                            itens.Add(MapearHistorico(reader))
                        End While
                    End Using

                    Return CType(itens, IList(Of HistoricoSolicitacao))
                End Function)
        End Function

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

        Private Shared Function MapearHistorico(reader As SqlDataReader) As HistoricoSolicitacao
            Dim observacao As String = Nothing

            If reader("Observacao") IsNot DBNull.Value Then
                observacao = CStr(reader("Observacao"))
            End If

            Return HistoricoSolicitacao.Restaurar(
                CInt(reader("Id")),
                CInt(reader("SolicitacaoId")),
                CType(CInt(reader("StatusAnterior")), StatusSolicitacao),
                CType(CInt(reader("NovoStatus")), StatusSolicitacao),
                CDate(reader("DataAlteracao")),
                observacao)
        End Function
    End Class
End Namespace
