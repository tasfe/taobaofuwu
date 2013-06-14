using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core;
using System.Web;
using RCSoft.Core.Domain;
using RCSoft.Services.Customers;

namespace RCSoft.Web.Framework
{
    public partial class WebWorkContext : IWorkContext
    {
        private const string CustomerCookieName = "RCSoft.customer";

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;

        public Customer _cachedCustomer;
        public WebWorkContext(HttpContextBase httpContext,ICustomerService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
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
            //if (_cachedCustomer != null)
            //    return _cachedCustomer;
            //Customer customer = null;
            //if (_httpContext != null)
            //{ 
            //    if()
            //}
            return null;
        }
    }
}
