Namespace SolicitacoesAtendimento.Infrastructure.Queries
    Public NotInheritable Class HistoricoSolicitacaoSqlQueries
        Private Sub New()
        End Sub

        Public Const Inserir As String =
            "INSERT INTO dbo.HistoricoSolicitacaoStatus (SolicitacaoId, StatusAnterior, NovoStatus, DataAlteracao, Observacao) " &
            "VALUES (@SolicitacaoId, @StatusAnterior, @NovoStatus, @DataAlteracao, @Observacao); " &
            "SELECT CAST(SCOPE_IDENTITY() AS INT);"

        Public Const ObterPorSolicitacaoId As String =
            "SELECT Id, SolicitacaoId, StatusAnterior, NovoStatus, DataAlteracao, Observacao " &
            "FROM dbo.HistoricoSolicitacaoStatus " &
            "WHERE SolicitacaoId = @SolicitacaoId " &
            "ORDER BY DataAlteracao ASC, Id ASC;"
    End Class
End Namespace
