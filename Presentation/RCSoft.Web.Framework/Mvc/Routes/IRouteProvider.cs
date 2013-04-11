using System.Web.Routing;

namespace RCSoft.Web.Framework.Mvc.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);
        int Priority { get; }
    }
}
