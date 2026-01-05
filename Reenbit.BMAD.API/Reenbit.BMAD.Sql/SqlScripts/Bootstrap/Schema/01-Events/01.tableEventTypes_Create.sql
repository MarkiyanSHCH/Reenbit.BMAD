--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM sys.tables
    WHERE name = 'EventTypes' AND SCHEMA_NAME(schema_id) = 'dbo')
    BEGIN
        DROP TABLE [dbo].[EventTypes];
    END
GO
------------------------------------------------------------------------------
CREATE TABLE [dbo].[EventTypes] (
    [Id] INT NOT NULL IDENTITY(1, 1)
        CONSTRAINT PK_EventTypes PRIMARY KEY,
	[Value] NVARCHAR(50) NOT NULL
);
GO
--============================================================================