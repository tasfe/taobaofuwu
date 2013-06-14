using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Core.Domain
{
    public partial class Customer : BaseEntity
    {
        private ICollection<CustomerRole> _customerRoles;

        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
        }

        public virtual Guid CustomerGuid { get; set; }

        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }
    }
}
