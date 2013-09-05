namespace RCSoft.Plugin.ExternalAuth.Taobao.Core
{
    public static class Provider
    {
        public static string SystemName
        {
            get { return "ExternalAuth.Taobao"; }
        }
        public static string AccessTokenURL
        {
            get { return "https://oauth.tbsandbox.com/token"; }
        }
        public static string Authorize
        {
            get { return "https://oauth.tbsandbox.com/authorize"; }
        }
    }
}
