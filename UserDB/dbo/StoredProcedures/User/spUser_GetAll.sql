CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
begin
	select Id, AccountName, HashedPassword
	from dbo.[User];
end
