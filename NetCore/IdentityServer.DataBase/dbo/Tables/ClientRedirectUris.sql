CREATE TABLE [dbo].[ClientRedirectUris] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [RedirectUri] NVARCHAR (2000) NOT NULL,
    [ClientId]    INT             NOT NULL,
    CONSTRAINT [PK_ClientRedirectUris] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientRedirectUris_ClientId]
    ON [dbo].[ClientRedirectUris]([ClientId] ASC);

