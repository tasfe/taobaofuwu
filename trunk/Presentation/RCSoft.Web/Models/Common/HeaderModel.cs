using RCSoft.Web.Framework.Mvc;

namespace RCSoft.Web.Models.Common
{
    public partial class HeaderModel:BaseModel
    {
        public bool IsAuthenticated { get; set; }
        public string CustomerUsernameOrEmail{get;set;}

        public string AlertMessage { get; set; }
    }
}