Imports System
Imports System.Configuration
Imports System.Data.SqlClient

Namespace SolicitacoesAtendimento.Infrastructure.Data
    Public Class SqlConnectionFactory
        Private ReadOnly _connectionString As String

        Public Sub New()
            Me.New("SolicitacoesAtendimento")
        End Sub

        Public Sub New(connectionStringName As String)
            If String.IsNullOrWhiteSpace(connectionStringName) Then
                Throw New ArgumentException("O nome da connection string e obrigatorio.", NameOf(connectionStringName))
            End If

            Dim settings = ConfigurationManager.ConnectionStrings(connectionStringName)

            If settings Is Nothing OrElse String.IsNullOrWhiteSpace(settings.ConnectionString) Then
                Throw New InvalidOperationException($"A connection string '{connectionStringName}' nao foi encontrada.")
            End If

            _connectionString = settings.ConnectionString
        End Sub

        Public Overridable Function CreateOpenConnection() As SqlConnection
            Dim connection = New SqlConnection(_connectionString)
            connection.Open()
            Return connection
        End Function
    End Class
End Namespace
