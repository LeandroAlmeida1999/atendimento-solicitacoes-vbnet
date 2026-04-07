Imports System
Imports System.Collections.Generic
Imports SolicitacoesAtendimento.Domain.Entities
Imports SolicitacoesAtendimento.Domain.Enums

Namespace SolicitacoesAtendimento.Domain.Interfaces
    Public Interface ISolicitacaoRepository
        Function Inserir(solicitacao As Solicitacao) As Integer
        Function ObterPorId(solicitacaoId As Integer) As Solicitacao
        Function Listar(
            status As StatusSolicitacao?,
            prioridade As PrioridadeSolicitacao?,
            dataCriacaoInicial As DateTime?,
            dataCriacaoFinal As DateTime?,
            nomeSolicitante As String) As IList(Of Solicitacao)
        Sub AtualizarStatus(solicitacao As Solicitacao)
    End Interface
End Namespace
