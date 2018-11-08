using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.DataAccessLayer.Interfaces.Entities
{
    public interface IPrivateFilesSharedWithUsersRepository : IGenericDataRepository<PrivateFilesSharedWithUserModel>
    {
    }
}
