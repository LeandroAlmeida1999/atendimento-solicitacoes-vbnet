<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoCreate.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoCreate" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastrar Solicitacao</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Cadastrar Solicitacao</h1>
            <p>
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
            </p>

            <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" />
            <asp:ValidationSummary ID="vsErros" runat="server" HeaderText="Corrija os campos abaixo:" />

            <div>
                <label for="txtTitulo">Titulo</label><br />
                <asp:TextBox ID="txtTitulo" runat="server" MaxLength="150" Width="420" />
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Informe o titulo." Display="Dynamic" />
            </div>

            <div>
                <label for="txtDescricao">Descricao</label><br />
                <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="6" Width="420" />
                <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Informe a descricao." Display="Dynamic" />
            </div>

            <div>
                <label for="txtNomeSolicitante">Solicitante</label><br />
                <asp:TextBox ID="txtNomeSolicitante" runat="server" Width="420" />
                <asp:RequiredFieldValidator ID="rfvNomeSolicitante" runat="server" ControlToValidate="txtNomeSolicitante" ErrorMessage="Informe o nome do solicitante." Display="Dynamic" />
            </div>

            <div>
                <label for="ddlPrioridade">Prioridade</label><br />
                <asp:DropDownList ID="ddlPrioridade" runat="server" Width="220" />
            </div>

            <div style="margin-top: 16px;">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" />
            </div>
        </div>
    </form>
</body>
</html>
