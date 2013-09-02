using RCSoft.Services.Authentication.External;
using System.Web;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using System.Collections.Generic;
using Top.Api.Util;
using Jayrock.Json;
using System.IO;

namespace RCSoft.Plugin.ExternalAuth.Taobao.Core
{
    public class TaobaoProviderAuthorizer : IOAuthProviderTaobaoAuthorizer
    {
        private readonly IExternalAuthorizer _authorizer;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly TaobaoExternalAuthSettings _taobaoExternalAuthSettings;
        private readonly HttpContextBase _httpContext;
        private TaobaoApplication _taobaoApplication;

        public TaobaoProviderAuthorizer(IExternalAuthorizer authorizer, IOpenAuthenticationService openAuthenticationService, TaobaoExternalAuthSettings taobaoExternalAuthSettings, HttpContextBase httpContext)
        {
            this._authorizer = authorizer;
            this._openAuthenticationService = openAuthenticationService;
            this._taobaoExternalAuthSettings = taobaoExternalAuthSettings;
            this._httpContext = httpContext;
        }

        public TaobaoApplication TaobaoApplication
        {
            get { return _taobaoApplication ?? (_taobaoApplication = new TaobaoApplication(_taobaoExternalAuthSettings.ClientKeyIdentifier, _taobaoExternalAuthSettings.ClientSecret, _taobaoExternalAuthSettings.SiteUrl)); }
        }

        public AuthorizeState Authorize(string returnUrl,string code)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("grant_type", "authorization_code");
            param.Add("code", code);
            param.Add("client_id", TaobaoApplication.AppId);
            param.Add("client_secret", TaobaoApplication.AppSecret);
            param.Add("redirect_uri", "http://localhost:3844/");
            param.Add("view", "web");
            WebUtils webutils = new WebUtils();
            string responseJosn = webutils.DoPost(Provider.AccessTokenURL, param);
            JsonReader reader = new JsonTextReader(new StringReader(responseJosn));
            JsonObject json=new JsonObject();
            json.Import(reader);
            var parameters = new OAuthAuthenticationParameters(Provider.SystemName) 
            {
                ExternalIdentifier = json["taobao_user_id"].ToString(),
                OAuthToken = json["refresh_token"].ToString(),
                OAuthAccessToken = json["access_token"].ToString(),
                ExternalDisplayIdentifier = json["taobao_user_nick"].ToString()
            };
            var result = _authorizer.Authorize(parameters);

            return new AuthorizeState(returnUrl, result);
            //var customerToken = json.(responseJosn);
            //ITopClient myTopClient = new DefaultTopClient(TaobaoApplication.SiteUrl, TaobaoApplication.AppId, TaobaoApplication.AppSecret);
            //UserSellerGetRequest req = new UserSellerGetRequest();
            //req.Fields = "nick,user_id,type";
            //UserSellerGetResponse rsp = myTopClient.Execute(req, "sandbox_b_00");//执行API请求并将该类转换为response对象
            //if (rsp.IsError)
            //    return new AuthorizeState(returnUrl, OpenAuthenticationStatus.Error);
            //else
            //    return new AuthorizeState(returnUrl, OpenAuthenticationStatus.Authenticated);
        }

        public string RedirectLoginUrl(string returnUrl)
        {
            return Provider.Authorize + "?response_type=code&view=web&client_id=" + TaobaoApplication.AppId + "&redirect_uri=http://localhost:3844/Plugins/ExternalAuthTaobao/Auth";
        }
    }
}
