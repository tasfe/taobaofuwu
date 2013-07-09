using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RCSoft.Services.Customers;
using RCSoft.Core.Domain.Customers;
using RCSoft.Core;
using System.Web.Mvc;
using RCSoft.Web.Models.Common;

namespace RCSoft.Web.Controllers
{
    public partial class CommonController : BaseController
    {
        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        public CommonController(CustomerSettings customerSettings, IWorkContext workContext)
        {
            this._customerSettings = customerSettings;
            this._workContext = workContext;
        }
        public ActionResult Header()
        {
            var customer = _workContext.CurrentCustomer;

            var model = new HeaderModel()
            {
                IsAuthenticated = customer == null,
                CustomerUsernameOrEmail = _customerSettings.UsernameEnabled ? customer.Username : customer.Email,
                AlertMessage = string.Empty
            };

            return PartialView(model);
        }
    }
}