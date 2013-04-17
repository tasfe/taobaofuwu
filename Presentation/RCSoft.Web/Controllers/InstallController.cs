using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RCSoft.Web.Models.Install;

namespace RCSoft.Web.Controllers
{
    public class InstallController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(InstallModel model)
        {
            return View(model);
        }
    }
}