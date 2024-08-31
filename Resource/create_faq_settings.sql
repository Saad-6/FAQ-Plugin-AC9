SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FAQSettings' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[FAQSettings](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [AllowAnonymousUsersToAnswerQuestions] [bit] NOT NULL,
        [DefaultResponderName][nvarchar](255) NOT NULL,
     CONSTRAINT [PK_FAQSettings] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO
