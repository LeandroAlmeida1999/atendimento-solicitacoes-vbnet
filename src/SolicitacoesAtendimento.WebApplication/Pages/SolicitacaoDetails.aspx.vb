Imports System
Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.WebApplication.App_Start

Namespace SolicitacoesAtendimento.WebApplication.Pages
    Public Class SolicitacaoDetails
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

        Private Sub CarregarDados()
            Try
                Dim solicitacaoId = ObterSolicitacaoId()
                Dim dto = AppService.ObterDetalhes(solicitacaoId)

                litId.Text = dto.Id.ToString()
                litTitulo.Text = Server.HtmlEncode(dto.Titulo)
                litDescricao.Text = Server.HtmlEncode(dto.Descricao).Replace(Environment.NewLine, "<br />")
                litNomeSolicitante.Text = Server.HtmlEncode(dto.NomeSolicitante)
                litDataCriacao.Text = dto.DataCriacao.ToString("dd/MM/yyyy HH:mm")
                litStatus.Text = dto.Status.ToString()
                litPrioridade.Text = dto.Prioridade.ToString()
                lnkAtualizarStatus.NavigateUrl = $"~/Pages/SolicitacaoUpdateStatus.aspx?id={dto.Id}"

                gvHistorico.DataSource = dto.Historicos
                gvHistorico.DataBind()
            Catch ex As Exception
                ExibirMensagem(ex.Message)
                lnkAtualizarStatus.Visible = False
                gvHistorico.DataSource = Nothing
                gvHistorico.DataBind()
            End Try
        End Sub

        Private Function ObterSolicitacaoId() As Integer
            Dim rawId = Request.QueryString("id")
            Dim solicitacaoId As Integer

            If Not Integer.TryParse(rawId, solicitacaoId) OrElse solicitacaoId <= 0 Then
                Throw New InvalidOperationException("Informe um identificador de solicitacao valido.")
            End If

            Return solicitacaoId
        End Function

        Private Sub ExibirMensagem(mensagem As String)
            lblMensagem.Text = Server.HtmlEncode(mensagem)
            lblMensagem.ForeColor = Drawing.Color.DarkRed
        End Sub
    End Class
End Namespace
