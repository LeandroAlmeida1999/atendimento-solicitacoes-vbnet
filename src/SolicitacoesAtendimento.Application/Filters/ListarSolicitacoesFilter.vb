Imports System
Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.Filters
    Public Class ListarSolicitacoesFilter
        Public Property Status As StatusSolicitacao?
        Public Property Prioridade As PrioridadeSolicitacao?
        Public Property DataCriacaoInicial As DateTime?
        Public Property DataCriacaoFinal As DateTime?
        Public Property NomeSolicitante As String
    End Class
End Namespace
