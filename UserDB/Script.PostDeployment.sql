--if not exists (select 1 from dbo.[User])
--BEGIN
--    DECLARE @HashedPassword varbinary(max)
--    DECLARE @Salt varbinary(16)

--    SET @HashedPassword = CONVERT(varbinary(max), 'felipe123')

--    -- Set the salt value (this will be passed as a parameter)
--    SET @Salt = @Salt

--    INSERT INTO dbo.[User] (AccountName, HashedPassword, Salt)
--    VALUES ('Admin', @HashedPassword, @Salt);
--END