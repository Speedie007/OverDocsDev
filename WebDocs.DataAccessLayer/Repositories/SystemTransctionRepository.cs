using WebDocs.DomainModels.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DataAccessLayer.Interfaces.Entities;

namespace WebDocs.DataAccessLayer.Repositories
{
    public class SystemTransctionRepository : GenericDataRepository<SystemTransactionLogsModel>, ISystemTransactionRepository
    {
    }
}
