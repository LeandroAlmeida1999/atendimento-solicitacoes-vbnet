IF OBJECT_ID(N'dbo.HistoricoSolicitacaoStatus', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.HistoricoSolicitacaoStatus;
END;
GO

IF OBJECT_ID(N'dbo.Solicitacao', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Solicitacao;
END;
GO

CREATE TABLE dbo.Solicitacao
(
    Id INT IDENTITY(1,1) NOT NULL,
    Titulo NVARCHAR(150) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    NomeSolicitante NVARCHAR(150) NOT NULL,
    DataCriacao DATETIME NOT NULL,
    Status INT NOT NULL,
    Prioridade INT NOT NULL,
    CONSTRAINT PK_Solicitacao PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT CK_Solicitacao_Titulo_NaoVazio CHECK (LEN(LTRIM(RTRIM(Titulo))) > 0),
    CONSTRAINT CK_Solicitacao_Descricao_NaoVazia CHECK (LEN(LTRIM(RTRIM(Descricao))) > 0),
    CONSTRAINT CK_Solicitacao_NomeSolicitante_NaoVazio CHECK (LEN(LTRIM(RTRIM(NomeSolicitante))) > 0),
    CONSTRAINT CK_Solicitacao_Status CHECK (Status IN (1, 2, 3, 4)),
    CONSTRAINT CK_Solicitacao_Prioridade CHECK (Prioridade IN (1, 2, 3))
);
GO

CREATE TABLE dbo.HistoricoSolicitacaoStatus
(
    Id INT IDENTITY(1,1) NOT NULL,
    SolicitacaoId INT NOT NULL,
    StatusAnterior INT NOT NULL,
    NovoStatus INT NOT NULL,
    DataAlteracao DATETIME NOT NULL,
    Observacao NVARCHAR(500) NULL,
    CONSTRAINT PK_HistoricoSolicitacaoStatus PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_HistoricoSolicitacaoStatus_Solicitacao
        FOREIGN KEY (SolicitacaoId) REFERENCES dbo.Solicitacao(Id),
    CONSTRAINT CK_HistoricoSolicitacaoStatus_StatusAnterior CHECK (StatusAnterior IN (1, 2, 3, 4)),
    CONSTRAINT CK_HistoricoSolicitacaoStatus_NovoStatus CHECK (NovoStatus IN (1, 2, 3, 4)),
    CONSTRAINT CK_HistoricoSolicitacaoStatus_Observacao
        CHECK (Observacao IS NULL OR LEN(LTRIM(RTRIM(Observacao))) > 0)
);
GO

CREATE INDEX IX_Solicitacao_Status
    ON dbo.Solicitacao(Status);
GO

CREATE INDEX IX_Solicitacao_Prioridade
    ON dbo.Solicitacao(Prioridade);
GO

CREATE INDEX IX_Solicitacao_DataCriacao
    ON dbo.Solicitacao(DataCriacao);
GO

CREATE INDEX IX_Solicitacao_NomeSolicitante
    ON dbo.Solicitacao(NomeSolicitante);
GO

CREATE INDEX IX_Solicitacao_Filtros
    ON dbo.Solicitacao(Status, Prioridade, DataCriacao)
    INCLUDE (NomeSolicitante, Titulo);
GO

CREATE INDEX IX_HistoricoSolicitacaoStatus_SolicitacaoId
    ON dbo.HistoricoSolicitacaoStatus(SolicitacaoId, DataAlteracao);
GO
