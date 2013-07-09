using RCSoft.Services.Localization;
using RCSoft.Core.Domain.Customers;
using System.Web.Security;
using RCSoft.Core;
using System;

namespace RCSoft.Services.Customers
{
    public partial class CustomerAuthenticationService:ICustomerAuthenticationService
    {
        #region 字段
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly CustomerSettings _customerSettings;
        private readonly IWebHelper _webHelper;
        #endregion

        #region 构造函数
        public CustomerAuthenticationService(ICustomerService customerService, ILocalizationService localizationService, CustomerSettings customerSettings,IWebHelper webHelper)
        {
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._customerSettings = customerSettings;
            this._webHelper = webHelper;
        } 
        #endregion

        #region 方法

        public bool ValidateCustomer(string usernameOrEmail, string password)
        {
            Customer customer = null;
            if (_customerSettings.UsernameEnabled)
                customer = _customerService.GetCustomerByUsername(usernameOrEmail);
            else
                customer = _customerService.GetCustomerByEmail(usernameOrEmail);

            if (customer == null || customer.Deleted || !customer.Active)
                return false;

            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            bool isValid = pwd == customer.Password;
            if (isValid)
            {
                customer.LastIpAddress = _webHelper.GetCurrentIpAddress();
                customer.LastLoginDate = DateTime.Now;
                _customerService.UpdateCustomer(customer);
            }
            return isValid;
        } 
        #endregion
    }
}
