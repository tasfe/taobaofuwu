using System.Linq;
using System.Collections.Generic;
using RCSoft.Core.Domain.Configuration;
using RCSoft.Core.Configuration;
using RCSoft.Core.Data;
using System;
using RCSoft.Core;
using RCSoft.Core.Infrastructure;

namespace RCSoft.Services.Configuration
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public partial class SettingService:ISettingService
    {
        #region 字段
        private readonly IRepository<Setting> _settingRepository;
        #endregion

        #region 构造函数
        public SettingService(IRepository<Setting> settingRepository)
        {
            this._settingRepository = settingRepository;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 添加一个配置项
        /// </summary>
        /// <param name="setting">配置项</param>
        /// <param name="clearCache">是否缓存</param>
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("配置项");
            _settingRepository.Insert(setting);
        }

        /// <summary>
        /// 更新配置项
        /// </summary>
        /// <param name="setting">配置项</param>
        /// <param name="clearCahe">是否缓存</param>
        public virtual void UpdateSetting(Setting setting, bool clearCahe = true)
        {
            if (setting == null)
                throw new ArgumentNullException("配置项");
            _settingRepository.Update(setting);
        }

        /// <summary>
        /// 根据编号获取设置
        /// </summary>
        /// <param name="settingId">编号</param>
        /// <returns></returns>
        public virtual Setting GetSettingById(int settingId)
        {
            if (settingId == 0)
                return null;
            var setting = _settingRepository.GetById(settingId);
            return setting;
        }

        /// <summary>
        /// 根据键值获取设置
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public virtual Setting GetSettingByKey(string key)
        {
            if (String.IsNullOrEmpty(key))
                return null;

            key = key.Trim().ToLowerInvariant();

            var settings=GetAllSettings();
            if (settings.ContainsKey(key))
            {
                var id = settings[key].Key;
                return GetSettingById(id);
            }
            return null;
        }

        /// <summary>
        /// 根据键值获取值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public virtual T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;
            key = key.Trim().ToLowerInvariant();

            var settings = GetAllSettings();
            if (settings.ContainsKey(key))
            {
                return CommonHelper.To<T>(settings[key].Value);
            }
            return defaultValue;
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="clearCache">是否缓存</param>
        public virtual void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            key = key.Trim().ToLowerInvariant();

            var settings = GetAllSettings();

            Setting setting = null;
            string valueStr = CommonHelper.GetRCSoftCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);
            if (settings.ContainsKey(key))
            {
                var settingId = settings[key].Key;
                setting = GetSettingById(settingId);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);

            }
            else
            {
                setting = new Setting() { Name = key, Value = valueStr, };
                InsertSetting(setting, clearCache);
            }
        }

        /// <summary>
        /// 删除设置
        /// </summary>
        /// <param name="setting">配置项</param>
        public virtual void DeleteSetting(Setting setting)
        { 
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Delete(setting);
        }

        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, KeyValuePair<int, string>> GetAllSettings()
        {
            var query = from s in _settingRepository.Table
                        orderby s.Name
                        select s;
            var settings = query.ToList();
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var s in settings)
            {
                var resourceName = s.Name.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(s.Id, s.Value));
            }
            return dictionary;
        }

        /// <summary>
        /// 保存配置项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="settingInstance"></param>
        public virtual void SaveSetting<T>(T settingInstance) where T : ISettings, new()
        {
            EngineContext.Current.Resolve<IConfigurationProvider<T>>().SaveSettings(settingInstance);
        }

        /// <summary>
        /// 删除所有设置项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public virtual void DeleteSetting<T>() where T : ISettings, new()
        {
            EngineContext.Current.Resolve<IConfigurationProvider<T>>().DeleteSettings();
        }
        #endregion
    }
}
