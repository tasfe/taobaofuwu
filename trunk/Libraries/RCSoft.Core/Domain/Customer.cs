using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain
{
    public partial class Customer : BaseEntity
    {
        public Customer()
        { 
            
        }

        public virtual Guid CustomerGuid { get; set; }
    }
}
