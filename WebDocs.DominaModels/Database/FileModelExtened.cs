using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.Database
{
    using System;
    using System.Collections.Generic;

    using WebDocs.DomainModels.Interfaces.Entities;
    public partial class FileModel : IEntity
    {
        public string FullName
        {
            get
            {
                return this.Name + "." + this.Extension;
            }
            set { }
        }

        

    }
}
