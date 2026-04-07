Imports System
Imports System.Data.SqlClient
Imports SolicitacoesAtendimento.Domain.Interfaces

Namespace SolicitacoesAtendimento.Infrastructure.Data
    Public Class SqlTransactionAdapter
        Implements ITransaction

        Private ReadOnly _connection As SqlConnection
        Private ReadOnly _transaction As SqlTransaction
        Private ReadOnly _executionContext As SqlExecutionContext
        Private _completed As Boolean
        Private _disposed As Boolean

        Public Sub New(connection As SqlConnection, transaction As SqlTransaction, executionContext As SqlExecutionContext)
            If connection Is Nothing Then
                Throw New ArgumentNullException(NameOf(connection))
            End If

            If transaction Is Nothing Then
                Throw New ArgumentNullException(NameOf(transaction))
            End If

            If executionContext Is Nothing Then
                Throw New ArgumentNullException(NameOf(executionContext))
            End If

            _connection = connection
            _transaction = transaction
            _executionContext = executionContext
        End Sub

        Public Sub Commit() Implements ITransaction.Commit
            If _disposed OrElse _completed Then
                Return
            End If

            _transaction.Commit()
            _completed = True
        End Sub

        Public Sub Rollback() Implements ITransaction.Rollback
            If _disposed OrElse _completed Then
                Return
            End If

            _transaction.Rollback()
            _completed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            If _disposed Then
                Return
            End If

            Try
                If Not _completed Then
                    _transaction.Rollback()
                End If
            Catch
            End Try

            _executionContext.Clear()
            _transaction.Dispose()
            _connection.Dispose()
            _disposed = True
        End Sub
    End Class
End Namespace
