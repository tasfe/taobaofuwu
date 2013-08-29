using RCSoft.Services.Authentication.External;

namespace RCSoft.Plugin.ExternalAuth.Taobao.Core
{
    public interface IOAuthProviderTaobaoAuthorizer : IExternalProviderAuthorizer
    {
        string RedirectLoginUrl(string returnUrl);
    }
}
