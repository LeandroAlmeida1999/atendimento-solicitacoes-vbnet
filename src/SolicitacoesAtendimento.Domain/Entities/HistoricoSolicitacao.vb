Imports System
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.Domain.Exceptions

Namespace SolicitacoesAtendimento.Domain.Entities
    Public Class HistoricoSolicitacao
        Public Property Id As Integer
        Public Property SolicitacaoId As Integer
        Public Property StatusAnterior As StatusSolicitacao
        Public Property NovoStatus As StatusSolicitacao
        Public Property DataAlteracao As DateTime
        Public Property Observacao As String

        Private Sub New()
        End Sub

        Public Shared Function Criar(
            solicitacaoId As Integer,
            statusAnterior As StatusSolicitacao,
            novoStatus As StatusSolicitacao,
            observacao As String) As HistoricoSolicitacao

            If solicitacaoId <= 0 Then
                Throw New DomainException("O identificador da solicitacao deve ser valido para gerar historico.")
            End If

            ValidarStatus(statusAnterior, NameOf(statusAnterior))
            ValidarStatus(novoStatus, NameOf(novoStatus))

            Return New HistoricoSolicitacao With {
                .SolicitacaoId = solicitacaoId,
                .StatusAnterior = statusAnterior,
                .NovoStatus = novoStatus,
                .DataAlteracao = DateTime.Now,
                .Observacao = NormalizarObservacao(observacao)
            }
        End Function

        Public Shared Function Restaurar(
            id As Integer,
            solicitacaoId As Integer,
            statusAnterior As StatusSolicitacao,
            novoStatus As StatusSolicitacao,
            dataAlteracao As DateTime,
            observacao As String) As HistoricoSolicitacao

            If id <= 0 Then
                Throw New DomainException("O identificador do historico deve ser valido.")
            End If

            If solicitacaoId <= 0 Then
                Throw New DomainException("O identificador da solicitacao deve ser valido para restaurar historico.")
            End If

            If dataAlteracao = DateTime.MinValue Then
                Throw New DomainException("A data de alteracao do historico deve ser valida.")
            End If

            ValidarStatus(statusAnterior, NameOf(statusAnterior))
            ValidarStatus(novoStatus, NameOf(novoStatus))

            Return New HistoricoSolicitacao With {
                .Id = id,
                .SolicitacaoId = solicitacaoId,
                .StatusAnterior = statusAnterior,
                .NovoStatus = novoStatus,
                .DataAlteracao = dataAlteracao,
                .Observacao = NormalizarObservacao(observacao)
            }
        End Function

        Private Shared Sub ValidarStatus(status As StatusSolicitacao, nomeCampo As String)
            If Not [Enum].IsDefined(GetType(StatusSolicitacao), status) Then
                Throw New DomainException($"O status informado em {nomeCampo} e invalido.")
            End If
        End Sub

        Private Shared Function NormalizarObservacao(observacao As String) As String
            If String.IsNullOrWhiteSpace(observacao) Then
                Return Nothing
            End If

            Return observacao.Trim()
        End Function
    End Class
End Namespace
