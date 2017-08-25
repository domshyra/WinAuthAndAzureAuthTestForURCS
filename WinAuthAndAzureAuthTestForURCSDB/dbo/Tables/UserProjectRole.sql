CREATE TABLE [dbo].[UserProjectRole] (
    [UserProjectRoleId] INT IDENTITY (1, 1) NOT NULL,
    [UserAccountID]     INT NOT NULL,
    [RoleId]            INT NOT NULL,
    [ProjectID]         INT NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserProjectRoleId] ASC),
    CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY ([UserAccountID]) REFERENCES [dbo].[UserAccount] ([UserAccountID])
);

