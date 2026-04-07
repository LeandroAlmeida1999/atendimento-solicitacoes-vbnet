Imports SolicitacoesAtendimento.Application.Interfaces
Imports SolicitacoesAtendimento.Application.Services
Imports SolicitacoesAtendimento.Infrastructure.Data
Imports SolicitacoesAtendimento.Infrastructure.Repositories

Namespace SolicitacoesAtendimento.WebApplication.App_Start
    Public NotInheritable Class AppServiceFactory
        Private Sub New()
        End Sub

        Public Shared Function CreateSolicitacaoAppService() As ISolicitacaoAppService
            Dim executionContext = New SqlExecutionContext()
            Dim connectionFactory = New SqlConnectionFactory()

            Dim solicitacaoRepository = New SolicitacaoRepository(connectionFactory, executionContext)
            Dim historicoRepository = New HistoricoSolicitacaoRepository(connectionFactory, executionContext)
            Dim unitOfWork = New UnitOfWork(connectionFactory, executionContext)

            Return New SolicitacaoAppService(solicitacaoRepository, historicoRepository, unitOfWork)
        End Function
    End Class
End Namespace
