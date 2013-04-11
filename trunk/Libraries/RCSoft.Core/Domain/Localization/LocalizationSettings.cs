using System;
using RCSoft.Core.Configuration;

namespace RCSoft.Core.Domain.Localization
{
    public class LocalizationSettings:ISettings
    {
        public int DefaultAdminLanguageId { get; set; }

        public bool UseImagesForLanguageSelection { get; set; }

        public bool SeoFriendlUrlsForLanguagesEnagled { get; set; }

        public bool LoadAllLocaleRecordsOnStartup { get; set; }
    }
}
