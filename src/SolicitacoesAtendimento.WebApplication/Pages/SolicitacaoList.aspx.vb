Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Web.UI.WebControls
Imports SolicitacoesAtendimento.Application.DTOs
Imports SolicitacoesAtendimento.Application.Filters
Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.Domain.Enums
Imports SolicitacoesAtendimento.WebApplication.App_Start

Namespace SolicitacoesAtendimento.WebApplication.Pages
    Public Class SolicitacaoList
        Inherits System.Web.UI.Page

        Private ReadOnly Property AppService As ISolicitacaoAppService
            Get
                Return AppServiceFactory.CreateSolicitacaoAppService()
            End Get
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                BindStatus()
                BindPrioridades()
                CarregarSolicitacoes()
            End If
        End Sub

        Protected Sub btnPesquisar_Click(sender As Object, e As EventArgs) Handles btnPesquisar.Click
            CarregarSolicitacoes()
        End Sub

        Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
            ddlStatus.SelectedIndex = 0
            ddlPrioridade.SelectedIndex = 0
            txtNomeSolicitante.Text = String.Empty
            txtDataInicial.Text = String.Empty
            txtDataFinal.Text = String.Empty
            ExibirMensagem(String.Empty, False)
            CarregarSolicitacoes()
        End Sub

        Private Sub CarregarSolicitacoes()
            Try
                Dim filtro = ConstruirFiltro()
                gvSolicitacoes.DataSource = AppService.Listar(filtro)
                gvSolicitacoes.DataBind()
            Catch ex As Exception
                gvSolicitacoes.DataSource = New List(Of SolicitacaoListItemDto)()
                gvSolicitacoes.DataBind()
                ExibirMensagem(ex.Message, True)
            End Try
        End Sub

        Private Function ConstruirFiltro() As ListarSolicitacoesFilter
            Dim dataInicial = ParseNullableDate(txtDataInicial.Text)
            Dim dataFinal = ParseNullableDate(txtDataFinal.Text)

            If dataInicial.HasValue AndAlso dataFinal.HasValue AndAlso dataInicial.Value.Date > dataFinal.Value.Date Then
                Throw New InvalidOperationException("A data inicial nao pode ser maior que a data final.")
            End If

            If dataFinal.HasValue Then
                dataFinal = dataFinal.Value.Date.AddDays(1).AddTicks(-1)
            End If

            Return New ListarSolicitacoesFilter With {
                .Status = ParseNullableStatus(ddlStatus.SelectedValue),
                .Prioridade = ParseNullablePrioridade(ddlPrioridade.SelectedValue),
                .NomeSolicitante = txtNomeSolicitante.Text,
                .DataCriacaoInicial = dataInicial,
                .DataCriacaoFinal = dataFinal
            }
        End Function

        Private Sub BindStatus()
            ddlStatus.Items.Clear()
            ddlStatus.Items.Add(New ListItem("Todos", String.Empty))

            For Each status As StatusSolicitacao In [Enum].GetValues(GetType(StatusSolicitacao))
                ddlStatus.Items.Add(New ListItem(status.ToString(), CInt(status).ToString()))
            Next
        End Sub

        Private Sub BindPrioridades()
            ddlPrioridade.Items.Clear()
            ddlPrioridade.Items.Add(New ListItem("Todas", String.Empty))

            For Each prioridade As PrioridadeSolicitacao In [Enum].GetValues(GetType(PrioridadeSolicitacao))
                ddlPrioridade.Items.Add(New ListItem(prioridade.ToString(), CInt(prioridade).ToString()))
            Next
        End Sub

        Private Shared Function ParseNullableStatus(value As String) As StatusSolicitacao?
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If

            Return CType(Convert.ToInt32(value), StatusSolicitacao)
        End Function

        Private Shared Function ParseNullablePrioridade(value As String) As PrioridadeSolicitacao?
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If

            Return CType(Convert.ToInt32(value), PrioridadeSolicitacao)
        End Function

        Private Shared Function ParseNullableDate(value As String) As DateTime?
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        End Function

        Private Sub ExibirMensagem(mensagem As String, isError As Boolean)
            lblMensagem.Text = Server.HtmlEncode(mensagem)
            lblMensagem.ForeColor = If(isError, Drawing.Color.DarkRed, Drawing.Color.DarkGreen)
        End Sub
    End Class
End Namespace
