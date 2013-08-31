using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCSoft.Web.Framework.Themes
{
    public interface IThemeContext
    {
        /// <summary>
        /// Get or set current theme for desktops (e.g. darkOrange)
        /// </summary>
        string WorkingDesktopTheme { get; set; }

        /// <summary>
        /// Get current theme for mobile (e.g. Mobile)
        /// </summary>
        string WorkingMobileTheme { get; }
    }
}
