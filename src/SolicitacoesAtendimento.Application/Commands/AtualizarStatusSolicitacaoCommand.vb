Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.Commands
    Public Class AtualizarStatusSolicitacaoCommand
        Public Property SolicitacaoId As Integer
        Public Property NovoStatus As StatusSolicitacao
        Public Property Observacao As String
    End Class
End Namespace
