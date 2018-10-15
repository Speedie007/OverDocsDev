-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists


CREATE PROCEDURE Files_U_SetFileStatus
--DROP PROCEDURE Files_U_SetFileStatus
	@FileID int,
	@FileLookupStatusID int
AS
	UPDATE [dbo].[Files]
   SET 
      [FileLookupStatusID] = @FileLookupStatusID
     
 WHERE FileID = @FileID;

