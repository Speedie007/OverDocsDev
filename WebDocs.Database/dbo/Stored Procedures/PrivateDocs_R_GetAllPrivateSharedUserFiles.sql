-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PrivateDocs_R_GetAllPrivateSharedUserFiles]
--DROP PROCUDURE PrivateDocs_R_GetAllPrivateSharedUserFiles

	-- Add the parameters for the stored procedure here
	@CurrentlyLoggedingUserID nvarchar(200)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [FileID]
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
      ,[NameOfFileOwner]
      ,[UserThatLastUpdatedFile]
      ,[RefID]
      ,[FullFileName]
	  , '' AS FileType
	   ,[IdOfUserThatDownloadedTheFile]
  FROM [WebDocs].[dbo].[View_PrivateDocView_AllSharedPrivateFiles]
   where [RefID] = @CurrentlyLoggedingUserID;
END

