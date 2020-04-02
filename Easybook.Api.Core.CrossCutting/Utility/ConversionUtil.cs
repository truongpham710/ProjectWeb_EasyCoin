using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.Core.CrossCutting.Utility
{
    public class ConversionUtil
    {
        /// <summary>
        /// Convert object to String type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static string ToString(Object obj, string defaultVal = "")
        {
            if (obj != null) return obj.ToString();
            else return "";
        }

        /// <summary>
        /// Convert object to Integer 32 type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static int ToInteger(Object obj, int defaultVal = 0)
        {
            int intValue = defaultVal;
            String strValue = ToString(obj);
            if (strValue != "")
            {
                if (!int.TryParse(strValue, out intValue))
                    return defaultVal;
            }
            return intValue;
        }

        /// <summary>
        /// Convert object to Integer 64 type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static Int64 ToInteger64(Object obj, Int64 defaultVal = 0)
        {
            Int64 intValue = defaultVal;
            String strValue = ToString(obj);
            if (strValue != "")
            {
                if (!Int64.TryParse(strValue, out intValue))
                    return defaultVal;
            }
            return intValue;
        }

        /// <summary>
        /// Convert object to boolean
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static bool ToBoolean(Object obj, bool defaultVal = false)
        {
            bool boolValue = defaultVal;
            String strValue = ToString(obj);
            if (strValue != "")
            {
                if (strValue.ToLower().Equals("false"))
                    return false;
                else if (strValue.ToLower().Equals("true"))
                    return true;

                if (strValue.ToLower().Equals("0"))
                    return false;
                else if (strValue.ToLower().Equals("1"))
                    return true;

                if (!bool.TryParse(strValue, out boolValue))
                    return defaultVal;
            }
            return boolValue;
        }

        /// <summary>
        /// Convert Object to Float type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static float ToFloat(Object obj, float defaultValue = 0)
        {
            float floatReturn = defaultValue;
            String strValue = ToString(obj);

            if (strValue != "")
                float.TryParse(strValue, out floatReturn);
            return floatReturn;
        }

        /// <summary>
        /// Convert object to Decimal type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static Decimal ToDecimal(Object obj, Decimal defaultVal = 0)
        {
            Decimal decimalValue = defaultVal;
            String strValue = ToString(obj);
            if (strValue != "")
                Decimal.TryParse(strValue, out decimalValue);
            return decimalValue;
        }

        /// <summary>
        /// Convert object to Double type
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static double ToDouble(Object obj, double defaultValue = 0)
        {
            double doubleReturn = defaultValue;
            String strValue = ToString(obj);

            if (strValue != "")
                double.TryParse(strValue, out doubleReturn);
            return doubleReturn;
        }

        /// <summary>
        /// Convert object to nullable datetime
        /// </summary>
        /// <param name="obj">target object</param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(Object obj)
        {
            DateTime dt;
            String strValue = ToString(obj);

            if (DateTime.TryParse(strValue, out dt)) return dt;
            else return null;
        }

        /// <summary>
        /// Convert object to datetime
        /// </summary>
        /// <param name="obj">target object</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static DateTime ToDateTime(Object obj, DateTime defaultValue)
        {
            try
            {
                DateTime dt = new DateTime();
                String strValue = ToString(obj);

                if (DateTime.TryParse(strValue, out dt)) return dt;
                else return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert string into Datetime (default format: yyyyMMdd)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime FromStringToDatetime(string value, DateTime defaultValue, string format = "yyyyMMddHHmmssffff")
        {
            try
            {
                return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Convert string into Datetime (default format: yyyyMMdd)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? FromStringToDatetime(string value, string format = "yyyyMMddHHmmssffff")
        {
            try
            {
                return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convert datetime to unix timestamp
        /// </summary>
        /// <param name="value">valid datetime</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <returns></returns>
        public static long ToUnixTimestamp(DateTime value, long defaultValue)
        {
            try
            {
                var date = new DateTime(1970, 1, 1, 0, 0, 0, value.Kind);
                var unixTimestamp = System.Convert.ToInt64((value - date).TotalSeconds);

                return unixTimestamp;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// UnixTimeStampToDateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
        /// <summary>
        /// Convert Nullable datetime to timestamp (default format: yyyyMMdd)
        /// </summary>
        /// <param name="value">nullable datetime</param>
        /// <param name="defaultVal">default value to be return if conversion failed</param>
        /// <param name="format">datetime format for timestamp (eg: yyyyMMdd)</param>
        /// <returns></returns>
        public static String ToTimestamp(Nullable<DateTime> value, string defaultValue = "", string format = "yyyyMMdd")
        {
            try
            {
                if (value != null)
                    return value.Value.ToString("yyyyMMdd");
                else
                    return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert Datetime into timestamp (default format: yyyyMMdd)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToTimestamp(DateTime value, string defaultValue = "", string format = "yyyyMMdd")
        {
            try
            {
                return value.ToString(format);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert string to language enum (eg: en, ms,zh, th...)
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <returns></returns>
        public static LanguageEnum ToLanguageEnum(string lang)
        {
            switch (lang)
            {
                case "en":
                    return LanguageEnum.en;
                case "ms":
                    return LanguageEnum.ms;
                case "zh":
                    return LanguageEnum.zh;
                case "th":
                    return LanguageEnum.th;
                default:
                    return LanguageEnum.en;
            }
        }
        public enum LanguageEnum
        {
            en, //English
            ms, //Malay
            zh, //Chinese
            th, //Thai
            id, //Indo
            vi, //Vietnam
                //la,//Laos
        }
      
    }
}
