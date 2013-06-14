using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Core.Domain.Security
{
    public class DefaultPermissionRecord
    {
        public DefaultPermissionRecord()
        {
            this.PermissionRecords = new List<PermissionRecord>();
        }

        public string CustomerRoleSystemName { get; set; }

        public IEnumerable<PermissionRecord> PermissionRecords { get; set; }
    }
}
