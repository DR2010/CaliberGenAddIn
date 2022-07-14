
/****** Object:  Table [dbo].[auditdetail]    Script Date: 07/15/2010 15:02:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditdetail]') AND type in (N'U'))
DROP TABLE [dbo].[auditdetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_audit_auditheader]') AND parent_object_id = OBJECT_ID(N'[dbo].[audit]'))
ALTER TABLE [dbo].[audit] DROP CONSTRAINT [FK_audit_auditheader]
GO

/****** Object:  Table [dbo].[audit]    Script Date: 07/15/2010 15:02:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[audit]') AND type in (N'U'))
DROP TABLE [dbo].[audit]
GO


/****** Object:  Table [dbo].[auditheader]    Script Date: 07/15/2010 15:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[auditheader]') AND type in (N'U'))
DROP TABLE [dbo].[auditheader]
GO



/****** Object:  Table [dbo].[auditheader]    Script Date: 07/15/2010 14:57:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auditheader](
	[audit_header_id] [int] IDENTITY(1,1) NOT NULL,
	[ConvertedFileName] [nvarchar](50) NOT NULL,
	[OriginalFileName] [nvarchar](50) NOT NULL,
	[ImportDateTime] [datetime] NOT NULL,
	[ImportUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_auditheader] PRIMARY KEY CLUSTERED 
(
	[audit_header_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[audit]    Script Date: 07/15/2010 14:56:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[audit](
	[audit_header_id] [int] NOT NULL,
	[audit_id] [int] IDENTITY(1,1) NOT NULL,
	[object_GUID] [nvarchar](50) NOT NULL,
	[object_type] [nvarchar](50) NOT NULL,
	[audit_user] [nvarchar](50) NOT NULL,
	[audit_datetime] [datetime] NOT NULL,
	[ea_object_type] [nvarchar](20) NOT NULL,
	[change_type] [nvarchar](10) NOT NULL,
	[object_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_audit] PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



ALTER TABLE [dbo].[audit]  WITH CHECK ADD  CONSTRAINT [FK_audit_auditheader] FOREIGN KEY([audit_header_id])
REFERENCES [dbo].[auditheader] ([audit_header_id])
GO

ALTER TABLE [dbo].[audit] CHECK CONSTRAINT [FK_audit_auditheader]
GO

/****** Object:  Table [dbo].[auditdetail]    Script Date: 07/15/2010 14:56:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auditdetail](
	[audit_id] [int] NOT NULL,
	[property] [nvarchar](1) NOT NULL,
	[old_value] [nvarchar](1) NOT NULL,
	[new_value] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_auditdetail] PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC,
	[property] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


