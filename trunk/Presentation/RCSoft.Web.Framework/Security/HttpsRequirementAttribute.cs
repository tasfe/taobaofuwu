using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RCSoft.Core.Data;

namespace RCSoft.Web.Framework.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public HttpsRequirementAttribute(SslRequirement sslRequirement)
        {
            this.SslRequirement = sslRequirement;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new NotImplementedException("filterContext");
            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;

            // only redirect for GET requests, 
            // otherwise the browser might not propagate the verb and request body correctly.
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var currentConnectionSecured = filterContext.HttpContext.Request.IsSecureConnection;
            //currentConnectionSecured = webHelper.IsCurrentConnectionSecured();


            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;
        }

        public SslRequirement SslRequirement { get; set; }
    }
}
