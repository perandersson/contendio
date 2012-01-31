/****** Object:  Table [dbo].[replaceme_BinaryValue]    Script Date: 01/31/2012 18:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_BinaryValue](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[Value] [image] NOT NULL,
 CONSTRAINT [PK_replaceme_BinaryValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replaceme_StringValue]    Script Date: 01/31/2012 18:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replaceme_StringValue](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Type] [uniqueidentifier] NOT NULL,
	[Value] [text] NOT NULL,
 CONSTRAINT [PK_replaceme_StringValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replaceme_NodeType]    Script Date: 01/31/2012 18:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_NodeType](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [varchar](64) NOT NULL,
 CONSTRAINT [PK_replaceme_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (N'e2027deb-7d8f-40c8-9bc1-17cc45913efc', N'value:string')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (N'17f1ce16-bbe8-48ee-a8f2-17cdf6b5e4a2', N'node:root')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (N'6d30fe6f-0cc7-4c39-a0a6-c2f5cf6562ef', N'node:node')
INSERT [dbo].[replaceme_NodeType] ([Id], [Name]) VALUES (N'20fe158b-0416-43e0-b29e-ddb657d4f0c2', N'value:binary')
/****** Object:  Table [dbo].[replaceme_Node]    Script Date: 01/31/2012 18:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_Node](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[NodeId] [uniqueidentifier] NULL,
	[NodeTypeId] [uniqueidentifier] NOT NULL,
	[Path] [text] NOT NULL,
 CONSTRAINT [PK_replaceme_Node] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[replaceme_Node] ([Id], [Name], [NodeId], [NodeTypeId], [Path]) VALUES (N'1cb489b1-79db-4a80-9e7c-7f5589c7f7a6', N'/', NULL, N'17f1ce16-bbe8-48ee-a8f2-17cdf6b5e4a2', N'/')
/****** Object:  Table [dbo].[replaceme_NodeValue]    Script Date: 01/31/2012 18:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[replaceme_NodeValue](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[NodeTypeId] [uniqueidentifier] NOT NULL,
	[StringValueId] [uniqueidentifier] NULL,
	[BinaryValueId] [uniqueidentifier] NULL,
	[NodeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_replaceme_NodeValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_replaceme_BinaryValue_Id]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_BinaryValue] ADD  CONSTRAINT [DF_replaceme_BinaryValue_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_replaceme_Node_Id]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_Node] ADD  CONSTRAINT [DF_replaceme_Node_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_replaceme_Type_Id]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeType] ADD  CONSTRAINT [DF_replaceme_Type_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_replaceme_NodeValue_Id]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeValue] ADD  CONSTRAINT [DF_replaceme_NodeValue_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_replaceme_StringValue_Id]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_StringValue] ADD  CONSTRAINT [DF_replaceme_StringValue_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  ForeignKey [FK_replaceme_Node_replaceme_Node]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_Node]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_Node_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
GO
ALTER TABLE [dbo].[replaceme_Node] CHECK CONSTRAINT [FK_replaceme_Node_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_Node_replaceme_Types]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_Node]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_Node_replaceme_Types] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_Node] CHECK CONSTRAINT [FK_replaceme_Node_replaceme_Types]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_BinaryValue]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_BinaryValue] FOREIGN KEY([BinaryValueId])
REFERENCES [dbo].[replaceme_BinaryValue] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_BinaryValue]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_Node]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[replaceme_Node] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_Node]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_StringValue]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_StringValue] FOREIGN KEY([StringValueId])
REFERENCES [dbo].[replaceme_StringValue] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_StringValue]
GO
/****** Object:  ForeignKey [FK_replaceme_NodeValue_replaceme_Types]    Script Date: 01/31/2012 18:38:34 ******/
ALTER TABLE [dbo].[replaceme_NodeValue]  WITH CHECK ADD  CONSTRAINT [FK_replaceme_NodeValue_replaceme_Types] FOREIGN KEY([NodeTypeId])
REFERENCES [dbo].[replaceme_NodeType] ([Id])
GO
ALTER TABLE [dbo].[replaceme_NodeValue] CHECK CONSTRAINT [FK_replaceme_NodeValue_replaceme_Types]
GO
