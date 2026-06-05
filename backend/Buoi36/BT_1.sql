CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) PRIMARY KEY,
	Full_name nvarchar(50) NOT NULL,
	Gender varchar(15),
	Age int,
	City nvarchar(50),
	[Weight] float,
)
GO
