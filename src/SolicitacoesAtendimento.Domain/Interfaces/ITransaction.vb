Imports System

Namespace SolicitacoesAtendimento.Domain.Interfaces
    Public Interface ITransaction
        Inherits IDisposable

        Sub Commit()
        Sub Rollback()
    End Interface
End Namespace
