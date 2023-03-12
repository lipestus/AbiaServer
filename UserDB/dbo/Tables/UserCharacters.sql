CREATE TABLE [dbo].[UserCharacters]
(
	CharacterName varchar(50) PRIMARY KEY,
    UserId int NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id)
)
