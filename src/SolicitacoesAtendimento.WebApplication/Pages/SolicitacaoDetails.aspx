<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoDetails.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoDetails" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes da Solicitacao</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Detalhes da Solicitacao</h1>
            <p>
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
                |
                <asp:HyperLink ID="lnkAtualizarStatus" runat="server">Alterar status</asp:HyperLink>
            </p>

            <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" />

            <table>
                <tr><th align="left">Id</th><td><asp:Literal ID="litId" runat="server" /></td></tr>
                <tr><th align="left">Titulo</th><td><asp:Literal ID="litTitulo" runat="server" /></td></tr>
                <tr><th align="left">Descricao</th><td><asp:Literal ID="litDescricao" runat="server" /></td></tr>
                <tr><th align="left">Solicitante</th><td><asp:Literal ID="litNomeSolicitante" runat="server" /></td></tr>
                <tr><th align="left">Criacao</th><td><asp:Literal ID="litDataCriacao" runat="server" /></td></tr>
                <tr><th align="left">Status</th><td><asp:Literal ID="litStatus" runat="server" /></td></tr>
                <tr><th align="left">Prioridade</th><td><asp:Literal ID="litPrioridade" runat="server" /></td></tr>
            </table>

            <h2>Historico</h2>
            <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhuma alteracao de status registrada." GridLines="Both">
                <Columns>
                    <asp:BoundField DataField="DataAlteracao" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="False" />
                    <asp:BoundField DataField="StatusAnterior" HeaderText="Status anterior" />
                    <asp:BoundField DataField="NovoStatus" HeaderText="Novo status" />
                    <asp:BoundField DataField="Observacao" HeaderText="Observacao" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
