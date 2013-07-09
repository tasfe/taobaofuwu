using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RCSoft.Core.Infrastructure;
using RCSoft.Services.Security;

namespace RCSoft.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute:FilterAttribute,IAuthorizationFilter
    {
        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private IEnumerable<UserAuthorizeAttribute> GetAuthorizeAttributes(ActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes(typeof(UserAuthorizeAttribute), true)
                .Concat(descriptor.ControllerDescriptor.GetCustomAttributes(typeof(UserAuthorizeAttribute), true))
                .OfType<UserAuthorizeAttribute>();
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new NotImplementedException("filterContext");
            var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            if (!permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel))
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
