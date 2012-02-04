/****** Object:  Table [dbo].[replaceme_NodeType]    Script Date: 02/02/2012 23:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_NodeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
 CONSTRAINT [PK_replaceme_NodeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[replaceme_NodeType] ON
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (1, N'node:root')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (2, N'node:node')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (3, N'value:string')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (4, N'value:binary')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (5, N'value:date')
SET IDENTITY_INSERT [dbo].[replaceme_NodeType] OFF
/****** Object:  Table [dbo].[replaceme_Node]    Script Date: 02/02/2012 23:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_Node](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[NodeId] [bigint] NULL,
	[NodeTypeId] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ChangedDate] [datetime] NOT NULL,
	[Index] [int] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_replaceme_Node] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[replaceme_Node] ON
INSERT [dbo].[replaceme_Node] ([Id], [Name], [NodeId], [NodeTypeId], [AddedDate], [ChangedDate], [Index]) VALUES (1, N'/', NULL, 1, GETDATE(), GETDATE(), 0)
SET IDENTITY_INSERT [dbo].[replaceme_Node] OFF
/****** Object:  Table [dbo].[replaceme_NodeValue]    Script Date: 02/02/2012 23:09:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_NodeValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[NodeTypeId] [int] NOT NULL,
	[NodeId] [bigint] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ChangedDate] [datetime] NOT NULL,
	[StringValue] [varchar](max) NULL,
	[BinaryValue] [image] NULL,
	[DateTimeValue] [datetime] NULL,
	[Index] [int] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_replaceme_NodeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_replaceme_Node_replaceme_NodeType]    Script Date: 02/02/2012 23:09:46 ******/
ALTER TABLE [dbo].[replaceme_Node]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_Node_replaceme_NodeType] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
GO
ALTER TABLE [dbo].[replaceme_Node] CHECK CONSTRAINT [FK_replaceme_Node_replaceme_NodeType]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_Node]    Script Date: 02/02/2012 23:09:46 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_NodeType]    Script Date: 02/02/2012 23:09:46 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType]
GO
