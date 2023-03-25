CREATE PROCEDURE [dbo].[spWorld_GetAll]
AS
BEGIN
SELECT id, name, ip, port
FROM dbo.[worlds];
END
