Imports System
Imports System.Collections.Generic
Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.DTOs
    Public Class SolicitacaoDetalhesDto
        Public Sub New()
            
            Historicos = New List(Of HistoricoSolicitacaoDto)()
        End Sub

        Public Property Id As Integer
        Public Property Titulo As String
        Public Property Descricao As String
        Public Property NomeSolicitante As String
        Public Property DataCriacao As DateTime
        Public Property Status As StatusSolicitacao
        Public Property Prioridade As PrioridadeSolicitacao
        Public Property Historicos As IList(Of HistoricoSolicitacaoDto)
    End Class
End Namespace
