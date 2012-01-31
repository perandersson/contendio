/****** Object:  Table [dbo].[replaceme_NodeType]    Script Date: 01/31/2012 21:15:11 ******/
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
/****** Object:  Table [dbo].[replaceme_Node]    Script Date: 01/31/2012 21:15:11 ******/
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
	[Path] [text] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ChangedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_replaceme_Node] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[replaceme_Node] ON
INSERT [dbo].[replaceme_Node] ([Id], [Name], [NodeId], [NodeTypeId], [Path], [AddedDate], [ChangedDate]) VALUES (1, N'/', NULL, 1, N'/', GETDATE(), GETDATE())
SET IDENTITY_INSERT [dbo].[replaceme_Node] OFF
/****** Object:  Table [dbo].[replaceme_DateValue]    Script Date: 01/31/2012 21:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_DateValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [datetime] NOT NULL,
 CONSTRAINT [PK_replaceme_DateValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replaceme_BinaryValue]    Script Date: 01/31/2012 21:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_BinaryValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [image] NOT NULL,
 CONSTRAINT [PK_replaceme_BinaryValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replaceme_StringValue]    Script Date: 01/31/2012 21:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_StringValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Value] [text] NOT NULL,
 CONSTRAINT [PK_replaceme_StringValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replaceme_NodeValue]    Script Date: 01/31/2012 21:15:11 ******/
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
	[StringValueId] [bigint] NULL,
	[BinaryValueId] [bigint] NULL,
	[DateValueId] [bigint] NULL,
	[NodeId] [bigint] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ChangedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_replaceme_NodeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_replaceme_Node_replaceme_NodeType]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_Node]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_Node_replaceme_NodeType] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
GO
ALTER TABLE [dbo].[replaceme_Node] CHECK CONSTRAINT [FK_replaceme_Node_replaceme_NodeType]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_BinaryValue]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_BinaryValue] FOREIGN KEY([BinaryValueId])
REFERENCES [dbo].[replaceme_BinaryValue] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_BinaryValue]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_DateValue]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_DateValue] FOREIGN KEY([DateValueId])
REFERENCES [dbo].[replaceme_DateValue] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_DateValue]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_Node]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_NodeType]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_StringValue]    Script Date: 01/31/2012 21:15:11 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_StringValue] FOREIGN KEY([StringValueId])
REFERENCES [dbo].[replaceme_StringValue] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_StringValue]
GO
