Imports System
Imports System.Web.UI.WebControls
Imports SolicitacoesAtendimento.Application.Commands
Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.WebApplication.App_Start

Namespace SolicitacoesAtendimento.WebApplication.Pages
    Public Class SolicitacaoCreate
        Inherits System.Web.UI.Page

        Private ReadOnly Property AppService As ISolicitacaoAppService
            Get
                Return AppServiceFactory.CreateSolicitacaoAppService()
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                BindPrioridades()
            End If
        End Sub

        Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
            If Not Page.IsValid Then
                Return
            End If

            Try
                Dim solicitacaoId = AppService.Criar(New CriarSolicitacaoCommand With {
                    .Titulo = txtTitulo.Text,
                    .Descricao = txtDescricao.Text,
                    .NomeSolicitante = txtNomeSolicitante.Text,
                    .Prioridade = CType(Convert.ToInt32(ddlPrioridade.SelectedValue), PrioridadeSolicitacao)
                })

                Response.Redirect($"~/Pages/SolicitacaoDetails.aspx?id={solicitacaoId}", False)
                Context.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                ExibirMensagem(ex.Message, True)
            End Try
        End Sub

        Private Sub BindPrioridades()
            ddlPrioridade.Items.Clear()

            For Each prioridade As PrioridadeSolicitacao In [Enum].GetValues(GetType(PrioridadeSolicitacao))
                ddlPrioridade.Items.Add(New ListItem(prioridade.ToString(), CInt(prioridade).ToString()))
            Next
        End Sub

        Private Sub ExibirMensagem(mensagem As String, isError As Boolean)
            lblMensagem.Text = Server.HtmlEncode(mensagem)
            lblMensagem.ForeColor = If(isError, Drawing.Color.DarkRed, Drawing.Color.DarkGreen)
        End Sub
    End Class
End Namespace
