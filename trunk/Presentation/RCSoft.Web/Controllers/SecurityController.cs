using RCSoft.Core;
using RCSoft.Services.Security;
using RCSoft.Services.Customers;
using System.Web.Mvc;

namespace RCSoft.Web.Controllers
{
    public partial class SecurityController : BaseController
    {
        #region 字段
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerService _customerService;
        #endregion

        #region 构造函数
        public SecurityController(IWorkContext workContext, IPermissionService permissionService, ICustomerService customerService)
        {
            this._workContext = workContext;
            this._permissionService = permissionService;
            this._customerService = customerService;
        }
        #endregion

        #region 方法
        public ActionResult AccessDenied(string pageUrl)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null)
                return View();
            return View();
        }
        #endregion
    }
}