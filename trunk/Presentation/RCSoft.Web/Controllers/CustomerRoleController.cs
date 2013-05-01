using System.Linq;
using System.Web.Mvc;
using RCSoft.Services.Customers;
using Telerik.Web.Mvc;
using RCSoft.Web.Models.Customers;

namespace RCSoft.Web.Controllers
{
    public class CustomerRoleController : BaseController
    {
        #region 字段
        private readonly ICustomerService _customerService;
        #endregion

        #region 构造函数
        public CustomerRoleController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        #endregion

        #region 角色
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var customerRoles = _customerService.GetAllCustomerRoles(true);
            var gridModel = new GridModel<CustomerRoleModel>
            {
                Data = customerRoles.Select(x => x.ToModel()),
                Total = customerRoles.Count()
            };
            return View(gridModel);
        }
        #endregion
    }
}