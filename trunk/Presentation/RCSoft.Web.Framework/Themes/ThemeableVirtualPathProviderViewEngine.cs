using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace RCSoft.Web.Framework.Themes
{
    public abstract class ThemeableVirtualPathProviderViewEngine : VirtualPathProviderViewEngine
    {
        #region 字段
        internal Func<string, string> GetExtensionThunk;

        private readonly string[] _emptyLocations = null;
        #endregion

        #region 构造函数
        protected ThemeableVirtualPathProviderViewEngine()
        {
            GetExtensionThunk = new Func<string, string>(VirtualPathUtility.GetExtension);
        }
        #endregion

        #region 方法
        //protected virtual string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string theme, string cacheKeyPrefix, bool useCache, bool mobile, out string[] searchedLocations)
        //{
        //    searchedLocations = _emptyLocations;
        //    if (string.IsNullOrEmpty(name))
        //        return string.Empty;
        //    string areaName = GetAreaName(controllerContext.RouteData);
        //    if(!string.IsNullOrEmpty(areaName) && areaName.Equals)
        //}
        #endregion
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            throw new NotImplementedException();
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            throw new NotImplementedException();
        }
    }
}
