//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDocs.DomainModels.Database
{
    using System;
    using System.Collections.Generic;
    
     using WebDocs.DomainModels.Interfaces.Entities;
    public partial class UserThatDownloadedFileModel: IEntity
    {
        public int FileID { get; set; }
    	
        public int UserIDThatDownloadedFIle { get; set; }
    	
        public System.DateTime DateDownloaded { get; set; }
    	
        public bool HasFileBeenReturned { get; set; }
    	
    		public EntityState EntityState { get; set; }
    		
        public virtual UsersModel AspNetUser { get; set; }
        public virtual FileModel File { get; set; }
    }
}
