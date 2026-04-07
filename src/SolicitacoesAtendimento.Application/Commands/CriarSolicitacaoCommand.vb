Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Application.Commands
    Public Class CriarSolicitacaoCommand
        Public Property Titulo As String
        Public Property Descricao As String
        Public Property NomeSolicitante As String
        Public Property Prioridade As PrioridadeSolicitacao
    End Class
End Namespace
