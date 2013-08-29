using RCSoft.Core.Configuration;

namespace RCSoft.Plugin.ExternalAuth.Taobao
{
    public class TaobaoExternalAuthSettings : ISettings
    {
        public string ClientKeyIdentifier { get; set; }
        public string ClientSecret { get; set; }
        public string SiteUrl { get; set; }
    }
}
