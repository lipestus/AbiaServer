CREATE PROCEDURE [dbo].[spUser_Get]
	@Id int
AS
begin
	select Id, AccountName, HashedPassword
	from dbo.[User]
	where Id = @Id;
end
