Imports System
Imports System.Collections.Generic
Imports SolicitacoesAtendimento.Application.Commands
Imports SolicitacoesAtendimento.Application.DTOs
Imports SolicitacoesAtendimento.Application.Filters
Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.Domain.Entities
Imports SolicitacoesAtendimento.Domain.Interfaces

Namespace SolicitacoesAtendimento.Application.Services
    Public Class SolicitacaoAppService
        Implements ISolicitacaoAppService

        Private ReadOnly _solicitacaoRepository As ISolicitacaoRepository
        Private ReadOnly _historicoSolicitacaoRepository As IHistoricoSolicitacaoRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Public Sub New(
            solicitacaoRepository As ISolicitacaoRepository,
            historicoSolicitacaoRepository As IHistoricoSolicitacaoRepository,
            unitOfWork As IUnitOfWork)

            If solicitacaoRepository Is Nothing Then
                Throw New ArgumentNullException(NameOf(solicitacaoRepository))
            End If

            If historicoSolicitacaoRepository Is Nothing Then
                Throw New ArgumentNullException(NameOf(historicoSolicitacaoRepository))
            End If

            If unitOfWork Is Nothing Then
                Throw New ArgumentNullException(NameOf(unitOfWork))
            End If

            _solicitacaoRepository = solicitacaoRepository
            _historicoSolicitacaoRepository = historicoSolicitacaoRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Criar(command As CriarSolicitacaoCommand) As Integer Implements ISolicitacaoAppService.Criar
            If command Is Nothing Then
                Throw New ArgumentNullException(NameOf(command))
            End If

            Dim solicitacao As Solicitacao = Solicitacao.Criar(
                command.Titulo,
                command.Descricao,
                command.NomeSolicitante,
                command.Prioridade)

            Dim solicitacaoId = _solicitacaoRepository.Inserir(solicitacao)
            solicitacao.Id = solicitacaoId

            Return solicitacaoId
        End Function

        Public Function Listar(filter As ListarSolicitacoesFilter) As IList(Of SolicitacaoListItemDto) Implements ISolicitacaoAppService.Listar
            Dim filtro = If(filter, New ListarSolicitacoesFilter())

            Dim solicitacoes = _solicitacaoRepository.Listar(
                filtro.Status,
                filtro.Prioridade,
                filtro.DataCriacaoInicial,
                filtro.DataCriacaoFinal,
                filtro.NomeSolicitante)

            Dim itens = New List(Of SolicitacaoListItemDto)()

            If solicitacoes Is Nothing Then
                Return itens
            End If

            For Each solicitacao In solicitacoes
                itens.Add(MapearParaListItemDto(solicitacao))
            Next

            Return itens
        End Function

        Public Function ObterDetalhes(solicitacaoId As Integer) As SolicitacaoDetalhesDto Implements ISolicitacaoAppService.ObterDetalhes
            If solicitacaoId <= 0 Then
                Throw New ArgumentException("O identificador da solicitacao deve ser valido.", NameOf(solicitacaoId))
            End If

            Dim solicitacao = _solicitacaoRepository.ObterPorId(solicitacaoId)

            If solicitacao Is Nothing Then
                Throw New InvalidOperationException("Solicitacao nao encontrada.")
            End If

            Dim historicos = _historicoSolicitacaoRepository.ObterPorSolicitacaoId(solicitacaoId)

            Return MapearParaDetalhesDto(solicitacao, historicos)
        End Function

        Public Sub AtualizarStatus(command As AtualizarStatusSolicitacaoCommand) Implements ISolicitacaoAppService.AtualizarStatus
            If command Is Nothing Then
                Throw New ArgumentNullException(NameOf(command))
            End If

            If command.SolicitacaoId <= 0 Then
                Throw New ArgumentException("O identificador da solicitacao deve ser valido.", NameOf(command.SolicitacaoId))
            End If

            Dim solicitacao = _solicitacaoRepository.ObterPorId(command.SolicitacaoId)

            If solicitacao Is Nothing Then
                Throw New InvalidOperationException("Solicitacao nao encontrada.")
            End If

            Using transaction = _unitOfWork.BeginTransaction()
                Try
                    Dim historico = solicitacao.AtualizarStatus(command.NovoStatus, command.Observacao)

                    _solicitacaoRepository.AtualizarStatus(solicitacao)
                    _historicoSolicitacaoRepository.Inserir(historico)

                    transaction.Commit()
                Catch
                    transaction.Rollback()
                    Throw
                End Try
            End Using
        End Sub

        Private Shared Function MapearParaListItemDto(solicitacao As Solicitacao) As SolicitacaoListItemDto
            If solicitacao Is Nothing Then
                Return Nothing
            End If

            Return New SolicitacaoListItemDto With {
                .Id = solicitacao.Id,
                .Titulo = solicitacao.Titulo,
                .NomeSolicitante = solicitacao.NomeSolicitante,
                .DataCriacao = solicitacao.DataCriacao,
                .Status = solicitacao.Status,
                .Prioridade = solicitacao.Prioridade
            }
        End Function

        Private Shared Function MapearParaDetalhesDto(
            solicitacao As Solicitacao,
            historicos As IList(Of HistoricoSolicitacao)) As SolicitacaoDetalhesDto

            Dim dto = New SolicitacaoDetalhesDto With {
                .Id = solicitacao.Id,
                .Titulo = solicitacao.Titulo,
                .Descricao = solicitacao.Descricao,
                .NomeSolicitante = solicitacao.NomeSolicitante,
                .DataCriacao = solicitacao.DataCriacao,
                .Status = solicitacao.Status,
                .Prioridade = solicitacao.Prioridade
            }

            If historicos Is Nothing Then
                Return dto
            End If

            For Each historico In historicos
                dto.Historicos.Add(MapearParaHistoricoDto(historico))
            Next

            Return dto
        End Function

        Private Shared Function MapearParaHistoricoDto(historico As HistoricoSolicitacao) As HistoricoSolicitacaoDto
            If historico Is Nothing Then
                Return Nothing
            End If

            Return New HistoricoSolicitacaoDto With {
                .Id = historico.Id,
                .SolicitacaoId = historico.SolicitacaoId,
                .StatusAnterior = historico.StatusAnterior,
                .NovoStatus = historico.NovoStatus,
                .DataAlteracao = historico.DataAlteracao,
                .Observacao = historico.Observacao
            }
        End Function
    End Class
End Namespace
