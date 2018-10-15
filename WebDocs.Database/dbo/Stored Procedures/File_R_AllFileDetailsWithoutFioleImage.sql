
CREATE PROCEDURE File_R_AllFileDetailsWithoutFioleImage
--create PROCEDURE File_R_AllFileDetailsWithoutFioleImage

@FileID int 

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
  FROM [WebDocs].[dbo].[Files]
  Where [FileID] = @FileID
