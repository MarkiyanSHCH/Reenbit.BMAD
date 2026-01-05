--============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO
------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[EventTypes] ON;
------------------------------------------------------------------------------
MERGE [dbo].[EventTypes] AS target
    USING (
        VALUES 
            (1, 'PageView'),
			(2, 'Click'),
            (3, 'Purchase')
        ) AS source ([Id], [Value])
    ON (target.[Id] = source.[Id])
    WHEN MATCHED
        THEN UPDATE SET target.[Value] = source.[Value]
    WHEN NOT MATCHED BY target
        THEN INSERT ([Id], [Value]) VALUES (source.[Id], source.[Value])
    WHEN NOT MATCHED BY source
        THEN DELETE;
------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[EventTypes] OFF;
--============================================================================