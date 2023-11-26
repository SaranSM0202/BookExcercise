CREATE DATABASE Book;

USE [Book]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 11/26/2023 12:44:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Publisher] [varchar](100) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[AuthorLastName] [varchar](50) NOT NULL,
	[AuthorFirstName] [varchar](50) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [dbo].[Book] ([Publisher], [Title], [AuthorLastName], [AuthorFirstName], [Price])
VALUES
('PublisherA', 'BookTitle1', 'Doe', 'John', 25.99),
('PublisherB', 'BookTitle2', 'Smith', 'Emily', 19.95),
('PublisherC', 'BookTitle3', 'Johnson', 'Michael', 32.50);