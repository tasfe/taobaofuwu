

namespace RCSoft.Plugin.ExternalAuth.Taobao.Core
{
    public class TaobaoApplication
    {
        public TaobaoApplication(string clientkeeyIdentifier, string clientSecret,string siteUrl)
        {
            AppId = clientkeeyIdentifier;
            AppSecret = clientSecret;
            SiteUrl = siteUrl;
        }

        public string AppId { get; private set; }
        public string AppSecret { get; private set; }
        public string SiteUrl { get; private set; }
    }
}
