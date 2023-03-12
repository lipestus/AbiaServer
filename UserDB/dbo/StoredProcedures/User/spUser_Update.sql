CREATE PROCEDURE [dbo].[spUser_Update]
    @Id int,
    @AccountName nvarchar(50),
    @HashedPassword varbinary(64)
AS
BEGIN
    UPDATE dbo.[User]
    SET AccountName = @AccountName, HashedPassword = @HashedPassword
    WHERE Id = @Id;
END
