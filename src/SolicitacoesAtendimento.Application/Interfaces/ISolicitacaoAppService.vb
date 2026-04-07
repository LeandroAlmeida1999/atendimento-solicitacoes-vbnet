Imports System.Collections.Generic
Imports SolicitacoesAtendimento.Application.Commands
Imports SolicitacoesAtendimento.Application.DTOs
Imports SolicitacoesAtendimento.Application.Filters

Namespace SolicitacoesAtendimento.Application.Interfaces
    Public Interface ISolicitacaoAppService
        Function Criar(command As CriarSolicitacaoCommand) As Integer
        Function Listar(filter As ListarSolicitacoesFilter) As IList(Of SolicitacaoListItemDto)
        Function ObterDetalhes(solicitacaoId As Integer) As SolicitacaoDetalhesDto
        Sub AtualizarStatus(command As AtualizarStatusSolicitacaoCommand)
    End Interface
End Namespace
