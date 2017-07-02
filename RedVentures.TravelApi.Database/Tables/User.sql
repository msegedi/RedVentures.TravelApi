﻿CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserUid] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    CONSTRAINT [AK_User_UserUid] UNIQUE ([UserUid]) 
)
