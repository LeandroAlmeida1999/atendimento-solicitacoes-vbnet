Imports System.Data.SqlClient

Namespace SolicitacoesAtendimento.Infrastructure.Data
    Public Class SqlExecutionContext
        Public Property CurrentConnection As SqlConnection
        Public Property CurrentTransaction As SqlTransaction

        Public ReadOnly Property HasActiveTransaction As Boolean
            Get
                Return CurrentConnection IsNot Nothing AndAlso CurrentTransaction IsNot Nothing
            End Get
        End Property

        Public Sub Clear()
            CurrentConnection = Nothing
            CurrentTransaction = Nothing
        End Sub
    End Class
End Namespace
