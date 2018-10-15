-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[PublicDocs_R_GetMostRecentFileVersion]
--DROP Procedure PublicDocs_R_GetMostRecentFileVersion
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	
CREATE TABLE #MyTEmpTable
(
		[FileID] int
      ,[ParentFileID] int
      ,[UserIDOfFileOwner] nvarchar(100)
      ,[UserIDOfLastUploaded] nvarchar(100)
      ,[CurrentFileStatus] varchar(50)
      ,[FileLookupStatusID] int
      ,[CurrentFileShareStatus] varchar(500)
      ,[FileShareStatusID] int
      ,[ContentType] varchar(500)
      ,[FileName] varchar(500)
      ,[FileExtension] varchar(10)
      ,[FullFileName] varchar(500)
      ,[FileSize] int
      ,[CurrentVersionNumber] int
      ,[DateCreated] datetime
      ,[NameOfFileOwner] varchar(500)
      ,[NameOfUserThatLastUpdatedFile] varchar(500)
	  ,[ListOfUserIDThatTheFileISsharedWith] varchar(MAX)
	  ,[IdOfUserThatDownloadedTheFile] varchar(500)
	  ,ComponentLevel int
);

 Create table #TempOfAllSharedUsersID(
		UserID Varchar(500)
 );

----Start
DECLARE @FileID int 


DECLARE vendor_cursor CURSOR FOR   
SELECT
		[FileID]
	FROM View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile
	where [ParentFileID]  = 0; 

OPEN vendor_cursor  

FETCH NEXT FROM vendor_cursor   
INTO @FileID  

WHILE @@FETCH_STATUS = 0  
BEGIN  
    ----START OF CURSOR 

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
    SELECT
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
   WHERE b.Fileid = @FileID
         
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
        ON bom.[ParentFileID] = p.[FileID]
       
)
insert into #MyTEmpTable
SELECT top 1 [FileID]
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
        ,ComponentLevel 
FROM LastFileUpdated AS p
    --INNER JOIN Production.Product AS pr
    --ON p.<component_id, sysname, ComponentID> = pr.ProductID
ORDER BY ComponentLevel desc,[FileID],[ParentFileID]

--END OF COURsOR
    FETCH NEXT FROM vendor_cursor 
	INTO @FileID  
END   
CLOSE vendor_cursor;  
DEALLOCATE vendor_cursor;  


-----All new stuff
Declare @FileIDToDetermineUseredUser int;

Declare @InnerLoop_UserID varchar(500);
Declare @InnerLoop_ListOfUserID varchar(max);



DECLARE UserIDOfTheUsersThatTheFileIsSharedWith_Cursor CURSOR FOR   
SELECT
		[FileID]
	FROM #MyTEmpTable; 

OPEN UserIDOfTheUsersThatTheFileIsSharedWith_Cursor  

FETCH NEXT FROM UserIDOfTheUsersThatTheFileIsSharedWith_Cursor   
INTO @FileIDToDetermineUseredUser  

WHILE @@FETCH_STATUS = 0  
BEGIN  
    ----START OF CURSOR 

	-----New Stuff IOnner Loop

	set @InnerLoop_ListOfUserID = '';
			

			Declare @LentghtOfUSERIDSet int;
													DECLARE UserIdOfSharedDocs_Cursor CURSOR FOR   
																		SELECT [UserIDOfSharedDocs]
																		  FROM [WebDocs].[dbo].[FileSharedWithUsers]
																		  where FileID = @FileIDToDetermineUseredUser; 

																	OPEN UserIdOfSharedDocs_Cursor  

																	FETCH NEXT FROM UserIdOfSharedDocs_Cursor   
																	INTO @InnerLoop_UserID  

																	WHILE @@FETCH_STATUS = 0  
																	BEGIN  
																	------------start
																							---------------begin

																								set @LentghtOfUSERIDSet = Convert(int,Len(@InnerLoop_ListOfUserID));
																								--print 'Length of user id set before entering if statement : ' + convert(varchar(max),@LentghtOfUSERIDSet);
																								IF @LentghtOfUSERIDSet > 0
																								BEGIN
																										--print  'inside of BEGIN  ifstatement : ';
																										SET @InnerLoop_ListOfUserID = '' + convert(varchar(max),@InnerLoop_ListOfUserID) + '?';
																										--print  'inside of end  ifstatement : ';
																								END

																								SET @InnerLoop_ListOfUserID = convert(varchar(max),@InnerLoop_ListOfUserID) + convert(varchar(max), @InnerLoop_UserID);
																								--END OF COURsOR
																	-----------End
																	--END OF COURsOR
														FETCH NEXT FROM UserIdOfSharedDocs_Cursor 
														INTO @InnerLoop_UserID  
													END   
													CLOSE UserIdOfSharedDocs_Cursor;  
													DEALLOCATE UserIdOfSharedDocs_Cursor;

													Update #MyTEmpTable
													SET ListOfUserIDThatTheFileISsharedWith = @InnerLoop_ListOfUserID
													where @FileIDToDetermineUseredUser = FileID;
	----EndOF New Stuff Inner Loop
	--END OF COURsOR
    FETCH NEXT FROM UserIDOfTheUsersThatTheFileIsSharedWith_Cursor 
	INTO @FileIDToDetermineUseredUser  
END   
CLOSE UserIDOfTheUsersThatTheFileIsSharedWith_Cursor;  
DEALLOCATE UserIDOfTheUsersThatTheFileIsSharedWith_Cursor; 


--End of new stuff


select [FileID]
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
      ,[NameOfUserThatLastUpdatedFile],
		'' AS FileType
		,[ListOfUserIDThatTheFileISsharedWith]
		,[IdOfUserThatDownloadedTheFile]
	   from #MyTEmpTable
	   where [FileShareStatusID] != 6;
---End

DROP TABLE #MyTEmpTable;
DROP TABLE #TempOfAllSharedUsersID;
END

