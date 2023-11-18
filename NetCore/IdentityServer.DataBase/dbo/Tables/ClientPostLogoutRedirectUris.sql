CREATE TABLE [dbo].[ClientPostLogoutRedirectUris] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [PostLogoutRedirectUri] NVARCHAR (2000) NOT NULL,
    [ClientId]              INT             NOT NULL,
    CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientPostLogoutRedirectUris_ClientId]
    ON [dbo].[ClientPostLogoutRedirectUris]([ClientId] ASC);

