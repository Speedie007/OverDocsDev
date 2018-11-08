using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.DataAccessLayer.Repositories
{
    public class UserThatDownloadedFileRepository : GenericDataRepository<UserThatDownloadedFileModel>, IUserThatDownloadedFileRepository
    {

    }
}
