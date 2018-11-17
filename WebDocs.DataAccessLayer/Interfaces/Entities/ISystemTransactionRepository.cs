using WebDocs.DomainModels.Database;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DataAccessLayer.Interfaces.Entities
{
    public interface ISystemTransactionRepository : IGenericDataRepository<SystemTransactionLogsModel>
    {
    }
}
