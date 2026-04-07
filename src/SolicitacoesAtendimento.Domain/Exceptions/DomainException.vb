Imports System

Namespace SolicitacoesAtendimento.Domain.Exceptions
    Public Class DomainException
        Inherits Exception

        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class
End Namespace
