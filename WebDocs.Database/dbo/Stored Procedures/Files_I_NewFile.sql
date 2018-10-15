


Create PROCEDURE Files_I_NewFile
--DROP PROCEDURE Files_I_NewFile

			@ParentFileID int
           ,@UserIDOfFileOwner nvarchar(500)
           ,@UserIDOfLastUploaded nvarchar(500)
           ,@FileLookupStatusID int
          , @FileShareStatusID int
           ,@ContentType varchar(500)
          , @FileName varchar(500)
          , @FileExtension varchar(50)
          , @FileImage image
          , @FileSize int
          , @CurrentVersionNumber int
          
	 
AS
	
INSERT INTO [dbo].[Files]
           ([ParentFileID]
           ,[UserIDOfFileOwner]
           ,[UserIDOfLastUploaded]
           ,[FileLookupStatusID]
           ,[FileShareStatusID]
           ,[ContentType]
           ,[FileName]
           ,[FileExtension]
           ,[FileImage]
           ,[FileSize]
           ,[CurrentVersionNumber]
           ,[DateCreated])
     VALUES
           (@ParentFileID 
           ,@UserIDOfFileOwner 
           ,@UserIDOfLastUploaded 
           ,@FileLookupStatusID 
          , @FileShareStatusID 
           ,@ContentType 
          , @FileName 
          , @FileExtension
          , @FileImage 
          , @FileSize 
          , @CurrentVersionNumber 
          , GetDate() );

		

		  SELECT [FileID]
      ,[ParentFileID]
      ,[UserIDOfFileOwner]
      ,[UserIDOfLastUploaded]
      ,[FileLookupStatusID]
      ,[FileShareStatusID]
      ,[ContentType]
      ,[FileName]
      ,[FileExtension]
      ,[FileImage]
      ,[FileSize]
      ,[CurrentVersionNumber]
      ,[DateCreated]
  FROM [dbo].[Files]
  where [FileID] = @@IDENTITY;

 

		   