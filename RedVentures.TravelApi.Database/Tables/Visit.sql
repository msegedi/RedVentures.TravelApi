﻿CREATE TABLE [dbo].[Visit]
(
	[VisitId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VisitUid] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [UserId] INT NOT NULL, 
    [CityId] INT NOT NULL, 
    [DateTimeAdded] DATETIME NOT NULL DEFAULT GETUTCDATE() 
)