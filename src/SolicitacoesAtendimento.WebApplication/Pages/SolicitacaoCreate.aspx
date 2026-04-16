<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitacaoCreate.aspx.vb" Inherits="SolicitacoesAtendimento.WebApplication.Pages.SolicitacaoCreate" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastrar Solicitacao</title>
    <link href="../Content/site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-shell">
            <section class="hero-card">
                <h1 class="hero-title">Cadastrar Solicitacao</h1>
                <p class="hero-subtitle">Registre uma nova solicitacao de atendimento com os dados principais.</p>
                <div class="actions-row">
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Pages/SolicitacaoList.aspx">Voltar para listagem</asp:HyperLink>
                </div>
            </section>

            <section class="panel-card">
                <asp:Label ID="lblMensagem" runat="server" EnableViewState="false" CssClass="message" />
                <asp:ValidationSummary ID="vsErros" runat="server" HeaderText="Corrija os campos abaixo:" CssClass="validation-summary" />

                <div class="form-grid">
                    <div class="field-full">
                        <label for="txtTitulo">Titulo</label>
                        <asp:TextBox ID="txtTitulo" runat="server" MaxLength="150" CssClass="text-input" />
                        <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ErrorMessage="Informe o titulo." Display="Dynamic" CssClass="validator" />
                    </div>

                    <div class="field-full">
                        <label for="txtDescricao">Descricao</label>
                        <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Rows="6" CssClass="text-area" />
                        <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Informe a descricao." Display="Dynamic" CssClass="validator" />
                    </div>

                    <div class="field">
                        <label for="txtNomeSolicitante">Solicitante</label>
                        <asp:TextBox ID="txtNomeSolicitante" runat="server" CssClass="text-input" />
                        <asp:RequiredFieldValidator ID="rfvNomeSolicitante" runat="server" ControlToValidate="txtNomeSolicitante" ErrorMessage="Informe o nome do solicitante." Display="Dynamic" CssClass="validator" />
                    </div>

                    <div class="field">
                        <label for="ddlPrioridade">Prioridade</label>
                        <asp:DropDownList ID="ddlPrioridade" runat="server" CssClass="text-select" />
                    </div>
                </div>

                <div class="button-row">
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" />
                </div>
            </section>
        </div>
    </form>
</body>
</html>
