Imports System
Imports System.Web.UI

Namespace SolicitacoesAtendimento.WebApplication
    Public Class Global_asax
        Inherits System.Web.HttpApplication

        Protected Sub Application_Start(sender As Object, e As EventArgs)
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None
        End Sub
    End Class
End Namespace
