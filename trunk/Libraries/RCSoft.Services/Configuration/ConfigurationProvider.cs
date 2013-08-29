using System;
using System.Collections.Generic;
using System.Linq;
using RCSoft.Core.Configuration;
using RCSoft.Core;
using RCSoft.Core.Domain.Configuration;

namespace RCSoft.Services.Configuration
{
    public class ConfigurationProvider<TSettings> : IConfigurationProvider<TSettings> where TSettings : ISettings, new()
    {
        readonly ISettingService _settingService;

        public ConfigurationProvider(ISettingService settingService)
        {
            this._settingService = settingService;
            this.BuildConfiguration();
        }
        public TSettings Settings { get; protected set; }
        private void BuildConfiguration()
        {
            Settings = Activator.CreateInstance<TSettings>();

            var properties = from prop in typeof(TSettings).GetProperties()
                             where prop.CanWrite && prop.CanRead
                             let setting = _settingService.GetSettingByKey<string>(typeof(TSettings).Name + "." + prop.Name)
                             where setting != null
                             where CommonHelper.GetRCSoftCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string))
                             where CommonHelper.GetRCSoftCustomTypeConverter(prop.PropertyType).IsValid(setting)
                             let value = CommonHelper.GetRCSoftCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting)
                             select new { prop, value };
            properties.ToList().ForEach(p => p.prop.SetValue(Settings, p.value, null));
        }

        public void SaveSettings(TSettings settings)
        {
            var properties = from prop in typeof(TSettings).GetProperties()
                             where prop.CanWrite && prop.CanRead
                             where CommonHelper.GetRCSoftCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string))
                             select prop;
            foreach (var prop in properties)
            {
                string key = typeof(TSettings).Name + "." + prop.Name;

                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    _settingService.SetSetting(key, value, false);
                else
                    _settingService.SetSetting(key, "", false);
            }
            this.Settings = settings;
        }
        public void DeleteSettings()
        {
            var properties = from prop in typeof(TSettings).GetProperties()
                             select prop;

            var settingList = new List<Setting>();
            foreach (var prop in properties)
            {
                string key = typeof(TSettings).Name + "." + prop.Name;
                var setting = _settingService.GetSettingByKey(key);
                if (setting != null)
                    settingList.Add(setting);
            }

            foreach (var setting in settingList)
                _settingService.DeleteSetting(setting);

        }
    }
}
