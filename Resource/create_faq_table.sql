SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Check if the FAQ table does not exist and create it
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FAQ' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[FAQ](
        [Id] [int] IDENTITY(1,1) ,
        [Question] [nvarchar](255) NOT NULL,
        [Answer] [nvarchar](255) NULL,
        [AnswerBy] [nvarchar](255) NULL,
        [UserId] [int] NULL,
        [CreatedDate] [datetime] NULL,
        [LastModified] [datetime] NULL,
        [ProductId] [int] NOT NULL,
        [IsAnswered] [bit] NULL,
        [Visibility] [bit] NULL
     CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- Add foreign key constraint only if it does not already exist
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_FAQ_ac_Users' AND parent_object_id = OBJECT_ID('dbo.FAQ'))
BEGIN
    ALTER TABLE [dbo].[FAQ] WITH CHECK ADD CONSTRAINT [FK_FAQ_ac_Users] FOREIGN KEY([UserId])
    REFERENCES [dbo].[ac_Users] ([UserId])
    ON DELETE SET NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_FAQ_Products' AND parent_object_id = OBJECT_ID('dbo.FAQ'))
BEGIN
    ALTER TABLE [dbo].[FAQ] WITH CHECK ADD CONSTRAINT [FK_FAQ_Products] FOREIGN KEY([ProductId])
    REFERENCES [dbo].[ac_Products] ([ProductId]);
END
GO




       