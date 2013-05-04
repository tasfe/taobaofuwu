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

        public Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {
            throw new NotImplementedException();
        }

        public string GetResource(string resourceKey)
        {
            throw new NotImplementedException();
        }

        public string GetResource(string resourceKey, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            throw new NotImplementedException();
        }

        public string ExportResourcesToXml(Language language)
        {
            throw new NotImplementedException();
        }

        public void ImportResourcesFromXml(Language language, string xml)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
