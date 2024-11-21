CREATE DATABASE [AdventureWorks_DB];

USE [AdventureWorks_DB]
GO

/** Object:  Table [dbo].[User]    Script Date: 21/11/2024 10:52:21 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[User] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------------------------------------------------------------------
USE [AdventureWorks_DB]
GO

/** Object:  Table [dbo].[Photo]    Script Date: 21/11/2024 10:51:55 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Photo](
	[PhotoID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[PhotoFile] [varbinary](max) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[Owner] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[PhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD FOREIGN KEY([Owner])
REFERENCES [dbo].[User] ([User])
GO
----------------------------------------------------------------------------------------------
USE [AdventureWorks_DB]
GO

/** Object:  Table [dbo].[Comment]    Script Date: 21/11/2024 10:50:02 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[User] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[Body] [nvarchar](max) NULL,
	[PhotoID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([PhotoID])
REFERENCES [dbo].[Photo] ([PhotoID])
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([User])
REFERENCES [dbo].[User] ([User])
GO
