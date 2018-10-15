﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDocs.DataAccessLayer.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WebDocsEntities : DbContext
    {
        public WebDocsEntities()
            : base("name=WebDocsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<FileCategory> FileCategories { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<FileSharedWithUser> FileSharedWithUsers { get; set; }
        public virtual DbSet<LookupTable_FileCategories> LookupTable_FileCategories { get; set; }
        public virtual DbSet<LookupTable_FileStatuses> LookupTable_FileStatuses { get; set; }
        public virtual DbSet<LookupTable_ShareStatues> LookupTable_ShareStatues { get; set; }
        public virtual DbSet<LookupTableNotificationType> LookupTableNotificationTypes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<UserThatDownloadedFile> UserThatDownloadedFiles { get; set; }
        public virtual DbSet<View_FileDownloads_ViewAllFilesThatHaveBeenDownloaded> View_FileDownloads_ViewAllFilesThatHaveBeenDownloaded { get; set; }
        public virtual DbSet<View_PrivateDocView_AllSharedPrivateFiles> View_PrivateDocView_AllSharedPrivateFiles { get; set; }
        public virtual DbSet<View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile> View_PublicDocView_AllFilesWithOwnerAndUserThatLastUpdatedFile { get; set; }
        public virtual DbSet<View_ReturnDocs_AllFilesThatHaveBeenDownloaded> View_ReturnDocs_AllFilesThatHaveBeenDownloaded { get; set; }
        public virtual DbSet<View_UserDocs_AllUserCreatedDocs> View_UserDocs_AllUserCreatedDocs { get; set; }
    
        public virtual ObjectResult<File_R_AllFileDetailsWithoutFioleImage_Result> File_R_AllFileDetailsWithoutFioleImage(Nullable<int> fileID)
        {
            var fileIDParameter = fileID.HasValue ?
                new ObjectParameter("FileID", fileID) :
                new ObjectParameter("FileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<File_R_AllFileDetailsWithoutFioleImage_Result>("File_R_AllFileDetailsWithoutFioleImage", fileIDParameter);
        }
    
        public virtual ObjectResult<Files_I_NewFile_Result> Files_I_NewFile(Nullable<int> parentFileID, string userIDOfFileOwner, string userIDOfLastUploaded, Nullable<int> fileLookupStatusID, Nullable<int> fileShareStatusID, string contentType, string fileName, string fileExtension, byte[] fileImage, Nullable<int> fileSize, Nullable<int> currentVersionNumber)
        {
            var parentFileIDParameter = parentFileID.HasValue ?
                new ObjectParameter("ParentFileID", parentFileID) :
                new ObjectParameter("ParentFileID", typeof(int));
    
            var userIDOfFileOwnerParameter = userIDOfFileOwner != null ?
                new ObjectParameter("UserIDOfFileOwner", userIDOfFileOwner) :
                new ObjectParameter("UserIDOfFileOwner", typeof(string));
    
            var userIDOfLastUploadedParameter = userIDOfLastUploaded != null ?
                new ObjectParameter("UserIDOfLastUploaded", userIDOfLastUploaded) :
                new ObjectParameter("UserIDOfLastUploaded", typeof(string));
    
            var fileLookupStatusIDParameter = fileLookupStatusID.HasValue ?
                new ObjectParameter("FileLookupStatusID", fileLookupStatusID) :
                new ObjectParameter("FileLookupStatusID", typeof(int));
    
            var fileShareStatusIDParameter = fileShareStatusID.HasValue ?
                new ObjectParameter("FileShareStatusID", fileShareStatusID) :
                new ObjectParameter("FileShareStatusID", typeof(int));
    
            var contentTypeParameter = contentType != null ?
                new ObjectParameter("ContentType", contentType) :
                new ObjectParameter("ContentType", typeof(string));
    
            var fileNameParameter = fileName != null ?
                new ObjectParameter("FileName", fileName) :
                new ObjectParameter("FileName", typeof(string));
    
            var fileExtensionParameter = fileExtension != null ?
                new ObjectParameter("FileExtension", fileExtension) :
                new ObjectParameter("FileExtension", typeof(string));
    
            var fileImageParameter = fileImage != null ?
                new ObjectParameter("FileImage", fileImage) :
                new ObjectParameter("FileImage", typeof(byte[]));
    
            var fileSizeParameter = fileSize.HasValue ?
                new ObjectParameter("FileSize", fileSize) :
                new ObjectParameter("FileSize", typeof(int));
    
            var currentVersionNumberParameter = currentVersionNumber.HasValue ?
                new ObjectParameter("CurrentVersionNumber", currentVersionNumber) :
                new ObjectParameter("CurrentVersionNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Files_I_NewFile_Result>("Files_I_NewFile", parentFileIDParameter, userIDOfFileOwnerParameter, userIDOfLastUploadedParameter, fileLookupStatusIDParameter, fileShareStatusIDParameter, contentTypeParameter, fileNameParameter, fileExtensionParameter, fileImageParameter, fileSizeParameter, currentVersionNumberParameter);
        }
    
        public virtual int Files_U_SetFileStatus(Nullable<int> fileID, Nullable<int> fileLookupStatusID)
        {
            var fileIDParameter = fileID.HasValue ?
                new ObjectParameter("FileID", fileID) :
                new ObjectParameter("FileID", typeof(int));
    
            var fileLookupStatusIDParameter = fileLookupStatusID.HasValue ?
                new ObjectParameter("FileLookupStatusID", fileLookupStatusID) :
                new ObjectParameter("FileLookupStatusID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Files_U_SetFileStatus", fileIDParameter, fileLookupStatusIDParameter);
        }
    
        public virtual ObjectResult<PrivateDocs_R_GetAllPrivateSharedUserFiles_Result> PrivateDocs_R_GetAllPrivateSharedUserFiles(string currentlyLoggedingUserID)
        {
            var currentlyLoggedingUserIDParameter = currentlyLoggedingUserID != null ?
                new ObjectParameter("CurrentlyLoggedingUserID", currentlyLoggedingUserID) :
                new ObjectParameter("CurrentlyLoggedingUserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PrivateDocs_R_GetAllPrivateSharedUserFiles_Result>("PrivateDocs_R_GetAllPrivateSharedUserFiles", currentlyLoggedingUserIDParameter);
        }
    
        public virtual int PublicDocs_R_GetMostRecentFileVersion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PublicDocs_R_GetMostRecentFileVersion");
        }
    
        public virtual ObjectResult<PublicDocs_R_GetSelectedFileHistory_Result> PublicDocs_R_GetSelectedFileHistory(Nullable<int> fileID)
        {
            var fileIDParameter = fileID.HasValue ?
                new ObjectParameter("FileID", fileID) :
                new ObjectParameter("FileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PublicDocs_R_GetSelectedFileHistory_Result>("PublicDocs_R_GetSelectedFileHistory", fileIDParameter);
        }
    
        public virtual ObjectResult<UserDocs_R_GetAllUserCreatedDocs_Result> UserDocs_R_GetAllUserCreatedDocs(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserDocs_R_GetAllUserCreatedDocs_Result>("UserDocs_R_GetAllUserCreatedDocs", userIDParameter);
        }
    }
}
