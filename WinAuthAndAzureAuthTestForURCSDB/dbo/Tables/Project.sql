CREATE TABLE [dbo].[Project] (
    [ProjectID]       INT            IDENTITY (1, 1) NOT NULL,
    [ProjectName]     NVARCHAR (255) NOT NULL,
    [ProjectLocation] NVARCHAR (255) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC)
);

