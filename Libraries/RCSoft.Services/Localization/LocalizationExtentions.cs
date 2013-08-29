using System;
using System.Linq.Expressions;
using RCSoft.Core;
using RCSoft.Core.Domain.Localization;
using RCSoft.Core.Infrastructure;
using System.Reflection;
using RCSoft.Core.Plugins;

namespace RCSoft.Services.Localization
{
    public static class LocalizationExtentions
    {
        public static string GetLocalized<T>(this T entity, Expression<Func<T, string>> keySelector) where T : BaseEntity, ILocalizedEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            return GetLocalized(entity, keySelector);
        }

        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : BaseEntity, ILocalizedEntity
        {
            return GetLocalized<T, string>(entity, keySelector, returnDefaultValue, ensureTwoPublishedLanguages);
        }

        public static TPropType GetLocalized<T, TPropType>(this T entity,
            Expression<Func<T, TPropType>> keySelector,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : BaseEntity, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            TPropType result = default(TPropType);
            string resultStr = string.Empty;

            //load localized value
            string localeKeyGroup = typeof(T).Name;
            string localeKey = propInfo.Name;

            //set default value if required
            if (String.IsNullOrEmpty(resultStr) && returnDefaultValue)
            {
                var localizer = keySelector.Compile();
                result = localizer(entity);
            }

            return result;
        }

        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, IWorkContext workContext)
           where T : struct
        {
            if (workContext == null)
                throw new ArgumentNullException("workContext");

            return GetLocalizedEnum<T>(enumValue, localizationService);
        }

        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService)
            where T : struct
        {
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");

            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            //localized value
            string resourceName = string.Format("Enums.{0}.{1}",
                typeof(T).ToString(),
                //Convert.ToInt32(enumValue)
                enumValue.ToString());
            string result = localizationService.GetResource(resourceName, false, "", true);

            //set default value if required
            if (String.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }
        /// <summary>
        /// 添加插件本地化资源
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="resourceName">资源名称</param>
        public static void DeletePluginLocaleResource(this BasePlugin plugin, string resourceName)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            DeletePluginLocaleResource(plugin, localizationService, resourceName);
        }
        /// <summary>
        /// 删除插件本地化资源
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="localizationService">资源接口</param>
        /// <param name="resourceName">资源名称</param>
        public static void DeletePluginLocaleResource(BasePlugin plugin, ILocalizationService localizationService, string resourceName)
        {
            if (plugin == null)
                throw new ArgumentNullException("插件");
            if (localizationService == null)
                throw new ArgumentNullException("本地资源");
            var lsr = localizationService.GetLocaleStringResourceByName(resourceName, false);

            if (lsr != null)
                localizationService.DeleteLocaleStringResource(lsr);
        }
        /// <summary>
        /// 添加插件本地化资源
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="resourceName">资源名称</param>
        /// <param name="resourceValue">资源值</param>
        public static void AddOrUpdatePluginLocaleResource(this BasePlugin plugin,
            string resourceName, string resourceValue)
        {
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            AddOrUpdatePluginLocaleResource(plugin, localizationService,resourceName, resourceValue);
        }
        /// <summary>
        /// 添加插件本地化资源
        /// </summary>
        /// <param name="plugin">插件</param>
        /// <param name="localizationService">本地资源接口</param>
        /// <param name="resourceName">资源名称</param>
        /// <param name="resourceValue">资源值</param>
        public static void AddOrUpdatePluginLocaleResource(BasePlugin plugin, ILocalizationService localizationService, string resourceName, string resourceValue)
        {
            //actually plugin instance is not required
            if (plugin == null)
                throw new ArgumentNullException("插件");
            if (localizationService == null)
                throw new ArgumentNullException("资源接口");

            var lsr = localizationService.GetLocaleStringResourceByName(resourceName, false);
            if (lsr == null)
            {
                lsr = new LocaleStringResource()
                {
                    ResourceName = resourceName,
                    ResourceValue = resourceValue
                };
                localizationService.InsertLocaleStringResource(lsr);
            }
            else
            {
                lsr.ResourceValue = resourceValue;
                localizationService.UpdateLocaleStringResource(lsr);
            }
        }


        /// <summary>
        /// Get localized friendly name of a plugin
        /// </summary>
        /// <typeparam name="T">Plugin</typeparam>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedFriendlyName<T>(this T plugin, ILocalizationService localizationService, bool returnDefaultValue = true)
            where T : IPlugin
        {
            if (localizationService == null)
                throw new ArgumentNullException("资源接口");

            if (plugin == null)
                throw new ArgumentNullException("插件");

            if (plugin.PluginDescriptor == null)
                throw new ArgumentException("不能加载插件描述文件");

            string systemName = plugin.PluginDescriptor.SystemName;
            //localized value
            string resourceName = string.Format("Plugins.FriendlyName.{0}",
                systemName);
            string result = localizationService.GetResource(resourceName, false, "", true);

            //set default value if required
            if (String.IsNullOrEmpty(result) && returnDefaultValue)
                result = plugin.PluginDescriptor.FriendlyName;

            return result;
        }

        /// <summary>
        /// Get localized friendly name of a plugin
        /// </summary>
        /// <typeparam name="T">Plugin</typeparam>
        /// <param name="plugin">Plugin</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="localizedFriendlyName">Localized friendly name</param>
        /// <returns>Localized value</returns>
        public static void SaveLocalizedFriendlyName<T>(this T plugin,ILocalizationService localizationService,string localizedFriendlyName)
            where T : IPlugin
        {
            if (localizationService == null)
                throw new ArgumentNullException("资源接口");

            if (plugin == null)
                throw new ArgumentNullException("插件");

            if (plugin.PluginDescriptor == null)
                throw new ArgumentException("不能加载插件描述文件");

            string systemName = plugin.PluginDescriptor.SystemName;
            //localized value
            string resourceName = string.Format("Plugins.FriendlyName.{0}", systemName);
            var resource = localizationService.GetLocaleStringResourceByName(resourceName, false);

            if (resource != null)
            {
                if (string.IsNullOrWhiteSpace(localizedFriendlyName))
                {
                    //delete
                    localizationService.DeleteLocaleStringResource(resource);
                }
                else
                {
                    //update
                    resource.ResourceValue = localizedFriendlyName;
                    localizationService.UpdateLocaleStringResource(resource);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(localizedFriendlyName))
                {
                    //insert
                    resource = new LocaleStringResource()
                    {
                        ResourceName = resourceName,
                        ResourceValue = localizedFriendlyName,
                    };
                    localizationService.InsertLocaleStringResource(resource);
                }
            }
        }
    }
}
