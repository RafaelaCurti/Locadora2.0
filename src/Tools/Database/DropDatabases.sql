ALTER DATABASE ${database}
SET SINGLE_USER --or RESTRICTED_USER
WITH ROLLBACK IMMEDIATE;
DROP DATABASE ${database};
