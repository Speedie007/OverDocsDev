using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Interfaces.Entities;

namespace WebDocs.DomainModels.Database
{
    public partial class FileArchiveModel : IEntity
    {

        public string FullFileName
        {
            get
            {
                return this.Name + "." + this.Extension;
            }
            set { }
        }
    }
}
