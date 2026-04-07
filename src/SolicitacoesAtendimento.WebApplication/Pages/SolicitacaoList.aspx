<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoList.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitacoes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Solicitacoes</h1>
            <p>
                <asp:HyperLink ID="lnkNovaSolicitacao" runat="server" NavigateUrl="~/Pages/SolicitacaoCreate.aspx">Cadastrar nova solicitacao</asp:HyperLink>
            </p>

            <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" />

            <fieldset>
                <legend>Filtros</legend>

                <div>
                    <label for="ddlStatus">Status</label><br />
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="180" />
                </div>

                <div>
                    <label for="ddlPrioridade">Prioridade</label><br />
                    <asp:DropDownList ID="ddlPrioridade" runat="server" Width="180" />
                </div>

                <div>
                    <label for="txtNomeSolicitante">Solicitante</label><br />
                    <asp:TextBox ID="txtNomeSolicitante" runat="server" Width="240" />
                </div>

                <div>
                    <label for="txtDataInicial">Data inicial</label><br />
                    <asp:TextBox ID="txtDataInicial" runat="server" TextMode="Date" />
                </div>

                <div>
                    <label for="txtDataFinal">Data final</label><br />
                    <asp:TextBox ID="txtDataFinal" runat="server" TextMode="Date" />
                </div>

                <div style="margin-top: 12px;">
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" />
                    <asp:Button ID="btnLimpar" runat="server" Text="Limpar filtros" CausesValidation="false" />
                </div>
            </fieldset>

            <asp:GridView ID="gvSolicitacoes" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhuma solicitacao encontrada." GridLines="Both">
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
            </asp:GridView>
        </div>
    </form>
</body>
</html>
