<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoList.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitacoes</title>
    <link href="../Content/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <section class="hero-card">
                <h1 class="hero-title">Solicitacoes</h1>
                <p class="hero-subtitle">Acompanhe, filtre e navegue pelas solicitacoes de atendimento cadastradas.</p>
                <div class="actions-row">
                <asp:HyperLink ID="lnkNovaSolicitacao" runat="server" NavigateUrl="~/Pages/SolicitacaoCreate.aspx">Cadastrar nova solicitacao</asp:HyperLink>
                </div>
            </section>

            <section class="panel-card">
                <h2 class="section-title">Filtros</h2>
                <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" CssClass="message" />

                <div class="filters-grid">
                    <div class="field">
                        <label for="ddlStatus">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="text-select" />
                    </div>

                    <div class="field">
                        <label for="ddlPrioridade">Prioridade</label>
                        <asp:DropDownList ID="ddlPrioridade" runat="server" CssClass="text-select" />
                    </div>

                    <div class="field">
                        <label for="txtNomeSolicitante">Solicitante</label>
                        <asp:TextBox ID="txtNomeSolicitante" runat="server" CssClass="text-input" />
                    </div>

                    <div class="field">
                        <label for="txtDataInicial">Data inicial</label>
                        <asp:TextBox ID="txtDataInicial" runat="server" TextMode="Date" CssClass="text-input" />
                    </div>

                    <div class="field">
                        <label for="txtDataFinal">Data final</label>
                        <asp:TextBox ID="txtDataFinal" runat="server" TextMode="Date" CssClass="text-input" />
                    </div>
                </div>

                <div class="button-row">
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary" />
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" CausesValidation="false" CssClass="btn btn-secondary" />
                </div>
            </section>

            <section class="table-card">
                <asp:GridView ID="gvSolicitacoes" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhuma solicitacao encontrada." GridLines="None" CssClass="data-grid">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                        <asp:BoundField DataField="NomeSolicitante" HeaderText="Solicitante" />
                        <asp:BoundField DataField="DataCriacao" HeaderText="Criacao" DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="False" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Prioridade" HeaderText="Prioridade" />
                        <asp:HyperLinkField HeaderText="Detalhes" Text="Visualizar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Pages/SolicitacaoDetails.aspx?id={0}" />
                        <asp:HyperLinkField HeaderText="Status" Text="Alterar status" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Pages/SolicitacaoUpdateStatus.aspx?id={0}" />
                    </Columns>
                    <EmptyDataRowStyle CssClass="empty-text" />
                </asp:GridView>
            </section>
        </div>
    </form>
</body>
</html>
