CREATE VIEW dbo.View_AllFiles
AS
SELECT        AllFiles.FileID AS Ref#, AllFiles.UserIDOfFileOwner AS UpdateByID, AllFiles.UserIDOfLastUploaded, AllFiles.FileLookupStatusID, AllFiles.FileShareStatusID, AllFiles.ContentType AS [Content Type], AllFiles.FileName AS Name, 
                         AllFiles.FileExtension AS Extension, AllFiles.FileSize AS Size, AllFiles.CurrentVersionNumber AS Version, AllFiles.DateCreated AS [Date Created], 
                         tblUserThatUploadLast.FirstName + ' ' + tblUserThatUploadLast.LastName AS [Updated By], tblFileOwner.FirstName + ' ' + tblFileOwner.LastName AS [File Owner], AllFiles.FileName + '.' + AllFiles.FileExtension AS FileFullName, 
                         LookupTable_FileShareStatues_1.FileShareStatus AS Availablity, LookupTable_FileViewStatuses_1.FileStatus AS Status, ISNULL(dbo.AspNetUsers.FirstName + ' ' + dbo.AspNetUsers.LastName, 'NA') AS [Locked By], 
                         ISNULL(UserThatDownloadedFile_1.UserIDThatDownloadedFIle, 0) AS UserIDOFPersonThatDownloadedFile
FROM            dbo.UserThatDownloadedFile AS UserThatDownloadedFile_1 INNER JOIN
                         dbo.AspNetUsers ON UserThatDownloadedFile_1.UserIDThatDownloadedFIle = dbo.AspNetUsers.Id RIGHT OUTER JOIN
                         dbo.Files AS AllFiles INNER JOIN
                         dbo.AspNetUsers AS tblUserThatUploadLast ON AllFiles.UserIDOfLastUploaded = tblUserThatUploadLast.Id INNER JOIN
                         dbo.LookupTable_FileViewStatuses ON AllFiles.FileLookupStatusID = dbo.LookupTable_FileViewStatuses.FileLookupStatusID INNER JOIN
                         dbo.LookupTable_FileShareStatues ON AllFiles.FileShareStatusID = dbo.LookupTable_FileShareStatues.FileShareStatusID INNER JOIN
                         dbo.AspNetUsers AS tblFileOwner ON AllFiles.UserIDOfFileOwner = tblUserThatUploadLast.Id INNER JOIN
                         dbo.LookupTable_FileViewStatuses AS LookupTable_FileViewStatuses_1 ON AllFiles.FileLookupStatusID = LookupTable_FileViewStatuses_1.FileLookupStatusID INNER JOIN
                         dbo.LookupTable_FileShareStatues AS LookupTable_FileShareStatues_1 ON AllFiles.FileShareStatusID = LookupTable_FileShareStatues_1.FileShareStatusID ON UserThatDownloadedFile_1.FileID = AllFiles.FileID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_AllFiles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'3
               Bottom = 288
               Right = 626
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserThatDownloadedFile_1"
            Begin Extent = 
               Top = 279
               Left = 777
               Bottom = 409
               Right = 1009
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 362
               Left = 393
               Bottom = 492
               Right = 617
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1695
         Width = 1980
         Width = 1710
         Width = 1545
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 840
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 6075
         Alias = 1785
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_AllFiles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4[50] 3) )"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[75] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4[60] 2) )"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -111
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblUserThatUploadLast"
            Begin Extent = 
               Top = 33
               Left = 25
               Bottom = 95
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupTable_FileViewStatuses"
            Begin Extent = 
               Top = 173
               Left = 744
               Bottom = 269
               Right = 979
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupTable_FileShareStatues"
            Begin Extent = 
               Top = 6
               Left = 911
               Bottom = 102
               Right = 1138
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblFileOwner"
            Begin Extent = 
               Top = 96
               Left = 26
               Bottom = 174
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "LookupTable_FileViewStatuses_1"
            Begin Extent = 
               Top = 278
               Left = 69
               Bottom = 374
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupTable_FileShareStatues_1"
            Begin Extent = 
               Top = 382
               Left = 71
               Bottom = 478
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AllFiles"
            Begin Extent = 
               Top = 13
               Left = 43', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_AllFiles';

