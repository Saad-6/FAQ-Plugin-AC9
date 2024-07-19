SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FAQ](
    [Id] [int] IDENTITY(1,1) ,
    [Question] [nvarchar](255) NOT NULL,
    [Answer] [nvarchar](255) NULL,
    [UserId] [int] NULL,
    [CreatedDate] [datetime] NULL,
    [ProductId] [int] NOT NULL,
    [IsAnswered] [bit] NULL,
    [Visibility] [bit] NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH ( IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[FAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_ac_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[ac_Users] ([UserId])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[FAQ] CHECK CONSTRAINT [FK_FAQ_ac_Users]
GO
