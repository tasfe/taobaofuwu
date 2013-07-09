using System.Web.Routing;
using RCSoft.Web.Framework.Mvc;

namespace RCSoft.Web.Models.Customers
{
    public partial class ExternalAuthenticationMethodModel:BaseModel
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }
}