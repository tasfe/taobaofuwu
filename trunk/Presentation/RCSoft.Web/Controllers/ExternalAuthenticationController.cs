using System.Collections.Generic;
using System.Web.Mvc;
using RCSoft.Services.Authentication.External;
using RCSoft.Web.Models.Customers;
using System.Web.Routing;

namespace RCSoft.Web.Controllers
{
    public partial class ExternalAuthenticationController : BaseController
    {
        #region 字段
        private readonly IOpenAuthenticationService _openAuthenticationService; 
        #endregion

        #region 构造函数
        public ExternalAuthenticationController(IOpenAuthenticationService openAuthenticationService)
        {
            this._openAuthenticationService = openAuthenticationService;
        }
        #endregion

        #region 方法
        public RedirectResult RemoveParameterAssociation(string returnUrl)
        {
            ExternalAuthorizerHelper.RemoveParameters();
            return Redirect(returnUrl);
        }
        [ChildActionOnly]
        public ActionResult ExternalMethods()
        {
            var model = new List<ExternalAuthenticationMethodModel>();

            var externalAuthenticationMethods = _openAuthenticationService.LoadActiveExternalAuthenticationMethods();
            foreach (var eam in externalAuthenticationMethods)
            {
                var eamModel = new ExternalAuthenticationMethodModel();

                string actionName;
                string controllerName;
                RouteValueDictionary routeValues;
                eam.GetPublicInfoRoute(out actionName, out controllerName, out routeValues);
                eamModel.ActionName = actionName;
                eamModel.ControllerName = controllerName;
                eamModel.RouteValues = routeValues;

                model.Add(eamModel);
            }

            return PartialView(model);
        }
        #endregion
    }
}