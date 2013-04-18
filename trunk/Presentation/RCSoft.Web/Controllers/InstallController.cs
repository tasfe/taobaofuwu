using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RCSoft.Web.Models.Install;
using RCSoft.Web.Infrastructure.Installation;

namespace RCSoft.Web.Controllers
{
    public class InstallController : BaseController
    {
        #region 属性
        private readonly IInstallationLocalizationService _locService;
        #endregion

        #region 构造函数
        public InstallController(IInstallationLocalizationService locService)
        {
            this._locService = locService;
        } 
        #endregion

        #region 方法

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(InstallModel model)
        {
            if(string.IsNullOrEmpty(model.SqlServerName))
                ModelState.AddModelError("",_locService.GetResource("SqlServerNameRequired"));
            if (string.IsNullOrEmpty(model.SqlDatabaseName))
                ModelState.AddModelError("", _locService.GetResource("DataBaseNameRequired"));
            if (string.IsNullOrEmpty(model.SqlServerUsername))
                ModelState.AddModelError("", _locService.GetResource("SqlServerUserNameRequired"));
            if (string.IsNullOrEmpty(model.SqlServerPassword))
                ModelState.AddModelError("", _locService.GetResource("SQLServerPasswordRequired"));
            return View(model);
        } 
        #endregion
    }
}