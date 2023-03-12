if not exists (select 1 from dbo.[User])
BEGIN
    DECLARE @HashedPassword varbinary(max)
    SET @HashedPassword = CONVERT(varbinary(max), 'my varchar value')

    INSERT INTO dbo.[User] (AccountName, HashedPassword)
    VALUES ('Admin', @HashedPassword);
END
