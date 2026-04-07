Imports System
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.Domain.Exceptions

Namespace SolicitacoesAtendimento.Domain.Entities
    Public Class Solicitacao
        Public Property Id As Integer
        Public Property Titulo As String
        Public Property Descricao As String
        Public Property NomeSolicitante As String
        Public Property DataCriacao As DateTime
        Public Property Status As StatusSolicitacao
        Public Property Prioridade As PrioridadeSolicitacao

        Private Sub New()
        End Sub

        Public Shared Function Criar(titulo As String, descricao As String, nomeSolicitante As String, prioridade As PrioridadeSolicitacao) 
            As Solicitacao

            Dim solicitacao = New Solicitacao With {
                .Titulo = NormalizarTextoObrigatorio(titulo, "O titulo da solicitacao e obrigatorio.", 150, "O titulo da solicitacao deve ter no maximo 150 caracteres."),
                .Descricao = NormalizarTextoObrigatorio(descricao, "A descricao da solicitacao e obrigatoria."),
                .NomeSolicitante = NormalizarTextoObrigatorio(nomeSolicitante, "O nome do solicitante e obrigatorio."),
                .DataCriacao = DateTime.Now,
                .Status = StatusSolicitacao.Aberto,
                .Prioridade = prioridade
            }

            solicitacao.ValidarPrioridade()

            Return solicitacao
        End Function

        Public Function AtualizarStatus(novoStatus As StatusSolicitacao, observacao As String) As HistoricoSolicitacao
            ValidarStatus(novoStatus)
            ValidarTransicao(novoStatus)

            Dim statusAnterior = Status
            Status = novoStatus

            If Id <= 0 Then
                Throw New DomainException("A solicitacao deve possuir identificador valido para gerar historico de status.")
            End If

            Return HistoricoSolicitacao.Criar(Id, statusAnterior, novoStatus, observacao)
        End Function

        Private Sub ValidarPrioridade()
            If Not [Enum].IsDefined(GetType(PrioridadeSolicitacao), Prioridade) Then
                Throw New DomainException("A prioridade informada e invalida.")
            End If
        End Sub

        Private Shared Sub ValidarStatus(status As StatusSolicitacao)
            If Not [Enum].IsDefined(GetType(StatusSolicitacao), status) Then
                Throw New DomainException("O status informado e invalido.")
            End If
        End Sub

        Private Sub ValidarTransicao(novoStatus As StatusSolicitacao)
            If Status = StatusSolicitacao.Cancelado AndAlso novoStatus = StatusSolicitacao.Concluido Then
                Throw New DomainException("Nao e permitido concluir uma solicitacao cancelada.")
            End If

            If Status = StatusSolicitacao.Concluido AndAlso novoStatus = StatusSolicitacao.EmAndamento Then
                Throw New DomainException("Nao e permitido mudar para EmAndamento uma solicitacao ja concluida.")
            End If
        End Sub

        Private Shared Function NormalizarTextoObrigatorio(
            valor As String,
            mensagemObrigatoriedade As String,
            Optional tamanhoMaximo As Integer? = Nothing,
            Optional mensagemTamanho As String = Nothing) As String

            If String.IsNullOrWhiteSpace(valor) Then
                Throw New DomainException(mensagemObrigatoriedade)
            End If

            Dim textoNormalizado = valor.Trim()

            If tamanhoMaximo.HasValue AndAlso textoNormalizado.Length > tamanhoMaximo.Value Then
                Throw New DomainException(mensagemTamanho)
            End If

            Return textoNormalizado
        End Function
    End Class
End Namespace
