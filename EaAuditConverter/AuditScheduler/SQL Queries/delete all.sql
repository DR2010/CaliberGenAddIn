INSERT INTO [EA_LOCAL].[dbo].[t_snapshot]
           ([SnapshotID]
           ,[SeriesID]
           ,[Position]
           ,[SnapshotName]
           ,[Notes]
           ,[Style]
           ,[ElementID]
           ,[ElementType]
           ,[StrContent]
           ,[BinContent1]
           ,[BinContent2])
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
  FROM [EA_LOCAL].[dbo].[auditsnapshot]
  
DELETE FROM [EA_LOCAL].[dbo].[auditdetail]     
GO

DELETE FROM [EA_LOCAL].[dbo].[audit]
GO

DELETE FROM [EA_LOCAL].[dbo].[auditheader]    
GO

DELETE FROM [EA_LOCAL].[dbo].[auditsnapshot]
GO

DELETE FROM [EA_LOCAL].[dbo].auditerrorlog
GO

DBCC CHECKIDENT
(
  'audit',
  RESEED,0
)

DBCC CHECKIDENT
(
  'auditheader',
  RESEED,0
)

DBCC CHECKIDENT
(
  'auditdetail',
  RESEED,0
)


