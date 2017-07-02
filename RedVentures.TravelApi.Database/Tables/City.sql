CREATE TABLE [dbo].[City]
(
	[CityId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StateId] INT NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Location] [sys].[geography] NOT NULL, 
    CONSTRAINT [FK_City_State] FOREIGN KEY ([StateId]) REFERENCES [State]([StateId]), 
    CONSTRAINT [AK_City_StateId_Name] UNIQUE ([StateId],[Name])
)
