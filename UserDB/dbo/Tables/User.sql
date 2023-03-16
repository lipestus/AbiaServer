CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AccountName] NVARCHAR(50) NOT NULL, 
    HashedPassword varbinary(64) NOT NULL, 
    [Salt] VARBINARY(16) NOT NULL
)
