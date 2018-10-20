﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDocs.DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using WebDocs.DomainModels.Database;
    using WebDocs.DomainModels;
    
    public partial class WebDocsEntities : DbContext
    {
        public WebDocsEntities()
            : base("name=WebDocsEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UsersModel> UsersModels { get; set; }
        public virtual DbSet<ChatModel> ChatModels { get; set; }
        public virtual DbSet<EmailSetting> EmailSettings { get; set; }
        public virtual DbSet<FileArchiveModel> FileArchiveModels { get; set; }
        public virtual DbSet<FileCategoryModel> FileCategoryModels { get; set; }
        public virtual DbSet<CategoriesModel> CategoriesModels { get; set; }
        public virtual DbSet<FileShareStatusModel> FileShareStatusModels { get; set; }
        public virtual DbSet<FileStatusModel> FileStatusModels { get; set; }
        public virtual DbSet<NotificationTypesModel> NotificationTypesModels { get; set; }
        public virtual DbSet<NotificationModel> NotificationModels { get; set; }
        public virtual DbSet<PrivateFilesSharedWithUser> PrivateFilesSharedWithUsers { get; set; }
        public virtual DbSet<UserChatModel> UserChatModels { get; set; }
        public virtual DbSet<UserThatDownloadedFileModel> UserThatDownloadedFileModels { get; set; }
        public virtual DbSet<FileBlobModel> FileBlobModels { get; set; }
        public virtual DbSet<FileModel> FileModels { get; set; }
    }
}