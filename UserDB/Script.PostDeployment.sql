USE [AbiaServerDB]
IF NOT EXISTS (SELECT * FROM [worlds] WHERE [name] = 'Bekhora')
BEGIN
    INSERT INTO [worlds] ([name], [ip], [port])
    VALUES ('Bekhora', 127001, 4299)
END