using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCSoft.Core.Domain.Localization;
using RCSoft.Core.Data;

namespace RCSoft.Services.Localization
{
    public partial class LocalizationService : ILocalizationService
    {
        #region 字段
        private readonly IRepository<LocaleStringResource> _lsrRepository;
        #endregion

        #region 构造函数
        public LocalizationService(IRepository<LocaleStringResource> lsrRepository)
        {
            this._lsrRepository = lsrRepository;
        }
        #endregion

        #region 方法
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException("localStringResource");
            _lsrRepository.Delete(localeStringResource);
        }

        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;
            var localeStringResource = _lsrRepository.GetById(localeStringResourceId);
            return localeStringResource;
        }

        public LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            throw new NotImplementedException();
        }

        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName, bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.ResourceName == resourceName
                        select lsr;
            var localeStringResource = query.FirstOrDefault();
            return localeStringResource;
        }

        public virtual IList<LocaleStringResource> GetAllResources()
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        select l;
            var locales = query.ToList();
            return locales;
        }

        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException("localStringResource");
            _lsrRepository.Insert(localeStringResource);
        }

        public void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues()
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        select l;
            var locales = query.ToList();
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }
            return dictionary;
        }

        public virtual string GetResource(string resourceKey)
        {
            return GetResource(resourceKey, true, "", false);
        }

        public virtual string GetResource(string resourceKey, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            var query = from l in _lsrRepository.Table
                        where l.ResourceName == resourceKey
                        select l.ResourceValue;
            if (query.FirstOrDefault() != null)
                result = query.FirstOrDefault();
            if (string.IsNullOrEmpty(result))
            {
                if (logIfNotFound)
                {
                    //TO DO Log the warning

                }
                if (!String.IsNullOrEmpty(defaultValue))
                    result = defaultValue;
                else
                    if (!returnEmptyIfNotFound)
                        result = resourceKey;

            }
            return result;
        }

        public void ImportResourcesFromXml(string xml)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
