CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountName] NVARCHAR(50) NULL, 
    HashedPassword varbinary(64) NOT NULL
)
