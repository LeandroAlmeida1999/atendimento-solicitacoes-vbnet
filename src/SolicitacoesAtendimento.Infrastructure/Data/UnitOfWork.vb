Imports System
Imports SolicitacoesAtendimento.Domain.Interfaces

Namespace SolicitacoesAtendimento.Infrastructure.Data
    Public Class UnitOfWork
        Implements IUnitOfWork

        Private ReadOnly _connectionFactory As SqlConnectionFactory
        Private ReadOnly _executionContext As SqlExecutionContext

        Public Sub New(connectionFactory As SqlConnectionFactory, executionContext As SqlExecutionContext)
            If connectionFactory Is Nothing Then
                Throw New ArgumentNullException(NameOf(connectionFactory))
            End If

            If executionContext Is Nothing Then
                Throw New ArgumentNullException(NameOf(executionContext))
            End If

            _connectionFactory = connectionFactory
            _executionContext = executionContext
        End Sub

        Public Function BeginTransaction() As ITransaction Implements IUnitOfWork.BeginTransaction
            If _executionContext.HasActiveTransaction Then
                Throw New InvalidOperationException("Ja existe uma transacao ativa no contexto atual.")
            End If

            Dim connection = _connectionFactory.CreateOpenConnection()
            Dim transaction = connection.BeginTransaction()

            _executionContext.CurrentConnection = connection
            _executionContext.CurrentTransaction = transaction

            Return New SqlTransactionAdapter(connection, transaction, _executionContext)
        End Function
    End Class
End Namespace
