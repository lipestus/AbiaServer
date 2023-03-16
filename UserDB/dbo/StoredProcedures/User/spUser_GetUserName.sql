CREATE PROCEDURE [dbo].[spUser_GetUserName]
    @AccountName NVARCHAR(50)
AS
BEGIN
    SELECT Id, AccountName, HashedPassword
    FROM dbo.[User]
    WHERE AccountName = @AccountName;
END
