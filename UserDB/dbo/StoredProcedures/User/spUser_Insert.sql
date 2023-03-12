CREATE PROCEDURE [dbo].[spUser_Insert]
	@AccountName nvarchar(50),
	@HashedPassword varbinary(64)
AS
BEGIN
    INSERT INTO dbo.[User] (AccountName, HashedPassword)
    VALUES (@AccountName, @HashedPassword);
END
