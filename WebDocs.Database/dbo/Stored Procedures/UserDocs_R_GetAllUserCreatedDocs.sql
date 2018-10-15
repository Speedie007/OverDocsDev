

CREATE PROCEDURE [dbo].[UserDocs_R_GetAllUserCreatedDocs]
--DROP PROCEDURE [dbo].[UserDocs_R_GetAllUserCreatedDocs]
	@UserID nVarchar(200)
AS
	SELECT
	 [FileID]
      ,[ParentFileID]
      ,[UserIDOfFileOwner]
      ,[UserIDOfLastUploaded]
      ,[FileLookupStatusID]
      ,[FileShareStatusID]
      ,[ContentType]
      ,[FileName]
      ,[FileExtension]
      ,[FileSize]
      ,[CurrentVersionNumber]
      ,[DateCreated]
      ,[FileStatus]
      ,[FileShareStatus]
      ,[UserThatLastUpdated]
      ,[FullFileName]
	  , '' AS FileType
  FROM [WebDocs].[dbo].[View_UserDocs_AllUserCreatedDocs]
  where [UserIDOfFileOwner] = @UserID;

