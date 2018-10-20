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
    public partial class FileArchiveModel: IEntity
    {
        public int FileArchiveID { get; set; }
    	
        public int FileID { get; set; }
    	
        public int UserIDOfLastUploaded { get; set; }
    	
        public int FileLookupStatusID { get; set; }
    	
        public int FileShareStatusID { get; set; }
    	
        public string ContentType { get; set; }
    	
        public string FileName { get; set; }
    	
        public string FileExtension { get; set; }
    	
        public int FileSize { get; set; }
    	
        public byte[] FileImage { get; set; }
    	
        public int CurrentVersionNumber { get; set; }
    	
        public System.DateTime DateCreated { get; set; }
    	
        public System.DateTime DateArchived { get; set; }
    	
    		public EntityState EntityState { get; set; }
    		
        public virtual FileModel File { get; set; }
    }
}
