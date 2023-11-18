CREATE TABLE [dbo].[ApiResourceScopes] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Scope]         NVARCHAR (200) NOT NULL,
    [ApiResourceId] INT            NOT NULL,
    CONSTRAINT [PK_ApiResourceScopes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiResourceScopes_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiResourceScopes_ApiResourceId]
    ON [dbo].[ApiResourceScopes]([ApiResourceId] ASC);

