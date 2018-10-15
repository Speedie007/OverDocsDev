CREATE VIEW [dbo].[View_UserDocs_AllUserCreatedDocs]
AS
SELECT        dbo.Files.FileID, dbo.Files.ParentFileID, dbo.Files.UserIDOfFileOwner, dbo.Files.UserIDOfLastUploaded, dbo.Files.FileLookupStatusID, dbo.Files.FileShareStatusID, dbo.Files.ContentType, dbo.Files.FileName, 
                         dbo.Files.FileExtension, dbo.Files.FileSize, dbo.Files.CurrentVersionNumber, dbo.Files.DateCreated, dbo.LookupTable_FileStatuses.FileStatus, dbo.LookupTable_ShareStatues.FileShareStatus, 
                         dbo.AspNetUsers.FirstName + ' ' + dbo.AspNetUsers.LastName AS UserThatLastUpdated, dbo.Files.FileName + '.' + dbo.Files.FileExtension AS FullFileName, '' AS FileType
FROM            dbo.Files INNER JOIN
                         dbo.LookupTable_FileStatuses ON dbo.Files.FileLookupStatusID = dbo.LookupTable_FileStatuses.FileLookupStatusID INNER JOIN
                         dbo.LookupTable_ShareStatues ON dbo.Files.FileShareStatusID = dbo.LookupTable_ShareStatues.FileShareStatusID INNER JOIN
                         dbo.AspNetUsers ON dbo.Files.UserIDOfLastUploaded = dbo.AspNetUsers.Id

