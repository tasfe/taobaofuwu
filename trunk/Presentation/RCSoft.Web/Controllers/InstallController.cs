using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using RCSoft.Web.Models.Install;
using RCSoft.Web.Infrastructure.Installation;
using RCSoft.Core.Data;

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
            if (ModelState.IsValid)
            {
                var settingsManager = new DataSettingsManager();
                try
                {
                    string connectionSettings = null;
                    connectionSettings = CreateConnectionString(model.SqlAuthenticationType == "windowsauthentication", model.SqlServerName, model.SqlDatabaseName, model.SqlServerUsername, model.SqlServerPassword, 0);
                    if (!SqlServerDatabaseExists(connectionSettings))
                    {
                        var ErrorCreateDataBase = CreateDatabase(connectionSettings);
                        if (!String.IsNullOrEmpty(ErrorCreateDataBase))
                            throw new Exception(ErrorCreateDataBase);
                    }

                    var settings = new DataSettings()
                    {
                        DataProvider = "SQLServer",
                        DataConnectionString = connectionSettings
                    };
                    settingsManager.SaveSettings(settings);

                    DataSettingsHelper.ResetCache();
                }
                catch (Exception exception)
                {
                    DataSettingsHelper.ResetCache();
                    settingsManager.SaveSettings(new DataSettings
                    {
                        DataProvider = null,
                        DataConnectionString = null
                    });

                    ModelState.AddModelError("", string.Format(_locService.GetResource("SetupFailed"), exception.Message));
                }
            }
            return View(model);
        }
        #endregion

        #region 工具
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        [NonAction]
        protected string CreateDatabase(string connectionString)
        {
            try
            {
                //创建数据库连接
                var builder = new SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;
                //创建连接数据库为"master"库，master库肯定存在
                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("CREATE DATABASE [{0}]", databaseName);
                using (var conn = new SqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Format(_locService.GetResource("DatabaseCreationError"), ex.Message);
            }
        }
        /// <summary>
        /// 检查数据库是否已经存在
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>是否安装</returns>
        [NonAction]
        protected bool SqlServerDatabaseExists(string connectionString)
        {
            try
            {
                //测试连接
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 创建数据库连接字符串
        /// </summary>
        /// <param name="trustedConnection">认真类型，是否可信任</param>
        /// <param name="serverName">SQL服务器名称</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="userName">数据库用户名</param>
        /// <param name="password">数据库密码</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [NonAction]
        protected string CreateConnectionString(bool trustedConnection,
            string serverName, string databaseName,
            string userName, string password, int timeout = 0)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = trustedConnection;
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            if (!trustedConnection)
            {
                builder.UserID = userName;
                builder.Password = password;
            }
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = true;
            if (timeout > 0)
            {
                builder.ConnectTimeout = timeout;
            }
            return builder.ConnectionString;
        }
        #endregion
    }
}