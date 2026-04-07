Namespace SolicitacoesAtendimento.Infrastructure.Queries
    Public NotInheritable Class SolicitacaoSqlQueries
        Private Sub New()
        End Sub

        Public Const Inserir As String =
            "INSERT INTO dbo.Solicitacao (Titulo, Descricao, NomeSolicitante, DataCriacao, Status, Prioridade) " &
            "VALUES (@Titulo, @Descricao, @NomeSolicitante, @DataCriacao, @Status, @Prioridade); " &
            "SELECT CAST(SCOPE_IDENTITY() AS INT);"

        Public Const ObterPorId As String =
            "SELECT Id, Titulo, Descricao, NomeSolicitante, DataCriacao, Status, Prioridade " &
            "FROM dbo.Solicitacao " &
            "WHERE Id = @Id;"

        Public Const AtualizarStatus As String =
            "UPDATE dbo.Solicitacao " &
            "SET Status = @Status " &
            "WHERE Id = @Id;"

        Public Const ListarBase As String =
            "SELECT Id, Titulo, Descricao, NomeSolicitante, DataCriacao, Status, Prioridade " &
            "FROM dbo.Solicitacao " &
            "WHERE 1 = 1"
    End Class
End Namespace
