<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoDetails.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoDetails" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes da Solicitacao</title>
    <link href="../Content/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <section class="hero-card">
                <h1 class="hero-title">Detalhes da Solicitacao</h1>
                <p class="hero-subtitle">Visualize os dados principais e o historico de alteracoes.</p>
                <div class="actions-row">
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
                <asp:HyperLink ID="lnkAtualizarStatus" runat="server">Alterar status</asp:HyperLink>
                </div>
            </section>

            <section class="panel-card">
                <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" CssClass="message" />

                <dl class="info-grid">
                    <div class="info-item">
                        <dt>Id</dt>
                        <dd><asp:Literal ID="litId" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Titulo</dt>
                        <dd><asp:Literal ID="litTitulo" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Descricao</dt>
                        <dd><asp:Literal ID="litDescricao" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Solicitante</dt>
                        <dd><asp:Literal ID="litNomeSolicitante" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Criacao</dt>
                        <dd><asp:Literal ID="litDataCriacao" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Status</dt>
                        <dd><asp:Literal ID="litStatus" runat="server" /></dd>
                    </div>
                    <div class="info-item">
                        <dt>Prioridade</dt>
                        <dd><asp:Literal ID="litPrioridade" runat="server" /></dd>
                    </div>
                </dl>
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
