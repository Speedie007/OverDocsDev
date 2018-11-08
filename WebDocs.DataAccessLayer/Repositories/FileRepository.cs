using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DomainModels.Database;

namespace WebDocs.DataAccessLayer.Repositories
{
    public class FileRepository : GenericDataRepository<FileModel>, IFileRepository, IFileProcessingRepository
    {
    }
}
