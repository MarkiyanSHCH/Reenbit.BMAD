--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spEvents_InsertEvent'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE [dbo].[spEvents_InsertEvent];
    END
GO
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[spEvents_InsertEvent]
    @UserId NVARCHAR(100),
    @TypeId INT,
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    --------------------------------------------------------------------------
    -- Validate:
    --------------------------------------------------------------------------
    IF @UserId IS NULL
    BEGIN
        RAISERROR ('Must pass a @UserId parameter.', 11, 1);
        RETURN -1;
    END

    IF @TypeId IS NULL OR @TypeId <= 0
    BEGIN
        RAISERROR ('Must pass a @TypeId parameter.', 11, 2);
        RETURN -1;
    END

    IF @Description IS NULL
    BEGIN
        RAISERROR ('Must pass a @Description parameter.', 11, 3);
        RETURN -1;
    END

    --------------------------------------------------------------------------
    -- Insert:
    --------------------------------------------------------------------------
    INSERT INTO [dbo].[Events](
        [UserId],
        [TypeId],
        [Description]) 
    VALUES(
        @UserId,
        @TypeId,
        @Description);
END
GO
--============================================================================