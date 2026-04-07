Imports System
Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.DTOs
    Public Class HistoricoSolicitacaoDto
        Public Property Id As Integer
        Public Property SolicitacaoId As Integer
        Public Property StatusAnterior As StatusSolicitacao
        Public Property NovoStatus As StatusSolicitacao
        Public Property DataAlteracao As DateTime
        Public Property Observacao As String
    End Class
End Namespace
