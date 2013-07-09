using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core;
using System.Web;
using RCSoft.Services.Customers;
using RCSoft.Core.Domain.Customers;
using RCSoft.Services.Authentication;

namespace RCSoft.Web.Framework
{
    public partial class WebWorkContext : IWorkContext
    {
        private const string CustomerCookieName = "RCSoft.customer";

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWebHelper _webHelper;

        public Customer _cachedCustomer;
        public WebWorkContext(HttpContextBase httpContext, ICustomerService customerService, IAuthenticationService authenticationService,IWebHelper webHelper)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._authenticationService = authenticationService;
            this._webHelper = webHelper;
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
            if (_cachedCustomer != null)
                return _cachedCustomer;
            Customer customer = null;
            if (_httpContext != null)
            {
                if (customer == null || customer.Deleted || !customer.Active)
                {
                    customer = _authenticationService.GetAuthenticateCustomer();
                }
                else
                { 
                    string currentIpAddress=_webHelper.GetCurrentIpAddress();
                    if (!string.IsNullOrEmpty(currentIpAddress))
                    {
                        if (!currentIpAddress.Equals(customer.LastIpAddress))
                        {
                            customer.LastIpAddress = currentIpAddress;
                            _customerService.UpdateCustomer(customer);
                        }
                    }
                }
            }
            return customer;
        }
    }
}
