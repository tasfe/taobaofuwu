using System;
using System.Web;
using RCSoft.Services.Customers;
using System.Web.Security;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Services.Authentication
{
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSetting;
        private TimeSpan _expirationTimeSpan;
        private Customer _cachedCustomer;

        public FormsAuthenticationService(HttpContextBase httpContext, CustomerSettings customerSetting, ICustomerService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._customerSetting = customerSetting;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }
        public void SignIn(Core.Domain.Customers.Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.Now.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(
                1/*version*/,
                _customerSetting.UsernameEnabled ? customer.Username : customer.Email,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                _customerSetting.UsernameEnabled ? customer.Username : customer.Email,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            _httpContext.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }

        public void SignOut()
        {
            _cachedCustomer=null;
            FormsAuthentication.SignOut();
        }

        public Customer GetAuthenticateCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;
            if (_httpContext == null || _httpContext.Request == null || !_httpContext.Request.IsAuthenticated || !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }
            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticateCustomerFormTicket(formsIdentity.Ticket);
            if (customer != null && !customer.Deleted && customer.Active)
            {
                _cachedCustomer = customer;
            }
            return _cachedCustomer;
        }

        public virtual Customer GetAuthenticateCustomerFormTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            var usernameOrEmail = ticket.UserData;
            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var customer = _customerSetting.UsernameEnabled ? _customerService.GetCustomerByUsername(usernameOrEmail) : _customerService.GetCustomerByEmail(usernameOrEmail);
            return customer;
        }
    }
}
