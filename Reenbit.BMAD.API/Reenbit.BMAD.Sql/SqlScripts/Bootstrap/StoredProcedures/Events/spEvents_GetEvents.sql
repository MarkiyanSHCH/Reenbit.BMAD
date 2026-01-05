--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spEvents_GetEvents'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE [dbo].[spEvents_GetEvents];
    END
GO
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spEvents_GetEvents]
AS
BEGIN
    SET NOCOUNT ON;

    --------------------------------------------------------------------------
    -- Return:
    --------------------------------------------------------------------------
	SELECT
		events.[Id],
		events.[UserId],
		events.[TypeId],
		events.[Description],
        events.[CreatedAt]
    FROM [dbo].[Events] AS events
END
GO
--============================================================================