Imports System
Imports System.Collections.Generic
Imports System.Web.UI.WebControls
Imports SolicitacoesAtendimento.Application.Commands
Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.WebApplication.App_Start

Namespace SolicitacoesAtendimento.WebApplication.Pages
    Public Class SolicitacaoUpdateStatus
        Inherits System.Web.UI.Page

        Private ReadOnly Property AppService As ISolicitacaoAppService
            Get
                Return AppServiceFactory.CreateSolicitacaoAppService()
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                CarregarDados()
            End If
        End Sub

        Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
            Try
                Dim solicitacaoId = Convert.ToInt32(hfSolicitacaoId.Value)

                AppService.AtualizarStatus(New AtualizarStatusSolicitacaoCommand With {
                    .SolicitacaoId = solicitacaoId,
                    .NovoStatus = CType(Convert.ToInt32(ddlNovoStatus.SelectedValue), StatusSolicitacao),
                    .Observacao = txtObservacao.Text
                })

                Response.Redirect($"~/Pages/SolicitacaoDetails.aspx?id={solicitacaoId}", False)
                Context.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                ExibirMensagem(ex.Message)
                CarregarDados()
            End Try
        End Sub

        Private Sub CarregarDados()
            Dim solicitacaoId = ObterSolicitacaoId()
            Dim dto = AppService.ObterDetalhes(solicitacaoId)

            hfSolicitacaoId.Value = dto.Id.ToString()
            litTitulo.Text = Server.HtmlEncode(dto.Titulo)
            litNomeSolicitante.Text = Server.HtmlEncode(dto.NomeSolicitante)
            litStatusAtual.Text = dto.Status.ToString()
            litPrioridade.Text = dto.Prioridade.ToString()
            lnkDetalhes.NavigateUrl = $"~/Pages/SolicitacaoDetails.aspx?id={dto.Id}"

            BindStatusDisponiveis(dto.Status)

            gvHistorico.DataSource = dto.Historicos
            gvHistorico.DataBind()
        End Sub

        Private Function ObterSolicitacaoId() As Integer
            If Not String.IsNullOrWhiteSpace(hfSolicitacaoId.Value) Then
                Return Convert.ToInt32(hfSolicitacaoId.Value)
            End If

            Dim rawId = Request.QueryString("id")
            Dim solicitacaoId As Integer

            If Not Integer.TryParse(rawId, solicitacaoId) OrElse solicitacaoId <= 0 Then
                Throw New InvalidOperationException("Informe um identificador de solicitacao valido.")
            End If

            Return solicitacaoId
        End Function

        Private Sub BindStatusDisponiveis(statusAtual As StatusSolicitacao)
            ddlNovoStatus.Items.Clear()

            For Each status In ObterStatusPermitidos(statusAtual)
                ddlNovoStatus.Items.Add(New ListItem(status.ToString(), CInt(status).ToString()))
            Next

            btnSalvar.Enabled = ddlNovoStatus.Items.Count > 0

            If ddlNovoStatus.Items.Count = 0 Then
                ExibirMensagem("Nao ha transicoes disponiveis para o status atual.", True)
            End If
        End Sub

        Private Shared Function ObterStatusPermitidos(statusAtual As StatusSolicitacao) As IList(Of StatusSolicitacao)
            Dim itens = New List(Of StatusSolicitacao)()

            For Each status As StatusSolicitacao In [Enum].GetValues(GetType(StatusSolicitacao))
                If status = statusAtual Then
                    Continue For
                End If

                If statusAtual = StatusSolicitacao.Cancelado AndAlso status = StatusSolicitacao.Concluido Then
                    Continue For
                End If

                If statusAtual = StatusSolicitacao.Concluido AndAlso status = StatusSolicitacao.EmAndamento Then
                    Continue For
                End If

                itens.Add(status)
            Next

            Return itens
        End Function

        Private Sub ExibirMensagem(mensagem As String, Optional isError As Boolean = True)
            lblMensagem.Text = Server.HtmlEncode(mensagem)
            lblMensagem.ForeColor = If(isError, Drawing.Color.DarkRed, Drawing.Color.DarkGreen)
        End Sub
    End Class
End Namespace
