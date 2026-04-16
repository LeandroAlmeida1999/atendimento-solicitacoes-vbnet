<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoUpdateStatus.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoUpdateStatus" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Atualizar Status</title>
    <link href="../Content/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <section class="hero-card">
                <h1 class="hero-title">Atualizar Status</h1>
                <p class="hero-subtitle">Aplique uma transicao de status e registre uma observacao opcional.</p>
                <div class="actions-row">
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
                <asp:HyperLink ID="lnkDetalhes" runat="server">Ver detalhes</asp:HyperLink>
                </div>
            </section>

            <section class="panel-card">
                <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" CssClass="message" />
                <asp:HiddenField ID="hfSolicitacaoId" runat="server" />

                <dl class="info-grid" style="margin-bottom: 20px;">
                    <div class="info-item">
                        <dt>Titulo</dt>
                        <dd><asp:Literal ID="litTitulo" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Solicitante</dt>
                        <dd><asp:Literal ID="litNomeSolicitante" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Status atual</dt>
                        <dd><asp:Literal ID="litStatusAtual" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Prioridade</dt>
                        <dd><asp:Literal ID="litPrioridade" runat="server" /></dd>
                    </div>
                </dl>

                <div class="form-grid">
                    <div class="field">
                        <label for="ddlNovoStatus">Novo status</label>
                        <asp:DropDownList ID="ddlNovoStatus" runat="server" CssClass="text-select" />
                    </div>

                    <div class="field-full">
                        <label for="txtObservacao">Observacao</label>
                        <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="4" CssClass="text-area" />
                    </div>
                </div>

                <div class="button-row">
                    <asp:Button ID="btnSalvar" runat="server" Text="Atualizar status" CssClass="btn btn-primary" />
                </div>
            </section>

            <section class="table-card">
                <h2 class="section-title">Historico</h2>
                <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhuma alteracao de status registrada." GridLines="None" CssClass="data-grid">
                    <Columns>
                        <asp:BoundField DataField="DataAlteracao" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="False" />
                        <asp:BoundField DataField="StatusAnterior" HeaderText="Status anterior" />
                        <asp:BoundField DataField="NovoStatus" HeaderText="Novo status" />
                        <asp:BoundField DataField="Observacao" HeaderText="Observacao" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="empty-text" />
                </asp:GridView>
            </section>
        </div>
    </form>
</body>
</html>
