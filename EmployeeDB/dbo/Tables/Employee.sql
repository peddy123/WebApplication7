CREATE TABLE [dbo].[Employee] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50)  NULL,
    [Email]      VARCHAR (100) NULL,
    [Department] INT           NULL,
    [PhotoPath]  VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

