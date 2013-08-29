using RCSoft.Core.Plugins;
using RCSoft.Services.Authentication.External;
using RCSoft.Services.Configuration;
using System.Web.Routing;
using RCSoft.Services.Localization;

namespace RCSoft.Plugin.ExternalAuth.Taobao
{
    public class TaobaoExternalAuthMethod : BasePlugin, IExternalAuthenticationMethod
    {
        #region 字段
        private readonly ISettingService _settingService;
        #endregion

        #region 构造函数
        public TaobaoExternalAuthMethod(ISettingService settingService)
        {
            this._settingService = settingService;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ExternalAuthTaobao";
            routeValues = new RouteValueDictionary() { { "Namespaces", "RCSoft.Plugin.ExternalAuth.Taobao.Controllers" }, { "area", null } };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        public void GetPublicInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "ExternalAuthTaobao";
            routeValues = new RouteValueDictionary() { { "Namespaces", "RCSoft.Plugin.ExternalAuth.Taobao.Controllers" }, { "area", null } };
        }

        public override void Install()
        {
            var settings = new TaobaoExternalAuthSettings()
            {
                ClientKeyIdentifier = "",
                ClientSecret = "",
            };

            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Taobao.Login", "使用淘宝账户登录.");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientKeyIdentifier", "淘宝客户端APP Key.");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientKeyIdentifier.Hint", "输入您的淘宝客户端APP Key.");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientSecret", "淘宝客户端APP Secret.");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientSecret.Hint", "输入您的淘宝客户端APP Secret.");
        }


        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<TaobaoExternalAuthSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.Taobao.Login");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientKeyIdentifier");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientKeyIdentifier.Hint");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientSecret");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.Taobao.ClientSecret.Hint");
        
            base.Uninstall();
        }
        #endregion
    }
}
