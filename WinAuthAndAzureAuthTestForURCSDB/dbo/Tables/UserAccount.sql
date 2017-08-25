CREATE TABLE [dbo].[UserAccount] (
    [UserAccountID] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (255) NULL,
    [LastName]      NVARCHAR (255) NULL,
    [Email]         NVARCHAR (50)  NULL,
    [UserName]      NVARCHAR (50)  NULL,
    [Password]      NVARCHAR (255) NULL,
    [Locked]        SMALLINT       NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserAccountID] ASC)
);

