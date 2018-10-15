CREATE VIEW dbo.View_PrivateDocView_AllSharedPrivateFiles
AS
SELECT        dbo.Files.FileID, dbo.Files.ParentFileID, dbo.Files.UserIDOfFileOwner, dbo.Files.UserIDOfLastUploaded, dbo.Files.FileLookupStatusID, dbo.Files.FileShareStatusID, dbo.Files.ContentType, dbo.Files.FileName, 
                         dbo.Files.FileExtension, dbo.Files.FileSize, dbo.Files.CurrentVersionNumber, dbo.Files.DateCreated, dbo.LookupTable_FileStatuses.FileStatus, 
                         dbo.AspNetUsers.FirstName + ' ' + dbo.AspNetUsers.LastName AS NameOfFileOwner, AspNetUsers_1.FirstName + ' ' + AspNetUsers_1.LastName AS UserThatLastUpdatedFile, 
                         dbo.FileSharedWithUsers.UserIDOfSharedDocs AS RefID, dbo.Files.FileName + '.' + dbo.Files.FileExtension AS FullFileName, '' AS FileType, ISNULL(dbo.UserThatDownloadedFile.UserIDThatDownloadedFIle, '') 
                         AS IdOfUserThatDownloadedTheFile
FROM            dbo.Files INNER JOIN
                         dbo.FileSharedWithUsers ON dbo.Files.FileID = dbo.FileSharedWithUsers.FileID INNER JOIN
                         dbo.LookupTable_FileStatuses ON dbo.Files.FileLookupStatusID = dbo.LookupTable_FileStatuses.FileLookupStatusID INNER JOIN
                         dbo.AspNetUsers ON dbo.Files.UserIDOfFileOwner = dbo.AspNetUsers.Id INNER JOIN
                         dbo.AspNetUsers AS AspNetUsers_1 ON dbo.Files.UserIDOfLastUploaded = AspNetUsers_1.Id LEFT OUTER JOIN
                         dbo.UserThatDownloadedFile ON dbo.Files.FileID = dbo.UserThatDownloadedFile.FileID

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
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
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
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Files"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FileSharedWithUsers"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupTable_FileStatuses"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 366
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 366
               Left = 38
               Bottom = 496
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers_1"
            Begin Extent = 
               Top = 498
               Left = 38
               Bottom = 628
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserThatDownloadedFile"
            Begin Extent = 
               Top = 6
               Left = 288
               Bottom = 136
               Right = 520
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_PrivateDocView_AllSharedPrivateFiles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'dths = 11
         Column = 1440
         Alias = 900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_PrivateDocView_AllSharedPrivateFiles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_PrivateDocView_AllSharedPrivateFiles';

