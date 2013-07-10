using System.Web.Mvc;
using RCSoft.Services.Configuration;

namespace RCSoft.Plugin.ExternalAuth.Taobao.Controllers
{
    public class ExternalAuthTaobaoController : Controller
    {
        private readonly ISettingService _settingService;

        public ExternalAuthTaobaoController(ISettingService settingService)
        {
            this._settingService = settingService;
        }
    }
}
