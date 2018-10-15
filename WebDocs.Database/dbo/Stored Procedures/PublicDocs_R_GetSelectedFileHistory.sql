
CREATE PROCEDURE [dbo].[PublicDocs_R_GetSelectedFileHistory]
--DROP PROCEDURE PublicDocs_R_GetSelectedFileHistory
	@FileID int
AS
	WITH LastFileUpdated([FileID]
      ,[ParentFileID]
      ,[UserIDOfFileOwner]
      ,[UserIDOfLastUploaded]
      ,[CurrentFileStatus]
      ,[FileLookupStatusID]
      ,[CurrentFileShareStatus]
      ,[FileShareStatusID]
      ,[ContentType]
      ,[FileName]
      ,[FileExtension]
      ,[FullFileName]
      ,[FileSize]
      ,[CurrentVersionNumber]
      ,[DateCreated]
      ,[NameOfFileOwner]
      ,[NameOfUserThatLastUpdatedFile]
	  ,[ListOfUserIDThatTheFileISsharedWith] 
	  ,[IdOfUserThatDownloadedTheFile]
	   ,ComponentLevel) AS
(

Select
    b.[FileID]
      ,b.[ParentFileID]
      ,b.[UserIDOfFileOwner]
      ,b.[UserIDOfLastUploaded]
      ,b.[CurrentFileStatus]
      ,b.[FileLookupStatusID]
      ,b.[CurrentFileShareStatus]
      ,b.[FileShareStatusID]
      ,b.[ContentType]
      ,b.[FileName]
      ,b.[FileExtension]
      ,b.[FullFileName]
      ,b.[FileSize]
      ,b.[CurrentVersionNumber]
      ,b.[DateCreated]
      ,b.[NameOfFileOwner]
      ,b.[NameOfUserThatLastUpdatedFile]
		,b.[ListOfUserIDThatTheFileISsharedWith] 
		,b.[IdOfUserThatDownloadedTheFile]
		,0 AS ComponentLevel
		
	
	FROM View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile AS b
	--Change to where PArentid = 0 just now
   WHERE b.[FileID] = @FileID
    UNION ALL
   SELECT 
	
	bom.[FileID]
      ,bom.[ParentFileID]
      ,bom.[UserIDOfFileOwner]
      ,bom.[UserIDOfLastUploaded]
      ,bom.[CurrentFileStatus]
      ,bom.[FileLookupStatusID]
      ,bom.[CurrentFileShareStatus]
      ,bom.[FileShareStatusID]
      ,bom.[ContentType]
      ,bom.[FileName]
      ,bom.[FileExtension]
      ,bom.[FullFileName]
      ,bom.[FileSize]
      ,bom.[CurrentVersionNumber]
      ,bom.[DateCreated]
      ,bom.[NameOfFileOwner]
      ,bom.[NameOfUserThatLastUpdatedFile]
	  ,bom.[ListOfUserIDThatTheFileISsharedWith]
	  ,bom.[IdOfUserThatDownloadedTheFile]
		,ComponentLevel + 1
    FROM View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile AS bom 
        INNER JOIN LastFileUpdated AS p
        ON bom.[FileID]= p.[ParentFileID] 
        
)
SELECT p.[FileID]
      ,p.[ParentFileID]
      ,p.[UserIDOfFileOwner]
      ,p.[UserIDOfLastUploaded]
      ,p.[CurrentFileStatus]
      ,p.[FileLookupStatusID]
      ,p.[CurrentFileShareStatus]
      ,p.[FileShareStatusID]
      ,p.[ContentType]
      ,p.[FileName]
      ,p.[FileExtension]
      ,p.[FullFileName]
      ,p.[FileSize]
      ,p.[CurrentVersionNumber]
      ,p.[DateCreated]
      ,p.[NameOfFileOwner]
      ,p.[NameOfUserThatLastUpdatedFile]
	  ,'' AS FileType
	  ,p.[ListOfUserIDThatTheFileISsharedWith] 
	  ,p.[IdOfUserThatDownloadedTheFile]
FROM LastFileUpdated AS p
where p.FileID != @FileID
order by ComponentLevel desc
 --INNER JOIN View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile AS pr
 --   ON p.[FileID]= pr.[ParentFileID] 
