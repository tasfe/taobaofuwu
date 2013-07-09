using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RCSoft.Web.Framework.Controllers;

namespace RCSoft.Web.Controllers
{
    [UserAuthorize]
    public partial class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}