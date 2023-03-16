CREATE PROCEDURE [dbo].[spUser_Insert]
    @AccountName nvarchar(50),
    @HashedPassword varbinary(max),
    @Salt varbinary(16),
    @ErrorMessage nvarchar(max) OUTPUT
AS
BEGIN
    SET @ErrorMessage = NULL;
    IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE AccountName = @AccountName)
    BEGIN
        INSERT INTO dbo.[User] (AccountName, HashedPassword, Salt)
        VALUES (@AccountName, @HashedPassword, @Salt);
    END
    ELSE
    BEGIN
        SET @ErrorMessage = 'Account name already taken.';
    END
END
