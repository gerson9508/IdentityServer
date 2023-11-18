CREATE TABLE [dbo].[IdentityResourceClaims] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [IdentityResourceId] INT            NOT NULL,
    [Type]               NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_IdentityResourceClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IdentityResourceClaims_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [dbo].[IdentityResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IdentityResourceClaims_IdentityResourceId]
    ON [dbo].[IdentityResourceClaims]([IdentityResourceId] ASC);

