<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoUpdateStatus.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoUpdateStatus" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Atualizar Status</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Atualizar Status</h1>
            <p>
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
                |
                <asp:HyperLink ID="lnkDetalhes" runat="server">Ver detalhes</asp:HyperLink>
            </p>

            <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" />
            <asp:HiddenField ID="hfSolicitacaoId" runat="server" />

            <table>
                <tr><th align="left">Titulo</th><td><asp:Literal ID="litTitulo" runat="server" /></td></tr>
                <tr><th align="left">Solicitante</th><td><asp:Literal ID="litNomeSolicitante" runat="server" /></td></tr>
                <tr><th align="left">Status atual</th><td><asp:Literal ID="litStatusAtual" runat="server" /></td></tr>
                <tr><th align="left">Prioridade</th><td><asp:Literal ID="litPrioridade" runat="server" /></td></tr>
            </table>

            <div>
                <label for="ddlNovoStatus">Novo status</label><br />
                <asp:DropDownList ID="ddlNovoStatus" runat="server" Width="220" />
            </div>

            <div>
                <label for="txtObservacao">Observacao</label><br />
                <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Rows="4" Width="420" />
            </div>

            <div style="margin-top: 16px;">
                <asp:Button ID="btnSalvar" runat="server" Text="Atualizar status" />
            </div>

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
