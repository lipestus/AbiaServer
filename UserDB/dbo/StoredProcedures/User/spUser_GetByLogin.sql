CREATE PROCEDURE [dbo].[spUser_GetByLogin]
	@AccountName nvarchar(50),
	@HashedPassword varbinary(64)
AS
begin
	select Id, AccountName, HashedPassword, Salt
	from dbo.[User]
	where AccountName = @AccountName
	and HashedPassword = @HashedPassword;
end
