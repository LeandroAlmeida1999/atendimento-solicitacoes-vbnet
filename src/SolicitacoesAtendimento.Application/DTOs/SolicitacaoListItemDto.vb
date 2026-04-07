Imports System
Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.DTOs
    Public Class SolicitacaoListItemDto
        Public Property Id As Integer
        Public Property Titulo As String
        Public Property NomeSolicitante As String
        Public Property DataCriacao As DateTime
        Public Property Status As StatusSolicitacao
        Public Property Prioridade As PrioridadeSolicitacao
    End Class
End Namespace
