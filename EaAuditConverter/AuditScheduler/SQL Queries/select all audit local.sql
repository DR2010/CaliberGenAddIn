/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [audit_header_id]
	  ,[Status]
      ,[PositionFrom]
      ,[ImportDateTime]
      ,[ImportUser]
  FROM [EA_LOCAL].[dbo].[auditheader] order by audit_header_id desc
  
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [audit_header_id]
      ,[audit_id]
      ,[object_GUID]
      ,[object_type]
      ,[audit_user]
      ,[audit_datetime]
      ,[ea_object_type]
      ,[change_type]
      ,[object_name]
  FROM [EA_LOCAL].[dbo].[audit] order by audit_header_id desc, audit_id asc

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [audit_header_id]
      ,[audit_id]
      ,[audit_detail_id]
      ,[property]
      ,[old_value]
      ,[new_value]
  FROM [EA_LOCAL].[dbo].[auditdetail] order by audit_header_id desc, audit_id asc, audit_detail_id asc
  

SELECT TOP 1000 [SnapshotID]
      ,[SeriesID]
      ,[Position]
      ,[SnapshotName]
      ,[Notes]
      ,[Style]
      ,[ElementID]
      ,[ElementType]
      ,[StrContent]
      ,[BinContent1]
      ,[BinContent2]
  FROM [EA_LOCAL].[dbo].[auditsnapshot]
  
  SELECT TOP 1000 [AuditHeaderId]
      ,[CreationOrder]
      ,[DateTime]
      ,[UserAccount]
      ,[ErrorType]
      ,[Description]
  FROM [EA_LOCAL].[dbo].[auditerrorlog] order by AuditHeaderId desc, CreationOrder asc
  
SELECT [SnapshotID]
      ,[SeriesID]
      ,[Position]
      ,[SnapshotName]
      ,[Notes]
      ,[Style]
      ,[ElementID]
      ,[ElementType]
      ,[StrContent]
      ,[BinContent1]
      ,[BinContent2]
  FROM [EA_LOCAL].[dbo].[t_snapshot]
GO

