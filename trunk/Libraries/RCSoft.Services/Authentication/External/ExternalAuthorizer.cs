using System;
using RCSoft.Services.Customers;
using RCSoft.Core;
using RCSoft.Core.Domain.Customers;
using RCSoft.Core.Domain.Localization;

namespace RCSoft.Services.Authentication.External
{
    public partial class ExternalAuthorizer : IExternalAuthorizer
    {
        #region 字段
        private readonly IAuthenticationService _authenticationService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly ICustomerAuthenticationService _customerAuthenticationService;
        private readonly IWorkContext _workContext;
        private readonly CustomerSettings _customerSettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly LocalizationSettings _localizationSettings;
        #endregion

        #region 构造函数
        public ExternalAuthorizer(IAuthenticationService authenticationService, IOpenAuthenticationService openAuthenticationService, ICustomerAuthenticationService customerAuthenticationService, IWorkContext workContext, CustomerSettings customerSettings,
            ExternalAuthenticationSettings externalAuthenticationSettings, LocalizationSettings localizationSettings)
        {
            this._authenticationService = authenticationService;
            this._openAuthenticationService = openAuthenticationService;
            this._customerAuthenticationService = customerAuthenticationService;
            this._workContext = workContext;
            this._customerSettings = customerSettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings; ;
            this._localizationSettings = localizationSettings;
        }
        #endregion


        #region 方法
        private bool AccountAlreadyExists(Customer userFound, Customer userLoggedIn)
        {
            return userFound != null && userLoggedIn != null;
        }

        public virtual AuthorizationResult Authorize(OpenAuthenticationParameters parameters)
        {
            var userFound = _openAuthenticationService.GetUser(parameters);

            var userLoggedIn = _workContext.CurrentCustomer.IsUserLogin() ? _workContext.CurrentCustomer : null;

            if (AccountAlreadyExists(userFound, userLoggedIn))
            {
                if (AccountIsAssignedToLoggedOnAccount(userFound, userLoggedIn))
                {
                    return new AuthorizationResult(OpenAuthenticationStatus.Authenticated);
                }

                var result = new AuthorizationResult(OpenAuthenticationStatus.Error);
                result.AddError("账户已经分配");
                return result;
            }
            if (AccountDoesNotExistAndUserIsNotLoggedOn(userFound, userLoggedIn))
            {
                ExternalAuthorizerHelper.StroeParametersForRoundTrip(parameters);
                //if()
            }
            return null;
        }

        private bool AccountDoesNotExistAndUserIsNotLoggedOn(Customer userFound, Customer userLoggedIn)
        {
            throw new NotImplementedException();
        }

        private bool AccountIsAssignedToLoggedOnAccount(Customer userFound, Customer userLoggedIn)
        {
            return userFound.Id.Equals(userLoggedIn.Id);
        }
        #endregion
    }
}
