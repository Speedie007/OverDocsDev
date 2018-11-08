using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.Common.Helper.Sorting
{
    public static class FilesFieldSorting
    {
        public static IList<FileModel> SortFields(IList<FileModel> Rtn, string sortOrder)
        {

            if (Rtn.Count > 0)
            {
                switch (sortOrder)
                {
                    case "FullName_desc":
                        Rtn = Rtn.OrderByDescending(s => s.FullFileName).ToList();
                        break;

                    case "DateCreated":
                        Rtn = Rtn.OrderBy(s => s.Created).ToList();
                        break;
                    case "DateCreated_desc":
                        Rtn = Rtn.OrderByDescending(s => s.Created).ToList();
                        break;

                    case "NameOfFileOwner":
                        Rtn = Rtn.OrderBy(s => s.FileOwner.UserFullName).ToList();
                        break;
                    case "NameOfFileOwner_desc":
                        Rtn = Rtn.OrderByDescending(s => s.FileOwner.UserFullName).ToList();
                        break;

                    case "FileID":
                        Rtn = Rtn.OrderBy(s => s.FileID).ToList();
                        break;
                    case "FileID_desc":
                        Rtn = Rtn.OrderByDescending(s => s.FileID).ToList();
                        break;

                    case "NameOfPersonLastUpdatedBy":
                        Rtn = Rtn.OrderBy(s => s.PersonThatLastUpdatedFile.UserFullName).ToList();
                        break;
                    case "NameOfPersonLastUpdatedBy_desc":
                        Rtn = Rtn.OrderByDescending(s => s.PersonThatLastUpdatedFile.UserFullName).ToList();
                        break;

                    case "FileSize":
                        Rtn = Rtn.OrderBy(s => s.Size).ToList();
                        break;
                    case "FileSize_desc":
                        Rtn = Rtn.OrderByDescending(s => s.Size).ToList();
                        break;

                    case "CurrentFileStatus":
                        Rtn = Rtn.OrderBy(s => s.FileViewStatuses.Status).ToList();
                        break;
                    case "CurrentFileStatus_desc":
                        Rtn = Rtn.OrderByDescending(s => s.FileViewStatuses.Status).ToList();
                        break;

                    case "CurrentFileShareStatus":
                        Rtn = Rtn.OrderBy(s => s.FileShareStatues.Status).ToList();
                        break;
                    case "CurrentFileShareStatus_desc":
                        Rtn = Rtn.OrderByDescending(s => s.FileShareStatues.Status).ToList();
                        break;

                    case "Version":
                        Rtn = Rtn.OrderBy(s => s.CurrentVersionNumber).ToList();
                        break;
                    case "Version_desc":
                        Rtn = Rtn.OrderByDescending(s => s.CurrentVersionNumber).ToList();
                        break;
                    default:  // Name ascending 
                        Rtn = Rtn.OrderBy(s => s.FullFileName).ToList();
                        break;
                };
            }


            return Rtn;
        }
    }
}
