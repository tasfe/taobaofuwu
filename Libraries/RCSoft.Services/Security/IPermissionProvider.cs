using System.Collections.Generic;
using RCSoft.Core.Domain.Security;

namespace RCSoft.Services.Security
{
    public interface IPermissionProvider
    {
        IEnumerable<PermissionRecord> GetPermissions();
        IEnumerable<DefaultPermissionRecord> GetDefaultPermissions();
    }
}
