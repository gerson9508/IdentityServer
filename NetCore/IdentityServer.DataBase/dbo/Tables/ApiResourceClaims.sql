CREATE TABLE [dbo].[ApiResourceClaims] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ApiResourceId] INT            NOT NULL,
    [Type]          NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ApiResourceClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiResourceClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiResourceClaims_ApiResourceId]
    ON [dbo].[ApiResourceClaims]([ApiResourceId] ASC);

