using System.Collections.Generic;
using RCSoft.Core.Domain.Security;

namespace RCSoft.Services.Security
{
    public partial class StandardPermissionProvider:IPermissionProvider
    {
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "Access admin area", SystemName = "AccessAdminPanel", Category = "Standard" };
        public static readonly PermissionRecord ManageCatalog = new PermissionRecord { Name = "Manage Catalog", SystemName = "ManageCatalog", Category = "类别" };

        public IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                ManageCatalog
            };
        }

        public IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    CustomerRoleSystemName="Admin",
                    PermissionRecords=new[]
                    {
                        AccessAdminPanel,
                        ManageCatalog
                    }
                }
            };
        }
    }
}
