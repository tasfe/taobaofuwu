using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using System.ComponentModel;
using System.Globalization;
using RCSoft.Core.ComponentModel;

namespace RCSoft.Core
{
    /// <summary>
    /// 公共类
    /// </summary>
    public partial class CommonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new RCSoftException("电子邮件地址无效.");
            }

            return output;
        }
        /// <summary>
        /// 验证电子邮件地址否是是一个有效的格式
        /// </summary>
        /// <param name="email">电子邮件地址</param>
        /// <returns>返回true如果地址有效，返回false如果地址无效</returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }
        /// <summary>
        /// 确保字符串不超过指定长度
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>如果输入小于指定长度，返回原有字符串，如果超过则截取指定长度</returns>
        public static string EnsureMaximumLength(string str, int maxLength)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
                return str.Substring(0, maxLength);
            else
                return str;
        }
        /// <summary>
        /// 确保字符串不为空
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        private static AspNetHostingPermissionLevel? _trustLevel = null;
        /// <summary>
        /// 找到正在运行的应用的信用等级（http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx）
        /// </summary>
        /// <returns></returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            { 
                //设置最低等级
                _trustLevel = AspNetHostingPermissionLevel.None;

                //
                foreach (AspNetHostingPermissionLevel trustLevel in
                    new AspNetHostingPermissionLevel[]{
                    AspNetHostingPermissionLevel.Unrestricted,
                    AspNetHostingPermissionLevel.High,
                    AspNetHostingPermissionLevel.Medium,
                    AspNetHostingPermissionLevel.Low,
                    AspNetHostingPermissionLevel.Minimal
                    })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break;
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

        public static bool OneToManyCollectionWrapperEnabled
        {
            get {
                bool enabled = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["OneToManyCollectionWrapperEnabled"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["OneToManyCollectionWrapperEnabled"]);
                return enabled;
            }
        }

        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetRCSoftCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetRCSoftCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }
        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }
        public static TypeConverter GetRCSoftCustomTypeConverter(Type type)
        {
            //we can't use the following code in order to register our custom type descriptors
            //TypeDescriptor.AddAttributes(typeof(List<int>), new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            //so we do it manually here

            

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();
            //if (type == typeof(ShippingOption))
            //    return new ShippingOptionTypeConverter();
            //if (type == typeof(List<ShippingOption>) || type == typeof(IList<ShippingOption>))
            //    return new ShippingOptionListTypeConverter();

            return TypeDescriptor.GetConverter(type);
        }
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }
    }
}
