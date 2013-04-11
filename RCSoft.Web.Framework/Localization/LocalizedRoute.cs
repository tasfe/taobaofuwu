using System;
using System.Web.Routing;
using System.Web;
using RCSoft.Core.Data;
using RCSoft.Core.Infrastructure;
using RCSoft.Core.Domain.Localization;

namespace RCSoft.Web.Framework.Localization
{
    public class LocalizedRoute:Route
    {
        #region 字段
        private bool? _seoFirendlyUrlForLanguagesEnagled;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="routeHandler"></param>
        public LocalizedRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="routeHandler"></param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        /// <param name="routeHandler"></param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaults"></param>
        /// <param name="constraints"></param>
        /// <param name="dataTokens"></param>
        /// <param name="routeHandler"></param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }
        #endregion

        #region 方法

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (DataSettingsHelper.DatabaseIsInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
                string applicationPath = httpContext.Request.ApplicationPath;
                if (virtualPath.IsLocalizedUrl(applicationPath, false))
                {
                    string rawUrl = httpContext.Request.RawUrl;
                    var newVirtualPath = rawUrl.RemoveLocalizedPathFromRawUrl(applicationPath);
                    if (string.IsNullOrEmpty(newVirtualPath))
                        newVirtualPath = "/";
                    newVirtualPath = newVirtualPath.RemoveApplicationPathFromRawUrl(applicationPath);
                    newVirtualPath = "~" + newVirtualPath;
                    httpContext.RewritePath(newVirtualPath, true);
                }
            }
            RouteData data = base.GetRouteData(httpContext);
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData data = base.GetVirtualPath(requestContext, values);
            if (DataSettingsHelper.DatabaseIsInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                if (data != null)
                {
                    string rawUrl = requestContext.HttpContext.Request.RawUrl;
                    string applicationPath = requestContext.HttpContext.Request.ApplicationPath;
                    if (rawUrl.IsLocalizedUrl(applicationPath, true))
                    {
                        data.VirtualPath = string.Concat(rawUrl.GetLanguageSeoCodeFromUrl(applicationPath, true), "/",
                                                         data.VirtualPath);
                    }
                }
            }
            return data;
        }

        public virtual void ClearSeoFriendlyUrlsCachedValue()
        {
            _seoFirendlyUrlForLanguagesEnagled = null;
        }
        #endregion

        #region 属性

        public bool SeoFriendlyUrlsForLanguagesEnabled
        {
            get
            {
                if (!_seoFirendlyUrlForLanguagesEnagled.HasValue)
                    _seoFirendlyUrlForLanguagesEnagled = EngineContext.Current.Resolve<LocalizationSettings>().SeoFriendlUrlsForLanguagesEnagled;
                return _seoFirendlyUrlForLanguagesEnagled.Value;
            }
        }

        #endregion
    }
}
