using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RCSoft.Web.Controllers
{
    public partial class HomeController:BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}