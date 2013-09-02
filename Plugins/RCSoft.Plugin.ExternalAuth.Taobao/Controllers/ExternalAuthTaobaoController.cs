using System.Web.Mvc;
using RCSoft.Services.Configuration;
using RCSoft.Services.Authentication.External;
using RCSoft.Core.Domain.Customers;
using RCSoft.Core;
using RCSoft.Plugin.ExternalAuth.Taobao.Models;
using RCSoft.Plugin.ExternalAuth.Taobao.Core;

namespace RCSoft.Plugin.ExternalAuth.Taobao.Controllers
{
    public class ExternalAuthTaobaoController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IOAuthProviderTaobaoAuthorizer _oAuthProviderTaobaoAuthorizer;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IWorkContext _workContext;

        public ExternalAuthTaobaoController(ISettingService settingService,IOAuthProviderTaobaoAuthorizer oAuthProviderTaobaoAuthorizer,IOpenAuthenticationService openAuthenticationService,ExternalAuthenticationSettings externalAuthenticationiSettings,IWorkContext workContext)
        {
            this._settingService = settingService;
            this._oAuthProviderTaobaoAuthorizer = oAuthProviderTaobaoAuthorizer;
            this._externalAuthenticationSettings = externalAuthenticationiSettings;
            this._openAuthenticationService = openAuthenticationService;
            this._workContext = workContext;
        }

        [ChildActionOnly]
        public ActionResult PublicInfo()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            var processor = _openAuthenticationService.LoadExternalAuthenticationMethodBySystemName("ExternalAuth.Taobao");
            if (processor == null || !processor.IsMethodActive(_externalAuthenticationSettings) || !processor.PluginDescriptor.Installed)
            {
                throw new RCSoftException("淘宝模块不能被加载!");
            }
            var viewModel = new LoginModel();

            TryUpdateModel(viewModel);

            var result = _oAuthProviderTaobaoAuthorizer.RedirectLoginUrl(returnUrl);
            return new RedirectResult(result);
        }
        public ActionResult Authorizer(string returnUrl, string code, string state)
        {
            var result = _oAuthProviderTaobaoAuthorizer.Authorize(returnUrl,code);
            if (_workContext.CurrentCustomer != null)
                return new RedirectResult("~/");
            return View();
        }
    }
}
