--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM sys.tables
    WHERE name = 'Events' AND SCHEMA_NAME(schema_id) = 'dbo')
    BEGIN
        DROP TABLE [dbo].[Events];
    END
GO
------------------------------------------------------------------------------
CREATE TABLE [dbo].[Events] (
    [Id] UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT DF_Events_Id DEFAULT NEWSEQUENTIALID()
        CONSTRAINT PK_Events PRIMARY KEY,
	[UserId] NVARCHAR(100) NOT NULL,
    [TypeId] INT NULL
        CONSTRAINT FK_Events_EventTypes
            FOREIGN KEY REFERENCES [dbo].[EventTypes]([Id]),
	[Description] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE()
);
GO
--============================================================================