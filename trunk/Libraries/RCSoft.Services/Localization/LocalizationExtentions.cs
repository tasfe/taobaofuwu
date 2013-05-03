using System;
using System.Linq.Expressions;
using RCSoft.Core;
using RCSoft.Core.Domain.Localization;
using RCSoft.Core.Infrastructure;
using System.Reflection;

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

    }
}
