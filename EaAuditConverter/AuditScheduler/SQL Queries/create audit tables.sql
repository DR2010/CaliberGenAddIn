USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditerrorlog]    Script Date: 11/02/2010 10:22:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditerrorlog]') AND type in (N'U'))
DROP TABLE [dbo].[auditerrorlog]
GO


USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditdetail]    Script Date: 11/02/2010 10:22:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditdetail]') AND type in (N'U'))
DROP TABLE [dbo].[auditdetail]
GO

USE [EA_LOCAL]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_audit_auditheader]') AND parent_object_id = OBJECT_ID(N'[dbo].[audit]'))
ALTER TABLE [dbo].[audit] DROP CONSTRAINT [FK_audit_auditheader]
GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[audit]    Script Date: 11/02/2010 10:22:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[audit]') AND type in (N'U'))
DROP TABLE [dbo].[audit]
GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditheader]    Script Date: 11/02/2010 10:22:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditheader]') AND type in (N'U'))
DROP TABLE [dbo].[auditheader]
GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditsnapshot]    Script Date: 11/02/2010 13:27:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditsnapshot]') AND type in (N'U'))
DROP TABLE [dbo].[auditsnapshot]
GO


USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditheader]    Script Date: 11/02/2010 10:02:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auditheader](
	[audit_header_id] [int] IDENTITY(1,1) NOT NULL,
	[PositionFrom] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[ImportDateTime] [datetime] NULL,
	[ImportUser] [nvarchar](50) NULL,
 CONSTRAINT [PK_auditheader] PRIMARY KEY CLUSTERED 
(
	[audit_header_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[audit]    Script Date: 11/02/2010 10:02:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[audit](
	[audit_header_id] [int] NOT NULL,
	[audit_id] [int] NOT NULL,
	[object_GUID] [nvarchar](50) NOT NULL,
	[object_type] [nvarchar](50) NOT NULL,
	[audit_user] [nvarchar](50) NOT NULL,
	[audit_datetime] [datetime] NOT NULL,
	[ea_object_type] [nvarchar](20) NULL,
	[change_type] [nvarchar](10) NULL,
	[object_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_audit_1] PRIMARY KEY CLUSTERED 
(
	[audit_header_id] ASC,
	[audit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[audit]  WITH CHECK ADD  CONSTRAINT [FK_audit_auditheader] FOREIGN KEY([audit_header_id])
REFERENCES [dbo].[auditheader] ([audit_header_id])
GO

ALTER TABLE [dbo].[audit] CHECK CONSTRAINT [FK_audit_auditheader]
GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditdetail]    Script Date: 11/02/2010 10:02:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auditdetail](
	[audit_header_id] [int] NOT NULL,
	[audit_id] [int] NOT NULL,
	[audit_detail_id] [int] NOT NULL,
	[property] [nvarchar](50) NOT NULL,
	[old_value] [nvarchar](max) NOT NULL,
	[new_value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_auditdetail_1] PRIMARY KEY CLUSTERED 
(
	[audit_header_id] ASC,
	[audit_id] ASC,
	[audit_detail_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[auditdetail]  WITH CHECK ADD  CONSTRAINT [FK_auditdetail_audit1] FOREIGN KEY([audit_header_id], [audit_id])
REFERENCES [dbo].[audit] ([audit_header_id], [audit_id])
GO

ALTER TABLE [dbo].[auditdetail] CHECK CONSTRAINT [FK_auditdetail_audit1]
GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditsnapshot]    Script Date: 11/02/2010 10:03:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auditsnapshot](
	[SnapshotID] [nvarchar](50) NOT NULL,
	[SeriesID] [nvarchar](50) NOT NULL,
	[Position] [int] NOT NULL,
	[SnapshotName] [nvarchar](100) NOT NULL,
	[Notes] [ntext] NULL,
	[Style] [nvarchar](100) NULL,
	[ElementID] [nvarchar](50) NOT NULL,
	[ElementType] [nvarchar](50) NOT NULL,
	[StrContent] [ntext] NULL,
	[BinContent1] [image] NULL,
	[BinContent2] [image] NULL,
 CONSTRAINT [PK_auditsnapshot] PRIMARY KEY CLUSTERED 
(
	[SnapshotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

USE [EA_LOCAL]
GO

/****** Object:  Table [dbo].[auditerrorlog]    Script Date: 11/02/2010 10:03:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[auditerrorlog](
	[DateTime] [datetime] NOT NULL,
	[UserAccount] [varchar](50) NOT NULL,
	[ErrorType] [varchar](20) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[AuditHeaderId] [int] NULL,
	[CreationOrder] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


