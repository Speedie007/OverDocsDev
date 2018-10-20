using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebDocs.DomainModels.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebDocs.DomainModels.Interfaces.Entities;
    public partial class UsersModel : IEntity
    {
        [Display(Name="Name")]
        public string UserFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set { }
        }
    }
}
