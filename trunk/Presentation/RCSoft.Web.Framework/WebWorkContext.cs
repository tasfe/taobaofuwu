using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core;
using System.Web;
using RCSoft.Core.Domain;

namespace RCSoft.Web.Framework
{
    public partial class WebWorkContext:IWorkContext
    {
        private const string CustomerCookieName = "RCSoft.customer";

        private readonly HttpContextBase _httpContext;
        public Customer _cachedCustomer;
        public WebWorkContext(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }
        public Customer CurrentCustomer
        {
            get
            {
                return GetCurrentCustomer();
            }
            set
            {
                _cachedCustomer = value;
            }
        }

        private Customer GetCurrentCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
