USE [master]
GO

IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'scraperel')
 CREATE DATABASE [scraperel]


If not Exists (select loginname from master.dbo.syslogins 
	where name = 'scraperel-user')

begin

CREATE LOGIN [scraperel-user] WITH PASSWORD=N'aiF0bGkUNuoEZI0n',
	DEFAULT_DATABASE=[scraperel], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

end

GO

use [scraperel]

IF NOT EXISTS (SELECT name 
                FROM [sys].[database_principals]
                WHERE [type] = 'S' AND name = N'scraperel-user')

begin

CREATE USER [scraperel-user] FOR LOGIN [scraperel-user]
	WITH DEFAULT_SCHEMA = [dbo]

EXEC sp_addrolemember N'db_owner', N'scraperel-user'


end

