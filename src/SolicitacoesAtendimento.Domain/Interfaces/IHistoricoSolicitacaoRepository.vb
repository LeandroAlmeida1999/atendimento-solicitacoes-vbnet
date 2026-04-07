Imports System.Collections.Generic
Imports SolicitacoesAtendimento.Domain.Entities

Namespace SolicitacoesAtendimento.Domain.Interfaces
    Public Interface IHistoricoSolicitacaoRepository
        Function Inserir(historico As HistoricoSolicitacao) As Integer
        Function ObterPorSolicitacaoId(solicitacaoId As Integer) As IList(Of HistoricoSolicitacao)
    End Interface
End Namespace
