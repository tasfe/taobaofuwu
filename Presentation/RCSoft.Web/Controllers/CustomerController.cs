using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RCSoft.Core;
using RCSoft.Services.Customers;
using RCSoft.Web.Framework.Controllers;
using RCSoft.Web.Models.Customers;
using RCSoft.Services.Localization;
using RCSoft.Services.Authentication;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Web.Controllers
{
    public class CustomerController : BaseController
    {
        #region 字段
        private readonly IAuthenticationService _authenticationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerAuthenticationService _customerAuthenticationService;
        private readonly CustomerSettings _customerSettings;
        #endregion

        #region 构造函数
        public CustomerController(IAuthenticationService authenticationService, IWorkContext workContext, ICustomerService customerService, ILocalizationService localizationService, ICustomerAuthenticationService customerAuthenticationService, CustomerSettings customerSettings)
        {
            this._authenticationService = authenticationService;
            this._workContext = workContext;
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._customerAuthenticationService = customerAuthenticationService;
            this._customerSettings = customerSettings;
        }
        #endregion

        #region 方法

        public ActionResult Login()
        {
            //如果已经登录跳转到首页
            if (_workContext.CurrentCustomer != null)
                return RedirectToRoute("HomePage");
            var model = new LoginModel();
            model.UsernameEnabled = false;
            model.DisplayCaptcha = false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.UsernameEnabled ? string.IsNullOrWhiteSpace(model.Username) : string.IsNullOrWhiteSpace(model.Email))
                {
                    ModelState.AddModelError("", model.UsernameEnabled ? _localizationService.GetResource("Account.Login.Fields.UserName.Required") : _localizationService.GetResource("Account.Login.Fields.Email.Required"));
                }
                else if (string.IsNullOrWhiteSpace(model.Password))
                {
                    ModelState.AddModelError("", _localizationService.GetResource("Account.Login.Fields.Password.Required"));
                }
                else
                {
                    if (_customerAuthenticationService.ValidateCustomer(_customerSettings.UsernameEnabled ? model.Username : model.Email, model.Password))
                    {
                        var customer = model.UsernameEnabled ? _customerService.GetCustomerByUsername(model.Username) : _customerService.GetCustomerByEmail(model.Email);
                        _authenticationService.SignIn(customer, model.RememberMe);
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToRoute("HomePage");
                    }
                    else
                    {
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToRoute("Login");
        }
        [NonAction]
        protected bool IsUserLogin()
        {
            return _workContext.CurrentCustomer.IsUserLogin();
        } 
        #endregion
    }
}