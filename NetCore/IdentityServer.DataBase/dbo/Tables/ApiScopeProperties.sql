CREATE TABLE [dbo].[ApiScopeProperties] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [ScopeId] INT             NOT NULL,
    [Key]     NVARCHAR (250)  NOT NULL,
    [Value]   NVARCHAR (2000) NOT NULL,
    CONSTRAINT [PK_ApiScopeProperties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiScopeProperties_ApiScopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [dbo].[ApiScopes] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiScopeProperties_ScopeId]
    ON [dbo].[ApiScopeProperties]([ScopeId] ASC);

