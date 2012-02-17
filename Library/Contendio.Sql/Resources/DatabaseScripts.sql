/****** Object:  Table [dbo].[replaceme_NodeType]    Script Date: 02/09/2012 18:00:43 ******/
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
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (6, N'value:int')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (7, N'value:bool')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (8, N'attribute:unmovable')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (9, N'attribute:readonly')
SET IDENTITY_INSERT [dbo].[replaceme_NodeType] OFF
/****** Object:  Table [dbo].[replaceme_Node]    Script Date: 02/09/2012 18:00:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_Node](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NULL,
	[NodeId] [bigint] NULL,
	[NodeTypeId] [int] NOT NULL,
	[AddedDate] [datetime] NOT NULL,
	[ChangedDate] [datetime] NOT NULL,
	[Index] [int] NOT NULL,
 CONSTRAINT [PK_replaceme_Node] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[replaceme_Node] ON
INSERT [dbo].[replaceme_Node] ([Id], [Name], [NodeId], [NodeTypeId], [AddedDate], [ChangedDate], [Index]) VALUES (1, NULL, NULL, 1, GETDATE(), GETDATE(), 0)
SET IDENTITY_INSERT [dbo].[replaceme_Node] OFF
/****** Object:  Table [dbo].[replaceme_NodeValue]    Script Date: 02/09/2012 18:00:43 ******/
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
	[IntValue] [int] NULL,
	[BoolValue] [bit] NULL,
	[Index] [int] NOT NULL,
 CONSTRAINT [PK_replaceme_NodeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[replaceme_NodeAttribute]    Script Date: 02/09/2012 18:00:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_NodeAttribute](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NodeId] [bigint] NOT NULL,
	[NodeTypeId] [int] NOT NULL,
 CONSTRAINT [PK_replaceme_NodeAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[replaceme_NodeAttribute] ON
INSERT [dbo].[replaceme_NodeAttribute] ([Id], [NodeId], [NodeTypeId]) VALUES (1, 1, 8)
SET IDENTITY_INSERT [dbo].[replaceme_NodeAttribute] OFF
/****** Object:  Default [DF__replaceme__Index__4CBA3605]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_Node] ADD  DEFAULT ((0)) FOR [Index]
GO
/****** Object:  Default [DF__replaceme__Index__4F96A2B0]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_NodeValue] ADD  DEFAULT ((0)) FOR [Index]
GO
/****** Object:  ForeignKey [FK_replaceme_Node_replaceme_NodeType]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_Node]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_Node_replaceme_NodeType] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
GO
ALTER TABLE [dbo].[replaceme_Node] CHECK CONSTRAINT [FK_replaceme_Node_replaceme_NodeType]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeAttribute_replaceme_Node]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_NodeAttribute]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeAttribute_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[replaceme_NodeAttribute] CHECK CONSTRAINT [FK_replaceme_NodeAttribute_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeAttribute_replaceme_NodeType]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_NodeAttribute]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeAttribute_replaceme_NodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeAttribute] CHECK CONSTRAINT [FK_replaceme_NodeAttribute_replaceme_NodeType]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_Node]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_NodeType]    Script Date: 02/09/2012 18:00:43 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_NodeType]
GO
