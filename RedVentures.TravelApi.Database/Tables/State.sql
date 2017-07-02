CREATE TABLE [dbo].[State]
(
	[StateId] INT NOT NULL PRIMARY KEY, 
    [Code] CHAR(2) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL 
    CONSTRAINT [AK_State_Code] UNIQUE ([Code])
)
