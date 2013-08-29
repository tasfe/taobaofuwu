using System.Collections.Generic;
using RCSoft.Core.Domain.Configuration;
using RCSoft.Core.Configuration;

namespace RCSoft.Services.Configuration
{
    public partial interface ISettingService
    {
        /// <summary>
        /// 根据编号获取设置
        /// </summary>
        /// <param name="settingId">编号</param>
        /// <returns></returns>
        Setting GetSettingById(int settingId);

        /// <summary>
        /// 根据键值获取设置
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        Setting GetSettingByKey(string key);

        /// <summary>
        /// 根据键值获取值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        T GetSettingByKey<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="clearCache">是否缓存</param>
        void SetSetting<T>(string key, T value, bool clearCache = true);

        /// <summary>
        /// 删除设置
        /// </summary>
        /// <param name="setting">配置项</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// 获取所有配置项
        /// </summary>
        /// <returns></returns>
        IDictionary<string, KeyValuePair<int, string>> GetAllSettings();

        /// <summary>
        /// 保存配置项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="settingInstance"></param>
        void SaveSetting<T>(T settingInstance) where T : ISettings, new();

        /// <summary>
        /// 删除所有设置项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        void DeleteSetting<T>() where T : ISettings, new();
    }
}
